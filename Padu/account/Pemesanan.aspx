<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="Pemesanan.aspx.cs" Inherits="Padu.account.Pemesanan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .mdl {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.5;
            filter: alpha(opacity=50);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 115px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 95px;
                width: 95px;
            }
    </style>

    <style type="text/css">
        .top {
            float: left;
        }

        .dataTables_filter {
            float: left !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="background: #fafafa">
        <br />
        <div class="row">
            <div class="col-sm-12 col-md-3 col-lg-3">
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
            <div class="col-sm-12 col-md-9 col-lg-9">
                <div class="col-md-12 col-lg-12">
                    <div class="alert alert-success" role="alert">
                        <p><span><strong>PEMESANAN MATA KULIAH DIPERUNTUKKAN BAGI SELURUH MAHASISWA AKTIF (UKT DAN NON UKT)</strong></span></p>
                        <p><span>MAHASISWA WAJIB MEMESAN MATA KULIAH SEBELUM MASA PENGISIAN KRS</span></p>
                        <p>Panduan KRS, <asp:HyperLink ID="HyperLinkKRS" runat="server" NavigateUrl="~/doc/alur_krs.pdf" Target="_blank">download</asp:HyperLink></p>
                    </div>
                </div>

                <asp:Panel ID="PanelPenawaran" runat="server">
                    <asp:UpdatePanel ID="PanelDosenWali" runat="server">
                        <ContentTemplate>

                            <%--Semester Pemesanan--%>
                            <div class="col-md-12 col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading ui-draggable-handle">
                                        <h5><strong>-- === PEMESANAN MATA KULIAH === -- </strong></h5>
                                        <asp:Label ID="LbSemester" runat="server" ForeColor="#FF3300"></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList CssClass="form-control" ID="DLTahun" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;&nbsp;</td>
                                                <td>
                                                    <asp:DropDownList CssClass="form-control" ID="DLSemester" runat="server">
                                                        <asp:ListItem Value="-1">-- Pilih Semester --</asp:ListItem>
                                                        <asp:ListItem Value="1">Gasal</asp:ListItem>
                                                        <asp:ListItem Value="2">Genap</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="BtnSubmit" CssClass="btn btn-primary" runat="server" Text="SUBMIT" OnClick="BtnSubmit_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="PanelMsg" runat="server">
                                <div class="col-md-12 col-lg-12">
                                    <div class="alert alert-danger" role="alert">
                                        Jumlah Maksimal Pengambilan SKS =  
                                        <asp:Label ID="LbMaxSKS" runat="server" Text=""></asp:Label>
                                        <br />
                                         <strong> HUBUNGI PRODI JIKA ADA MATA KULIAH BELUM DITAWARAKAN</strong>
                                    </div>
                                </div>
                            </asp:Panel>

                            <%-- Mata Kuliah Ditawarkan--%>
                            <div class=" col-md-6 col-lg-6">
                                <asp:Panel ID="PanelMakulDitawarkan" runat="server">
                                    <div class="panel panel-default">
                                        <div class="panel-heading ui-draggable-handle">
                                            <strong>DAFTAR MATA KULIAH DITAWARKAN</strong>
                                        </div>
                                        <div class="panel-body">
                                            <div class="table-responsive">
                                                <asp:GridView CssClass="table table-bordered" ID="GvMakulDitawarkan" runat="server" OnRowDataBound="GvMakulDitawarkan_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="Btn_Pesan" runat="server" OnClick="Btn_Pesan_Click" Text="PESAN" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>

                            <%-- Mata Kuliah Dipesan--%>
                            <div class="col-md-6 col-lg-6">
                                <asp:Panel ID="PanelMakulDipesan" runat="server">
                                    <div class="panel panel-default">
                                        <div class="panel-heading ui-draggable-handle">
                                            <strong>MATA KULIAH DIPESAN</strong>
                                        </div>
                                        <div class="panel-body">
                                            <div class="table-responsive">
                                                <asp:GridView CssClass="table table-bordered" ID="GvMakulDiPesan" runat="server" OnRowDataBound="GvMakulDiPesan_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="BtnHapus" runat="server" OnClick="BtnHapus_Click" Text="HAPUS" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="mdl">
                            <div class="center">
                                <img alt="" src="../images/loading135.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

            </div>
        </div>
    </div>
</asp:Content>
