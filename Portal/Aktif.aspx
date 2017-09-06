<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="Aktif.aspx.cs" Inherits="Portal.WebForm10" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVAktif').DataTable({
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
        table#ctl00_ContentPlaceHolder1_GVAktif tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVAktif tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVAktif tbody tr:nth-child(odd)
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
                        <strong>Mahasiswa Aktif</strong></div>
                    <div class="panel-body">

                        <table class="table-condensed">
                        <tr>
                            <td style="vertical-align: top">
                                Tahun
                            </td>
                            <td>
                                <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                    Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                    LoadingText="Loading" PromptText="Tahun">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                Semester
                            </td>
                            <td>
                                <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control" 
                                    AutoPostBack="True" onselectedindexchanged="DLSemester_SelectedIndexChanged">
                                    <asp:ListItem>Semester</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    </div>
                </div>
                <br />
                <div>
                    <br />
                    <br />
                    <asp:Panel ID="PanelProdi" runat="server">
                        <asp:Panel ID="PanelMakul" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading ui-draggable-handle">
                                    <strong>Daftar Mahasiswa Tiap Mata Kuliah</strong></div>
                                <div class="panel-body">
                                    <asp:GridView ID="GVAktif" runat="server" 
                                        CssClass="table table-condensed table-bordered table-hover" 
                                        onrowdatabound="GVAktif_RowDataBound" onprerender="GVAktif_PreRender">
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
