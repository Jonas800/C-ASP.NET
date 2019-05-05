<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="HoldAdmin.aspx.cs" Inherits="HoldAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">

        <h2>Hold</h2>
        <a class="admin_button" href="?opret=true">Opret nyt hold</a>
        <br />
        <asp:Panel ID="Panel_Hold_Create" runat="server" DefaultButton="Button_Hold_Create" Visible="false">

            <asp:Label ID="Label_Hold_Instruktoerer" runat="server" CssClass="label_contact" Text="Instruktører"></asp:Label>
            <asp:DropDownList ID="DropDownList_Hold_Instruktoerer" DataTextField="instructor_name" DataValueField="instructor_id" CssClass="textbox_contact" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Hold_Instruktoerer" InitialValue="Vælg instruktør" ControlToValidate="DropDownList_Hold_Instruktoerer" runat="server" ErrorMessage="Vælg en instruktør" ValidationGroup="Hold" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Hold_Stilarter" runat="server" CssClass="label_contact" Text="Stilarter"></asp:Label>
            <asp:DropDownList ID="DropDownList_Hold_Stilarter" DataTextField="style_name" DataValueField="style_id" CssClass="textbox_contact" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Hold_Stilarter" InitialValue="Vælg stilart" ControlToValidate="DropDownList_Hold_Stilarter" runat="server" ErrorMessage="Vælg en stilart" ValidationGroup="Hold" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Hold_Aldersgrupper" runat="server" CssClass="label_contact" Text="Aldersgrupper"></asp:Label>
            <asp:DropDownList ID="DropDownList_Hold_Aldersgrupper" DataTextField="agegroup_name" DataValueField="agegroup_id" CssClass="textbox_contact" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Hold_Aldersgrupper" InitialValue="Vælg aldersgruppe" ControlToValidate="DropDownList_Hold_Aldersgrupper" runat="server" ErrorMessage="Vælg en aldersgruppe" ValidationGroup="Hold" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Hold_Niveauer" runat="server" CssClass="label_contact" Text="Niveauer"></asp:Label>
            <asp:DropDownList ID="DropDownList_Hold_Niveauer" DataTextField="level_name" DataValueField="level_id" CssClass="textbox_contact" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Hold_Niveauer" InitialValue="Vælg niveau" ControlToValidate="DropDownList_Hold_Niveauer" runat="server" ErrorMessage="Vælg et niveau" ValidationGroup="Hold" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Hold_Name" runat="server" CssClass="label_contact" Text="Holdnummer"></asp:Label>
            <asp:TextBox ID="TextBox_Hold_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Hold_Name" ControlToValidate="TextBox_Hold_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Hold" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Hold_Price" runat="server" CssClass="label_contact" Text="Pris"></asp:Label>
            <asp:TextBox ID="TextBox_Hold_Price" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Hold_Price" ControlToValidate="TextBox_Hold_Price" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Hold" ForeColor="DarkRed"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Hold_Price" ControlToValidate="TextBox_Hold_Price" Display="Dynamic" ValidationExpression="\d+(?:,\d{1,2})?" runat="server" ErrorMessage="Skal være i formattet x,x" ValidationGroup="Hold" ForeColor="DarkRed"></asp:RegularExpressionValidator>
            <asp:RangeValidator ID="RangeValidator_Hold_Price" runat="server" ControlToValidate="TextBox_Hold_Price" Display="Dynamic" ErrorMessage="For højt tal" MinimumValue="0" MaximumValue="99999" ValidationGroup="Hold" ForeColor="DarkRed"></asp:RangeValidator>

            <asp:Button ID="Button_Hold_Create" runat="server" Text="Upload" CssClass="button" OnClick="Button_Hold_Create_Click" ValidationGroup="Hold" />

        </asp:Panel>
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>ID <a href="?column=team_id&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=team_id&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Holdnr. <a href="?column=team_number&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=team_number&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Instruktør <a href="?column=instructor_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=instructor_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Stilart <a href="?column=style_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=style_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Niveau <a href="?column=level_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=level_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Aldersgruppe <a href="?column=agegroup_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=agegroup_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Pris <a href="?column=team_price&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=team_price&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Tilmeldte</th>
                        <th></th>
                        <th></th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("team_id") %></td>
                    <td><%# Eval("team_number") %></td>
                    <td><%# Eval("instructor_name") %></td>
                    <td><%# Eval("style_name") %></td>
                    <td><%# Eval("level_name") %></td>
                    <td><%# Eval("agegroup_name") %></td>
                    <td><%# Eval("team_price") %> kr.</td>
                    <td><%# Eval("antal") %></td>
                    <td><a class="admin_button" href="HoldAdmin.aspx?id=<%# Eval("team_id") %>&ret=true">Rediger</a></td>
                    <td><a class="admin_button" href="HoldAdmin.aspx?id=<%# Eval("team_id") %>&slet=true" onclick="return confirm('Er du sikker?')">Slet</a></td>
                    <td><a class="admin_button" href="HoldAdmin.aspx?id=<%# Eval("team_id") %>&oversigt=true">Tilmeldte</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Repeater ID="Repeater_View" runat="server" OnItemDataBound="Repeater_View_ItemDataBound" Visible="false">
            <ItemTemplate>
                <h3><%# Eval("team_number") %></h3>
                <asp:Repeater ID="Repeater_nested" runat="server">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>ID <a href="?column=customer_id&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=customer_id&direction=DESC"><i class="fa fa-angle-down"></th>
                                <th>Fornavn <a href="?column=customer_firstname&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=customer_firstname&direction=DESC"><i class="fa fa-angle-down"></th>
                                <th>Efternavn <a href="?column=customer_lastname&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=customer_lastname&direction=DESC"><i class="fa fa-angle-down"></th>
                                <th>Email <a href="?column=user_email&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=user_email&direction=DESC"><i class="fa fa-angle-down"></th>
                                <th>By <a href="?column=customer_city&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=customer_city&direction=DESC"><i class="fa fa-angle-down"></th>
                                <th>Postnr. <a href="?column=customer_postal&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=customer_postal&direction=DESC"><i class="fa fa-angle-down"></th>
                                <th>Adresse</th>
                                <th>Telefon</th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("customer_id") %></td>
                            <td><%# Eval("customer_firstname") %></td>
                            <td><%# Eval("customer_lastname") %></td>
                            <td><%# Eval("user_email") %></td>
                            <td><%# Eval("customer_city") %></td>
                            <td><%# Eval("customer_postal") %></td>
                            <td><%# Eval("customer_address") %></td>
                            <td><%# Eval("customer_phone") %></td>
                            <td><a class="admin_button" href="HoldAdmin.aspx?id=<%# Eval("team_id") %>&oversigt=true&kunde_id=<%# Eval("teams_and_customers_id") %>&afmeld=true">Afmeld</a></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

