<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="FKrsMhs.aspx.cs" Inherits="akademik.am.WebForm37" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DataTables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVKrsMhs tr:hover { background-color: #d9edf7;}
        th{ color: White !important; background-color: rgb(51, 123, 102);}
        table#ctl00_ContentPlaceHolder1_GVKrsMhs tbody tr:nth-child(odd) { background-color: #fff;}
        table#ctl00_ContentPlaceHolder1_GVKrsMhs tbody tr:nth-child(odd){ background-color: #EEF7EE;}
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
                            <strong>KRS Mahasiswa (FEEDER)</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        Program Studi
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Semester
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="TbSemester" runat="server" 
                                                        MaxLength="5" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                </table>
                        </div>
                        <div class="panel-footer">
                            &nbsp;<asp:Button ID="BtnKRS" runat="server" Text="OK" OnClick="BtnKRS_Click"
                                CssClass="btn btn-primary" />
                            <asp:Label ID="LbKRSResult" runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:Panel ID="PanelKRSMhs" runat="server">
                        <hr />
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                Daftar KRS Mahasiswa</div>
                            <div class="panel-body">
                                <div class="box">
                                    <div class="box-body table-responsive">
                                        <asp:GridView ID="GVKrsMhs" runat="server" CssClass="table table-bordered table-condensed"
                                            OnPreRender="GVKrsMhs_PreRender">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                </div>
            </div>
            <br />
        </div>
    </div>

    <script type="text/javascript">
        $('#ctl00_ContentPlaceHolder1_GVKrsMhs').DataTable({
            'iDisplayLength': 25,
            'aLengthMenu': [[25, 50, 100, 200, 300, 400, 500, 600, 700, 1000, 1250, 1500, 2000, 4000, 5000, -1], [25, 50, 100, 200, 300, 400, 500, 600, 700, 1000, 1250, 1500, 2000, 4000, 5000, "All"]],
            language: {
                search: "Pencarian :",
                searchPlaceholder: "Ketik Kata Kunci"
            }
        });
    </script>

</asp:Content>
