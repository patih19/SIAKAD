<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListNilai.aspx.cs" Inherits="Portal.ListNilai" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rekap Nilai Semester</title>
    <link href="~/css/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- <link href="~/css2/boots.css" rel="stylesheet" type="text/css" /> -->
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
                        <strong>DAFTAR NILAI MAHASISWA</strong></td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Mata Kuliah</td>
                                <td class="text-center">
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LbMakul" runat="server" CssClass="style2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Semester Aktif</td>
                                <td class="text-center">
                                    :
                                    </td>
                                <td>
                                    <asp:Label ID="LbSmster" runat="server" CssClass="style2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pengampu 
                                </td>
                                <td class="text-center">
                                    :
                                    </td>
                                <td>
                                    <asp:Label ID="LbDosen" runat="server" CssClass="style2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Kelas 
                                </td>
                                <td class="text-center">
                                    :
                                    </td>
                                <td>
                                    <asp:Label ID="LbKls" runat="server" CssClass="style2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Jenis Kelas</td>
                                <td class="text-center">
                                    :
                                    </td>
                                <td>
                                    <asp:Label ID="LbJenisKelas" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" CssClass="table-bordered table-condensed">
                        </asp:GridView>
                    </td>
                </tr>
                </table>
    </div>
    </form>
</body>
</html>
