<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="FMaba.aspx.cs" Inherits="akademik.am.WebForm34" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DataTables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVMaba tr:hover { background-color: #d9edf7;}
        th{ color: White !important; background-color: rgb(51, 123, 102);}
        table#ctl00_ContentPlaceHolder1_GVMaba tbody tr:nth-child(odd) { background-color: #fff;}
        table#ctl00_ContentPlaceHolder1_GVMaba tbody tr:nth-child(odd){ background-color: #EEF7EE;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Mahasiswa Baru</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        Tahun Masuk</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" TargetControlID="DLTahun" runat="server"
                                                        Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="TahunAngkatan"
                                                        LoadingText="Loading" PromptText="Tahun">
                                                    </ajaxToolkit:CascadingDropDown>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                </table>
                        </div>
                        <div class="panel-footer">
                            &nbsp;<asp:Button ID="BtnJadwal" runat="server" Text="OK" OnClick="BtnJadwal_Click"
                                CssClass="btn btn-primary" />
                            <asp:Label ID="LbJadwalResult" runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:Panel ID="PanelMaba" runat="server">
                        <hr />
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                Daftar Mahasiswa Baru (FEEDER)</div>
                            <div class="panel-body">
                                <div class="box">
                                    <div class="box-body table-responsive">
                                        <div style="font-size:13px">
                                            <asp:GridView ID="GVMaba" runat="server" CssClass="table-bordered table-condensed"
                                                OnPreRender="GVMaba_PreRender">
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>

    <script type="text/javascript">
            $('#ctl00_ContentPlaceHolder1_GVMaba').DataTable({
                'iDisplayLength': 25,
                'aLengthMenu': [[25, 50, 100, 200, 300, 400, 500, 600, 700, 1000, 1250, 1500, 2000, 4000, 5000, -1], [25, 50,100, 200, 300, 400, 500, 600, 700, 1000, 1250, 1500, 2000, 4000, 5000,"All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
    </script>

</asp:Content>
