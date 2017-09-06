<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="JadwalUAS.aspx.cs" Inherits="Portal.WebForm6" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVJadwalUAS').DataTable({
                'iDisplayLength': 15,
                'aLengthMenu': [[15, 30, 45, 60, 90, 100, 200, -1], [15, 30, 45, 60, 90, 100, 200, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVJadwalUAS tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVJadwalUAS tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVJadwalUAS tbody tr:nth-child(odd)
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
                <div>
                <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Jadwal Ujian Akhir Semester (UAS)</strong> </div>
                        <div class="panel-body">
                    <table class="table-condensed">
                        <tr>
                            <td>
                                Tahun
                            </td>
                            <td>
                                <asp:DropDownList ID="DLTahun" runat="server" Width="120px" CssClass="form-control">
                                    <asp:ListItem>Tahun</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                    Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                    LoadingText="Loading" PromptText="Tahun">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Semester
                            </td>
                            <td>
                                <asp:DropDownList ID="DLSemester" runat="server" Width="120px" CssClass="form-control">
                                    <asp:ListItem>Semester</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="BtnJadwalUAS" runat="server" Text="OK" 
                                    OnClick="BtnJadwalUAS_Click" CssClass="btn btn-success" />
                                &nbsp;<asp:Label ID="LbJadwalResult" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                        </div>
                    </div>
                    <asp:Panel ID="PanelEditJadwal" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Update Jadwal Ujian Tengah Semester (UTS)</strong><asp:Label ID="LbIdJadwal"
                                    runat="server"></asp:Label></div>
                            <div class="panel-body">
                                <table class="table-condensed table-bordered">
                                    <tr style="background-color: #BCE8F1">
                                        <td>
                                            Mata Kuliah
                                        </td>
                                        <td>
                                            Dosen
                                        </td>
                                        <td>
                                            Kelas
                                        </td>
                                        <td>
                                            Jenis Kelas
                                        </td>
                                        <td>
                                            Hari
                                        </td>
                                        <td>
                                            Tanggal<br />
                                            (tahun-bulan-tanggal)
                                        </td>
                                        <td>
                                            Jam Mulai
                                        </td>
                                        <td>
                                            Jam Selesai
                                        </td>
                                        <td>
                                            Ruang
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LbMakul" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LbDosen" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LbKelas" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LbJenisKelas" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DLHari" runat="server">
                                                <asp:ListItem>Hari</asp:ListItem>
                                                <asp:ListItem>Senin</asp:ListItem>
                                                <asp:ListItem>Selasa</asp:ListItem>
                                                <asp:ListItem>Rabu</asp:ListItem>
                                                <asp:ListItem>Kamis</asp:ListItem>
                                                <asp:ListItem>Jumat</asp:ListItem>
                                                <asp:ListItem>Sabtu</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTanggal" runat="server" MaxLength="10" Width="80px" BackColor="#F0F0F0"
                                                ReadOnly="True"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TbTanggal"
                                                Format="yyyy-MM-dd">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbMulai" runat="server" MaxLength="5" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbSelesai" runat="server" MaxLength="5" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbRuang" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="9">
                                            <asp:Button ID="BtnUpJUas" runat="server" Text="Simpan" 
                                                OnClick="BtnUpJUas_Click" CssClass="btn btn-success" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="PanelJadwal" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Daftar Jadwal Ujian Akhir Semester (UAS)</strong></div>
                            <div class="panel-body">
                                <asp:GridView ID="GVJadwalUAS" runat="server" 
                                    CssClass="table table-condensed table-bordered table-hover" 
                                    OnRowDataBound="GVJadwalUAS_RowDataBound" onprerender="GVJadwalUAS_PreRender">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="BtnEditUAS" runat="server" Text="Update" 
                                                    OnClick="BtnEditUAS_Click" CssClass="btn btn-danger" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Update
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />

                        
                    </asp:Panel>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
