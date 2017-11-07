<%@ Page Title="" Language="C#" MasterPageFile="~/pasca/Pasca.Master" AutoEventWireup="true" CodeBehind="EditKrs.aspx.cs" Inherits="Padu.pasca.EditKrs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            color: #FF3300;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="content-header">
        <h1>Edit KRS</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="PanelMhs" runat="server">
                    <table class="table-condensed">
                        <tr>
                            <td colspan="2">
                                <h5>
                                    <strong>Kartu Rencana Studi (KRS)</strong></h5>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama
                            </td>
                            <td>:
                                    <asp:Label ID="LbNama" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>NPM
                            </td>
                            <td>:
                                    <asp:Label ID="LbNpm" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Kelas
                            </td>
                            <td>:
                                    <asp:Label ID="LbKelas" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Program Studi
                            </td>
                            <td>:
                                    <asp:Label ID="LbKdProdi" runat="server"></asp:Label>
                                &nbsp;-
                                    <asp:Label ID="LbProdi" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tahun</td>
                            <td>:
                                    <asp:Label ID="LbTahun" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Edit Kartu Rencana Studi (KRS)</strong>
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
                                    <asp:Button ID="BtnEditKRS" runat="server" OnClick="BtnEditKRS_Click" Text="Submit" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <asp:Panel ID="PanelEditKRS" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <%--<asp:Panel ID="PanelBdk" runat="server" CssClass="form-control" BackColor="#FFFF99">
                                <strong>Jumlah maksimal SKS :</strong>
                                <asp:Label ID="LbMaxSKS" runat="server" Text="" Style="font-weight: 700"></asp:Label>
                            </asp:Panel>--%>
                            <p></p>
                            <span class="auto-style1"><strong>Batal Tambah/Edit KRS Hanya Bisa Dilakukan 4x !!</strong></span><br />
                            <strong>Jumlah SKS =</strong>
                            <asp:Label ID="LbJumlahEditSKS" runat="server" Style="font-weight: 700"></asp:Label>
                            <br />
                            <asp:GridView ID="GVEditKRS" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                CssClass="table table-striped"
                                OnRowDataBound="GVEditKRS_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CBEdit" runat="server" OnCheckedChanged="CBEdit_CheckedChanged"
                                                AutoPostBack="True" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Pilih
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Sisa
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbSisa" runat="server" Style="font-weight: 700"></asp:Label>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update"
                        OnClick="BtnUpdate_Click" CssClass="btn btn-primary"
                        OnClientClick="return confirm('Anda Yakin Data Tersebut Benar ?');" />
                </asp:Panel>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
