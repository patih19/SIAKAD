<%@ Page Title="" Language="C#" MasterPageFile="~/pasca/Pasca.Master" AutoEventWireup="true" CodeBehind="krs.aspx.cs" Inherits="Padu.pasca.krs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-align: right;
            width: 42px;
        }
    </style>
<%--    <style type="text/css">
        .style2 {
            color: #FF3300;
        }

        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }

        .mdl {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.5;
            filter: alpha(opacity=50);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 115px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 95px;
                width: 95px;
            }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="content-header">
        <h1>KRS</h1>
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
                            <strong>Kartu Rencana Studi (KRS)</strong></div>
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
                                        <asp:Button ID="BtnKRS" runat="server" OnClick="BtnKRS_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                <asp:Panel ID="PanelKRS" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PanelBdk" runat="server" CssClass="form-control" BackColor="#FFFF99">
                                    <strong>Jumlah maksimal SKS :</strong>
                                    <asp:Label ID="LbMaxSKS" runat="server" Text="" style="font-weight: 700"></asp:Label>
                                </asp:Panel>
                                <p></p>
                                <strong>Jumlah SKS dipilih :</strong>
                                <asp:Label ID="LbJumlahSKS" runat="server" style="font-weight: 700"></asp:Label>
                                <br />
                                <asp:GridView ID="GVAmbilKRS" runat="server" CellPadding="4" GridLines="Horizontal" CssClass="table table-striped"
                                    onrowdatabound="GVAmbilKRS_RowDataBound" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CBMakul" runat="server" AutoPostBack="true" OnCheckedChanged="CBMakul_CheckedChanged" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Pilih
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="LbSisa" runat="server" style="font-weight: 700"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Sisa
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePnlSimpanKRS" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="BtnSimpan" runat="server" Text="Simpan" OnClick="BtnSimpan_Click"
                                    class="btn btn-default" OnClientClick="return confirm('Anda Yakin Data Tersebut Benar ?');"
                                    CssClass="btn btn-primary" />
                                <asp:Label ID="LbPostSuccess" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                <br />
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div class="mdl">
                                <div class="center">
                                    <img alt="" src="../images/loading135.gif" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
            </div>
        </div>
    </div>
</asp:Content>