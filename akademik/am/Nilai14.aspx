<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Nilai14.aspx.cs" Inherits="akademik.am.WebForm24" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
    <div class="row">
        <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
            <div>
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <h5 class="panel-title">
                            <strong>Data Mahasiswa</strong></h5>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    NPM
                                </td>
                                <td>
                                    <asp:TextBox ID="TBNpm" CssClass="form-control" runat="server" MaxLength="10" Width="130px"></asp:TextBox>
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
                                                    <asp:ListItem>Tahun</asp:ListItem>
                                                    <asp:ListItem Value="2014/2015">2014</asp:ListItem>
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
                            </table>
                    </div>
                    <div class="panel-footer">
                        <asp:Button CssClass="btn btn-primary" ID="BtnCari" runat="server" 
                            Text="Submit" onclick="BtnCari_Click" />
                    </div>
                </div>
                <hr />
                <asp:Panel ID="PanelMataKuliah" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <h3 class="panel-title">
                            <strong>Input Nilai Semester Mahasiswa</strong></h3>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                                <!----------------------- TAHUN NAGKATAN -------------------------->
                                <tr>
                                    <td>
                                        NPM</td>
                                    <td>
                                        <!-- Foto here -->
                                        :
                                        <asp:Label ID="LbNpm" runat="server">NPM</asp:Label>
                                    </td>
                                    <td style="opacity: 0">
                                        <asp:Label ID="LbIdProdi" runat="server" ForeColor="Transparent"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nama</td>
                                    <td>
                                        :
                                        <asp:Label ID="LbNama" runat="server">Nama</asp:Label>
                                    </td>
                                    <td style="opacity: 0">
                                        <asp:Label ID="LbAngkatan" runat="server" ForeColor="Transparent"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Program Studi</td>
                                    <td>
                                        :
                                        <asp:Label ID="LbProdi" runat="server">Program Studi</asp:Label>
                                    </td>
                                    <td style="opacity: 0">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Kelas</td>
                                    <td>
                                        :
                                        <asp:Label ID="LbKelas" runat="server">Kelas</asp:Label>
                                    </td>
                                    <td style="opacity: 0">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        <br />
                        <asp:GridView ID="GVMakul" runat="server" CellPadding="4" 
                            CssClass="table-bordered table-condensed" ForeColor="#333333" GridLines="None" 
                            OnRowCreated="GVPeserta_RowCreated">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Nilai
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DLNilai" runat="server" CssClass="form-control">
                                            <asp:ListItem>Nilai</asp:ListItem>
                                            <asp:ListItem>A</asp:ListItem>
                                            <asp:ListItem>B+</asp:ListItem>
                                            <asp:ListItem>B</asp:ListItem>
                                            <asp:ListItem>B-</asp:ListItem>
                                            <asp:ListItem>C+</asp:ListItem>
                                            <asp:ListItem>C</asp:ListItem>
                                            <asp:ListItem>C-</asp:ListItem>
                                            <asp:ListItem>D+</asp:ListItem>
                                            <asp:ListItem>D</asp:ListItem>
                                            <asp:ListItem>D-</asp:ListItem>
                                            <asp:ListItem>E</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                    </div>
                    <div class="panel-footer">
                        <asp:Button CssClass="btn btn-primary" ID="BtnSave" runat="server" 
                            Text="Simpan" />
                    </div>
                </div>










                </asp:Panel>
            </div>
            <br />
        </div>
        <br />
    </div>
    </div>
</asp:Content>
