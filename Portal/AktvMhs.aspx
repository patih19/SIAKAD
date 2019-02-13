<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="AktvMhs.aspx.cs" Inherits="Portal.WebForm20" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVAktvMhs').DataTable({
                'iDisplayLength': 100,
                'aLengthMenu': [[100, 250, 400, 600, 700, 900, 1000, -1], [100, 250, 400, 600, 700, 900, 1000, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVAktvMhs tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVAktvMhs tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVAktvMhs tbody tr:nth-child(odd)
        {
            background-color: #EEF7EE;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Aktivitas Kuliah Mahasiswa</strong></div>
                    <div class="panel-body">
                    <table class="table-condensed">
                        <tr>
                            <td>
                                Tahun
                                / Semester</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" TargetControlID="DLTahun" runat="server"
                                                Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                LoadingText="Loading" PromptText="Tahun">
                                            </ajaxToolkit:CascadingDropDown>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                <asp:DropDownList ID="DlSemester" runat="server" CssClass="form-control">
                                    <asp:ListItem>Semester</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Program Studi
                            </td>
                            <td>
                                <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
                    </div>
                    <div class="panel-footer">
                                &nbsp;<asp:Button ID="BtnAktvMhs" runat="server" Text="OK" 
                                    OnClick="BtnAktvMhs_Click" CssClass="btn btn-primary" />
                                <asp:Label ID="LbJadwalResult" runat="server"></asp:Label>
                    </div>
                </div>
                    <asp:Panel ID="PanelAktivitas" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Rekap Aktivitas Perkuliahan</strong></div>
                            <div class="panel-body">
                                <div class="table table-responsive">
                                    <asp:GridView ID="GVAktvMhs" runat="server" CssClass="table table-condensed table-bordered table-hover"
                                        OnPreRender="GVAktvMhs_PreRender">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
