<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="KRS.aspx.cs" Inherits="Portal.WebForm19" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Kartu Rencana Studi (KRS)</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    NPM
                                </td>
                                <td>
                                    <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="130px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="table-condensed">
                                        <!----------------------- TAHUN NAGKATAN -------------------------->
                                        <tr>
                                            <td>
                                                <asp:Label ID="LbNpm" runat="server">NPM</asp:Label>
                                            </td>
                                            <td>
                                                <!-- Foto here -->
                                                <asp:Label ID="LbNama" runat="server">Nama</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LbProdi" runat="server">Program Studi</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LbKelas" runat="server">Kelas</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LbTahun" runat="server">Tahun</asp:Label>
                                            </td>
                                            <td style="opacity: 0">
                                                <asp:Label ID="LbIdProdi" runat="server" ForeColor="Transparent"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Aksi
                                </td>
                                <td>
                                    &nbsp;<em><asp:RadioButton ID="RBList" runat="server" Text="Lihat KRS" GroupName="KRS" />
                                        &nbsp; </em>
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
                                                <%--                                            <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                                Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                LoadingText="Loading" PromptText="Tahun">
                                            </ajaxToolkit:CascadingDropDown>--%>
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
                        <asp:Button ID="BtnFilterMhs" CssClass="btn btn-primary" runat="server" Text="OK"
                            OnClick="BtnFilter_Click" />
                    </div>
                </div>
                <asp:Panel ID="PanelListKRS" runat="server">
                    <hr />
                    DATA KRS<asp:GridView ID="GVListKrs" runat="server" CssClass="table"
                        CellPadding="4" ForeColor="#333333" GridLines="None" 
                        OnRowDataBound="GVListKrs_RowDataBound" ShowFooter="True">
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
                </asp:Panel>
                <br />
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
