<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="keu.aspx.cs" Inherits="Padu.account.WebForm1" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- ==== Untuk membuat dropdown 1.) jquery,min.js dan 2.) bootstrap.min.js harus di definisikan di master dan content page urutannya jgn kebalik ==== -->
    <script src="../Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <link href="../css/print.css" rel="stylesheet" type="text/css" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet" type="text/javascript" />
    <style type="text/css">
        .style1
        {
            font-size: medium;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Ajax Script Manager -->
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
                <a href="<%= Page.ResolveUrl("~/account/pass") %>" class="list-group-item"><span
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
        <%--<div class="col-md-12">
                <table class="table-condensed table-bordered">
                    <tr>
                        <td>
                            NPM :
                        </td>
                        <td>
                            <asp:TextBox ID="TBNPM2" runat="server" BackColor="#F4F4F4" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="BtnView2" runat="server" OnClick="BtnView_Click" Text="View" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>--%>
            <div class="col-md-12">
                <table class="cetak">
                    <tr>
                        <td colspan="4" class="list-group-item-success" style="font-size: 14px">
                            KEUANGAN MAHASISWA
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table class="cetak2" style="background: transparent">
                                <tr>
                                    <td>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/Logo-Untidar-128-.png" Height="90px"
                                            Width="90px" />
                                    </td>
                                    <td>
                                        <h5>
                                            Kementerian 
                                            Riset, Teknologi, dan Pendidikan Tinggi</h5>
                                        <h4>
                                            UNIVERSITAS TIDAR
                                        </h4>
                                        <strong><span class="style1">Biro Administrasi Umum dan Keuangan</span></strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nama
                        </td>
                        <td>
                            <asp:Label ID="LbNama2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            Jumlah Kewajiban
                        </td>
                        <td>
                            <asp:Label ID="LbKewajiban2" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            NPM
                        </td>
                        <td>
                            <asp:Label ID="LbNPM2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            Jumlah Terbayar
                        </td>
                        <td>
                            <asp:Label ID="LbTerbayar" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Jenis Kelas
                        </td>
                        <td>
                            <asp:Label ID="LBKelas2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            Jumlah Kekurangan
                        </td>
                        <td>
                            <asp:Label ID="LbKurang" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tahun
                        </td>
                        <td>
                            <asp:Label ID="LBTahun2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <hr />
                <h5>
                    <strong>TAGIHAN MAHASISWA</strong></h5>
            </div>
            <div class="col-md-12">
                <table class="table-bordered table-condensed ">
                    <tr>
                        <td colspan="3">
                            Tagihan Awal :<asp:GridView ID="GVBebanAwal2" runat="server" CellPadding="4" CssClass="table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVBebanAwal_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tagihan Periodik
                            <br />
                            <asp:GridView ID="GVTagihanTotal2" runat="server" CellPadding="4" CssClass="table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVTagihanTotal_RowDataBound"
                                ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </td>
                        <td style="vertical-align: top">
                            Tagihan SKS&nbsp;
                            <asp:GridView ID="GVSks2" runat="server" CssClass="table-condensed" CellPadding="4"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVSks_RowDataBound" ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </td>
                        <td style="vertical-align: top">
                            Tagihan Skripsi<asp:GridView ID="GVSkripsi" runat="server" CellPadding="4" ForeColor="#333333"
                                GridLines="None" CssClass="table-condensed" OnRowDataBound="GVSkripsi_RowDataBound"
                                ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            Tagihan Akhir :<br />
                            <asp:GridView ID="GVTgKp" runat="server" CssClass="table-condensed" CellPadding="4"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVTgKp_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                            <asp:GridView ID="GVKkn" runat="server" CellPadding="4" CssClass="table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVKkn_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                            <asp:GridView ID="GVWisudua" runat="server" CellPadding="4" CssClass="table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVWisudua_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                            <asp:GridView ID="GVPplSd" runat="server" CellPadding="4" CssClass="table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVPplSd_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                            <asp:GridView ID="GVPplSma" runat="server" CellPadding="4" CssClass="table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVPplSma_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-12">
                <hr />
                <h5>
                    <strong>REKAP PEMBAYARAN</strong>
                </h5>
                1. Tagihan Belum Dibayar (Unpaid)
                <asp:GridView CssClass="table-condensed table-bordered" ID="GVPost2" runat="server"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVPost2_RowDataBound1">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
                <br />
                <asp:GridView CssClass="table-condensed table-bordered" ID="GVUnAkhir" runat="server"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
                2. Tagihan Sudah Dibayar / LUNAS (Paid)
                <br />
                <asp:GridView CssClass="table-condensed table-bordered" ID="GVBayar" runat="server"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVBayar_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
                <asp:GridView ID="GVLunasAkhir" CssClass="table-condensed table-bordered" runat="server"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVLunasAkhir_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <hr />
                <asp:Panel ID="PanelTblByrSKS2" runat="server">
                    <h5>
                        <strong>AKTIFASI PEMBAYARAN 
                            (<asp:Label ID="LblSmster" runat="server" Text=""></asp:Label>)</strong></h5>
                        <%--<table class="table-condensed table-bordered">
                        <tr>
                            <td>
                                Tahun Semester :
                            </td>
                            <td>
                                <asp:DropDownList ID="DLSemster2" runat="server">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDLSemester" runat="server" TargetControlID="DLSemster2"
                                    Category="DLSemester2" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="TopSemester"
                                    LoadingText="Loading" PromptText="Semester">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                            <td>
                                <asp:Button ID="BtnViewBiaya2" runat="server" OnClick="BtnViewBiaya_Click" Text="Lihat" />
                                &nbsp;Klik tombol Lihat untuk melakukan Aktifasi Pembayaran
                            </td>
                        </tr>
                    </table>--%>
                </asp:Panel>
                <asp:Panel ID="PanelAngsuran" runat="server">
                    <table class="table-condensed">
                        <tr>
                            <td style="background-color: #FCE189">
                                A. PEMBAYARAN ANGSURAN PERTAMA (Masa KRS)
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>
                                    <!-- <asp:Label ID="LbViewAngsuran2" runat="server"></asp:Label>
                                    <asp:GridView ID="GVAngsuran2" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                        ForeColor="#333333" GridLines="None" OnRowDataBound="GVAngsuran_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBPilih" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                    <asp:Panel ID="PanelSKS" runat="server">
                                        <span class="style4">Jumlah SKS Sudah Disetujui Pembimbing Akademik (PA), WAJIB !!!</span><br />
                                        <span class="style6">Jika belum hubungi PA</span><span class="style5"> </span>
                                        <table id="TableSKS" class="table-condensed table-bordered">
                                            <tr>
                                                <td>
                                                    Jumlah SKS
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:TextBox ID="TbTotalSKS2" runat="server" Width="30px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Angsuran ke 1, KHUSUS Angkatan (2013/2014) &amp; (2014/2015)
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelBayar" runat="server">
                                        <br />
                                        <asp:Button ID="BtnOk2" runat="server" OnClick="BtnOk_Click" Text="Bayar" Enabled="false" />
                                        <asp:Label ID="LbPostSuccess" runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="LbResultBayar2" runat="server"></asp:Label>
                                    </asp:Panel> -->
                                    Tagihan Otomatis Aktif Setelah Mahasiswa Mengisi KRS</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #FCE189">
                                B. PEMBAYARAN ANGSURAN KEDUA (Sebelum UTS)
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jumlah Kekurangan :
                                <asp:Label ID="LbKekurangan2" runat="server" Style="font-size: medium"></asp:Label>
                                <br />
                                <br />
                                Jika terdapat tagihan dan rekap pembayaran yang tidak sesuai kwitansi pembayaran<br />
                                segera perbaiki rekap pembayaran di bagian Pelayanan Keuangan BAUK<br />
                                sebelum melakukan Aktifasi Pembayaran<br />
                                <br />
                                <asp:Button ID="BtnBayar2" runat="server" OnClick="BtnBayar2_Click" Text="Bayar" />
                                <br />
                                <asp:Label ID="LbPostSuccess2" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="LbResultBayar3" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <br />
        </div>
    </div>
    </div>
</asp:Content>
