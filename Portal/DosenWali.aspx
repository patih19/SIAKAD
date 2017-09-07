<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="DosenWali.aspx.cs" Inherits="Portal.DosenWali" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
        table#TblQuota tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(133, 153, 154); }
        table#TblQuota tbody tr:nth-child(odd){ background-color :#fff;}
        table#TblQuota tbody tr:nth-child(odd){ background-color :#EEF7EE;}
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
                            <asp:UpdatePanel ID="UpPnlDosenWali" runat="server">
                                <ContentTemplate>
                                    <table class="table-bordered table-condensed table">
                                        <tr>
                                            <td style="background-color: #E1F0FF; padding-left: 10px">
                                                <strong>DOSEN PENGAMPU</strong></td>
                                            <td style="background-color: #E1F0FF; padding-left: 10px">&nbsp;</td>
                                            <td style="background-color: #E1F0FF; padding-left: 10px">
                                                <strong>MAHASISWA</strong></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PanelDosen" runat="server">
                                                    <div style="background-color: #FFFFEC">
                                                        <table class="table-condensed">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GVDosen" runat="server" CellPadding="4" CssClass=" table-condensed table-bordered"
                                                                        ForeColor="#333333" GridLines="None" OnPreRender="GVDosen_PreRender">
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

                                                    </div>
                                                </asp:Panel>
                                                <p></p>
                                            </td>
                                            <td></td>
                                            <td>

                                                <table class="table-condensed">
                                                    <tr>
                                                        <td>Tahun :
                                                        </td>
                                                        <td>
                                                            <div style="background-color: #FFFFEC">
                                                                <asp:DropDownList ID="DlThnAngkatan" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DLProdiDosen_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="background-color: #FFFFEC">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <table class="table-condensed">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GvMhs" runat="server" CellPadding="4" CssClass=" table-condensed table-bordered" ForeColor="#333333" GridLines="None" OnPreRender="GVDosen_PreRender">
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
                                                    </asp:Panel>
                                                </div>

                                                <p>
                                                </p>
                                            </td>
                                        </tr>
                                    </table>
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
                                <asp:Button ID="BtnSave" runat="server" Text="Tambah" OnClick="BtnSave_Click" CssClass="btn btn-success" />
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
