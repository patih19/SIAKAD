<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="CetakKartu.aspx.cs" Inherits="Portal.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div>
                    CETAK KRS & KHS<br />
                    &nbsp;<table class="table-condensed table-bordered">
                        <tr>
                            <td>
                                NPM : &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="130px" AutoPostBack="True"
                                    OnTextChanged="TBNpm_TextChanged" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="table-condensed">
                                    <!----------------------- TAHUN NAGKATAN -------------------------->
                                    <tr>
                                        <td>
                                            <asp:Label ID="LbNPM" runat="server" Text="NPM"></asp:Label>
                                        </td>
                                        <td>
                                            <!-- Foto here -->
                                            <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LbClass" runat="server" Text="Kelas"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LbThnAngkatan" runat="server" Text="Tahun Angkatan"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LbIdProdi" runat="server" ForeColor="Transparent"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                Jenis Kartu
                            </td>
                            <td>
                                &nbsp;<asp:RadioButton 
                                    ID="RbKRS" runat="server" Text="Kartu KRS" GroupName="kartu" />
                                &nbsp;<asp:RadioButton ID="RbKHS" runat="server" Text="Kartu KHS" 
                                    GroupName="kartu" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tahun / Semester
                            </td>
                            <td>
                                <table>
                                    <!----------------------- TAHUN NAGKATAN -------------------------->
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DLTahun" runat="server" Width="95px" CssClass="form-control">
                                                <asp:ListItem>Tahun</asp:ListItem>
                                                <asp:ListItem>2014</asp:ListItem>
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                                <asp:ListItem>2018</asp:ListItem>
                                                <asp:ListItem>2019</asp:ListItem>
                                                <asp:ListItem>2020</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <!-- Foto here -->
                                            <asp:DropDownList ID="DLSemester" runat="server" Width="120px" CssClass="form-control">
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
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="BtnFilterMhs" runat="server" Text="Cetak" OnClick="BtnFilterMhs_Click"
                                    OnClientClick="aspnetForm.target ='_blank';" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <br />
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
