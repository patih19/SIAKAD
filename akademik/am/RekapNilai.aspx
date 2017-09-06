<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="RekapNilai.aspx.cs" Inherits="akademik.am.WebForm28" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.3.3.6.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVAktif tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(51, 123, 102); }
        table#ctl00_ContentPlaceHolder1_GVAktif tbody tr:nth-child(odd){ background-color :#fff;}
        table#ctl00_ContentPlaceHolder1_GVAktif tbody tr:nth-child(odd){ background-color :#EEF7EE;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Rekap Nilai Mahasiswa Berdasarkan Mata Kuliah</strong></div>
                    <div class="panel-body">
                        <asp:Panel ID="PanelDosen" runat="server">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        Tahun / Semester
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <%--<asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                                        <asp:ListItem>2015</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                    <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" TargetControlID="DLTahun" runat="server"
                                                        Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                        LoadingText="Loading" PromptText="Tahun">
                                                    </ajaxToolkit:CascadingDropDown>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
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
                                        <asp:DropDownList ID="DLProdi" runat="server" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="DLProdi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <asp:GridView ID="GVAktif" runat="server" CssClass="table table-condensed table-bordered table-striped table-hover"
                                OnRowCreated="GVAktif_RowCreated" OnRowDataBound="GVAktif_RowDataBound" OnPreRender="GVAktif_PreRender">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Nilai
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbNotReady" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lihat">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkLihat" runat="server" OnClick="LnkLihat_Click" OnClientClick="aspnetForm.target ='_blank';">Buka</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <script type="text/jscript">
                $('#ctl00_ContentPlaceHolder1_GVAktif').DataTable({
                    'iDisplayLength': 50,
                    'aLengthMenu': [[50, 75, 100, 200, 300, 400, 500, 600, 700, -1], [50, 75, 100, 200, 300, 400, 500, 600, 700, "All"]],
                    language: {
                        search: "Pencarian :",
                        searchPlaceholder: "Ketik Kata Kunci"
                    }
                });
    </script>
</asp:Content>
