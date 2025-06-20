<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="display: flex; gap: 30px; align-items: flex-start;">
        <%--Left Columns--%>
        <div style="flex: 2;">
            <asp:UpdatePanel ID="UpdatePanelProducts" runat="server">
                <ContentTemplate>
                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">

                        <ajaxToolkit:TabPanel ID="Hamburgers" runat="server" HeaderText="Hamburgers">
                            <ContentTemplate>
                                <div class="flow-container">
                                    <asp:Repeater ID="RepeaterHamburgers" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="B_Product" runat="server" CommandName="Select" CommandArgument='<%# Eval("IdProduct") %>'>
                                                <div class="item">
                                                    <%# Eval("ProductName") %><br />
                                                    <img src='<%# ConvertByteArrayToBase64Image(CType(Eval("ProductPicture"), Byte())) %>' alt="immagine" />
                                                    <%# String.Format("{0:N2}€", Eval("ProductPrice")) %>
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>

                        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Appetizers">
                            <ContentTemplate>
                                <div class="flow-container">
                                    <asp:Repeater ID="RepeaterAppetizers" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="B_Product" runat="server" CommandName="Select" CommandArgument='<%# Eval("IdProduct") %>'>
                                                <div class="item">
                                                    <%# Eval("ProductName") %><br />
                                                    <img src='<%# ConvertByteArrayToBase64Image(CType(Eval("ProductPicture"), Byte())) %>' alt="immagine" />
                                                    <%# String.Format("{0:N2}€", Eval("ProductPrice")) %>
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>

                        <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Dessert">
                            <ContentTemplate>
                                <div class="flow-container">
                                    <asp:Repeater ID="RepeaterDessert" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="B_Product" runat="server" CommandName="Select" CommandArgument='<%# Eval("IdProduct") %>'>
                                                <div class="item">
                                                    <%# Eval("ProductName") %><br />
                                                    <img src='<%# ConvertByteArrayToBase64Image(CType(Eval("ProductPicture"), Byte())) %>' alt="immagine" />
                                                    <%# String.Format("{0:N2}€", Eval("ProductPrice")) %>
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>

                        <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Drinks">
                            <ContentTemplate>
                                <div class="flow-container">
                                    <asp:Repeater ID="RepeaterDrinks" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="B_Product" runat="server" CommandName="Select" CommandArgument='<%# Eval("IdProduct") %>'>
                                                <div class="item">
                                                    <%# Eval("ProductName") %><br />
                                                    <img src='<%# ConvertByteArrayToBase64Image(CType(Eval("ProductPicture"), Byte())) %>' alt="immagine" />
                                                    <%# String.Format("{0:N2}€", Eval("ProductPrice")) %>
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>

                        <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="Sauce">
                            <ContentTemplate>
                                <div class="flow-container">
                                    <asp:Repeater ID="RepeaterSauce" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="B_Product" runat="server" CommandName="Select" CommandArgument='<%# Eval("IdProduct") %>'>
                                                <div class="item">
                                                    <%# Eval("ProductName") %><br />
                                                    <img src='<%# ConvertByteArrayToBase64Image(CType(Eval("ProductPicture"), Byte())) %>' alt="immagine" />
                                                    <%# String.Format("{0:N2}€", Eval("ProductPrice")) %>
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>

                    </ajaxToolkit:TabContainer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <%--Right Columns--%>
        <div style="flex: 1; position: sticky; margin-top: 50px; top: 0; align-self: flex-start;">
            <asp:UpdatePanel ID="UpdatePanelCart" runat="server">
                <ContentTemplate>
                        <h3  style="text-align: Center; width: 300px;">Products selected</h3>
                    <asp:Repeater ID="RepeaterSelected" runat="server">
                        <ItemTemplate>
                            <div class="itemCart">
                                <%# Eval("ProductName") %>
                                <asp:Button ID="B_Remove" runat="server" Text="-" CommandName="Remove" CommandArgument='<%# Eval("IdProduct") %>' OnCommand="ItemButton" />
                                <%# Eval("ProductQuantity") %>
                                <asp:Button ID="B_Add" runat="server" Text="+" CommandName="Add" CommandArgument='<%# Eval("IdProduct") %>' OnCommand="ItemButton" />
                                <%# String.Format("{0:N2}€", Eval("BasePrice") * Eval("ProductQuantity")) %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label ID="L_TotalPrice" style="text-align: center" runat="server" Font-Bold="True" Font-Size="Large" Text="Total : 0,00€" Width="300px"></asp:Label><br />
                    <asp:Button ID="B_Order" runat="server" Text="Order" Width="363px" /><br />
                    <asp:Button ID="B_EmptyCart" runat="server" Text="Empty Cart" Width="363px" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <style>
        .flow-container {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-top: 10px;
            width: 1040px;
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
        }

        .item img {
            max-width: 100%;
            height: 140px;
            margin-top: 5px;
        }

        .itemCart {
            width: 300px;
            background-color: #e6ffe6;
            text-align: center;
            border: 1px solid #00cc66;
            border-radius: 5px;
            padding: 10px;
            font-weight: bold;
            margin-bottom: 10px;
        }
    </style>

</asp:Content>
