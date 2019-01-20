<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KartuUTS.aspx.cs" Inherits="Padu.account.KartuUts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <title>Kartu UTS</title>
    <style type="text/css">
        .style1
        {
            font-size: medium;
        }
        .style3
        {
            font-size: 12px;
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
    <div class="style3">
        <table align="center" class="table-condensed">
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logoUntidar17.png" 
                        Height="75px" />
                </td>
                <td class="text-center">
                    Kartu 
                    Ujian Tengah Semester<br />
                    <span class="style1">UNIVERSITAS TIDAR </span>
                </td>
                <td class="text-right">
                    <strong>SIM AKADEMIK</strong><br />
                    <span class="style3">Jl. Kapten Suparman 39 Potrobangsan Magelang 56116<br />
                        Telp. 0293-364113, Fax. 0293-362438</span></td>
            </tr>
            <tr>
                <td colspan="3">
                <br />
                    <table align="left">
                        <tr>
                            <td>
                                Nama</td>
                            <td class="text-center">
                                &nbsp;
                                :&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                            </td>
                            <td>
                                &nbsp;&nbsp; &nbsp; &nbsp;
                            </td>
                            <td>
                                Tahun Akademik </td>
                            <td class="text-center">
                                &nbsp;:&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LbTahun" runat="server" Text="Tahun Akademik"></asp:Label>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                NPM
                            </td>
                            <td class="text-center">
                                :
                                </td>
                            <td>
                                <asp:Label ID="LbNpm" runat="server" Text="NPM"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Semester</td>
                            <td class="text-center">
                                :</td>
                            <td>
                                <asp:Label ID="LbSemester" runat="server" Text="Semester"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenis Kelas
                            </td>
                            <td class="text-center">
                                :
                                </td>
                            <td>
                                <asp:Label ID="LbJenisKelas" runat="server" Text="Jenis Kelas"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Program Studi</td>
                            <td class="text-center">
                                :</td>
                            <td>
                                <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                            &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="GVJadwalUTS" runat="server" CssClass="table-condensed table-bordered"
                        HorizontalAlign="Left">
                        <Columns>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table align="center">
                        <tr>
                            <td class="text-center">
                                 <!-- <asp:Image ID="ImgMhs" runat="server" Height="165px" />
&nbsp; --></td>
                            <td class="text-center">
                                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td class="text-center">
                                KAJUR
                                <br />
                                <br />
                                <br />
                                (.................................)
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="GVUTSDetail" runat="server" CssClass="table-condensed table-bordered"
                        HorizontalAlign="Left">
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>