<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="Padu.account.WebForm10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <div class="text-center">
                        <span style="font-size: large" >KUISIONER PENILAIAN KINERJA DOSEN OLEH MAHASISWA</span></div>
                    <p></p>
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>From Kuisioner Penilaian </strong>
                        </div>
                        <div class="panel-body">
                             Penilaian kinerja dosen oleh mahasiswa melalui laman berikut, <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://goo.gl/forms/7Q92YtcaahMTqs042" Target="_blank">kuisioner</asp:HyperLink> 
                        </div>
                    </div>
                    <hr />
                    <div class="text-center">
                        <span style="font-size: large" >ALUR PENGISIAN DAN BIMBIGAN KRS</span></div>
                    <br />
                    <asp:Image ID="ImgKrs" CssClass=" img-responsive" runat="server" ImageUrl="~/doc/DIAGRAM ALIR SIPADU_0102-1.jpg" /><p></p>
                    <div class="alert alert-success" role="alert">
                            <span >Mahasiswa bidikmisi tahun 2014/2015 dan sebelumnya tidak ada perubahan alur aktivasi pembayaran </span>
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
