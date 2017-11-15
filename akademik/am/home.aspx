<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="akademik.am.WebForm12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVDosen tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVDosen tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVDosen tbody tr:nth-child(odd)
        {
            background-color: #EEF7EE;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <asp:Panel ID="PanelUji" runat="server">
        
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Rekap Data Mahasiswa</strong>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DLTahun" CssClass="form-control" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="DLSemester" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="-1">semester</asp:ListItem>
                                        <asp:ListItem Value="1">1 Gasal</asp:ListItem>
                                        <asp:ListItem Value="2">2 Genap</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:Button ID="BtnSubmit" CssClass="btn btn-success" runat="server" Text="submit" OnClick="BtnSubmit_Click" /></td>
                            </tr>
                        </table>
                        <hr />
                        <asp:Panel ID="PanelRekapAktif" runat="server">
                            <asp:GridView ID="GvMhsAktif" CssClass="table table-condensed table-bordered table-striped"  runat="server" OnRowDataBound="GvMhsAktif_RowDataBound" ShowFooter="True">
                            </asp:GridView>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>
    </div>
</asp:Content>
