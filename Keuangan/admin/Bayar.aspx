<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="Bayar.aspx.cs" Inherits="Keuangan.admin.WebForm9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
    <!-- Page Load -->
    <script type="text/javascript">
        $(window).load(function () {
            $("#pageloaddiv").fadeOut(500);
        });
    </script>

    <style type="text/css">
        #pageloaddiv
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 1000;
            background: url('../images/loading135.gif') no-repeat center center;
            background-color:Gray;
            filter:alpha(opacity=20);
            opacity:0.5;
        }
        .style31
        {
            background-color: #99CCFF;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- loading div to show loading image -->
    <div id="pageloaddiv"></div>

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
                    <a href="<%= Page.ResolveUrl("~/admin/Masa_Bayar.aspx.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
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
        <h4>Pembayaran Biaya Studi Akhir</h4>
        <hr />
        <table class="table-condensed">
            <tr>
                <td>
                    NPM :
                    <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                    &nbsp;<asp:Button ID="BtnViewMhs" runat="server" onclick="BtnViewMhs_Click" 
                        Text="View" />
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
            <asp:RadioButton ID="RbPosting" runat="server" GroupName="tagihan" 
                Text="Bayar Penuh" />
&nbsp;<asp:RadioButton ID="RbPostManual" runat="server" GroupName="tagihan" 
                Text="Bayar Tidak Penuh" />
            <br />
            <asp:Panel ID="PanelPenuh" runat="server">
            <br />
                <table class="table-bordered table-condensed">
                    <!----------------------- TAHUN NAGKATAN -------------------------->
                    <tr>
                        <td>
                            Tahun 
                        Pelaksanaan :
                        </td>
                        <td>
                            <asp:DropDownList ID="DLThnPelaksanaan" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="DLThnPelaksanaan_SelectedIndexChanged">
                                <asp:ListItem>Tahun</asp:ListItem>
                                <asp:ListItem>2014/2015</asp:ListItem>
                                <asp:ListItem>2015/2016</asp:ListItem>
                                <asp:ListItem>2016/2017</asp:ListItem>
                                <asp:ListItem>2017/2018</asp:ListItem>
                                <asp:ListItem>2018/2019</asp:ListItem>
                                <asp:ListItem>2019/2020</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<span>*</span>
                            <asp:Label ID="LBResultFilter" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <!----------------------- JURUSAN -------------------------->
                </table>
                <asp:Panel ID="PanelBiaya" runat="server">
                    <br />
                    <table class="table-condensed table-bordered">
                        <tr>
                            <td class="style31">
                                Kerja Praktik :</td>
                            <td class="style31">
                                KKN :</td>
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
                                                <asp:Button ID="BtnBayarKP" runat="server" onclick="BtnBayarKP_Click" 
                                                    Text="Bayar" />
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
                                                <asp:Button ID="BtnBayarKKN" runat="server" onclick="BtnBayarKKN_Click" 
                                                    Text="Bayar" />
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
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style31">
                                PPL I :</td>
                            <td class="style31">
                                WISUDA :</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GVPPLSD" runat="server" CellPadding="4" 
                                    CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                                    onrowdatabound="GVPPLSD_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="BtnBayarPPLI" runat="server" onclick="BtnBayarPPLI_Click" 
                                                    Text="Bayar" />
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
                                                <asp:Button ID="BtnBayarWisuda" runat="server" onclick="BtnBayarWisuda_Click" 
                                                    Text="Bayar" />
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
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style31">
                                PPL II :</td>
                            <td class="style31">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GVPPLSMA" runat="server" CellPadding="4" 
                                    CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                                    onrowdatabound="GVPPLSMA_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="BtnBayarPPLII" runat="server" onclick="BtnBayarPPLII_Click" 
                                                    Text="Bayar" />
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
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
            </asp:Panel>
            <asp:Panel ID="PanelTdkPenuh" runat="server">
            <br />
                <table class="table-bordered table-condensed">
                    <tr>
                        <td>Tahun Pelaksanaan</td>
                        <td>
                            <asp:DropDownList ID="DLthn" runat="server">
                                <asp:ListItem>Tahun</asp:ListItem>
                                <asp:ListItem>2014/2015</asp:ListItem>
                                <asp:ListItem>2015/2016</asp:ListItem>
                                <asp:ListItem>2016/2017</asp:ListItem>
                                <asp:ListItem>2017/2018</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Jenis Pembayaran</td>
                        <td>
                            <asp:DropDownList ID="DLPembayaran" runat="server">
                                <asp:ListItem>Pembayaran</asp:ListItem>
                                <asp:ListItem>Wisuda</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Jumlah Bayar</td>
                        <td>
                            <asp:TextBox ID="TbBiaya" runat="server" MaxLength="7"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="BtnBayar" runat="server" Text="Bayar" 
                                onclick="BtnBayar_Click" />
                            <br />
                            <asp:Label ID="LbBiaya" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
        </div>
    </div>
</div>
</asp:Content>
