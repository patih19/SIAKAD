<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="SKS.aspx.cs" Inherits="Keuangan.admin.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- ==== Untuk membuat dropdown 1.) jquery,min.js dan 2.) bootstrap.min.js harus di definisikan di master dan content page urutannya jgn kebalik ==== -->
    <script src="../Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <link href="../css/print.css" rel="stylesheet" type="text/css" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet" type="text/javascript" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <!-- Ajax Script Manager -->
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
<div class="container top-main-form" style="background: #fafafa">
        <div class=" row top-buffer">
            <!----------------------- MENU -------------------->
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
            <!-- END MENU -->
            <!-- CONTENT -->
            <div class="col-md-9">
                <strong><span class="style3">PEMBAYARAN MAHSISWA NON PERIODIK
                </span></strong>
                <hr /> 
                <strong>&nbsp;Mahasiswa:</strong>
                <br />
                &nbsp;filter data mahasiswa berdasarkan NPM &nbsp;<asp:Label ID="LbFilterMhs" runat="server"></asp:Label>
                &nbsp;
                <table class="table-condensed">
                    <tr>
                        <td>
                            NPM :
                            <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                            &nbsp;<asp:Button ID="BtnFilter" runat="server" Text="Filter" OnClick="BtnFilter_Click" />
                        </td>
                    </tr>
                </table>
                <table class="table-condensed table-bordered">
                    <!----------------------- TAHUN NAGKATAN -------------------------->
                    <tr>
                        <td>
                            <asp:Label ID="LbNPM" runat="server" Text="NPM"></asp:Label>
                        </td>
                        <td>
                            <!-- Foto here -->
                            <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LbClass" runat="server" Text="Kelas"></asp:Label>
                        </td>
                         <td>
                            <asp:Label ID="LbThnAngkatan" runat="server" Text="Tahun Angkatan"></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
                <!-- ----------------- Sekat Bayar Angsuran dan SKS -------------- -->
                <asp:Panel ID="PanelSKS" runat="server">
                    <asp:GridView ID="GVSKS" runat="server" CellPadding="4" 
                        CssClass="table-bordered table-condensed" ForeColor="#333333" 
                        GridLines="None" onrowdatabound="GVSKS_RowDataBound">
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
                    <h4><asp:Button ID="BtnEditSKS" runat="server" Text="Edit" 
                            OnClick="BtnEditSKS_Click" Font-Size="Small" /></h4>
                </asp:Panel >
                <asp:Panel ID="PanelEditSKS" runat="server">
                    <table class="table-condensed">
                        <tr>
                            <td>
                                Semester
                            </td>
                            <td>
                                <asp:TextBox ID="TBSemester" runat="server" Width="70px" BackColor="#FFFFCC" 
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                SKS
                            </td>
                            <td>
                                <asp:TextBox ID="TBSKS" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BtnUpdate" runat="server" Text="Update" 
                                    onclick="BtnUpdate_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <!--  END Contenet  -->
        </div>
        <!-- END CONTENT -->
</div>
</asp:Content>
