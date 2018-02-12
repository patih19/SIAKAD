<%@ Page Title="" Language="C#" MasterPageFile="~/pasca/Pasca.Master" AutoEventWireup="true" CodeBehind="khs.aspx.cs" Inherits="Padu.pasca.khs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="content-header">
        <h1>KHS</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                
                <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Kartu Hasil Studi (KHS)</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        Tahun/Semester
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
                                        <asp:Button ID="BtnKHS" runat="server" OnClick="BtnKHS_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                <asp:Panel ID="PanelKHS" runat="server">
                    <asp:GridView ID="GvKHS" runat="server" CssClass="table table-bordered table-condensed" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GvKHS_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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
                    <strong>Jumlah SKS : <asp:Label ID="LBSks" runat="server" Text="Label"></asp:Label></strong> <br />
                    <strong> IP Semester : <asp:Label ID="LbIPS" runat="server" Text="Label"></asp:Label></strong>
                    
                    </asp:Panel>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
