<%@ Page Title="" Language="C#" MasterPageFile="~/pasca/Pasca.Master" AutoEventWireup="true" CodeBehind="password.aspx.cs" Inherits="Padu.pasca.password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="content-header">
        <h1>Password</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Ganti Password</strong>
                    </div>
                    <div class="panel-body">
                        <table class="table-bordered table-condensed">
                            <tr style="background-color: #bce8f1">
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
                                <td colspan="2" style="background-color: #bce8f1">Password Baru</td>
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
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="BtGanti" CssClass="btn btn-danger" runat="server" Text="OK" OnClick="BtGanti_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>



            </div>
        </div>
    </div>
</asp:Content>
