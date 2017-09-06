<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="Padu.account.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <link href="../css/print.css" rel="stylesheet" type="text/css" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet" type="text/javascript" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
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
                <a href="<%= Page.ResolveUrl("~/account/KRS") %>" class="list-group-item"><span
                    class="glyphicon glyphicon-check"></span>&nbsp;KRS</a>
                <a href="<%= Page.ResolveUrl("~/account/KHS") %>" class="list-group-item"><span
                    class="glyphicon glyphicon-file"></span>&nbsp;KHS</a> 
                <a href="<%= Page.ResolveUrl("~/account/KartuUjian") %>" class="list-group-item"><span
                    class="glyphicon glyphicon-file"></span>&nbsp;Kartu Ujian</a> 
            </div>
            <div class="list-group">
                <a href="#" class="list-group-item" style="background-color: #87cefa">SYSTEM</a>
                <a href="<%= Page.ResolveUrl("~/account/Pass") %>" class="list-group-item"><span
                    class="glyphicon glyphicon-lock"></span>&nbsp;Ganti Password</a>
                <a id="keluar" runat="server" href="#" class="list-group-item"><span class="glyphicon glyphicon-log-out">
                </span>&nbsp;Logout </a>
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
                <h5>
                    <strong>Upload Foto</strong></h5>
                Ketentuan upload foto :
                <ol>
                    <li>File dalam format jpg, png atau jpeg</li>
                    <li>Ukuran file kurang dari 100 Kb</li>
                </ol>
                <br />
                <table class="table-condensed">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadFoto" runat="server" Height="20px" Width="221px" />
                            <br />
                            <!-- <asp:Button ID="BtnUpload" runat="server" OnClick="BtnUpload_Click" Text="Upload" /> -->
                            &nbsp;<asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="ImageFoto" runat="server" Height="150px" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <br />
        </div>
    </div>
    </div>
</asp:Content>
