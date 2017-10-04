<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Aktif.aspx.cs" Inherits="akademik.am.WebForm1" EnableEventValidation="false" %>
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
                        <strong>Rekap Mahasiswa Aktif</strong></div>
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
                                                            <%-- <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
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
                                                <asp:DropDownList ID="DLProdi" runat="server" AutoPostBack="True" CssClass="form-control"
                                                    OnSelectedIndexChanged="DLProdiDosen_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">Program Studi</asp:ListItem>
                                                    <asp:ListItem Value="20-201">S1 TEKNIK ELEKTRO</asp:ListItem>
                                                    <asp:ListItem Value="21-201">S1 TEKNIK MESIN</asp:ListItem>
                                                    <asp:ListItem Value="21-401">D3 TEKNIK MESIN</asp:ListItem>
                                                    <asp:ListItem Value="22-201">S1 TEKNIK SIPIL</asp:ListItem>
                                                    <asp:ListItem Value="54-211">S1 AGROTEKNOLOGI</asp:ListItem>
                                                    <asp:ListItem Value="60-201">S1 EKONOMI PEMBANGUNAN</asp:ListItem>
                                                    <asp:ListItem Value="62-401">D3 AKUNTANSI</asp:ListItem>
                                                    <asp:ListItem Value="63-201">S1 ILMU ADMINISTRASI NEGARA</asp:ListItem>
                                                    <asp:ListItem Value="88-201">S1 PENDIDIKAN BAHASA DAN SASTRA INDONESIA</asp:ListItem>
                                                    <asp:ListItem Value="88-203">S1 PENDIDIKAN BAHASA INGGRIS</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:GridView ID="GVAktif" runat="server" CellPadding="4" 
                                        CssClass="table-condensed table-bordered" ForeColor="#333333" 
                                        GridLines="None">
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
