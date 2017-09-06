<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Kartu.aspx.cs" Inherits="akademik.am.WebForm2" EnableEventValidation="false" %>
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
                            <strong>Cetak Kartu Ujian</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        NPM</td>
                                    <td>
                                <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="130px" AutoPostBack="True"
                                    OnTextChanged="TBNpm_TextChanged" CssClass="form-control" ></asp:TextBox>
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
                                        Jenis Kartu</td>
                                    <td>
                                        <asp:RadioButton ID="RbUTS" runat="server" Text="Kartu UTS" GroupName="kartu" />
                                &nbsp;<asp:RadioButton ID="RbUAS" runat="server" Text="Kartu UAS" GroupName="kartu" />
                                    </td>
                                </tr>
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
                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                </table>
                        </div>
                        <div class="panel-footer">
                                <asp:Button ID="BtnFilterMhs" runat="server" Text="Cetak" OnClick="BtnFilterMhs_Click"
                                   OnClientClick="aspnetForm.target ='_blank';" 
                                CssClass="btn btn-primary" />
                        </div>
                    </div>
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
