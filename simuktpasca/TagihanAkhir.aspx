<%@ Page Title="" Language="C#" MasterPageFile="~/Pasca.Master" AutoEventWireup="true" CodeBehind="TagihanAkhir.aspx.cs" Inherits="simuktpasca.TagihanAkhir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Item
        {
            height: 150px;
            width: 180px;
            margin:5px;
        }
        .Item, .Item td, .Item td
        {
            border: 1px solid #ccc;
        }
        .Item .header
        {
            background-color:#F7F7F7 !important;
            color: Black;
            font-size: 10pt;
            line-height: 200%;
        }
        .page_enabled, .page_disabled
        {
            display: inline-block;
            height: 30px;
            min-width: 30px;
            line-height: 30px;
            text-align: center;
            text-decoration: none;
            border: 1px solid #ccc;
            padding-left: 10px;
            padding-right: 10px;
        }
        .page_enabled
        {
            background-color: #eee;
            color: #000;
        }
        .page_disabled
        {
            background-color: #6C6C6C;
            color: #fff !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content-header">
        <h1>Master Tagihan</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet">
                    <asp:Panel ID="PanelMessage" runat="server">
                        <div class="alert alert-info" role="alert">
                            <asp:Label ID="LbMessage" runat="server" Text=""></asp:Label>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelEditTagihan" runat="server">
                        <div class="portlet-header">
                            <h3>EDIT TAGIHAN
                            </h3>
                        </div>
                        <div class="portlet-content">
                            <table class=" table-bordered table-condensed table-striped table-hover">
                                <thead>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>TOEFL</td>
                                        <td>
                                            <asp:TextBox ID="TbEditToefl" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>WISUDA</td>
                                        <td>
                                            <asp:TextBox ID="TbEditWisuda" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BtnCancel" CssClass="btn btn-danger" runat="server" Text="Batal" OnClick="BtnCancel_Click" />
                                            &nbsp;<asp:Button ID="BtnUpdate" runat="server" CssClass="btn btn-success" OnClick="BtnUpdate_Click" Text="Update" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelAddTagihan" runat="server">
                        <div class="portlet-header">
                            <h3>BUAT TAGIHAN
                            </h3>
                        </div>
                        <div class="portlet-content">
                            <table class=" table-bordered table-condensed table-striped table-hover">
                                <thead>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>TOEFL</td>
                                        <td>
                                            <asp:TextBox ID="TbAddToefl" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>WISUDA</td>
                                        <td>
                                            <asp:TextBox ID="TbAddWisuda" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SEMESTER</td>
                                        <td>
                                            <asp:TextBox ID="TbSemester" Placeholder="Contoh : 20171"  CssClass=" from form-control"  runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BtnBatal" CssClass="btn btn-danger" runat="server" Text="Batal" OnClick="BtnBatal_Click" />
                                            &nbsp;<asp:Button ID="BtnSimpan" runat="server" CssClass="btn btn-success" OnClick="BtnSimpan_Click" Text="Simpan" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelListTagihan" runat="server">
                        <div class="portlet-header">
                            <h3>Tagihan Akhir Per Semester</h3>
                        </div>
                        <div class="portlet-content">
                            <table class="table-condensed table-striped table-hover">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="DLTagihanProdi" CssClass="from form-control" runat="server"></asp:DropDownList></td>
                                    <td>
                                        <asp:Button ID="BtnOpenTagihan" runat="server" Text="Submit" OnClick="BtnOpenTagihan_Click" /></td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PanelContentTagihan" runat="server">
                            <br />
                            <!-- /.portlet-header -->
                            <div class="portlet-content">
                                <asp:Button ID="BtnAdd" CssClass="btn btn-danger" runat="server" Text="Buat Tagihan" OnClick="BtnAdd_Click" />
                                <br />
                                <br />
                                <asp:Repeater ID="RptTagihanPeriodik" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered table-striped table-hover" style="page-break-before: always"
                                            id="TblQuota">
                                            <thead>
                                                <tr style="background-color: #a3c4f7;">
                                                    <th>No.
                                                    </th>
                                                    <th>TOEFL
                                                    </th>
                                                    <th>WISUDA
                                                    </th>
                                                    <th>SEMESTER
                                                    </th>
                                                    <th>EDIT
                                                    </th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="page-break-inside: avoid !important">
                                            <td>
                                                <asp:Label ID="LbNomor" runat="server" Text='<%# Eval("No") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbNoDaftar" runat="server" Text='<%# Eval("TOEFL") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="LbNama" runat="server" Text='<%# Eval("WISUDA") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnEdit" OnClick="BtnEdit_Click" runat="server" Text="Edit"
                                                    CommandArgument='<%# Eval("ID BIAYA") + "," + Eval("ID PRODI") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="Lbpage" runat="server" CssClass="hidden" Text=""></asp:Label>
                            </div>
                            <!-- /.portlet-content -->
                        </asp:Panel>
                    </asp:Panel>

                </div>
                <!-- /.portlet -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
</asp:Content>
