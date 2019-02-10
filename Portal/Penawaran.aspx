<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="Penawaran.aspx.cs" Inherits="Portal.Penawaran" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>

    <script type="text/jscript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {

                $('#ctl00_ContentPlaceHolder1_GvMakul').DataTable({
                    'dom': '<"top"f>rtip',
                    'order': false,
                    'bPaginate': false,
                    "bInfo": false,
                    'iDisplayLength': 10,
                    'aLengthMenu': [[10, 25, 50, 75, 100, 200, 300, -1], [10, 25, 50, 75, 100, 200, 300, "All"]],
                    language: {
                        search: "Pencarian :",
                        searchPlaceholder: "Ketik Kata Kunci"
                    }
                });

            }
        }
    </script>
    <script type="text/jscript">
        $(document).ready(function () {

            $('#ctl00_ContentPlaceHolder1_GvMakul').DataTable({
                'dom': '<"top"f>rtip',
                'order': false,
                'bPaginate': false,
                "bInfo": false,
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
        table#ctl00_ContentPlaceHolder1_GvMakul tr:hover
        {
            background-color: #56688A;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
    </style>

    <style type="text/css">
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color:rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
       <br />
         <div class="row">
            <asp:Panel ID="PanelPenawaran" runat="server">
                <asp:UpdatePanel ID="PanelDosenWali" runat="server">
                    <ContentTemplate>
                        <%--Periode Penawaran Makul--%>
                        <div class="row">
                            <div class="col-xs-12 col-md-12 col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading ui-draggable-handle">
                                        <h5><strong>-- === PENAWARAN MATA KULIAH</strong> === --</h5> 
                                    </div>
                                    <div class="panel-body">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList CssClass="form-control" ID="DLTahun" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;&nbsp;</td>
                                                <td>
                                                    <asp:DropDownList CssClass="form-control" ID="DLSemester" runat="server">
                                                        <asp:ListItem Value="-1">-- Pilih Semester --</asp:ListItem>
                                                        <asp:ListItem Value="1">1 - Gasal</asp:ListItem>
                                                        <asp:ListItem Value="2">2 - Genap</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="BtnSubmit" CssClass="btn btn-primary" runat="server" Text="SUBMIT" OnClick="BtnSubmit_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <%--Daftar Mata Kuliah--%>
                            <asp:Panel ID="PanelMakul" runat="server">
                                <div class="col-xs-12 col-md-6 col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-heading ui-draggable-handle">
                                            <strong>DATAR MATA KULIAH</strong>
                                        </div>
                                        <div class="panel-body">
                                            <div class="table-responsive">
                                                <p>
                                                    <div class="bg-danger">
                                                        CERMATI MATA KULIAH SEBELUM DITAWARKAN
                                                    </div>
                                                </p>

                                            <asp:GridView ID="GvMakul" CssClass="table table-condensed table-hover table-bordered table-striped" runat="server" CellPadding="4" GridLines="Horizontal" OnRowDataBound="GvMakul_RowDataBound" OnPreRender="GvMakul_PreRender" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Action
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="BtnPenawaran" runat="server" OnClick="BtnPenawaran_Click" Text="Tawarkan" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                            </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>

                            <%--Makul Ditawarkan--%>
                            <asp:Panel ID="PanelMakulDitawarkan" runat="server">
                                <div class="col-xs-12 col-md-6 col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-heading ui-draggable-handle">
                                            <strong>MATA KULIAH DITAWARKAN</strong>
                                        </div>
                                        <div class="panel-body">
                                            <div class="table-responsive">
                                            <asp:GridView ID="GVPenawaran" CssClass="table table-condensed table-bordered table-striped" runat="server" CellPadding="4" GridLines="Horizontal" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" OnRowDataBound="GVPenawaran_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Action
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="BtnHapusPenawaran" runat="server" OnClick="BtnHapusPenawaran_Click" Text="Hapus" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                            </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:UpdateProgress ID="UpProgPenawaran" runat="server">
                <ProgressTemplate>
                    <div class="mdl">
                        <div class="center">
                            <img src="images/loading135.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <br />
         </div>
    </div>
</asp:Content>
