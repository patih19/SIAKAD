<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="RekapSKS.aspx.cs" Inherits="akademik.am.WebForm16" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>REKAP PENGAMBILAN SKS</strong></div>
                    <div class="panel-body">
                    <table class="table-condensed">
                        <tr>
                            <td>
                                Tahun / Semester
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
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
                                            <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control">
                                                <asp:ListItem>Semester</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Program Studi</td>
                            <td>
                                    <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control" 
                                        Width="400px">
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
                    </div>
                    <div class="panel-footer">
                                <asp:Button ID="BtnOK" runat="server" onclick="BtnOK_Click" Text="OK" 
                                    CssClass="btn btn-primary" />
                    </div>
                </div>
                <asp:Panel ID="PanelRekapSKS" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            Daftar Mahasiswa &amp; Jumlah SKS</div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        <span class="style111">Program Studi </span>
                                    </td>
                                    <td>
                                        &nbsp;:
                                        <asp:Label ID="LbIdProdi" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="LbProdi" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tahun / Semester
                                    </td>
                                    <td>
                                        &nbsp;:
                                        <asp:Label ID="LbTahun" runat="server"></asp:Label>
                                        &nbsp;/
                                        <asp:Label ID="LbSemester" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="GVRekapSKS" runat="server" CssClass="table-bordered table-condensed"
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
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
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
