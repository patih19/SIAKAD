<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="ValidasiKRS.aspx.cs" Inherits="Portal.ValidasiKRS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GvMonitorValidKRS').DataTable({
                'iDisplayLength': 50,
                'aLengthMenu': [[50, 75, 100, 150, 200, 300, -1], [50, 75, 100, 150, 200, 300, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>

    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GvMonitorValidKRS tr:hover {
            background-color: #d9edf7;
        }

        th {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }

        table#ctl00_ContentPlaceHolder1_GvMonitorValidKRS tbody tr:nth-child(odd) {
            background-color: #fff;
        }

        table#ctl00_ContentPlaceHolder1_GvMonitorValidKRS tbody tr:nth-child(odd) {
            background-color: #EEF7EE;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <br />
            <div class="panel panel-default">
                <div class="panel-heading ui-draggable-handle">
                    <h5><strong>MONITOR VALIDASI KRS</strong></h5>
                </div>
                <div class="panel-body">
                    <div class="table table-responsive">

                        <asp:GridView ID="GvMonitorValidKRS" CssClass="table table-condensed table-bordered " runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnPreRender="GvMonitorValidKRS_PreRender">
                            <AlternatingRowStyle BackColor="White" />
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

                    </div>
                </div>
            </div>

        </div>
   </div>
</asp:Content>
