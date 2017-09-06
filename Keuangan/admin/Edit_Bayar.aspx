<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="Edit_Bayar.aspx.cs" Inherits="Keuangan.admin.WebForm10" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <a href="<%= Page.ResolveUrl("~/admin/Post.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Posting Pembayaran</a>
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
            <!-- CONTENT -->
            <div class="col-md-9">
               <h4>Update Data Pembayaran Mahasiswa</h4>
                <table class="table-condensed table-bordered">
                    <tr>
                        <td>Nomor Pembayaran :</td>
                        <td>
                            <asp:TextBox ID="TBNoBill" runat="server" MaxLength="10" Width="120px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="BtnCariOffline" runat="server" Text="Cari" 
                                onclick="BtnCariOffline_Click" /></td>
                        <td>
                            <asp:Label ID="LbNoBill" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="PanelOffline" runat="server">
                    UPDATE TAGIHAN OFFLINE :<br />
                <asp:GridView ID="GVOfflinePeriodik" runat="server" BackColor="#DEBA84" 
                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    CssClass="table-condensed" CellSpacing="2" 
                    onrowdatabound="GVOfflinePeriodik_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnEditOffline" runat="server" Text="Edit" 
                                    onclick="BtnEditOffline_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
                <br />
                <asp:Panel ID="PanelUpdateOffline" runat="server">
                <table class="table-condensed table-bordered" id="TblUpdateOff">
                    <tr>
                        <td>
                            Biaya (Rp.) </td>
                        <td>
                            <asp:TextBox ID="TbBiayaOffline" runat="server" MaxLength="7" Width="80px" 
                                CssClass="form-control"></asp:TextBox></td>
                        <td>
                            biaya diisi tanpa koma atau titik
                            <asp:Label ID="LbBillNumber" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cicilan</td>
                        <td>
                            <asp:DropDownList ID="DlCicilanOff" runat="server" CssClass="form-control">
                                <asp:ListItem Value="-1">Cicilan</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BtnUpdateOffline" runat="server" CssClass="btn btn-success" 
                                onclick="BtnUpdateOffline_Click" Text="Update" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                </asp:Panel>  
                </asp:Panel> <!-- End Panel Offline -->
                <asp:Panel ID="PanelOnline" runat="server">
                    UPDATE TAGIHAN ONLINE UNPAID (BELUM DIBAYAR)<br />
                    <asp:GridView ID="GVOnline" runat="server" BackColor="#DEBA84" 
                        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        CssClass="table-condensed" onrowdatabound="GVOnline_RowDataBound" 
                        CellSpacing="2">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="BtnEditOnline" runat="server" onclick="BtnEditOnline_Click" 
                                        Text="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                    </asp:GridView>
                    <br />
                    <asp:Panel ID="PanelUpdateOnline" runat="server">
                        <table ID="TblUpdateOnline" class="table-condensed table-bordered">
                            <tr>
                                <td>
                                    Biaya (Rp.)
                                </td>
                                <td>
                                    <asp:TextBox ID="TbBiayaOnline" runat="server" MaxLength="7"
                                        CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    Biaya diisi tanpa koma atau titik
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Cicilan</td>
                                <td>
                                    <asp:DropDownList ID="DLCicilanOl" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Cicilan</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="LbBillOnline" runat="server" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" 
                                        onclick="Button1_Click" Text="Update" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
                <br />
                <br />

            </div>
        </div>
    </div>
</asp:Content>
