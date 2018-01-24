<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="PengajuanCuti.aspx.cs" Inherits="Padu.account.PengajuanCuti" %>
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
                    <asp:Panel ID="PanelCuti" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Pengajuan Cuti Kuliah</strong>
                            </div>
                            <div class="panel-body">
                                <strong>History Aktivitas Perkuliahan</strong><asp:GridView ID="GvAktivitasKuliah" CssClass="table  table-condensed" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                                <asp:Panel ID="PanelNilai" runat="server" Style="background-color: #FFFFFF">
                                    <strong>Pengajuan Cuti Kuliah</strong><br />
                                    Pilih semester cuti
                        <p>
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
                                            <asp:ListItem Value="1">1 - Gasal</asp:ListItem>
                                            <asp:ListItem Value="2">2 - Genap</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:Button ID="BtnSubmit" CssClass="btn btn-primary" runat="server" Text="Ajukan" OnClick="BtnSubmit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </p>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelStatusCuti" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Satus Cuti Akademik</strong>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="GVPengajuanCuti" runat="server" CssClass="table  table-condensed"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                                <asp:Panel ID="PanelDwnFormCuti" runat="server">
                                    Silahkan download formulir cuti kuliah pada tautan
                                <asp:HyperLink ID="HyDwnForm" runat="server" NavigateUrl="~/doc/Form-Permohonan-Cuti.docx">berikut,</asp:HyperLink>
                                    kemudian hubungi bagian Tata Usaha
                                </asp:Panel>

                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
