<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="AddLls.aspx.cs" Inherits="akademik.am.WebForm31" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>INPUT MAHASISWA LULUS</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td class="style4">
                                        <strong>Nomor Mahasiswa </strong>(NPM)
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbNpm" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Nama</strong>
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TBNama" runat="server" CssClass="form-control" Placeholder="Huruf Kapital"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Program Studi </strong>&nbsp;
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control">
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
                                <tr>
                                    <td class="style4">
                                        <strong>Tahun Angkatan</strong>
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbAngkatan" runat="server" CssClass="form-control" Placeholder="Tahun Masuk, ex:2010/2011"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Jenis Kelas</strong>
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="-1">Jenis</asp:ListItem>
                                            <asp:ListItem Value="A">Reguler</asp:ListItem>
                                            <asp:ListItem Value="C">Non Reguler</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Gender</strong>
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList CssClass="form-control" ID="DLSex" runat="server">
                                            <asp:ListItem Value="-1">Gender</asp:ListItem>
                                            <asp:ListItem>Laki-laki</asp:ListItem>
                                            <asp:ListItem>Perempuan</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                </table>
                        </div>
                        <div class="panel-footer">
                            <asp:Button ID="BtnInsert" runat="server" CssClass="btn btn-primary" Text="INPUT"
                                OnClick="BtnInsert_Click" />
                        </div>
                    </div>
                </div>
                <hr />
            </div>
        </div>
    </div>
</asp:Content>
