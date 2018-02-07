<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="Beban_Awal.aspx.cs" Inherits="Keuangan.admin.WebForm12" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            font-size: large;
        }
        .style31
        {
            font-size: large;
            color: #009933;
        }
        </style>
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
                <span class="style4">UPDATE KEKURANGAN / BEBAN AWAL</span><br /> 
                <hr />
                <strong>Filter Mahasiswa
                </strong>
                <table class="table-condensed table-bordered">
                    <tr>
                        <td>
                            NPM :
                            <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                            &nbsp;<asp:Button ID="BtnFilter" runat="server" Text="Filter" 
                                onclick="BtnFilter_Click" />
                        </td>
                        <td>
                            <asp:Label ID="LbNIM" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
                <table class="table-condensed table-bordered">
                    <!----------------------- TAHUN NAGKATAN -------------------------->
                    <tr>
                        <td >
                            <asp:Label ID="LbNPM" runat="server" Text="NPM"></asp:Label>
                        </td>
                        <td >
                            <!-- Foto here -->
                            <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="LbClass" runat="server" Text="Kelas"></asp:Label>
                        </td>
                         <td >
                            <asp:Label ID="LbThnAngkatan" runat="server" Text="Tahun Angkatan"></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
                <asp:Panel ID="PanelBeban" runat="server">
                    <strong>Beban Awal
                    </strong>
                    <asp:GridView ID="GVBeban" runat="server" CellPadding="4" 
                        CssClass="table-condensed table-bordered" ForeColor="#333333" 
                        GridLines="None" onrowdatabound="GVBeban_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="BtnUbah" runat="server" onclick="BtnUbah_Click" Text="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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
                    <br />
                    <table ID="TblUpdateOff" class="table-condensed table-bordered">
                        <tr>
                            <td>
                                Set Beban Awal = <span class="style31">Rp.0,-</span>
                                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="OK" />
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <asp:Panel ID="PanelUpdate" runat="server">
                        <strong>Update Beban Awal</strong><br />
                        <table ID="Table1" class="table-condensed table-bordered">
                            <tr>
                                <td>
                                    Ubah Beban Awal Rp. =
                                    <asp:TextBox ID="TbBeban" runat="server" MaxLength="8" Width="85px"></asp:TextBox>
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" OnClick="BtnUpdateOffline_Click" 
                                        Text="Update" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </asp:Panel>
                    <br />
                </asp:Panel>
            </div>
            <!--  END Contenet  -->
        </div>
        <!-- END CONTENT -->
</div>

</asp:Content>
