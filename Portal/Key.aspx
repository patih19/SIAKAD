<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="Key.aspx.cs" Inherits="Portal.WebForm9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Ganti Password</strong></div>
                    <div class="panel-body">
                        <table class="table-bordered table-condensed">
                            <tr style="background-color: #bce8f1">
                                <td colspan="2">
                                    Password Lama
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <em>Password</em>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbOldPw" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="background-color: #bce8f1">
                                    Password Baru
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <em>Password</em>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbNewPw" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <em>Retype Password</em>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbReNewPw" runat="server" CssClass="form-control" MaxLength="20"
                                        TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="BtGanti" runat="server" Text="OK" OnClick="BtGanti_Click" 
                                        CssClass="btn btn-success" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>