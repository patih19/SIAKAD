<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="MhsUjian.aspx.cs" Inherits="Portal.WebForm12" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVJadwal').DataTable({
                'iDisplayLength': 100,
                'aLengthMenu': [[100, 200, 300, 400, 500, 600, 700, -1], [100, 200, 300, 400, 500, 600, 700, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVJadwal tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd)
        {
            background-color: #EEF7EE;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Peserta Ujian</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                        <tr>
                            <td style="vertical-align: top">
                                Tahun / Semester
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                                <asp:ListItem>Tahun</asp:ListItem>
                                            </asp:DropDownList>
                                            <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                                Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                LoadingText="Loading" PromptText="Tahun">
                                            </ajaxToolkit:CascadingDropDown>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control">
                                                <asp:ListItem>Semester</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                Jenis Ujian
                            </td>
                            <td>
                                <asp:DropDownList ID="DLUjian" runat="server" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="DLUjian_SelectedIndexChanged">
                                    <asp:ListItem>Jenis Ujian</asp:ListItem>
                                    <asp:ListItem Value="uts">Ujian Tengah Semester</asp:ListItem>
                                    <asp:ListItem Value="uas">Ujian Akhir Semester</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    </div>
                </div>
                <div>
                    <asp:Panel ID="PanelProdi" runat="server">
                        <asp:Panel ID="PanelMakul" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading ui-draggable-handle">
                                    <strong>Cetak Peserta Ujian</strong></div>
                                <div class="panel-body">
                                    <asp:GridView ID="GVJadwal" runat="server" 
                                        CssClass="table table-condensed table-bordered table-hover" 
                                        OnRowDataBound="GVJadwal_RowDataBound" onprerender="GVJadwal_PreRender" 
                                        onrowcreated="GVJadwal_RowCreated">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Cetak
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnCetak" runat="server" OnClick="BtnCetak_Click" OnClientClick="aspnetForm.target ='_blank';"
                                                        Text="Cetak" CssClass="btn btn-success" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Unduh
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkDownExcel" runat="server" onclick="LnkDownExcel_Click">unduh</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="PanelJadwalUjian" runat="server">
                            <br />
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        <span class="style111">Program Studi </span>
                                    </td>
                                    <td>
                                        &nbsp;:
                                        <asp:Label ID="LbIdProdi" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="LbProdi" runat="server"></asp:Label>
                                        &nbsp;&nbsp;
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
                                        &nbsp;
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
                        </asp:Panel>
                    </asp:Panel>
                    <br />
                    <br />
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
