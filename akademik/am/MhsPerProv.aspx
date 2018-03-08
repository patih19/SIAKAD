<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="MhsPerProv.aspx.cs" Inherits="akademik.am.MhsPerProv" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <br />
            <br />
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Provinsi Asal Mahasiswa Aktif</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>Tahun / Semester
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" TargetControlID="DLTahun" runat="server"
                                                    Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                    LoadingText="Loading" PromptText="Tahun">
                                                </ajaxToolkit:CascadingDropDown>
                                            </td>
                                            <td>&nbsp;
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
                        </table>
                    </div>
                    <div class="panel-footer">
                        &nbsp;<asp:Button ID="BtnProvMhsAktif" runat="server" Text="OK"
                            CssClass="btn btn-primary" OnClick="BtnProvMhsAktif_Click" />
                    </div>
                </div>
                <p></p>
                <asp:Panel ID="PanelMhsProv" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            Jumlah Mahasiwa Aktif Per Provinsi</div>
                        <div class="panel-body">
                            <asp:GridView ID="GvMhsAktifPerProv" CssClass=" table" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="GvMhsAktifPerProv_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>
                        <div class="panel-footer">
                        </div>
                    </div>
                </asp:Panel>

            </div>
        </div>
        </div>
</asp:Content>
