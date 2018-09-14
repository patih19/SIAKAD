<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="Transkrip.aspx.cs" Inherits="Portal.Transkrip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <br />
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Transkrip Nilai Mahasiswa</strong>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>NPM</td>
                                <td>
                                    <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="130px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="BtnFilterMhs" runat="server" Text="Lihat"
                           CssClass="btn btn-primary" OnClick="BtnFilterMhs_Click" />
                        &nbsp;<strong><em>Transkrip Nilai Mahasiswa Mulai Tahun Angkatan 2015/2016 </em></strong>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <asp:Panel ID="PanelNilai" runat="server" CssClass=" panel panel-default" Style="background-color: #FFFFFF">
            <div class="row">
                <div class="col-xs-12 col-md-12 col-lg-12">
                    <br />
                    <table align="center" class="table-condensed">
                        <tr>
                            <td>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logoUntidar17.png"
                                    Height="80px" Width="80px" />
                            </td>
                            <td class="text-center"><strong>Transkrip Nilai</strong><br />
                                <span>&nbsp;<strong><span class="auto-style1">UNIVERSITAS TIDAR</span></strong> </span>
                            </td>
                            <td class="text-right">
                                <strong>SIM AKADEMIK</strong><br />
                                <span>Jl. Kapten Suparman No.39 Magelang 56116
                            <br />
                                    Telp. 0293-364113, Fax. 0293-362438</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><strong>
                                <hr height="5" style="color: purple; background-color: purple;"></hr>
                            </strong></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <!----------------------- TAHUN NAGKATAN -------------------------->
                                    <tr>
                                        <td>NPM
                                        </td>
                                        <td>&nbsp;:&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="LbNPM" runat="server" Text="NPM"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Nama
                                        </td>
                                        <td>&nbsp;:&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Program Studi
                                        </td>
                                        <td>&nbsp;:&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Kelas
                                        </td>
                                        <td>&nbsp;:&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="LbClass" runat="server" Text="Kelas"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Tahun Angkatan
                                        </td>
                                        <td>&nbsp;:&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="LbThnAngkatan" runat="server" Text="Tahun Angkatan"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GVTrans" runat="server"
                                    CssClass="table-condensed table-bordered" HorizontalAlign="Left" ShowFooter="True" OnRowDataBound="GVTrans_RowDataBound">
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><strong>IPK :</strong>
                                <asp:Label ID="LbIPK" runat="server" Text="" Style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                                <td colspan="3">
                                    <table align="center">
                                        <tr>
                                            <td class="text-center">&nbsp;</td>
                                            <td class="text-center">&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                            <td class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                            <td class="text-center">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                    </table>
                    <br />
                </div>
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
