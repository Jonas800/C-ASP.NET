<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="Main" runat="server">
    <nav class="admin_nav">
        <a href="Admin.aspx?nav=users">Brugere</a>
        <a href="Admin.aspx?nav=products">Produkter</a>
        <a href="Admin.aspx?nav=categories">Kategorier</a>
    </nav>
    <div class="wrapper">
        <%--USERS--%>
        <asp:Panel ID="Panel_Users" runat="server" DefaultButton="Button_User_Create" Visible="false">
            <h2>Brugere</h2>

            <asp:Label ID="Label_User_Name" runat="server" Text="Brugernavn" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Name" ControlToValidate="TextBox_User_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Firstname" runat="server" Text="Fornavn" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Firstname" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Firstname" ControlToValidate="TextBox_User_Firstname" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Lastname" runat="server" Text="Efternavn" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Lastname" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Lastname" ControlToValidate="TextBox_User_Lastname" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Password" runat="server" Text="Kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Password" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:Label ID="Label_User_Password_Error" runat="server" Text="" ForeColor="DarkRed" CssClass="error_message"></asp:Label>

            <asp:Label ID="Label_User_Password_Repeat" runat="server" Text="Gentag kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Password_Repeat" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator_User_Password" ControlToCompare="TextBox_User_Password_Repeat" ControlToValidate="TextBox_User_Password" ForeColor="DarkRed" runat="server" ErrorMessage="Kodeord er ikke ens" Display="Dynamic"></asp:CompareValidator>
            <br />

            <asp:Button ID="Button_User_Create" runat="server" Text="Opret" CssClass="button" OnClick="Button_User_Create_Click" ValidationGroup="Users" />
            <br />
            <br />
            <h3 class="label_contact">Eksisterende brugere</h3>
            <asp:Repeater ID="Repeater_Users" runat="server">
                <ItemTemplate>
                    <div class="rep_list">
                        <%# Eval("user_firstname") %> "<%# Eval("user_login") %>" <%# Eval("user_lastname") %>
                        <div class="rep_list_control">
                            <a href="?nav=users&action=edit&id=<%# Eval("user_id") %>">Ret</a>
                            <a href="?nav=users&action=delete&id=<%# Eval("user_id") %>" onclick="return confirm('Dette vil slette brugere!')">Slet</a>
                        </div>
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>

        <%--PRODUCTS--%>
        <asp:Panel ID="Panel_Products" runat="server" Visible="false">
            <h2>Produkter</h2>
            <asp:Panel ID="Panel_Product_Create" runat="server" DefaultButton="Button_Product_Create">
                <asp:Label ID="Label_Product_Categories" runat="server" CssClass="label_contact" Text="Produktkategori"></asp:Label>
                <asp:DropDownList ID="DropDownList_Product_Categories" DataTextField="category_name" DataValueField="category_id" CssClass="textbox_contact" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Product_Categories" InitialValue="Vælg kategori" ControlToValidate="DropDownList_Product_Categories" runat="server" ErrorMessage="Vælg en kategori" ValidationGroup="Products" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

                <asp:Label ID="Label_Product_Name" runat="server" CssClass="label_contact" Text="Produktnavn"></asp:Label>
                <asp:TextBox ID="TextBox_Product_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Product_Name" ControlToValidate="TextBox_Product_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Products" ForeColor="DarkRed"></asp:RequiredFieldValidator>

                <asp:Label ID="Label_Product_Price" runat="server" CssClass="label_contact" Text="Pris"></asp:Label>
                <asp:TextBox ID="TextBox_Product_Price" runat="server" CssClass="textbox_contact"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Product_Price" ControlToValidate="TextBox_Product_Price" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Products" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator_Product_Price" ControlToValidate="TextBox_Product_Price" Display="Dynamic" ValidationExpression="\d+(?:,\d{1,2})?" runat="server" ErrorMessage="Skal være i formattet x,x" ValidationGroup="Products" ForeColor="DarkRed"></asp:RegularExpressionValidator>
                <asp:RangeValidator ID="RangeValidator_Product_Price" runat="server" ControlToValidate="TextBox_Product_Price" Display="Dynamic" ErrorMessage="For højt tal" MinimumValue="0" MaximumValue="99999" ValidationGroup="Products" ForeColor="DarkRed"></asp:RangeValidator>

                <asp:Label ID="Label_Product_Description" runat="server" CssClass="label_contact" Text="Beskrivelse"></asp:Label>
                <asp:TextBox ID="TextBox_Product_Description" runat="server" TextMode="MultiLine" CssClass="textbox_contact textbox_description"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Product_Description" ControlToValidate="TextBox_Product_Description" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Products" ForeColor="DarkRed"></asp:RequiredFieldValidator>

                <asp:Label ID="Label_Product_Image" runat="server" CssClass="label_contact" Text="Upload billede"></asp:Label>
                <asp:Image ID="Image_Old_Picture" runat="server" /><br />

                <asp:FileUpload ID="FileUpload_Product_Image" runat="server" CssClass="fileupload" />
                <asp:Label ID="Label_Product_Image_Error" runat="server" CssClass="error_message"></asp:Label>
                <br />
                <asp:HiddenField ID="HiddenField_Old_Picture" runat="server" />


                <asp:Button ID="Button_Product_Create" runat="server" Text="Upload" CssClass="button" OnClick="Button_Product_Create_Click" ValidationGroup="Products" />

            </asp:Panel>
            <h3 class="label_contact">Eksisterende produkter</h3>
            <asp:Repeater ID="Repeater_Product" runat="server">
                <ItemTemplate>
                    <div class="products">
                        <img src="<%# Eval("product_image", "/billeder/thumbs/{0}") %>" />
                        <div class="picture_control2">

                            <h3><%# Eval("product_name") %></h3>
                            <label>Kategori: <%# Eval("category_name") %> </label>
                            <br />
                            <label>Pris: <%# Eval("product_price") %> kr,-</label>
                            <br />
                            <label>Uploaded af: <%# Eval("user_login") %></label><br />
                            <label>Tid: <%# Eval("product_datetime") %></label><br />


                        </div>
                        <h4>Beskrivelse</h4>
                        <label><%# Eval("product_description").ToString().Replace(Environment.NewLine,"<br />") %></label><br />
                        <div class="picture_control">
                            <a href="?nav=products&action=edit&id=<%# Eval("product_id") %>">Ret</a>
                            <a href="?nav=products&action=delete&id=<%# Eval("product_id") %>" onclick="return confirm('Dette vil slette produktet!')">Slet</a>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>

        <%--CATEGORIES--%>
        <asp:Panel ID="Panel_Categories" runat="server" CssClass="categories" Visible="false">
            <h2>Kategorier</h2>
            <br />
            <asp:Repeater ID="Repeater_Categories" runat="server">
                <ItemTemplate>
                    <div class="rep_list">

                        <%# Eval("category_name") %>
                        <div class="rep_list_control">
                            <a href="?nav=categories&action=edit&id=<%# Eval("category_id") %>">Ret</a> <a href="?nav=categories&action=delete&id=<%# Eval("category_id") %>" onclick="return confirm('Dette vil slette kategorien!')">Slet</a>

                        </div>
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="Label_Message" runat="server"></asp:Label>
            <br />
            <asp:Panel ID="Panel_Category_Create" runat="server" DefaultButton="Button_Category_Create">
                <asp:Label ID="Label_Category_Name" runat="server" CssClass="label_contact" Text="Opret kategori"></asp:Label>

                <asp:TextBox ID="TextBox_Category_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Category_Name" runat="server" ControlToValidate="TextBox_Category_Name" ErrorMessage="Skal udfyldes" ForeColor="DarkRed"></asp:RequiredFieldValidator><br />

                <asp:Button ID="Button_Category_Create" runat="server" CssClass="button" Text="Send" OnClick="Button_Category_Create_Click" />
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>


