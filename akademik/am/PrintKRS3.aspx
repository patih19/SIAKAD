<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintKRS3.aspx.cs" Inherits="akademik.am.PrintKRS3" %>

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
    <div>
    <table align="center" class="table-condensed">
        <tr>
            <td>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Lg-Kemenristek.png" 
                    Height="92px" Width="92px" />
            </td>
            <td class="text-center">
                Kartu Rencana Studi<br />
                <span class="style1">&nbsp;UNIVERSITAS TIDAR </span> 
            </td>
            <td class="text-right">
                <strong>SIM AKADEMIK</strong><br />
                <span class="style2">Jl. Kapten Suparman 39 Potrobangsan Magelang 56116 <br />
                Telp. 0293-364113, Fax. 0293-362438</span>
            </td>
        </tr>
        <tr>
            <td colspan="3"><strong><hr height="5" style="color:purple;background-color:purple;"></hr></strong></td>
        </tr>
        <tr>
            <td colspan="3">
                <table align="left">
                    <tr>
                        <td>
                            Nama </td>
                        <td>
                            &nbsp;:
                            <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                            &nbsp;
                        </td>
                        <td>
                            Tahun Akademik&nbsp; </td>
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
                            Program Studi
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;:
                            <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                 </td>
        </tr>
        <tr>
            <td colspan="3">
                 <asp:GridView ID="GVMakul" runat="server" 
                     CssClass="table-condensed table-bordered" HorizontalAlign="Left" 
                     onrowdatabound="GVMakul_RowDataBound" ShowFooter="True">
                 </asp:GridView>
                 </td>
        </tr>
        <tr>
            <td colspan="3">
        <table align="center">
            <tr>
                <td class="text-center">Pembimbing Akademik<br />
                    <br />
                    <br />
                    <br />
                    (...............................)</td>
                <td class="text-center">&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td class="text-center">Mahasiswa<br />
                    <br />
                    <br />
                    <br />
                    ( 
                    <asp:Label ID="LbMhsName" runat="server" Text="Nama Mahasiswa"></asp:Label>
&nbsp;)</td>
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
