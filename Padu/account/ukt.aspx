﻿<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="ukt.aspx.cs" Inherits="Padu.account.WebForm11" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table#ContentPlaceHolder1_GvHistoryPaid tr:hover { background-color: #d9edf7;}
        th{ color: White !important; background-color: rgb(51, 123, 102);}
        .style1
        {
            font-size: small;
        }
        .style2
        {
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
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
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Biodata Mahasiswa</strong>
                        </div>
                        <div class="panel-body">
                            <table class="table table-hover">
                                <tr>
                                    <td>NPM</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbNPM" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbNama" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Program Studi</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbProdi" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tahun Angkatan</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbAngkatan" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>UKT</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbUkt" runat="server" Text="Rp0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Jumlah Terbayar</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbTerbayar" runat="server">Rp0</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Jumlah Kekurangan</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbKekurangan" runat="server">Rp0</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Beasiswa</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbBeasiswa" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pembimbing Akademik</td>
                                    <td class="text-center">:</td>
                                    <td>
                                        <asp:Label ID="LbDosenPa" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <asp:Panel ID="PanelMessage" runat="server">
                        <strong>
                            <asp:Label ID="LbTagihan" runat="server" Text=""></asp:Label></strong>
                        <strong>
                            <asp:Label ID="LbMsg" runat="server" ForeColor="#FF3300"></asp:Label></strong>
                        <p></p>
                    </asp:Panel>
                    <asp:Panel ID="PanelPembayaran" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Daftar Pembayaran</strong>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:GridView CssClass="table table-bordered" ID="GvHistoryPaid" runat="server" OnRowDataBound="GvHistory_RowDataBound"
                                        OnPreRender="GvHistoryPaid_PreRender">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                       <%-- <div class="alert alert-warning" role="alert">
                            <span class="style2">Pembayaran biaya UKT hanya dapat dibayarkan melalui <strong>Bank JATENG &amp; Bank BTN</strong> dengan cara menunjukkan NPM anda kepada Teller bank, panduan aktifasi pembayaran </span>
                            <asp:HyperLink ID="LinkDwnPanduan" runat="server" CssClass="btn btn-success" NavigateUrl="~/doc/Panduan Aktifasi Pembayaran.pdf" Target="_blank">  download </asp:HyperLink>
                        </div>--%>
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Aktivasi Pembayaran UKT</strong>
                            </div>
                            <div class="panel-body">
                                <em><span class="style1">&nbsp;pilih tahun dan semester</span></em>
                                <table class="table-condensed">
                                    <tr>
                                        <td>
                                            <%--<ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" TargetControlID="DLTahun" runat="server"
                                            Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="OneTopSemester"
                                            LoadingText="Loading" PromptText="Tahun">
                                        </ajaxToolkit:CascadingDropDown>
                                        <asp:DropDownList CssClass="form-control" ID="DLTahun" runat="server">
                                        </asp:DropDownList>--%>
                                            <asp:DropDownList CssClass="form-control" ID="DLTahun" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="-1">Semester</asp:ListItem>
                                                <asp:ListItem Value="1">1 (Gasal)</asp:ListItem>
                                                <asp:ListItem Value="2">2 (Genap)</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnOpenAktv" runat="server" Text="submit"
                                                OnClick="BtnOpenAktv_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <p></p>
                                <asp:Panel ID="PanelAktivasi" runat="server">
                                    Pada awal semester setiap mahasiswa diwajibkan melakukan aktivasi pembayaran dan 
                                membayar biaya UKT sebelum masa pengisian KRS, adapun proses aktivasi pembayaran 
                                dibagi menjadi tiga kelompok mahasiswa yaitu :<br />
                                    <p></p>
                                    <table>
                                        <tr>
                                            <td><strong>1.</strong></td>
                                            <td><strong>Bidikmisi</strong></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Aktivasi pembayaran pada semester
                                    <asp:Label ID="LbSemesterBdk" runat="server">[semester]</asp:Label>
                                                &nbsp;setelah proses validasi peserta bidikmisi selesai dilakukan oleh bagian 
                                    Pelayanan Keuangan</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>status validasi :
                                        <asp:Label ID="LbStatusBdk" runat="server" ForeColor="Red"
                                            Style="font-weight: 700" Text="Pending."></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>2.</strong></td>
                                            <td>
                                                <strong>Non Bidikmisi</strong></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Aktivasi pembayaran sebelum masa pengisian KRS.</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>3.</strong></td>
                                            <td>
                                                <strong>Berprestasi</strong></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>Bagi mahasiswa berprestasi yang memperoleh pembebasan biaya UKT oleh Rektor 
                                        selama periode tertentu diwajibkan melakukan aktivasi pembayaran kemudian lapor 
                                        di bagian Pelayanan Keuangan sebelum masa pengisian KRS.</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <p></p>
                                                <hr />
                                                <asp:Button ID="ButtonAktivasi" runat="server"  CssClass="btn btn-primary" Text="Aktivasi Pembayaran" OnClick="ButtonAktivasi_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <br />
            </div>


        </div>
    </div>
</asp:Content>
