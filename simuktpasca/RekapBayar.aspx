<%@ Page Title="" Language="C#" MasterPageFile="~/Pasca.Master" AutoEventWireup="true" CodeBehind="RekapBayar.aspx.cs" Inherits="simuktpasca.RekapBayar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="content-header">
        <h1>Rekapitulasi Pembayaran</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet">
                    <asp:Panel ID="PanelListTagihan" runat="server">
                        <div class="portlet-header">
                            <h3>Berdasarkan Tahun Angkatan</h3>
                        </div>
                        <div class="portlet-content">
                            <table class="table-condensed table-striped table-hover">
                                <tr>
                                    <td>
                                        <asp:TextBox CssClass=" form form-control" placeholder="contoh : 2017/2018" ID="TbThnAngkatan" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnOpenTagihan" CssClass="btn btn-facebook" runat="server" Text="Submit" OnClick="BtnOpenTagihan_Click" /></td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PanelContentTagihan" runat="server">
                            <br />
                            <!-- /.portlet-header -->
                            <div class="portlet-content">
                                <asp:GridView ID="GvRekapBayar" CssClass=" table table-striped" runat="server" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#585858" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                            </div>
                            <!-- /.portlet-content -->
                        </asp:Panel>
                    </asp:Panel>

                </div>
                <!-- /.portlet -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
</asp:Content>
