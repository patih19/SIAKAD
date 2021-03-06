﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="AddJadwalKuliah2.aspx.cs" Inherits="Portal.WebForm23"  EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <strong>Tambah Jadwal Kuliah</strong></div>
                    <div class="panel-body">
                                    <div>
                    <table class="table-bordered table-condensed">
                        <tr>
                            <td style="vertical-align: top">
                                Mata Kuliah <em><span class="style1">&nbsp;</span></em>
                            </td>
                            <td>
                                <asp:Label ID="LbProdi" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                    style="font-size: 14px"></asp:Label>
                                &nbsp;<asp:Label ID="LbKodeMakul" runat="server" Font-Size="Medium" 
                                    ForeColor="#333399" style="font-size: 14px"></asp:Label>
                                &nbsp;
                                <asp:Label ID="LbMakul" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                    style="font-size: 14px"></asp:Label>
                                <br />
                                <asp:Panel ID="PanelMakul" runat="server">
                                    <table class="table-condensed table-bordered">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DLProdiMakul" runat="server" AutoPostBack="True" CssClass="form-control"
                                                    OnSelectedIndexChanged="DLProdiMakul_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="PanelDetailMakul" runat="server">
                                        <table class="table-condensed table-bordered">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GVMakul" CssClass="table-condensed table-bordered" runat="server"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Kode">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CBMakul" runat="server" AutoPostBack="True" OnCheckedChanged="CBMakul_CheckedChanged" />
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
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                Dosen Pengajar
                            </td>
                            <td>
                                <asp:Label ID="LbNidn" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                    style="font-size: 14px"></asp:Label>
                                &nbsp;<asp:Label ID="LbDosen" runat="server" Font-Size="Medium" 
                                    ForeColor="#333399" style="font-size: 14px"></asp:Label>
                                <asp:Panel ID="PanelDosen" runat="server">
                                    <table class="table-condensed table-bordered">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DLProdiDosen" runat="server" AutoPostBack="True" CssClass="form-control"
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
                                    <asp:Panel ID="PanelDetailDosen" runat="server">
                                        <table class="table-condensed table-bordered">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GVDosen" runat="server" CellPadding="4" CssClass="table-condensed table-bordered"
                                                        ForeColor="#333333" GridLines="None">
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
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Pembagian Kelas
                            </td>
                            <td>
                                <asp:DropDownList ID="DLKelas" runat="server" CssClass="form-control" 
                                    Width="90px">
                                    <asp:ListItem>Kelas</asp:ListItem>
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Hari
                            </td>
                            <td>
                                <asp:DropDownList ID="DLHari" runat="server" Width="90px" CssClass="form-control" >
                                    <asp:ListItem>Hari</asp:ListItem>
                                    <asp:ListItem>Senin</asp:ListItem>
                                    <asp:ListItem>Selasa</asp:ListItem>
                                    <asp:ListItem>Rabu</asp:ListItem>
                                    <asp:ListItem>Kamis</asp:ListItem>
                                    <asp:ListItem>Jumat</asp:ListItem>
                                    <asp:ListItem>Sabtu</asp:ListItem>
                                    <asp:ListItem>-</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jam Mulai
                            </td>
                            <td>
                                <asp:TextBox ID="TbMulai" runat="server" CssClass="form-control" MaxLength="5" Width="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jam Selesai
                            </td>
                            <td>
                                <asp:TextBox ID="TbSelesai" runat="server" CssClass="form-control" MaxLength="5" Width="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ruang Kuliah
                            </td>
                            <td>
                                <asp:TextBox ID="TbRuang" CssClass="form-control" runat="server" MaxLength="15" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenis Kelas
                            </td>
                            <td>
                                <asp:DropDownList ID="DLJenisKelas" runat="server" Width="150px" CssClass="form-control">
                                    <asp:ListItem>Jenis Kelas</asp:ListItem>
                                    <asp:ListItem>Reguler</asp:ListItem>
                                    <asp:ListItem>Non Reguler</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Quota / Kapasitas Mahasiswa&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="TbQuota" CssClass="form-control" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tahun
                            </td>
                            <td>
                                <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control" Width="100px">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                    Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                    LoadingText="Loading" PromptText="Tahun">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Semester
                            </td>
                            <td>
                                <asp:DropDownList ID="DLSemester" runat="server" Width="120px" 
                                    CssClass="form-control">
                                    <asp:ListItem>Semester</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbno_jadwal" runat="server" ForeColor="Transparent"></asp:Label>
                            </td>
                            <td>
                                <asp:Panel ID="PanelTambah" runat="server">
                                    <asp:Button ID="BtnSave" runat="server" Text="Tambah" OnClick="BtnSave_Click" 
                                        CssClass="btn btn-success" />
                                </asp:Panel>
                                <asp:Panel ID="PanelUpdate" runat="server">
                                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" 
                                        onclick="BtnUpdate_Click" CssClass="btn btn-success" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <asp:GridView ID="GVJadwal" runat="server" CssClass="table-condensed table-bordered"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVJadwal_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_Click" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                Edit
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnDelete" runat="server" Text="Delete" 
                                    OnClick="BtnDelete_Click" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                Delete
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
                    </div>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
