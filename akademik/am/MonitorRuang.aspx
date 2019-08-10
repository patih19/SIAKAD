<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="MonitorRuang.aspx.cs" Inherits="akademik.am.MonitorRuang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        table#TblQuota tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(133, 153, 154); }
        table#TblQuota tbody tr:nth-child(odd){ background-color :#fff;}
        table#TblQuota tbody tr:nth-child(odd){ background-color :#EEF7EE;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <strong>Informasi Penggunaan Ruang</strong>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DLProdi" CssClass=" form-control" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLSemester" CssClass=" form-control" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="BtnOpenJadwal" runat="server" Text="Lihat" CssClass=" btn btn-success"
                                        OnClick="BtnOpenJadwal_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr />
                <asp:Repeater ID="RepeaterHari" runat="server" OnItemDataBound="RepeaterHari_ItemDataBound">
                    <ItemTemplate>
                        <div class="panel panel-warning">
                            <div class="panel-heading">
                                <asp:HiddenField ID="NoHari" runat="server" Value='<%# Eval("NoHari") %>' />
                                <strong><asp:Label ID="LbHari" runat="server" Text='<%# Eval("Hari") %>'></asp:Label></strong>
                            </div>
                            <div class="panel-body">
                                <asp:Repeater ID="RepeaterRuang" runat="server" OnItemDataBound="RepeaterRuang_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="NoRuang" runat="server" Value='<%# Eval("NoRuang") %>' />
                                        <strong>Ruang :</strong> <strong>
                                            <asp:Label ID="LbRuang" runat="server" Text='<%# Eval("Ruang")%>'></asp:Label></strong>
                                        <br />
                                        <asp:Repeater ID="RepeaterJadwalRuang" runat="server">
                                            <HeaderTemplate>
                                                <table class=" table table-condensed" style="page-break-before: always" id="TblQuota">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                Kode
                                                            </th>
                                                            <th>
                                                                Makul
                                                            </th>
                                                            <th>
                                                                Program Studi
                                                            </th>
                                                            <th>
                                                                Ruang
                                                            </th>
                                                            <th>
                                                                Mulai
                                                            </th>
                                                            <th>
                                                                Selesai
                                                            </th>
                                                            <th>
                                                                Kelas
                                                            </th>
                                                            <th>
                                                                Jenis Kelas
                                                            </th>
                                                        </tr>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="page-break-inside: avoid !important">                                                    
                                                    <td>
                                                        <asp:Label ID="LbKodeMakul" runat="server" Text='<%# Eval("Kode") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblNomerDaftar" runat="server" Text='<%# Eval("Makul") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("Program Studi") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Ruang") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Jam Mulai") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Jam Selesai") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Kelas") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Jenis Kelas") %>' />
                                                    </td>
                                                    <%--<td>
                                <asp:Button ID="BtnSave" OnClick="BtnSave_Click" runat="server" Text="simpan" CommandArgument='<%#Eval("No") + "," + Eval("Kode Prodi") + "," + Eval("Tahun")+ "," + Eval("Quota") %>' />
                        OnClick="Button_Click"
                            </td> --%>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <hr />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
