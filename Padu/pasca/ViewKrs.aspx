<%@ Page Title="" Language="C#" MasterPageFile="~/pasca/Pasca.Master" AutoEventWireup="true" CodeBehind="ViewKrs.aspx.cs" Inherits="Padu.pasca.ViewKrs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="content-header">
        <h1>Lihat KRS</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Kartu Rencana Studi (KRS)</strong>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>Tahun/Semester
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control" Width="120px">
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
                                    <asp:DropDownList ID="DLSemester" runat="server"
                                        CssClass="form-control" Width="120px">
                                        <asp:ListItem>Semester</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="BtnViewKRS" runat="server" OnClick="BtnViewKRS_Click" Text="Submit" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:Panel ID="PanelListKRS" runat="server">
                    DATA KRS
                    <asp:GridView ID="GVListKrs" runat="server" CssClass="table table-striped table-bordered"
            CellPadding="4" ForeColor="#333333" GridLines="None"
            OnRowDataBound="GVListKrs_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
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
                    <asp:Button ID="BtnDwnKrs" runat="server" Text="Download"
                        CssClass="btn btn-success" OnClick="BtnDwnKrs_Click" />
                </asp:Panel>

            </div>
        </div>
    </div>
</asp:Content>
