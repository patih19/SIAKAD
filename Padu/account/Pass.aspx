<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="Pass.aspx.cs" Inherits="Padu.account.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style4
    {
        font-size: small;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="background: #fafafa">
        <br />
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">AKUN</a> <a
                        href="<%= Page.ResolveUrl("~/account/keuangan") %>" class="list-group-item"><span
                            class="glyphicon glyphicon-transfer"></span>&nbsp;Aktifasi Pembayaran</a>
                    <a href="#" class="list-group-item"><span
                        class="glyphicon glyphicon-picture "></span>&nbsp;Upload Foto</a>
                    <a href="<%= Page.ResolveUrl("~/account/biodata") %>"
                        class="list-group-item"><span class="glyphicon glyphicon-user"></span>&nbsp;Biodata</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">FASILITAS</a>
                    <a href="<%= Page.ResolveUrl("~/account/KRSNew") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;KRS</a>
                    <a href="<%= Page.ResolveUrl("~/account/KHS") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;KHS</a>
                    <a href="<%= Page.ResolveUrl("~/account/KartuUjian") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Kartu Ujian</a>
                    <a href="<%= Page.ResolveUrl("~/account/Transkrip") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Transkrip Nilai</a>
                    <a href="<%= Page.ResolveUrl("~/account/PengajuanCuti") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Pengajuan Cuti</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">SYSTEM</a>
                    <a href="<%= Page.ResolveUrl("~/account/pass") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-lock"></span>&nbsp;Ganti Password</a>
                    <a id="keluar" runat="server" href="#" class="list-group-item"><span class="glyphicon glyphicon-log-out"></span>&nbsp;Logout </a>
                </div>
                <!-- <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">LAMPIRAN</a>
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;Kartu Uijan </a>
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Surat Pernyataan</a> 
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Formulir Pendaftaran</a> 
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-ok">
                        </span>&nbsp;Cek List Berkas</a>
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-picture">
                        </span>&nbsp;Upload Foto</a>
                </div> -->
            </div>
            <div class="col-md-9">
                <div class="col-md-12">
                    <strong>Ganti Password</strong><br />
                    <br />
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
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="BtGanti" runat="server" Text="OK" onclick="BtGanti_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
