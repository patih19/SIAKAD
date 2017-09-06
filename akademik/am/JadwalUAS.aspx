<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="JadwalUAS.aspx.cs" Inherits="akademik.am.WebForm5" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Jadwal Ujian Akhir Semester (UAS)</strong></div>
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
                                        Tahun / Semester
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
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control">
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
                            &nbsp;<asp:Button ID="BtnJadwalUAS" runat="server" Text="OK" OnClick="BtnJadwalUAS_Click"
                                CssClass="btn btn-primary" />
                            &nbsp;<asp:Label ID="LbJadwalResult" runat="server"></asp:Label>
                        </div>
                    </div>
                    <hr />
                    <asp:Panel ID="PanelJadwal" runat="server">
                        <div class="panel panel-default">
                        <div class="panel-body">
                            <asp:GridView ID="GVJadwalUAS" runat="server" CellPadding="4" 
                                CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                                OnRowDataBound="GVJadwalUAS_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEditUAS" runat="server" OnClick="BtnEditUAS_Click" 
                                                Text="Update" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Update
                                        </HeaderTemplate>
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
                        </div>
                    </div>

                    </asp:Panel>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
