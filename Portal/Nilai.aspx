<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="Nilai.aspx.cs" Inherits="Portal.WebForm11" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
    <div class="row">
        <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
            <div>
                NILAI MAHASISWA<br />
                <br />
                <table class="table-bordered table-condensed">
                    <tr>
                        <td style="vertical-align: top">
                            Tahun / Semester
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                            <asp:ListItem>Tahun</asp:ListItem>
                                        </asp:DropDownList>
                                            <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                                Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                LoadingText="Loading" PromptText="Tahun">
                                            </ajaxToolkit:CascadingDropDown>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
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
                            Mata Kuliah
                        </td>
                        <td>
                            <asp:Panel ID="PanelProdi" runat="server">
                                <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control" AutoPostBack="True"
                                    OnSelectedIndexChanged="DLProdi_SelectedIndexChanged" Width="415px">
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
                                <asp:Panel ID="PanelMakul" runat="server">
                                <br />
                                    <asp:Label ID="Label1" runat="server" Style="font-style: italic; font-size: small" Text="pilih salah satu jadwal kuliah"></asp:Label>
                                    <asp:GridView ID="GVMakul" runat="server" CellPadding="4" CssClass="table-bordered table-condensed"
                                        ForeColor="#333333" GridLines="None">
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
                                    <asp:Label ID="LbIdProdi" runat="server"
                                        Style="font-size: 14px; color: #F2F2FF"></asp:Label>
                                    &nbsp;<asp:Label ID="LbKdMakul" runat="server" ForeColor="#F2F2FF" 
                                        Style="font-size: 14px; color: #F2F2FF"></asp:Label>
                                    &nbsp;<asp:Label ID="LbMakul" runat="server" ForeColor="Transparent" 
                                        Style="font-size: 14px; color: #F2F2FF"></asp:Label>
                                    &nbsp;<asp:Label ID="LBSKS" runat="server" ForeColor="Transparent" 
                                        Style="color: #F2F2FF; font-size: 14px"></asp:Label>
                                    &nbsp;<asp:Label ID="LbNIDN" runat="server" ForeColor="Transparent" 
                                        Style="font-size: 14px; color: #F2F2FF"></asp:Label>
                                    &nbsp;<asp:Label ID="LbDosen" runat="server" ForeColor="Transparent" 
                                        Style="font-size: 14px; color: #F2F2FF"></asp:Label>
                                    &nbsp;<asp:Label ID="LbKelas" runat="server" ForeColor="Transparent" 
                                        Style="font-size: 14px; color: #F2F2FF"></asp:Label>
                                    <br />

                                </asp:Panel>
                            </asp:Panel>
                        </td>
                    </tr>
                    </table>
                <hr />
                <asp:Panel ID="PanelPeserta" runat="server">
                    <table class="table-condensed">
                        <tr>
                            <td>
                                <strong>INPUT NILAI MAHASISWA </strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="table-condensed">
                                    <tr>
                                        <td>
                                            Mata Kuliah
                                        </td>
                                        <td>
                                            :
                                            <asp:Label ID="MakulLb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Tahun / Semester
                                        </td>
                                        <td>
                                            :
                                            <asp:Label ID="TahunLb" runat="server"></asp:Label>
                                            &nbsp;/
                                            <asp:Label ID="SemesterLb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Dosen
                                        </td>
                                        <td>
                                            :
                                            <asp:Label ID="DosenLb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GVPeserta" runat="server" CellPadding="4" 
                                    CssClass="table-bordered table-condensed" ForeColor="#333333" GridLines="None" 
                                    OnRowCreated="GVPeserta_RowCreated">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Nilai
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DLNilai" runat="server" CssClass="form-control">
                                                    <asp:ListItem>Nilai</asp:ListItem>
                                                    <asp:ListItem>A</asp:ListItem>
                                                    <asp:ListItem>B+</asp:ListItem>
                                                    <asp:ListItem>B</asp:ListItem>
                                                    <asp:ListItem>B-</asp:ListItem>
                                                    <asp:ListItem>C+</asp:ListItem>
                                                    <asp:ListItem>C</asp:ListItem>
                                                    <asp:ListItem>C-</asp:ListItem>
                                                    <asp:ListItem>D+</asp:ListItem>
                                                    <asp:ListItem>D</asp:ListItem>
                                                    <asp:ListItem>D-</asp:ListItem>
                                                    <asp:ListItem>E</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Simpan" />
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
