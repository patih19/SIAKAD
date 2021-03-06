﻿<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="Padu.account.WebForm10" %>
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
                    <a href="<%= Page.ResolveUrl("~/account/biodata") %>"
                        class="list-group-item"><span class="glyphicon glyphicon-user"></span>&nbsp;Biodata</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">FASILITAS</a>
                    <a href="<%= Page.ResolveUrl("~/account/Pemesanan") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;Pesan Mata Kuliah (Pra KRS)</a>
                    <a href="<%= Page.ResolveUrl("~/account/KRSNew") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;KRS TAHAP I</a>
                    <a href="<%= Page.ResolveUrl("~/account/KrsTahap2") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;KRS TAHAP II</a>
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
                    <%--<div class="text-center">
                        <span style="font-size: large" >KUISIONER PENILAIAN KINERJA DOSEN OLEH MAHASISWA</span></div>
                    <p></p>--%>
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>From Kuisioner Semester Gasal Tahun Akademik 2019/2020</strong></div>
                        <div class="panel-body">
                             Penilaian kinerja dosen oleh mahasiswa melalui laman berikut, <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://goo.gl/forms/7Q92YtcaahMTqs042" Target="_blank">kuisioner</asp:HyperLink> 
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Informasi Kegiatan Pada Semester Genap Tahun Akademik 2019/2020</strong></div>
                        <div class="panel-body">
                            <table class='table table-condensed table-hover'>
                                <thead>
                                    <tr>
                                        <th>NO.</th>
                                        <th>KEGIATAN</th>
                                        <th>TANGGAL</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style='background-color: #e9f3fd'>
                                        <td>1.</td>
                                        <td>Pemesanan Mata Kuliah</td>
                                        <td>3 - 4 Februari 2020</td>
                                    </tr>
                                    <tr style='background-color: #e9f3fd'>
                                        <td>2.</td>
                                        <td>KRS</td>
                                        <td>6,7,8 Februari 2020</td>
                                    </tr>
                                    <tr style='background-color: #e9f3fd'>
                                        <td>3.</td>
                                        <td>Pengajuan Cuti</td>
                                        <td>3 - 8 Februari 2020</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Prosedur Pembayaran UKT</strong></div>
                        <div class="panel-body">

                            <ol>
                                <li>Melakukan <a></a> 
                                    <asp:LinkButton ID="LinkBtnAktifasi" runat="server" PostBackUrl="~/account/Keuangan.aspx">aktifasi pembayaran</asp:LinkButton>
                                    setelah masa KHS
                                </li>
                                <li>Melakukan pembayaran UKT di <strong>Bank BTN atau Bank Jateng</strong>, sesuai tabel berikut :<p></p>
		                            <table class='table table-condensed table-hover'>
                                        <thead>
                                            <tr>
                                                <th>NO.</th>
                                                <th>TAHUN ANGKATAN</th>
                                                <th>FAKULTAS</th>
                                                <th>BANK</th>
                                             </tr>
                                        </thead>
                                        <tbody>
                                            <tr style='background-color: #f8fbd8'>
                                                <td>1.</td>
                                                <td>Mulai 2019/2020</td>
                                                <td>FAKULTAS KEGURUAN DAN ILMU PENDIDIKAN</td>
                                                <td>BANK BTN</td>
                                            </tr>
                                            <tr style='background-color: #f8fbd8'>
                                                <td>2.</td>
                                                <td>Mulai 2019/2020</td>
                                                <td>FAKULTAS EKONOMI</td>
                                                <td>BANK BTN</td>
                                            </tr>
                                            <tr style='background-color: #e9f3fd'>
                                                <td>3.</td>
                                                <td>Mulai 2019/2020</td>
                                                <td>FAKULTAS TEKNIK</td>
                                                <td>BANK JATENG</td>
                                            </tr>
                                            <tr style='background-color: #e9f3fd'>
                                                <td>4.</td>
                                                <td>Mulai 2019/2020</td>
                                                <td>FAKULTAS PERTANIAN</td>
                                                <td>BANK JATENG</td>
                                            </tr>
                                            <tr style='background-color: #e9f3fd'>
                                                <td>5.</td>
                                                <td>Mulai 2019/2020</td>
                                                <td>FAKULTAS ILMU SOSIAL DAN ILMU POLITIK</td>
                                                <td>BANK JATENG</td>
                                            </tr>
                                        </tbody>
                                    </table>
		                            <table class='table table-condensed table-hover'>
                                        <thead>
                                            <tr>
                                                <th>NO.</th>
                                                <th>TAHUN ANGKATAN</th>
                                                <th>FAKULTAS</th>
                                                <th>BANK</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr style='background-color: #e9f3fd'>
                                                <td>1.</td>
                                                <td>Sebelum 2019/2020</td>
                                                <td>SEMUA FAKULTAS</td>
                                                <td>BANK JATENG</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                                <li>Mahasiswa menunjukkan NPM kepada Teller Bank
                                </li>
                                <li>Mahasiswa memperoleh bukti pembayaran UKT
                                </li>
                            </ol>


                        </div>
                    </div>

                    <asp:Image ID="ImgKrs" CssClass=" img-responsive" runat="server" ImageUrl="~/doc/DIAGRAM ALIR SIPADU_0102-1.jpg" /><p></p>
                    <div class="alert alert-success" role="alert">
                            <span >Mahasiswa bidikmisi tahun 2014/2015 dan sebelumnya tidak ada perubahan mekanisme aktivasi pembayaran </span>
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
