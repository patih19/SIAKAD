<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Trans.aspx.cs" Inherits="akademik.am.WebForm25" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Transkrip Nilai Mahasiswa</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    NPM</td>
                                <td>
                                <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="130px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                    </div>
                    <div class="panel-footer">
                                <asp:Button ID="BtnFilterMhs" runat="server" Text="Lihat" 
                                OnClick="BtnFilterMhs_Click" CssClass="btn btn-primary" />
                    </div>
                </div>
                <br />
                <asp:Panel ID="PanelTranskrip" runat="server">
                    




                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Daftar Nilai Mahasiswa</strong></div>
                        <div class="panel-body">
                            <!-- <p>
                                Add <code>.table</code> to table to get default table</p> -->

                            <table>
                                <!----------------------- TAHUN NAGKATAN -------------------------->
                                <tr>
                                    <td>
                                        NPM
                                    </td>
                                    <td>
                                        &nbsp;:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="LbNPM" runat="server" Text="NPM"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Nama
                                    </td>
                                    <td>
                                        &nbsp;:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Program Studi
                                    </td>
                                    <td>
                                        &nbsp;:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Kelas
                                    </td>
                                    <td>
                                        &nbsp;:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="LbClass" runat="server" Text="Kelas"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Tahun Angkatan
                                    </td>
                                    <td>
                                        &nbsp;:&nbsp;
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
                            <asp:GridView ID="GVTrans" runat="server" CellPadding="4" 
                                CssClass="table-bordered table-condensed" ForeColor="#333333" GridLines="None" 
                                OnRowDataBound="GVTrans_RowDataBound" ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
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
                            <strong>IP Kumulatif : </strong>
                            <asp:Label ID="LbIPK" runat="server" style="font-weight: 700"></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-success" 
                                OnClick="BtnPrint_Click" Text="Cetak" />


                        </div>
                    </div>
                </asp:Panel>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
