<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Key.aspx.cs" Inherits="akademik.am.WebForm14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style4
    {
        font-size: small;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Ganti Password</strong></div>
                        <div class="panel-body">
                    <table class="table-bordered table-condensed">
                        <tr style="background-color:#bce8f1">
                            <td colspan="2">Password Lama</td>
                        </tr>
                        <tr>
                            <td class="style4"><em>Password</em></td>
                            <td>
                                <asp:TextBox ID="TbOldPw" runat="server" CssClass="form-control" MaxLength="20" 
                                    TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"  style="background-color:#bce8f1">Password Baru</td>
                        </tr>
                        <tr>
                            <td class="style4"><em>Password</em></td>
                            <td>
                                <asp:TextBox ID="TbNewPw" runat="server" CssClass="form-control" MaxLength="20" 
                                    TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4"><em>Retype Password</em></td>
                            <td>
                                <asp:TextBox ID="TbReNewPw" runat="server" CssClass="form-control" 
                                    MaxLength="20" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        </table>
                        </div>
                        <div class="panel-footer">
                                <asp:Button ID="BtGanti" runat="server" Text="OK" onclick="BtGanti_Click" 
                                    CssClass="btn btn-primary" />
                        </div>
                    </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
