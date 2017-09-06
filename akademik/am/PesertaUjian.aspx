<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="PesertaUjian.aspx.cs" Inherits="akademik.am.WebForm10" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            <strong>Peserta Ujian</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
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
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Jenis Ujian
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLUjian" runat="server" CssClass="form-control" Width="220px">
                                            <asp:ListItem>Jenis Ujian</asp:ListItem>
                                            <asp:ListItem Value="uts">Ujian Tengah Semester</asp:ListItem>
                                            <asp:ListItem Value="uas">Ujian Akhir Semester</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Program Studi
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLProgramStudi" runat="server" OnSelectedIndexChanged="DLProgramStudi_SelectedIndexChanged"
                                            AutoPostBack="true" Width="415px" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <hr />
                    <asp:Panel ID="PanelProdi" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Jadwal Ujian</strong></div>
                            <div class="panel-body">
                                <asp:Panel ID="PanelMakul" runat="server">
                                    <asp:Label ID="Label1" runat="server" Style="font-style: italic; font-size: small"
                                        Text="pilih salah satu jadwal ujian"></asp:Label>
                                    &nbsp;<asp:GridView ID="GVJadwal" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                        ForeColor="#333333" GridLines="None" OnRowDataBound="GVJadwal_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Pilih
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBSelect" runat="server" />
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
                                    <br />
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" OnClientClick="aspnetForm.target ='_blank';"
                                        Text="Cetak" />
                                </asp:Panel>
                                <asp:Panel ID="PanelJadwalUjian" runat="server">
                                    <table class="table-condensed">
                                        <tr>
                                            <td>
                                                <span class="style111">Program Studi </span>
                                            </td>
                                            <td>
                                                &nbsp;:
                                                <asp:Label ID="LbIdProdi" runat="server"></asp:Label>
                                                &nbsp;<asp:Label ID="LbProdi" runat="server"></asp:Label>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Mata Kuliah
                                            </td>
                                            <td>
                                                &nbsp;:
                                                <asp:Label ID="LbKdMakul" runat="server"></asp:Label>
                                                &nbsp;<asp:Label ID="LbMakul" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Dosen
                                            </td>
                                            <td>
                                                &nbsp;:
                                                <asp:Label ID="LbNIDN" runat="server"></asp:Label>
                                                &nbsp;<asp:Label ID="LbDosen" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Kelas
                                            </td>
                                            <td>
                                                &nbsp;:
                                                <asp:Label ID="LbKelas" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Jadwal
                                            </td>
                                            <td>
                                                &nbsp;:
                                                <asp:Label ID="LbJadwal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
    </div> </div>
</asp:Content>
