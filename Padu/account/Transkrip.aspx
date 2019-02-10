<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="Transkrip.aspx.cs" Inherits="Padu.account.transkrip" %>
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
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Transkrip Nilai</strong></div>
                        <div class="panel-body">
                            
                            <p>Transkrip nilai Mahasiswa mulai tahun angkatan 2015/2016</p>
                            <asp:Button ID="BtnViewTrans" CssClass="btn btn-success" runat="server" Text="Lihat" OnClick="BtnViewTrans_Click" />
                            
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="PanelNilai" runat="server" CssClass=" panel panel-default" style="background-color: #FFFFFF">
                        <br />
                        <table align="center" class="table-condensed">
                            <tr>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logoUntidar17.png"
                                        Height="80px" Width="80px" />
                                </td>
                                <td class="text-center">Transkrip Nilai<br />
                                    <span class="style1">&nbsp;UNIVERSITAS TIDAR </span>
                                </td>
                                <td class="text-right">
                                    <strong>SIM AKADEMIK</strong><br />
                                    <span class="style2">Jl. Kapten Suparman No.39 Magelang 56116
                            <br />
                                        Telp. 0293-364113, Fax. 0293-362438</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"><strong>
                                    <hr height="5" style="color: purple; background-color: purple;"></hr>
                                </strong></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <!----------------------- TAHUN NAGKATAN -------------------------->
                                        <tr>
                                            <td>NPM
                                            </td>
                                            <td>&nbsp;:&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LbNPM" runat="server" Text="NPM"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Nama
                                            </td>
                                            <td>&nbsp;:&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Program Studi
                                            </td>
                                            <td>&nbsp;:&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Kelas
                                            </td>
                                            <td>&nbsp;:&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LbClass" runat="server" Text="Kelas"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Tahun Angkatan
                                            </td>
                                            <td>&nbsp;:&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LbThnAngkatan" runat="server" Text="Tahun Angkatan"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LbIdProdi" runat="server" ForeColor="Transparent"
                                                    CssClass="hidden"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="GVTrans" runat="server"
                                        CssClass="table-condensed table-bordered" HorizontalAlign="Left"
                                        OnRowDataBound="GVTrans_RowDataBound" ShowFooter="True">
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"><strong>IPK :</strong>
                                    <asp:Label ID="LbIPK" runat="server" Text="" style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td colspan="3">
                                    <table align="center">
                                        <tr>
                                            <td class="text-center">&nbsp;</td>
                                            <td class="text-center">&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                            <td class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                            <td class="text-center">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                        </table>
                        <br />
                    </asp:Panel>   
                    <br />               
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
