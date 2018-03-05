<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="DosenWali.aspx.cs" Inherits="Portal.DosenWali" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>

    <script type="text/jscript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {

                $('#ctl00_ContentPlaceHolder1_GVDosenAdd').DataTable({
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

                $('#ctl00_ContentPlaceHolder1_GvMhsAdd').DataTable({
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

    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVDosenAdd').DataTable({
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

            $('#ctl00_ContentPlaceHolder1_GvMhsAdd').DataTable({
                'iDisplayLength': 150,
                'aLengthMenu': [[200, 300, 400, 500, -1], [200, 300, 400, 500, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });

        });
    </script>

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
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <h5><strong>DATA PEMBIMBING AKADEMIK</strong></h5></div>
                    <div class="panel-body">


                        <asp:Panel ID="PanelSPI" runat="server">
                            <asp:UpdatePanel ID="PanelDosenWali" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-xs-12 col-md-12 col-lg-12">
                                            <asp:Panel ID="PanelDaftarMhs" runat="server"> 
                                                <asp:Label ID="LbNmDosen" runat="server" Text=""></asp:Label>
                                                 <p></p>
                                                Daftar Mahasiswa                                               
                                                <asp:GridView ID="GVPeserta" CssClass="table-condensed table-bordered" runat="server" CellPadding="4" GridLines="None" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#7C6F57" />
                                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#E3EAEB" />
                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                                </asp:GridView>
                                                <hr />
                                            </asp:Panel>
                                        </div>
                                        <div class="col-xs-12 col-md-5 col-lg-5">
                                            <table class="table-bordered table-condensed table">
                                                <tr>
                                                    <td style="background-color: #E1F0FF; padding-left: 10px">
                                                        <strong>DOSEN PENGAMPU</strong></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table class="table">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GVDosenAdd" runat="server" CellPadding="1" CssClass=" table-condensed table-bordered"
                                                                        ForeColor="#333333" GridLines="None" OnPreRender="GVDosenAdd_PreRender">
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="CbDosen" runat="server" AutoPostBack="True" OnCheckedChanged="CbDosen_CheckedChanged" />
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
                                                        </table>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-xs-12 col-md-7 col-lg-7">
                                            <asp:Panel ID="PanelMhs" runat="server">
                                                <table class="table-bordered table-condensed table">
                                                    <tr>
                                                        <td style="background-color: #E1F0FF; padding-left: 10px">
                                                            <strong>MAHASISWA</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <table class=" table">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GvMhsAdd" runat="server" CellPadding="4" CssClass=" table-condensed" GridLines="Horizontal" OnPreRender="GvMhsAdd_PreRender" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="CbMhs" runat="server" AutoPostBack="True" OnCheckedChanged="CbMhs_CheckedChanged" />
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
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>                                            
                                        </div>
                                    </div>




                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <asp:UpdateProgress ID="UpProgSPI" runat="server">
                            <ProgressTemplate>
                                <div class="mdl">
                                    <div class="center">
                                        <img src="images/loading135.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                            <asp:Label CssClass="hidden" ID="lbno_jadwal" runat="server" ForeColor="Transparent"></asp:Label>
                            <asp:Panel ID="PanelTambah" runat="server">
                            </asp:Panel>
                            <asp:Panel ID="PanelUpdate" runat="server">
                                <%--<asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click"
                                    CssClass="btn btn-success" />--%>
                            </asp:Panel>
                        <br />
                    </div>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
