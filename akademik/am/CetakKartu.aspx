<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="CetakKartu.aspx.cs" Inherits="akademik.am.WebForm11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type = "text/javascript">
    function SetTarget() {
        document.forms[0].target = "_blank";
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Cetak KRS &amp; KHS</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    NPM
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
                                <td>
                                    Jenis
                                    Kartu</td>
                                <td>
                                    <asp:RadioButton 
                                    ID="RbKRS" runat="server" Text="Kartu KRS" GroupName="kartu" />
                                &nbsp;<asp:RadioButton ID="RbKHS" runat="server" Text="Kartu KHS" 
                                    GroupName="kartu" />
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
                        </table>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="BtnFilterMhs" CssClass="btn btn-primary"  runat="server" Text="Cetak" OnClientClick="SetTarget();" OnClick="BtnFilterMhs_Click"  />
                    </div>
                </div>
                <br />
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
