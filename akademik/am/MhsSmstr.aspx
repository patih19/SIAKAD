<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="MhsSmstr.aspx.cs" Inherits="akademik.am.WebForm26" EnableEventValidation="false" %>
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
                        <h6 class="panel-title">
                            <strong>Rekap Mahasiswa Per Semester</strong></h6>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    Tahun Angkatan 
                                </td>
                                <td>
                                    <asp:TextBox ID="TbThnAngkatan" runat="server" MaxLength="10" Width="130px" 
                                        CssClass="form-control" placeholder="ex:2014/2015"></asp:TextBox>
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
                                                <%--                                            <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                                Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                LoadingText="Loading" PromptText="Tahun">
                                            </ajaxToolkit:CascadingDropDown>--%>
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
                        <asp:Button ID="BtnFilterMhs" CssClass="btn btn-primary" runat="server" Text="OK"
                            OnClick="BtnFilter_Click" />
                    </div>
                </div>
                <asp:Panel ID="PanelRekap" runat="server">
                    <hr />
                    <table class="table table-striped table-bordered">
                        <tr>
                            <td>
                                <strong>PROGRAM STUDI</strong>
                            </td>
                            <td>
                                <strong>REGULER</strong>
                            </td>
                            <td>
                                <strong>NON REGULER</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 EKONOMI PEMBANGUNAN
                            </td>
                            <td>
                                <asp:Label ID="LbEkoReg" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbEkoNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                D3 AKUNTANSI
                            </td>
                            <td>
                                <asp:Label ID="LbAkunRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 ILMU ADMINISTRASI NEGARA
                            </td>
                            <td>
                                <asp:Label ID="LbSospolRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbSospolNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 PENDIDIKAN BAHASA INGGRIS
                            </td>
                            <td>
                                <asp:Label ID="LbInggrisRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbInggrisNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 PENDIDIKAN BAHASA DAN SASTRA INDONESIA
                            </td>
                            <td>
                                <asp:Label ID="LbIndonesiaRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbIndonesiaNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 AGROTEKNOLOGI
                            </td>
                            <td>
                                <asp:Label ID="LbAgroRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbAgroNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 TEKNIK ELEKTRO
                            </td>
                            <td>
                                <asp:Label ID="LbElektroRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbElektroNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 TEKNIK MESIN
                            </td>
                            <td>
                                <asp:Label ID="LbMesinRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbMesinNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                S1 TEKNIK SIPIL
                            </td>
                            <td>
                                <asp:Label ID="LbSipilRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbSipilNonRegTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                D3 TEKNIK MESIN
                            </td>
                            <td>
                                <asp:Label ID="LbOtoRegTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
