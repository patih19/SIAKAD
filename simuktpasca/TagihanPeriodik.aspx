<%@ Page Title="" Language="C#" MasterPageFile="~/Pasca.Master" AutoEventWireup="true" CodeBehind="TagihanPeriodik.aspx.cs" Inherits="simuktpasca.TagihanPeriodik" %>
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
                                        <td>SPP</td>
                                         <td>
                                             <asp:TextBox ID="TbEditSPP" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SARDIK</td>
                                         <td>
                                             <asp:TextBox ID="TbEditSARDIK" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MATRIKULASI</td>
                                         <td>
                                             <asp:TextBox ID="TbEditMATRIK" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SPI</td>
                                         <td>
                                             <asp:TextBox ID="TbEditSPI" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
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
                                        <td>SPP</td>
                                         <td>
                                             <asp:TextBox ID="TbAddSPP" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SARDIK</td>
                                         <td>
                                             <asp:TextBox ID="TbAddSardik" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MATRIKULASI</td>
                                         <td>
                                             <asp:TextBox ID="TbAddMatrik" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SPI</td>
                                         <td>
                                             <asp:TextBox ID="tbAddSPI" CssClass=" from form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TAHUN ANGKATAN</td>
                                         <td>
                                             <asp:TextBox ID="TbThnAngkatan" Placeholder=".... / ...." CssClass=" from form-control" runat="server"></asp:TextBox> <div style="color: #999999; font-size: small; font-style: italic;">contoh = 2017/2018</div> 
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
                            <h3>Tagihan Periodik Per Program Studi
                            </h3>
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
                                                    <th>SPP
                                                    </th>
                                                    <th>SARDIK
                                                    </th>
                                                    <th>MATRIKULASI
                                                    </th>
                                                    <th>SPI
                                                    </th>
                                                    <th>TAHUN ANGKATAN
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
                                                <asp:Label ID="lbNoDaftar" runat="server" Text='<%# Eval("SPP") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="LbNama" runat="server" Text='<%# Eval("SARDIK") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="LbSekolah" runat="server" Text='<%# Eval("MATRIKULASI") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="LbProvinsi" runat="server" Text='<%# Eval("SPI") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("TAHUN ANGKATAN") %>' />
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
        </div> <!-- /.row -->
    </div>

</asp:Content>
