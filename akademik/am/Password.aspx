<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="akademik.am.WebForm15" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Password Mahasiswa</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    NPM</td>
                                <td>
                            <asp:TextBox ID="TbNPM" runat="server" CssClass="form-control" MaxLength="10" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                    </div>
                    <div class="panel-footer">
                            <asp:Button ID="BtnCari" runat="server" Text="Cari" onclick="BtnCari_Click" 
                            CssClass="btn btn-primary" />
                            &nbsp;<asp:Label ID="LbResult" runat="server"></asp:Label>
                    </div>
                </div>
                <hr />
                <asp:Panel ID="PanelPassword" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            Nama &amp; Password</div>
                        <div class="panel-body">
                            <table class="table-bordered table-condensed">
                                <tr>
                                    <td>
                                        Nama
                                    </td>
                                    <td>
                                        :
                                        <asp:Label ID="LbNama" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password
                                    </td>
                                    <td>
                                        :
                                        <asp:Label ID="LbPasswd" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
