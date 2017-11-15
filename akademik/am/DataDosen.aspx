<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="DataDosen.aspx.cs" Inherits="akademik.am.DataDosen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVDosen').DataTable({
                'iDisplayLength': 200,
                'aLengthMenu': [[200, 300, 400, 500, 600, 700, -1], [200, 300, 400, 500, 600, 700, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVDosen tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVDosen tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVDosen tbody tr:nth-child(odd)
        {
            background-color: #EEF7EE;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <asp:Panel ID="PanelRekapDosen" CssClass=" panel panel-body"  runat="server">
                <asp:GridView CssClass="table table-condensed table-bordered table-striped table-hover" ID="GvRekapDosen" runat="server" OnRowDataBound="GvRekapDosen_RowDataBound" ShowFooter="True">
                </asp:GridView>
                </asp:Panel>
                <br />


            </div>
        </div>
    </div>
</asp:Content>
