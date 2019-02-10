<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="RekapPenawaran.aspx.cs" Inherits="Portal.RekapPenawaran" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>

    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVRekapPenawaran').DataTable({
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
        table#ctl00_ContentPlaceHolder1_GVDosen tr:hover {
            background-color: #d9edf7;
        }

        th {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }

        table#ctl00_ContentPlaceHolder1_GVDosen tbody tr:nth-child(odd) {
            background-color: #fff;
        }

        table#ctl00_ContentPlaceHolder1_GVDosen tbody tr:nth-child(odd) {
            background-color: #EEF7EE;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>REKAP PENAWARAN MATA KULIAH <asp:Label ID="LbSemester" runat="server" Text=""></asp:Label></strong> 
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="GVRekapPenawaran" runat="server"
                                CssClass="table table-bordered" OnPreRender="GVRekapPenawaran_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
