<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CetakPesertaUjian.aspx.cs" Inherits="akademik.am.CetakPesertaUjian" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <title>Peserta Ujian</title>
    <script type="text/javascript">
        function fixform() {
            if (opener.document.getElementById("aspnetForm").target != "_blank") return;
            opener.document.getElementById("aspnetForm").target = "";
            opener.document.getElementById("aspnetForm").action = opener.location.href;
        }
    </script>
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
                        DAFTAR 
                                    PESERTA UJIAN</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LbJenisUjian" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="style111">Program Studi </span>
                                </td>
                                <td>
                                    &nbsp;:
                                    <asp:Label ID="LbIdProdi" runat="server" CssClass="style2"></asp:Label>
                                    &nbsp;<asp:Label ID="LbProdi" runat="server" CssClass="style2"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Mata Kuliah
                                </td>
                                <td>
                                    &nbsp;:
                                    <asp:Label ID="LbKdMakul" runat="server" CssClass="style2"></asp:Label>
                                    &nbsp;<asp:Label ID="LbMakul" runat="server" CssClass="style2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Kelas
                                </td>
                                <td>
                                    &nbsp;:
                                    <asp:Label ID="LbKelas" runat="server" CssClass="style2"></asp:Label>
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
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="LbNIDN" runat="server" CssClass="style2" ForeColor="Transparent"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GVPeserta" runat="server" 
                            CssClass="table-bordered table-condensed">
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
                </table>
    </div>
    </form>
</body>
</html>
