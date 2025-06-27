@Imports System.Web.Script.Serialization
@Code
    'Serialize the view bag to json to use it in javascript
    Dim serializer = New JavaScriptSerializer()
    Dim orderDetailsJson As String = serializer.Serialize(ViewBag.OrderDetails)
End Code


<main>
    <!-- Cart -->
    <div id="infoBox" class="d-flex flex-wrap gap-2 mt-3">
    </div>

    <!--Total Price - Buttons: Order, EmptyCart-->
    <div class="d-flex gap-2">
        <div>
            Total Price: € <span id="totalPrice" class="fw-bold">0.00</span>
        </div>

        <button onclick="CreateOrder()" class="btn btn-primary">Order</button>

        <button onclick="EmptyCart()" class="btn btn-secondary">EmptyCart</button>
    </div>

    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist" id="myTab">
        <li class="nav-item" role="presentation">
            <button class="nav-link active" id="Hamburgers-tab" data-bs-toggle="tab" data-bs-target="#Hamburgers" type="button" role="tab" aria-controls="Hamburgers" aria-selected="false">Hamburgers</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="Appetizers-tab" data-bs-toggle="tab" data-bs-target="#Appetizers" type="button" role="tab" aria-controls="Appetizers" aria-selected="false">Appetizers</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="Dessert-tab" data-bs-toggle="tab" data-bs-target="#Dessert" type="button" role="tab" aria-controls="Dessert" aria-selected="false">Dessert</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="Drinks-tab" data-bs-toggle="tab" data-bs-target="#Drinks" type="button" role="tab" aria-controls="Drinks" aria-selected="false">Drinks</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="Sauce-tab" data-bs-toggle="tab" data-bs-target="#Sauce" type="button" role="tab" aria-controls="Sauce" aria-selected="false">Sauce</button>
        </li>
    </ul>

    <!-- Tab panels -->
    <div class="tab-content pt-3">

        <!--HAMBURGERS-->
        <div class="tab-pane fade show active" id="Hamburgers" role="tabpanel" aria-labelledby="Hamburgers-tab">
            <div class="d-flex flex-wrap gap-3">
                <div class="tab-content pt-3">
                    <div class="tab-pane fade show active" id="products" role="tabpanel" aria-labelledby="products-tab">
                        <div class="d-flex flex-wrap gap-3">
                            @If ViewBag.Hamburgers IsNot Nothing Then
                                For Each product In ViewBag.Hamburgers
                                    @Html.Partial("_Products", product)
                                Next
                            End If
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--APPETIZERS-->
        <div class="tab-pane fade" id="Appetizers" role="tabpanel" aria-labelledby="Appetizers-tab">
            <div class="d-flex flex-wrap gap-3">
                <div class="tab-content pt-3">
                    <div class="tab-pane fade show active" id="products" role="tabpanel" aria-labelledby="products-tab">
                        <div class="d-flex flex-wrap gap-3">
                            @If ViewBag.Appetizers IsNot Nothing Then
                                For Each product In ViewBag.Appetizers
                                    @Html.Partial("_Products", product)
                                Next
                            End If
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--DESSERT-->
        <div class="tab-pane fade" id="Dessert" role="tabpanel" aria-labelledby="Dessert-tab">
            <div class="d-flex flex-wrap gap-3">
                <div class="tab-content pt-3">
                    <div class="tab-pane fade show active" id="products" role="tabpanel" aria-labelledby="products-tab">
                        <div class="d-flex flex-wrap gap-3">
                            @If ViewBag.Dessert IsNot Nothing Then
                                For Each product In ViewBag.Dessert
                                    @Html.Partial("_Products", product)
                                Next
                            End If
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--DRINKS-->
        <div class="tab-pane fade" id="Drinks" role="tabpanel" aria-labelledby="Drinks-tab">
            <div class="d-flex flex-wrap gap-3">
                <div class="tab-content pt-3">
                    <div class="tab-pane fade show active" id="products" role="tabpanel" aria-labelledby="products-tab">
                        <div class="d-flex flex-wrap gap-3">
                            @If ViewBag.Drinks IsNot Nothing Then
                                For Each product In ViewBag.Drinks
                                    @Html.Partial("_Products", product)
                                Next
                            End If
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--SAUCE-->
        <div class="tab-pane fade" id="Sauce" role="tabpanel" aria-labelledby="Sauce-tab">
            <div class="d-flex flex-wrap gap-3">
                <div class="tab-content pt-3">
                    <div class="tab-pane fade show active" id="products" role="tabpanel" aria-labelledby="products-tab">
                        <div class="d-flex flex-wrap gap-3">
                            @If ViewBag.Sauce IsNot Nothing Then
                                For Each product In ViewBag.Sauce
                                    @Html.Partial("_Products", product)
                                Next
                            End If
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</main>

<!--JavaScript-->
<script>

    //Initializes the Cart

    //Assign this variable by iniecting the serialized JSON from the server-side ViewBag into the JavaScript context
    const orderDetails = @Html.Raw(orderDetailsJson);

    //If there are order details, add them to the cart
    if (orderDetails !== null) {

        //Wait for the DOM (Document Object Model) to be fully loaded before executing the script
        document.addEventListener("DOMContentLoaded", () => {

            //For each item in orderDetails, call handleProductClick with the product ID and quantity to add it to the cart
            orderDetails.forEach(item => {
                handleProductClick(item.IdProduct, item.OrderQuantity);
            });

        });

    }

    //Item in the cart
    const cartItems = [];
    TotalOrderPrice = 0

    //Function to empty the cart
    function EmptyCart() {

        //If the cart Is empty, do nothing
        if (cartItems.length === 0) {
            return;
        }

        //Delete all the details from the db
        fetch(`/GetInfo/EmptyCart?&idOrder=@ViewBag.IdCopy`);

        //Clear the Cart HTML
        ClearCart()

    }

    //Function to Create an Order
    function CreateOrder() {

        //If the cart Is empty, do nothing
        if (cartItems.length === 0) {
            return;
        }

        //Create a new Order
        fetch(`/GetInfo/CreateOrder?&idOrder=@ViewBag.IdCopy`);

        //Clear the Cart HTML
        ClearCart()
    }

    //Function to clear the HTML Cart and Set the price to zero
    function ClearCart()
    {

        //For each item in the cart, remove the card element from the DOM and clear the cartItems
        cartItems.forEach(item => {
            item.element.remove();
        });
        cartItems.length = 0

        //Reset the total price to zero
        const total = document.getElementById("totalPrice");
        total.textContent = "0.00";

    }

    //Handle the click on a image Product
    function handleProductClick(idProduct, quantity = 1) {

        //Check if the product is already in the cart, in that case increment the quanity
        const existingItem = cartItems.find(item => item.id === idProduct);
        if (existingItem) {

            //Simulate a click on the plus button
            existingItem.btnPlus.click();
            return;

        }

        //Start the fetch request to get the product name and price passing the product ID
        fetch('/GetInfo/GetProductNamePrice?id=' + idProduct)

            //Convert the response to JSON
            .then(response => response.json())
            .then(data => {

                //Create a new card element to display the product in the cart,
                const card = document.createElement("div");
                card.className = "card p-2 border rounded shadow-sm mb-2";
                card.style.width = "250px";

                //Create the elements for the product name
                const name = document.createElement("h5");
                name.textContent = data.name;
                name.className = "mb-2";

                //Create the elements for the product quantity
                const price = document.createElement("div");
                price.className = "text-muted";
                price.textContent = "Prezzo: € " + data.price.toFixed(2);

                //Create the elements for the product price
                const quantityWrapper = document.createElement("div");
                quantityWrapper.className = "d-flex align-items-center gap-2";

                //Create the button for decreasing the quantity
                const btnMinus = document.createElement("button");
                btnMinus.textContent = "-";
                btnMinus.className = "btn btn-sm btn-outline-secondary";

                //Create the elemtent fot the product quantity
                const quantityDisplay = document.createElement("span");
                quantityDisplay.textContent = quantity
                quantityDisplay.className = "px-2";


                //Create the button for increasing the quantity
                const btnPlus = document.createElement("button");
                btnPlus.textContent = "+";
                btnPlus.className = "btn btn-sm btn-outline-secondary";

                //Initialize the TotalPrice
                let TotalPrice = data.price * quantity;
                price.textContent = "Prezzo: € " + TotalPrice.toFixed(2);

                //Update the total price in the cart
                const total = document.getElementById("totalPrice");
                const prezzoFloat = parseFloat(total.textContent);
                total.textContent = (prezzoFloat + TotalPrice).toFixed(2);

                //Add event listener to hte plus button
                btnPlus.onclick = function ()
                {

                    quantity++;
                    CalculatePrice()

                    //Increase the quantity in the db
                    fetch(`/GetInfo/IncreaseDetails?idProduct=${idProduct}&idOrder=@ViewBag.IdCopy`);

                };

                //Add event listener to hte minus button
                btnMinus.onclick = function ()
                {

                    //if the quantity is greater than 1, decrease it, otherwise remove the item
                    if (quantity > 1) {

                        quantity--;
                        CalculatePrice();

                        //Decrease the quantity in the db
                        fetch(`/GetInfo/DecreaseDetails?idProduct=${idProduct}&idOrder=@ViewBag.IdCopy`);
                    }
                    else
                    {
                        RemoveItem();

                        //Remove product from the order in the db
                        fetch(`/GetInfo/DeleteDetails?idProduct=${idProduct}&idOrder=@ViewBag.IdCopy`);
                    }

                };

                //Function to remove the item from the cart
                function RemoveItem()
                {

                    //Find the index of the item in the cartItems array and remove it
                    const index = cartItems.findIndex(item => item.id === idProduct);
                    if (index !== -1) {
                        cartItems.splice(index, 1);
                        card.remove();
                        UpdateTotalPrice();
                    }

                }

                //Calculate the price of a single product
                function CalculatePrice()
                {

                    TotalPrice = data.price * quantity;
                    price.textContent = "Prezzo: € " + TotalPrice.toFixed(2);
                    quantityDisplay.textContent = quantity;
                    UpdateTotalPrice();

                }

                //Update the total price in the cart
                function UpdateTotalPrice()
                {
                    TotalOrderPrice = 0

                    //Retrive the total price element and calculate the total price
                    const total = document.getElementById("totalPrice");
                    cartItems.forEach(item => {
                        TotalOrderPrice += item.price * parseInt(item.quantityDisplay.textContent);
                    })
                    total.textContent = TotalOrderPrice.toFixed(2)
                }

                //Append the elements to the card
                quantityWrapper.appendChild(btnMinus);
                quantityWrapper.appendChild(quantityDisplay);
                quantityWrapper.appendChild(btnPlus);
                card.appendChild(name);
                card.appendChild(quantityWrapper);
                card.appendChild(price);

                //append the card to the infobox
                document.getElementById("infoBox").appendChild(card);

                //Save the elements in the CartItems array
                const item = {
                    id: idProduct,
                    name: data.name,
                    price: data.price,
                    quantity: quantity,
                    quantityDisplay: quantityDisplay,
                    btnPlus: btnPlus,
                    element: card
                };
                cartItems.push(item);

                //Insert the new details in the db
                fetch(`/GetInfo/NewDetails?idProduct=${idProduct}&idOrder=@ViewBag.IdCopy`);
            })

    }
</script>