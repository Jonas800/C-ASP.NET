<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Upload pictures --%>
    <div class="center2">
        <asp:Panel ID="Panel_Picture" runat="server" DefaultButton="Button_Post">
            <h2>Upload new picture</h2>
            <asp:DropDownList ID="DropDownList_Categories" DataTextField="category_name" DataValueField="category_id" runat="server"></asp:DropDownList>
            <br />
            <asp:Label ID="Label_Title" runat="server" Text="Title"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_Title" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label_Comment" runat="server" Text="Comment"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_Comment" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Label ID="Label_Picture" runat="server" Text="Upload billede"></asp:Label><br />
            <asp:FileUpload ID="FileUpload_Picture" runat="server" />
            <br />
            <br />
            <asp:Button ID="Button_Post" OnClick="Button_Post_Click" runat="server" Text="Upload" />

            <br />
            <br />

        </asp:Panel>
        <a class="logout" href="?logout=true">Log out</a>

    </div>


    <%-- Manage categories --%>
    <div class="center2">
        <h2>Manage categories</h2>
        <asp:Repeater ID="Repeater_Categories" runat="server">
            <ItemTemplate>
                <p><%# Eval("category_name") %> <a href="?obj=cat&action=edit&id=<%# Eval("category_id") %>">Edit</a> <a href="?obj=cat&action=delete&id=<%# Eval("category_id") %>" onclick="return confirm('This will delete the category')">Delete</a></p>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Label ID="Label_Message" runat="server"></asp:Label>
        <br />
        <asp:Panel ID="Panel_Category_Create" runat="server" DefaultButton="Button_Category_Create">
            <asp:Label ID="Label_Category_Input" runat="server" Text="Create a new category"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_Category_Input" runat="server"></asp:TextBox>

            <asp:Button ID="Button_Category_Create" runat="server" Text="Create" OnClick="Button_Category_Create_Click" />
        </asp:Panel>

        <asp:Panel ID="Panel_Category_Edit" runat="server" Visible="false" DefaultButton="Button_Category_Edit">
            <asp:Label ID="Label_Category_Edit" runat="server" Text="Edit selected category"></asp:Label>
            <asp:TextBox ID="TextBox_Category_Edit" runat="server"></asp:TextBox>
            <asp:Button ID="Button_Category_Edit" runat="server" Text="Edit" OnClick="Button_Category_Edit_Click" />
            <a href="Admin.aspx">Cancel</a>
        </asp:Panel>
        <br />
        <br />


    </div>
    <%-- Manage pictures --%>
    <div class="center2">
        <h2>Manage pictures</h2>
        <asp:Panel ID="Panel_Manage_Pictures" runat="server" DefaultButton="Button_Pictures" Visible="false">
            <asp:Image ID="Image_Old_Picture" runat="server" /><br />
            <asp:FileUpload ID="FileUpload_Pictures_New" runat="server" /><br />
            <asp:Label ID="Label_Pictures_Category" runat="server" Text="Category"></asp:Label>
            <asp:DropDownList ID="DropDownList_Pictures_Categories" DataTextField="category_name" DataValueField="category_id" runat="server"></asp:DropDownList>
            <asp:Label ID="Label_Pictures_Title" runat="server" Text="Change title for selected picture"></asp:Label><br />
            <asp:TextBox ID="TextBox_Pictures_Title" runat="server"></asp:TextBox><br />
            <asp:Label ID="Label_Pictures_Comment" runat="server" Text="Change comment for selected picture"></asp:Label><br />
            <asp:TextBox ID="TextBox_Pictures_Comment" runat="server"></asp:TextBox>
            <asp:HiddenField ID="HiddenField_Old_Picture" runat="server" />
            <br />
            <br />
            <asp:Button ID="Button_Pictures" runat="server" Text="Save" OnClick="Button_Pictures_Click" />

        </asp:Panel>
        <asp:Repeater ID="Repeater_Pictures" runat="server">
            <ItemTemplate>
                <div class="thumbnails_admin">
                    <img src="<%# Eval("picture_name", "/billeder/thumbs/{0}") %>" />

                    <div class="picture_control">
                        <a href="?obj=picture&action=edit&id=<%# Eval("picture_id") %>">Edit</a>
                        <a href="?obj=picture&action=delete&id=<%# Eval("picture_id") %>">Delete</a>
                    </div>
                    <div class="picture_control2">
                        <h3>Title: <%# Eval("picture_title") %></h3>
                        <label>Category: <%# Eval("category_name") %> </label>
                        <br />
                        <label>Uploaded by: <%# Eval("user_name") %></label><br />
                        <label>on <%# Eval("picture_datetime") %></label><br />
                        <h4>Description</h4>
                        <label><%# Eval("picture_comment") %></label><br />

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>


</asp:Content>

