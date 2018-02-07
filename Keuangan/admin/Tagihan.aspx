<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="Tagihan.aspx.cs" Inherits="Keuangan.admin.WebForm8" EnableEventValidation="false" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

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
        .style4
        {
            color: #535353;
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Ajax Script Manager -->
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
<div class="container top-main-form" style="background: #fafafa">
        <div class="row top-buffer">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">AKUN</a> 
                    <a href="<%= Page.ResolveUrl("~/admin/home.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-home "></span>
                        &nbsp;Beranda </a><a id="keluar" runat="server" href="#" class="list-group-item"><span
                            class="glyphicon glyphicon-log-out"></span>&nbsp;Logout </a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">BIAYA PERIODIK</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Biaya.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;Biaya Studi</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Angsuran.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Biaya Angsuran</a>
                    <a href="<%= Page.ResolveUrl("~/admin/SKS.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Edit SKS</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">BIAYA NON PERIODIK</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Biaya_Akhir.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;Biaya Studi Akhir</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Bayar.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Pembayaran</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">PEMBAYARAN</a>
                    <a href="<%= Page.ResolveUrl("~/admin/dispen.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Dispensasi</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Tagihan.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Tagihan</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Masa_Bayar.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Masa Pembayaran</a>
                    <a href="<%= Page.ResolveUrl("~/admin/InputTagihan.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Posting Kekurangan</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Edit_Bayar.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Perbarui Pembayaran</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Beban_Awal.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Beban Awal</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">KEAMANAN</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Pass.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-warning-sign ">
                    </span>&nbsp;Ganti Password </a>
                </div>
            </div>
            <div class="col-md-9">
            <div class="col-md-12">
                     <table class="table-condensed">
                        <tr>
                            <td>
                                NPM :
                            </td>
                            <td>
                                <asp:TextBox ID="TBNPM2" runat="server" BackColor="White" MaxLength="10"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BtnView2" runat="server" OnClick="BtnView_Click" Text="View" />
                            </td>
                        </tr>
                    </table>
                    <br />              
             </div>
                <asp:Panel ID="PanelContent" runat="server">
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
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/Logo-Untidar-88.png" Height="88px"
                                                    Width="88px" />
                                            </td>
                                            <td>
                                                <h5>
                                                    Kementerian Pendidikan dan Kebudayaan
                                                </h5>
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
                                    <asp:Label ID="LbTerbayar" runat="server" Text="Label"></asp:Label>
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
                                    <asp:Label ID="LbKurang" runat="server" Text="Label"></asp:Label>
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
                            <tr>
                                <td>
                                    Program Studi
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="LbProdi" runat="server" Text="Label"></asp:Label>
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
                                    Tagihan Awal<asp:GridView ID="GVBebanAwal2" runat="server" CellPadding="4" CssClass="table-condensed"
                                        ForeColor="#333333" GridLines="None" OnRowDataBound="GVBebanAwal_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tagihan Periodik
                                    <asp:GridView ID="GVTagihanTotal2" runat="server" CellPadding="4" CssClass="table-condensed"
                                        ForeColor="#333333" GridLines="None" OnRowDataBound="GVTagihanTotal_RowDataBound"
                                        ShowFooter="True">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                                <td style="vertical-align: top">
                                    Tagihan SKS
                                    <asp:GridView ID="GVSks2" runat="server" CellPadding="4" CssClass="table-condensed"
                                        ForeColor="#333333" GridLines="None" OnRowDataBound="GVSks_RowDataBound" ShowFooter="True">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                                <td style="vertical-align: top">
                                    Tagihan Skripsi<asp:GridView ID="GVSkripsi" runat="server" CssClass="table-condensed"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="GVSkripsi_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="PanelAkhir" runat="server">
                                        <div class="col-md-3">
                                            Tagihan Wisuda :
                                            <asp:GridView ID="GVWisuda" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVAkhir_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-3">
                                            Tagihan KKN :
                                            <asp:GridView ID="GVKkn" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVKkn_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-3">
                                            Tagihan KP :
                                            <asp:GridView ID="GVKP" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVKP_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5cacee" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="col-md-12">
                        <hr />
                        <h5>
                            <strong>REKAP PEMBAYARAN</strong>
                        </h5>
                        <asp:Panel ID="PanelPaid" runat="server">
                            TAGIHAN PERIODIK SUDAH DIBAYAR (PAID) :
                            <asp:GridView ID="GVPost2" runat="server" CellPadding="4" CssClass="table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVPost2_RowDataBound" ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="PanelUnpaid" runat="server">
                            <br />
                            TAGIHAN PERIODIK BELUM DIBAYAR (UNPAID) :
                            <asp:GridView ID="GVUnpaid" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVUnpaid_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="DelUnpaid" runat="server" OnClick="DelUnpaid_Click" Text="Hapus" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnOffline" runat="server" OnClick="BtnOffline_Click" Text="Offline" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;Pembayaran Offline&nbsp; =&gt; (Lunas dibayar Mahasiswa tanpa melalui Sistem
                                        BANK) &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="PanelPaid2" runat="server">
                            <br />
                            TAGIHAN AKHIR SUDAH DIBAYAR (PAID) :<br />
                            <asp:GridView ID="GVAkhirPaid" runat="server" CellPadding="4" ForeColor="#333333"
                                GridLines="None" CssClass="table-bordered table-condensed" ShowFooter="True"
                                OnRowDataBound="GVAkhirPaid_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="PanelUnpaid2" runat="server">
                            <br />
                            TAGIHAN AKHIR BELUM DIBAYAR (UNPAID) :<br />
                            <asp:GridView ID="GVUnpaidAkhir" runat="server" CellPadding="4" ForeColor="#333333"
                                GridLines="None" CssClass="table-bordered table-condensed" OnRowDataBound="GVUnpaidAkhir_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CbSelect2" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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
                        </asp:Panel>
                        <hr />
                        <h5>
                            <strong>TRANSAKSI PEMBAYARAN</strong></h5>
                        Biaya Angsuran :
                        <table class="table-condensed table-bordered">
                            <tr>
                                <td>
                                    Semester :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLSemster2" runat="server">
                                    </asp:DropDownList>
                                    <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DLSemster2"
                                        Category="DLSemester" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                        LoadingText="Loading" PromptText="Semester">
                                    </ajaxToolkit:CascadingDropDown>
                                </td>
                                <td>
                                    <asp:Button ID="BtnViewBiaya2" runat="server" OnClick="BtnViewBiaya_Click" Text="Lihat" />
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="LbViewAngsuran2" runat="server"></asp:Label>
                        <asp:GridView ID="GVAngsuran2" runat="server" CellPadding="4" ForeColor="#333333"
                            GridLines="None" CssClass="table-bordered table-condensed" OnRowDataBound="GVAngsuran_RowDataBound">
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
                        <br />
                        <asp:Panel ID="PanelTblByrSKS2" runat="server">
                            <table class="table-condensed table-bordered" id="Table1">
                                <tr>
                                    <td>
                                        Jumlah SKS
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:TextBox ID="TbTotalSKS2" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td class="style4">
                                        Angsuran ke 1, KHUSUS Angkatan (2013/2014) &amp; (2014/2015)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnOk2" runat="server" OnClick="BtnOk_Click" Text="Bayar" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="LbPostSuccess" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="LbResultBayar2" runat="server"></asp:Label>
                        </asp:Panel>
                        <br />
                    </div>
                </asp:Panel>

        </div>
    </div>
</asp:Content>
