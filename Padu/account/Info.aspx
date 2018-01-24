<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="Padu.account.WebForm10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: x-large;
        }
        .style2
        {
            background-color: #FF6600;
        }
        .style4
        {
            background-color: #FFCC00;
        }
        .style5
        {
            background-color: #FF9900;
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
                    <div class="text-center">
                        <span class="style1">PENGUMUMAN</span></div>
                    <br />
                <p>Diberitahukan kepada seluruh Mahasiswa aktif Universitas Tidar bahwa 
                    <span class="style2"><span class="style4">untuk  dapat mengikuti Ujian</span></span><span 
                        class="style4"> Tengah Semester (UTS)  dan 
                Ujian Akhir Semester (UAS)</span> Mahasiswa diwajibkan membayar tagihan semester ini 
                    sampai dengan <span class="style5">hari Jumat tanggal 8 April 2016.</span></p>
                <table class=" table-bordered table-condensed">
                    <tr>
                        <td colspan="2"><strong>Mahasiswa Baru (2015/2016)</strong></td>
                    </tr>
                    <tr>
                        <td>Non Bidikmisi</td>
                        <td>Melunasi tagihah semester ini bagi yang belum lunas, bagi yang sudah lunas dapat 
                            mengunduh kartu ujian</td>
                    </tr>
                    <tr>
                        <td>Bidikmisi</td>
                        <td>Mengunduh kartu ujian</td>
                    </tr>
                    <tr>
                        <td colspan="2"><strong>Mahasiswa Lama (Non 2015/2016)</strong></td>
                    </tr>
                    <tr>
                        <td>Non Bidikmisi</td>
                        <td>Melunasi tagihah semester ini</td>
                    </tr>
                    <tr>
                        <td>Bidikmisi</td>
                        <td>Melakukan aktivasi kemudian lapor di bagian Pelayanan Keuangan</td>
                    </tr>
                </table>
                <br />
                <p><strong><em>Bagi Mahasiswa yang masih memiliki tunggakan dapat dikonsultasikan di bagian Pelayanan Keuangan</em></strong></p>
                
                    <br />
                    <br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Alur Bayar UTS.png" />
                    <br />
                    Kartu Ujian dapat diunduh melalui menu <strong>&quot;Kartu Ujian&quot;</strong><br /><br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
