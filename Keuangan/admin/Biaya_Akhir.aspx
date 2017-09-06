<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="Biaya_Akhir.aspx.cs" Inherits="Keuangan.admin.WebForm6" EnableEventValidation="false"%>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Ajax Script Manager -->
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
<div class="container top-main-form" style="background: #fafafa">
    <div class=" row top-buffer">
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
        <div class="col-md-9">
        <h4>Master Biaya Studi Akhir</h4>
        <br />
            <table class="table-bordered table-condensed">
                <!----------------------- TAHUN NAGKATAN -------------------------->
                <tr>
                    <td>
                        Tahun 
                        Pelaksanaan :
                    </td>
                    <td>
                        <asp:DropDownList ID="DLThnPelaksanaan" runat="server">
                            <asp:ListItem>Tahun</asp:ListItem>
                            <asp:ListItem>2014/2015</asp:ListItem>
                            <asp:ListItem>2015/2016</asp:ListItem>
                            <asp:ListItem>2016/2017</asp:ListItem>
                            <asp:ListItem>2017/2018</asp:ListItem>
                            <asp:ListItem>2018/2019</asp:ListItem>
                            <asp:ListItem>2019/2020</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<span>*</span>
                    </td>
                </tr>
                <!----------------------- JURUSAN -------------------------->
                <tr>
                    <td>
                        Jurusan :</td>
                    <td>
                        <asp:DropDownList ID="DLProgStudi" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DLProgStudi_SelectedIndexChanged">
                            <asp:ListItem Value="-1">Program Studi</asp:ListItem>
                            <asp:ListItem Value="20-201">S1 TEKNIK ELEKTRO</asp:ListItem>
                            <asp:ListItem Value="21-201">S1 TEKNIK MESIN</asp:ListItem>
                            <asp:ListItem Value="21-401">D3 TEKNIK MESIN</asp:ListItem>
                            <asp:ListItem Value="22-201">S1 TEKNIK SIPIL</asp:ListItem>
                            <asp:ListItem Value="54-211">S1 AGROTEKNOLOGI</asp:ListItem>
                            <asp:ListItem Value="60-201">S1 EKONOMI PEMBANGUNAN</asp:ListItem>
                            <asp:ListItem Value="62-401">D3 AKUNTANSI</asp:ListItem>
                            <asp:ListItem Value="63-201">S1 ILMU ADMINISTRASI NEGARA</asp:ListItem>
                            <asp:ListItem Value="88-201">S1 PENDIDIKAN BAHASA DAN SASTRA INDONESIA</asp:ListItem>
                            <asp:ListItem Value="88-203">S1 PENDIDIKAN BAHASA INGGRIS</asp:ListItem>
                            <asp:ListItem>ALL</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<span>*</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="BtnCreate" runat="server" Text="Create" 
                            onclick="BtnCreate_Click" />
                    &nbsp;<asp:Label ID="LBResultFilter" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="PanelBuat" runat="server">
            <table class="table-condensed table-bordered">
                <tr>
                    <td colspan="10">Biaya Akhir diisi tanpa tanda titik atau koma, ex : 
                        500000</td>
                </tr>
                <tr>
                    <td>
                        KP<span class="style1"> (Rp)</span></td>
                    <td>
                        <asp:TextBox ID="TbKP" runat="server" Width="72px"></asp:TextBox>
                    </td>
                    <td>
                        PPL SD <span class="style1">(Rp)</span></td>
                    <td>
                        <asp:TextBox ID="TbPPLSD" runat="server" Width="72px"></asp:TextBox>
                    </td>
                    <td>
                        PPL SMA<span class="style1"> (Rp)</span></td>
                    <td>
                        <asp:TextBox ID="TbPPLSMA" runat="server" Width="72px"></asp:TextBox>
                    </td>
                    <td>
                        KKN<span class="style1"> (Rp)</span></td>
                    <td>
                        <asp:TextBox ID="TbKKN" runat="server" Width="72px"></asp:TextBox>
                    </td>
                    <td>
                        WISUDA <span class="style1">(Rp)</span></td>
                    <td>
                        <asp:TextBox ID="TbWisuda" runat="server" Width="72px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="BtnBuatOK" runat="server" Text="OK" onclick="BtnBuatOK_Click" />
                    </td>
                </tr>
            </table>
            <br />
            </asp:Panel>
            <asp:Panel ID="PanelEdit" runat="server">
            <table class="table-condensed table-bordered">
                <tr>
                    <td>Biaya: (Rp)</td>
                    <td>
                        <asp:TextBox ID="TBEditBiaya" runat="server" MaxLength="7" Width="75px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="TBEditBiayaUpdate" runat="server" Text="Update" 
                            onclick="TBEditBiayaUpdate_Click" />
                    </td>
                    <td>
                        <asp:Button ID="BtnFrefresh" runat="server" Text="Refresh" 
                            onclick="BtnFrefresh_Click" />
                    </td>
                    <td>
                        <asp:Label ID="LbSUmberEdit" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
             <br />
            </asp:Panel>
            <asp:Panel ID="PanelGVBiaya" runat="server">
            <table class="table-condensed table-bordered">
                <tr>
                    <td>
                        Kerja Praktik :</td>
                    <td>
                         PPL I :</td>
                    <td>
                        PPL II :</td>
                    <td>
                        KKN :</td>
                    <td>
                        WISUDA :</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GVKP" runat="server" CellPadding="4" 
                            CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                            onrowdatabound="GVKP_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="BtnEditKp" runat="server" Text="Edit" 
                                            onclick="BtnEditKp_Click" />
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
                    </td>
                    <td>
                        <asp:GridView ID="GVPPLSD" runat="server" CellPadding="4" 
                            CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                            onrowdatabound="GVPPLSD_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="BtnEditPPLSD" runat="server" Text="Edit" 
                                            onclick="BtnEditPPLSD_Click" />
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
                    </td>
                    <td>
                        <asp:GridView ID="GVPPLSMA" runat="server" CellPadding="4" 
                            CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                            onrowdatabound="GVPPLSMA_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="BtnPPLSMA" runat="server" Text="Edit" 
                                            onclick="BtnPPLSMA_Click" />
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
                    </td>
                    <td>
                        <asp:GridView ID="GVKKN" runat="server" CellPadding="4" 
                            CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                            onrowdatabound="GVKKN_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="BtnEditKKN" runat="server" Text="Edit" 
                                            onclick="BtnEditKKN_Click" />
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
                    </td>
                    <td>
                        <asp:GridView ID="GVWISUDA" runat="server" CellPadding="4" 
                            CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                            onrowdatabound="GVWISUDA_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="BtnEditWisuda" runat="server" Text="Edit" 
                                            onclick="BtnEditWisuda_Click" />
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
                    </td>
                </tr>
            </table>
            <br />
            </asp:Panel>
        </div>
    </div>
</div>
</asp:Content>
