<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Absensi.aspx.cs" Inherits="akademik.am.WebForm3" EnableEventValidation="false" %>
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
                        <strong>PRESENSI PERKULIAHAN</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                        <tr>
                            <td style="vertical-align: top">
                                Tahun / Semester</td>
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
                            <td style="vertical-align: top">
                                Program Studi</td>
                            <td>
                                <asp:Panel ID="PanelProdi" runat="server">
                                    <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnSelectedIndexChanged="DLProdi_SelectedIndexChanged" Width="400px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div class="panel-footer">
                                &nbsp;</div>
                </div>
                    <asp:Panel ID="PanelMakul" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Pilih Salah Satu Jadwal Perkuliahan</strong></div>
                            <div class="panel-body">
                                <div style="font-size:13px">
                                    <asp:GridView ID="GVMakul" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                        ForeColor="#333333" GridLines="None" 
                                        DataKeyNames="kode makul,nidn,kelas" onrowdatabound="GVMakul_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Pilih
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBSelect" runat="server" AutoPostBack="True" OnCheckedChanged="CBSelect_CheckedChanged" />
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
                            <div class="panel-footer">
                                &nbsp;</div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PanelPeserta" runat="server">
                        <hr />
                        DAFTAR PRESENSI MAHASISWA<table class="table-condensed table-bordered">
                            <tr>
                                <td>
                                    <table class="table-condensed">
                                        <tr>
                                            <td>
                                                <span class="style111">Program Studi </span>
                                            </td>
                                            <td>
                                                &nbsp;:
                                                <asp:Label ID="LbIdProdi" runat="server"></asp:Label>
                                                &nbsp;<asp:Label ID="LbProdi" runat="server"></asp:Label>
                                                &nbsp;</td>
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GVPeserta" runat="server" CellPadding="4" 
                                        CssClass="table-bordered table-condensed" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    No.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
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
                            <tr>
                                <td>
                                    <asp:Button ID="BtnCetak" runat="server" OnClick="BtnCetak_Click" Text="Cetak" OnClientClick="aspnetForm.target ='_blank';" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
