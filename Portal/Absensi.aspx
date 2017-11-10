<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="Absensi.aspx.cs" Inherits="Portal.WebForm1" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVMakul').DataTable({
                'iDisplayLength': 10,
                'aLengthMenu': [[10, 25, 50, 75, 100, 200, 300, -1], [10, 25, 50, 75, 100, 200, 300, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVMakul tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVMakul tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVMakul tbody tr:nth-child(odd)
        {
            background-color: #EEF7EE;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:toolkitscriptmanager>
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Presensi Perkuliahan</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td style="vertical-align: top">
                                    Tahun
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                        <asp:ListItem>Tahun</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
                                        <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2019</asp:ListItem>
                                        <asp:ListItem>2020</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    Semester
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnSelectedIndexChanged="DLSemester_SelectedIndexChanged">
                                        <asp:ListItem>Semester</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <asp:Panel ID="PanelProdi" runat="server">
                            <asp:Panel ID="PanelMakul" runat="server">
                                <asp:GridView ID="GVMakul" runat="server" CssClass="table table-condensed table-bordered table-hover"
                                    OnRowDataBound="GVMakul_RowDataBound" OnPreRender="GVMakul_PreRender">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Pilih
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CBSelect" runat="server" AutoPostBack="True" OnCheckedChanged="CBSelect_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="PanelPeserta" runat="server">
                            <strong>DAFTAR MAHASISWA</strong>
                            <table class="table-condensed table-bordered">
                                <tr>
                                    <td>
                                        <table class="table-condensed">
                                            <tr>
                                                <td>
                                                    <span class="style111">Program Studi </span>
                                                </td>
                                                <td>
                                                    &nbsp;:
                                                    <asp:Label ID="LbIdProdi" runat="server"></asp:Label>
                                                    &nbsp;<asp:Label ID="LbProdi" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Mata Kuliah
                                                </td>
                                                <td>
                                                    &nbsp;:
                                                    <asp:Label ID="LbKdMakul" runat="server"></asp:Label>
                                                    &nbsp;<asp:Label ID="LbMakul" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Dosen
                                                </td>
                                                <td>
                                                    &nbsp;:
                                                    <asp:Label ID="LbNIDN" runat="server"></asp:Label>
                                                    &nbsp;<asp:Label ID="LbDosen" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Kelas
                                                </td>
                                                <td>
                                                    &nbsp;:
                                                    <asp:Label ID="LbKelas" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Jadwal
                                                </td>
                                                <td>
                                                    &nbsp;:
                                                    <asp:Label ID="LbJadwal" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GVPeserta" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                            ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCetak" runat="server" OnClick="BtnCetak_Click" Text="Cetak" OnClientClick="aspnetForm.target ='_blank';"
                                            CssClass="btn btn-success" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
                <br />                
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
