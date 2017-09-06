<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="akademik.am.WebForm33" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <br />
        TAMBAH USER
        <br /><br />
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <table class="table">
                    <tr>
                        <td>
                            Username:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUsername" runat="server" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" OnClick="Submit" Text="Submit" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    
    
</asp:Content>
