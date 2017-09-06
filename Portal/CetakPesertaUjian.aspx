<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CetakPesertaUjian.aspx.cs" Inherits="Portal.CetakPesertaUjian" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <title>Peserta Ujian</title>
    <script type="text/javascript">
        function fixform() {
            if (opener.document.getElementById("aspnetForm").target != "_blank") return;
            opener.document.getElementById("aspnetForm").target = "";
            opener.document.getElementById("aspnetForm").action = opener.location.href;
        }
    </script>
    <style type="text/css">
        #GVPeserta tr
        {
            page-break-inside: avoid !important;
        }
        
        @media print 
        { 
            #GVPeserta tr
            {
                page-break-inside: avoid;
            }
        }
    </style>
</head>
<body onload="fixform()">
    <form id="form1" runat="server">
    <div>
        <table class="table-condensed">
            <tr>
                <td>
                    <table class="table-condensed">
                        <tr>
                            <td colspan="2">
                                <strong>DAFTAR PESERTA</strong>
                                <asp:Label ID="LbJenisUjian" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mata Kuliah / Kelas
                            </td>
                            <td>
                                &nbsp;:
                                <asp:Label ID="LbKdMakul" runat="server" CssClass="style2"></asp:Label>
                                &nbsp;<asp:Label ID="LbMakul" runat="server" CssClass="style2"></asp:Label>
                                &nbsp;/
                                <asp:Label ID="LbKelas" runat="server" CssClass="style2"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tahun / Semester
                            </td>
                            <td>
                                &nbsp;:
                                <asp:Label ID="LbTahun" runat="server"></asp:Label>
                                &nbsp;/
                                <asp:Label ID="LbSemester" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jadwal
                            </td>
                            <td>
                                &nbsp;:
                                <asp:Label ID="LbJadwal" runat="server" CssClass="style2"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Pengampu
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbDosen" runat="server"></asp:Label>
                                &nbsp;<asp:Label ID="LbNIDN" runat="server" CssClass="style2" ForeColor="Transparent"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GVPeserta" runat="server" CssClass="table-bordered table-condensed">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    No.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="pull-right table-condensed">
                        <tr>
                            <td>
                                Magelang, .........................<br />
                                Dosen
                                Pengampu</td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LbTtdDosen" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table class="pull-left table-condensed">
                        <tr>
                            <td>
                                <br />
                                Ketua Jurusan</td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LbKajur" runat="server">(.................................................)</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
