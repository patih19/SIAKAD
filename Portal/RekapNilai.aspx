﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="RekapNilai.aspx.cs" Inherits="Portal.WebForm14" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="Scripts/DataTables/bootstrap.3.3.6.css" rel="stylesheet" type="text/css" />--%>
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
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager> 
    <div class="container top-main-form" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Rekap Input Nilai Per Mata Kuliah</strong></div>
                    <div class="panel-body">
                                <asp:Panel ID="PanelDosen" runat="server">
                                    <table class="table-condensed">
                                        <tr>
                                            <td>
                                                Tahun / Semester</td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                            <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                                                Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                                LoadingText="Loading" PromptText="Tahun">
                                                            </ajaxToolkit:CascadingDropDown>
                                                        </td>
                                                        <td>&nbsp;</td>
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
                                                <asp:DropDownList ID="DLProdi" runat="server" AutoPostBack="True" 
                                                    CssClass="form-control" onselectedindexchanged="DLProdi_SelectedIndexChanged"
                                                    >
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <asp:GridView ID="GVAktif" runat="server" 
                                        CssClass="table table-condensed table-bordered table-hover" onrowcreated="GVAktif_RowCreated" 
                                        onrowdatabound="GVAktif_RowDataBound" onprerender="GVAktif_PreRender">
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
                                                    <asp:LinkButton ID="LnkLihat" runat="server" onclick="LnkLihat_Click" OnClientClick="aspnetForm.target ='_blank';">Buka</asp:LinkButton>
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
</asp:Content>
