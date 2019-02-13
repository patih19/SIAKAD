<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Portal.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Data Mahasiswa Aktif <asp:Label ID="LbSemAktif1" runat="server"></asp:Label></strong>
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
                                    <asp:Button ID="BtnSubmit" CssClass="btn btn-primary " runat="server" Text="submit" OnClick="BtnSubmit_Click" /></td>
                            </tr>
                        </table>
                        <hr />
                        <asp:Panel ID="PanelRekapAktif" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="GvMhsAktif" CssClass="table table-condensed table-bordered table-striped" runat="server" OnRowDataBound="GvMhsAktif_RowDataBound" ShowFooter="True" CellPadding="4" GridLines="None" ForeColor="#333333">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
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
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Rekap Bimbingan KRS <asp:Label ID="LbSemAktif2" runat="server"></asp:Label></strong>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DLTahun2" CssClass="form-control" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="DLSemester2" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="-1">semester</asp:ListItem>
                                        <asp:ListItem Value="1">1 Gasal</asp:ListItem>
                                        <asp:ListItem Value="2">2 Genap</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:Button ID="BtnBimKRS" CssClass="btn btn-primary " runat="server" Text="submit" OnClick="BtnBimKRS_Click" /></td>
                            </tr>
                        </table>
                        <hr />
                        <div class="table table-responsive">
                            <asp:GridView ID="GVBimbinganKRS" CssClass="table table-condensed table-bordered table-striped" runat="server" CellPadding="4" GridLines="None" ForeColor="#333333">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
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
                        <em><strong>Fitur Bimbingan KRS Mulai Aktif Pada Tahun Akademik 2017/2018 Semester Genap</strong> </em>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
