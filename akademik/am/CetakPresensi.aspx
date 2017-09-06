<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CetakPresensi.aspx.cs" Inherits="akademik.am.CetakPresensi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Presensi Mahasiswa</title>
    <!-- <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" /> -->
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" 
                        style="font-size: medium; font-weight: 700" Text="Presensi Perkuliahan"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Mata Kuliah</td>
                <td>:
                    <asp:Label ID="LbKdMakul" runat="server"></asp:Label>
&nbsp;-
                    <asp:Label ID="LbMakul" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tahun / Semester </td>
                <td>:
                    <asp:Label ID="LbTahun" runat="server"></asp:Label>
&nbsp;/
                    <asp:Label ID="LbSemester" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Dosen</td>
                <td>:
                    <asp:Label ID="LbNIDN" runat="server"></asp:Label>
&nbsp;-
                    <asp:Label ID="LbDosen" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Kelas</td>
                <td>:
                    <asp:Label ID="LbKelas" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Jadwal Kuliah</td>
                <td>:
                    <asp:Label ID="LbJadwal" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <!-- 
        <asp:GridView ID="GVAbsen" runat="server" CssClass="table-condensed" 
            Style="font-family: Arial, Helvetica, sans-serif; font-size: 11px;">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle Height="30" />
            <RowStyle Height="45" />
        </asp:GridView>
        -->

        <asp:Literal ID="literal1" runat="server" />
        <br />
        <br />
        <div class="container">
        <table class="pull-right table-condensed">
            <tr>
                <td>Magelang, .........................<br />
                Dosen</td>
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
        </div>
    </div>
    </form>
</body>
</html>
