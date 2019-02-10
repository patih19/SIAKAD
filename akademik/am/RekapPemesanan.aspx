<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="RekapPemesanan.aspx.cs" Inherits="akademik.am.RekapPemesanan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.3.3.6.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVJadwal tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(51, 123, 102); }
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd){ background-color :#fff;}
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd){ background-color :#EEF7EE;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Rekap Pemesanan Mata Kuliah</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        Program Studi
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="-1">Program Studi</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="panel-footer">
                            &nbsp;<asp:Button ID="BtnPesanan" runat="server" Text="OK" OnClick="BtnJadwal_Click"
                                CssClass="btn btn-primary" />
                            <asp:Label ID="LbJadwalResult" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <br />
                    <asp:Panel ID="PanelRekap" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="GvRekapPemesanan" runat="server" CssClass="table table-condensed" CellPadding="4" ForeColor="#333333" GridLines="None" OnPreRender="GvRekapPemesanan_PreRender">
                                    <AlternatingRowStyle BackColor="White" />
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
                            </div>
                            <div class="panel-footer">
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
