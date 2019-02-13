<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="MhsTunggu.aspx.cs" Inherits="Portal.MhsTunggu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
        <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GvMhsTunggu').DataTable({
                'iDisplayLength': 200,
                'aLengthMenu': [[200, 300, -1], [200, 300, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>

    <script type="text/jscript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {

                $('#ctl00_ContentPlaceHolder1_GvMhsTunggu').DataTable({
                    'dom': '<"top"f>rtip',
                    'bPaginate': false,
                    "bInfo": false,
                    'iDisplayLength': 10,
                    'aLengthMenu': [[10, 25, 50, 75, 100, 200, 300, -1], [10, 25, 50, 75, 100, 200, 300, "All"]],
                    language: {
                        search: "Pencarian :",
                        searchPlaceholder: "Ketik Kata Kunci"
                    }
                });

                $('#ctl00_ContentPlaceHolder1_GvMhsTunggu').DataTable({
                    'iDisplayLength': 150,
                    'aLengthMenu': [[200, 300, 400, 500, -1], [200, 300, 400, 500, "All"]],
                    language: {
                        search: "Pencarian :",
                        searchPlaceholder: "Ketik Kata Kunci"
                    }
                });
            }
        }
    </script>



    <%--<style type="text/css">
        .mdl
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.5;
            filter: alpha(opacity=50);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 115px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 95px;
            width: 95px;
        }
    </style>

    <style type="text/css">
        .top {
            float: left;
        }

        .dataTables_filter {
            float: left !important;
        }
    </style>--%>

    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GvMhsTunggu tr:hover {
            background-color: #d9edf7;
        }

        th {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }

        table#ctl00_ContentPlaceHolder1_GvMhsTunggu tbody tr:nth-child(odd) {
            background-color: #fff;
        }

        table#ctl00_ContentPlaceHolder1_GvMhsTunggu tbody tr:nth-child(odd) {
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
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Daftar Mahasiswa Tidak Input KRS</strong></div>
                    <div class="panel-body">
                        <div style="font-size:14px; color:black">
                        Berikut Daftar Mahasiswa Tidak Input KRS Pada Tahun Akademik <asp:Label ID="LbThnAkademik" runat="server" Text=""></asp:Label> Semester <asp:Label ID="LbSemester" runat="server" Text=""></asp:Label> <br />
                            Silahkan Tandai Mahasiswa Sudah Selesai Pendadaran Atau Tinggal Menunggu Wisuda
                        </div>
                            <hr />
                        <asp:Panel ID="PanelRekapAktif" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="GvMhsTunggu" CssClass="table table-condensed table-bordered table-hover" runat="server" OnRowCreated="GvMhsTunggu_RowCreated" OnPreRender="GvMhsTunggu_PreRender">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Kuliah Sudah Selesai ?
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CBSelesai" runat="server" OnCheckedChanged="CBSelesai_CheckedChanged" AutoPostBack="True" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:CheckBox ID="CbSelesai" runat="server" />
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
