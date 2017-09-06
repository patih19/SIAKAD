<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Ajar.aspx.cs" Inherits="akademik.am.WebForm21" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <strong>Rekap Dosen Mengajar</strong></div>
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
                                        <asp:DropDownList ID="DLProdiDosen" runat="server" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="DLProdiDosen_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="LbNidn" runat="server" Font-Size="Medium" ForeColor="#333399" Style="font-size: 14px"></asp:Label>
                                        &nbsp;<asp:Label ID="LbDosen" runat="server" Font-Size="Medium" ForeColor="#333399"
                                            Style="font-size: 14px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="PanelDetailDosen" runat="server">

                                            <asp:GridView ID="GVDosen" runat="server" CellPadding="4" CssClass="table-condensed table-bordered"
                                                ForeColor="#333333" GridLines="None" onrowcreated="GVDosen_RowCreated" 
                                                onrowdatabound="GVDosen_RowDataBound">
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
                            </asp:Panel>
                        </asp:Panel>
                        <asp:GridView ID="GVAjar" runat="server" CellPadding="4" CssClass="table-condensed table-bordered"
                            ForeColor="#333333" GridLines="None" OnRowDataBound="GVAjar_RowDataBound" 
                            ShowFooter="True" onrowcreated="GVAjar_RowCreated">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Nilai">
                                    <HeaderTemplate>
                                        Input Nilai
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LbNotReady" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lihat">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkLihat" runat="server" onclick="LnkLihat_Click" 
                                            onclientclick="aspnetForm.target ='_blank';">Buka</asp:LinkButton>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
