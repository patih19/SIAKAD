<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KHSCard.aspx.cs" Inherits="Padu.account.KHSCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <title>Kartu Rencana Studi</title>
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
        .style2
        {
            font-size: small;
        }
    </style>
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
    <table align="center" class="table-condensed">
        <tr>
            <td>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logoUntidar17.png" Height="80px" />
            </td>
            <td class="text-center" colspan="2">
                Kartu 
                Hasil Studi<br />
                <span class="style1">&nbsp;UNIVERSITAS TIDAR </span> 
            </td>
            <td class="text-right">
                <strong>SIM AKADEMIK</strong><br />
                <span class="style2"><em>Jl. Kapten S. Parman No.39 Magelang 56116 <br />
                Telp. 0293-364113, Fax. 0293-362438</em></span>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="text-left" colspan="2">
                &nbsp;</td>
            <td class="text-right">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <table align="left">
                    <tr>
                        <td>
                            Nama</td>
                        <td>
                            &nbsp;:
                            <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            &nbsp;
                        </td>
                        <td>
                            Tahun Akademik</td>
                        <td>
                            &nbsp;:
                            <asp:Label ID="LbTahun" runat="server" Text="Tahun Akademik"></asp:Label>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            NPM
                        </td>
                        <td>
                            &nbsp;:
                            <asp:Label ID="LbNpm" runat="server" Text="NPM"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Semester</td>
                        <td>
                            &nbsp;:
                            <asp:Label ID="LbSemester" runat="server" Text="Semester"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Jenis Kelas
                        </td>
                        <td>
                            &nbsp;:
                            <asp:Label ID="LbJenisKelas" runat="server" Text="Jenis Kelas"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Program Studi&nbsp; 
                        </td>
                        <td>
          &nbsp;:
                            <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                        </td>
                    </tr>
                </table>
                 </td>
        </tr>
        <tr>
            <td colspan="4">
                 <asp:GridView ID="GVMakul" runat="server" 
                     CssClass="table-condensed table-bordered" HorizontalAlign="Left" 
                     onrowdatabound="GVMakul_RowDataBound">
                     <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
                 <br />
                 </td>
        </tr>
        <tr>
            <td colspan="4"><strong>Jumlah SKS :</strong>
                <asp:Label ID="LBSks" runat="server" style="font-weight: 700"></asp:Label>
                <br />
                <strong>IP Semester :</strong>
                <asp:Label ID="LbIPS" runat="server" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                 <br />
                 </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                Ketua Jurusan<br />
                <br />
                <br />
                <br />
                <br />
                (........................................)</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        </table>
    </form>
</body>
</html>
