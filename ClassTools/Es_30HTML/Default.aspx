<!DOCTYPE html>
<html lang="it" x-data="shopApp()" x-init="init()">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>McDonald With Alpine</title>
    <script src="https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/cdn.min.js" defer></script>
    <script>

        //Basic Auth credentials
        var username = "admin";
        var password = "admin";

        //Function that generate the header authorization 
        function getBasicAuthHeader() {
            return "Basic " + btoa(username + ":" + password);
        }

        //Main Alpine.js function that returns the app and logic
        function shopApp() {
            return {
                //Index of active tab
                activeTab: 0,

                //Array of product categories
                categories: [],

                //Cart items
                cart: [],

                //Current Order Id
                orderId: 0,

                async init() {
                    await this.loadProducts(); //Load Products from the database
                    await this.loadOldOrder(); //Load last order
                },

                //Fetch all products and group them by category
                async loadProducts() {
                    try {
                        const res = await fetch("https://localhost:82/getallproducts/", {
                            method: "GET",
                            headers: {
                                "Authorization": getBasicAuthHeader()
                            }
                        });
                        const data = await res.json();
                        var grouped = {};
                        data.forEach(p => {
                            var category = p.ProductCategory;
                            if (!grouped[category]) grouped[category] = [];
                            grouped[category].push({
                                id: p.IdProduct,
                                name: p.ProductName,
                                price: p.ProductPrice,
                                image: p.ProductPicture ? "data:image/jpeg;base64," + bytesToBase64(p.ProductPicture) : null
                            });
                        });

                        //Convert grouped object to an array for Alpine
                        this.categories = Object.keys(grouped).map(cat => ({
                            name: cat,
                            products: grouped[cat]
                        }));
                    } catch (err) {
                        console.error("Error fetching products:", err);
                    }
                },

                // Load the last order in the cart
                async loadOldOrder() {
                    try {
                        const res = await fetch("https://localhost:82/getoldorder/", {
                            method: "GET",
                            headers: {
                                "Authorization": getBasicAuthHeader()
                            }
                        });
                        const data = await res.json();
                        this.orderId = data.IdOrder; 
                        var productMap = {};
                        this.categories.forEach(cat => {
                            cat.products.forEach(p => {
                                productMap[p.id] = p;
                            });
                        });

                        //Rebuild cart from old order details
                        data.Details.forEach(detail => {
                            var product = productMap[detail.IdProduct];
                            if (product) {
                                var existing = this.cart.find(i => i.product.id === product.id);
                                if (!existing) {
                                    this.cart.push({
                                        product: product,
                                        quantity: detail.OrderQuantity
                                    });
                                }
                            }
                        });
                    } catch (err) {
                        console.error("Error loading previous order:", err);
                    }
                },

                //Add a product to the cart or increase the quantity if it already exists
                async addToCart(product) {
                    var found = this.cart.find(i => i.product.id === product.id);
                    if (found) {
                        await this.increment(found);
                    } else {
                        this.cart.push({
                            product: product,
                            quantity: 1
                        });
                        var payload = {
                            IdOrder: this.orderId,
                            IdProduct: product.id,
                        };
                        await fetch("https://localhost:82/newdetails/", {
                            method: "POST",
                            headers: {
                                "Content-Type": "application/json",
                                "Authorization": getBasicAuthHeader()
                            },
                            body: JSON.stringify(payload)
                        });
                    }
                },

                //Increase quantity of a cart item
                async increment(item) {
                    item.quantity++;
                    var payload = {
                        IdOrder: this.orderId,
                        IdProduct: item.product.id,
                    };
                    await fetch("https://localhost:82/increasedetail/", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": getBasicAuthHeader()
                        },
                        body: JSON.stringify(payload)
                    });
                },

                //Decrease quantity of a cart item (and remove if 0)
                async decrement(item) {
                    item.quantity--;
                    var payload = {
                        IdOrder: this.orderId,
                        IdProduct: item.product.id,
                    };

                    //If quantity is less than 1, remove item from cart
                    if (item.quantity <= 0) {
                        this.cart = this.cart.filter(i => i !== item);
                        await fetch("https://localhost:82/deletedetail/", {
                            method: "POST",
                            headers: {
                                "Content-Type": "application/json",
                                "Authorization": getBasicAuthHeader()
                            },
                            body: JSON.stringify(payload)
                        });
                        return;
                    }
                    await fetch("https://localhost:82/decreasedetail/", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": getBasicAuthHeader()
                        },
                        body: JSON.stringify(payload)
                    });
                },

                //Calculate total price of the cart
                totalPrice() {
                    return this.cart.reduce((sum, i) => sum + i.product.price * i.quantity, 0);
                },

                // Create order and reset cart
                async order() {
                    alert('Order placed!\nTotal: ' + this.totalPrice().toFixed(2) + ' €');
                    await fetch("https://localhost:82/createorder/", {
                        method: "POST",
                        headers: {
                            "Authorization": getBasicAuthHeader()
                        }
                    });
                    await this.emptyCart();
                },

                //Empty the cart
                async emptyCart() {
                    this.cart = [];
                    var payload = {
                        IdOrder: this.orderId
                    };
                    await fetch("https://localhost:82/deletealldetails/", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": getBasicAuthHeader()
                        },
                        body: JSON.stringify(payload)
                    });
                }
            }
        }

        //Convert byte array to base64 image string
        function bytesToBase64(bytes) {
            var binary = '';
            bytes.forEach(function (b) { binary += String.fromCharCode(b); });
            return btoa(binary);
        }
    </script>
  </head>
  <body>
    <div class="main-container">

      <!-- Left column: tabs and products -->
      <div class="left-column">

        <!-- Create a tab for every category -->
        <div class="tabs">
          <template x-for="(cat, index) in categories" :key="cat.name">
            <div class="tab" :class="{ 'active': activeTab === index }" @click="activeTab = index" x-text="cat.name"></div>
          </template>
        </div>

        <!-- Show the products from every category -->
        <template x-for="(cat, index) in categories" :key="cat.name">
          <div x-show="activeTab === index" style="display:none;">
            <div class="flow-container">
              <template x-for="product in cat.products" :key="product.id">
                <div class="item" @click="addToCart(product)">
                  <div x-text="product.name"></div>
                  <img :src="product.image" :alt="product.name" />
                  <div x-text="product.price.toFixed(2) + ' €'"></div>
                </div>
              </template>
            </div>
          </div>
        </template>
      </div>

      <!-- Right column: Cart -->
      <div class="right-column">
        <h3>Products selected</h3>
        <template x-if="cart.length === 0">
          <div style="text-align:center; font-style: italic;">Cart is empty</div>
        </template>

        <!-- List of items in the cart -->
        <template x-for="item in cart" :key="item.product.id">
          <div class="itemCart">
            <div x-text="item.product.name"></div>
            <button @click="decrement(item)">-</button>
            <div x-text="item.quantity"></div>
            <button @click="increment(item)">+</button>
            <div x-text="(item.product.price * item.quantity).toFixed(2) + ' €'"></div>
          </div>
        </template>

        <!-- Total and action buttons -->
        <div class="total">Total: <span x-text="totalPrice().toFixed(2) + ' €'"></span>
        </div>
        <button id="order" @click="order()" :disabled="cart.length === 0">Order</button>
        <button id="emptyCart" @click="emptyCart()" :disabled="cart.length === 0">Empty Cart</button>
      </div>
    </div>
  </body>
</html>

<style>
  body {
    font-family: Arial, sans-serif;
    margin: 20px;
  }

  .tabs {
    display: flex;
    gap: 10px;
    margin-bottom: 10px;
    cursor: pointer;
  }

  .tab {
    padding: 8px 16px;
    border: 1px solid #3399ff;
    border-radius: 5px 5px 0 0;
    background-color: #cce5ff;
    font-weight: bold;
  }

  .tab.active {
    background-color: #3399ff;
    color: white;
  }

  .flow-container {
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
    margin-top: 0;
    max-width: 1040px;
  }

  .item {
    width: 200px;
    height: 240px;
    background-color: #cce5ff;
    text-align: center;
    border: 1px solid #3399ff;
    border-radius: 5px;
    padding: 10px;
    font-weight: bold;
    cursor: pointer;
    user-select: none;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
  }

  .item img {
    max-width: 100%;
    height: 140px;
    object-fit: cover;
    margin: 5px 0;
  }

  .main-container {
    display: flex;
    gap: 30px;
    align-items: flex-start;
  }

  .left-column {
    flex: 2;
  }

  .right-column {
    flex: 1;
    position: sticky;
    top: 50px;
    align-self: flex-start;
    max-width: 320px;
    background-color: #e6ffe6;
    border: 1px solid #00cc66;
    border-radius: 5px;
    padding: 15px;
  }

  .itemCart {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 10px;
    font-weight: bold;
  }

  .itemCart > div {
    flex: 1;
  }

  .itemCart button {
    width: 25px;
    height: 25px;
    font-weight: bold;
    cursor: pointer;
  }

  h3 {
    text-align: center;
    margin-bottom: 15px;
  }

  button#order,
  button#emptyCart {
    width: 100%;
    font-weight: bold;
    padding: 8px;
    margin-top: 10px;
    cursor: pointer;
  }

  .total {
    font-weight: bold;
    font-size: 1.2em;
    text-align: center;
    margin-top: 15px;
  }
</style>
