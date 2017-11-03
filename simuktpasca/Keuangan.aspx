<%@ Page Title="" Language="C#" MasterPageFile="~/Pasca.Master" AutoEventWireup="true" CodeBehind="Keuangan.aspx.cs" Inherits="simuktpasca.Keuangan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-header">
        <h1>Tagihan</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                <%--<div class="portlet-header">
                    <h3>Tagihan Akhir Per Semester</h3>
                </div>--%>
                <div class="portlet-content">
                    <table class="table-condensed table-striped table-hover">
                        <tr>
                            <td>
                                <asp:TextBox ID="TbNPM" Placeholder="Ketik NPM" CssClass="form form-control" runat="server" TextMode="Number"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BtnLihatTagihan" CssClass="btn btn-facebook" runat="server" Text="Lihat tagihan" OnClick="BtnLihatTagihan_Click" /></td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="PanelLihatTagihan" runat="server">
                    <br />
                    <hr />
                    <h4>1. Biodata Mahasiswa</h4>
                    <table class="table table-striped table-hover">
                        <tr>
                            <td>NPM</td>
                            <td class="text-right">:</td>
                            <td>
                                <asp:Label ID="LbNPM" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Nama</td>
                            <td class="text-right">:</td>
                            <td>
                                <asp:Label ID="LbNama" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Program Studi</td>
                            <td class="text-right">:</td>
                            <td>
                                <asp:Label ID="LbProdi" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tahun Angkatan</td>
                            <td class="text-right">:</td>
                            <td>
                                <asp:Label ID="LbAngkatan" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Jumlah Tagihan</td>
                            <td class="text-right">:</td>
                            <td>
                                <asp:Label ID="LbTagihan" runat="server">Rp0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Jumlah Terbayar</td>
                            <td class="text-right">:</td>
                            <td>
                                <asp:Label ID="LbTerbayar" runat="server">Rp0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Jumlah Kekurangan</td>
                            <td class="text-right">:</td>
                            <td>
                                <asp:Label ID="LbKekurangan" runat="server">Rp0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <h4>2. Tagihan Semester</h4>
                    <asp:GridView ID="GvTagihan" runat="server" class="table table-striped" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                    <hr />
                    <h4>3. History Pembayaran</h4>                    
                    <asp:GridView ID="GvHistoryBayar" class="table table-striped" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BorderStyle="None">
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#585858" Font-Bold="False" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" ForeColor="White" Font-Bold="True" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>

    </div>
</asp:Content>
