<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="PermohonanCuti.aspx.cs" Inherits="Portal.PermohonanCuti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .bs-example{
    	margin: 20px;

    }
    .panel .panel-title{
        font-size: 13px;
        font-weight: 400;
        line-height: 3px;
        display: block;
        float: left;
        color: #434a54;
    }
        .auto-style1 {
            color: #FF3300;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <asp:Panel ID="PanelDaftarPersetujuan" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Permohonan Cuti Kuliah</strong>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="GVCuti" CssClass="table table-condensed" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Persetujuan
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnAction" runat="server" Text="open" OnClick="BtnAction_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelPersetujuan" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Persetujuan Cuti Kuliah</strong>
                        </div>
                        <div class="panel-body">

                                <div class="panel-group" id="accordion">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h5 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">1. History Perkuliahan</a>
                                            </h5>
                                        </div>
                                        <div id="collapseOne" class="panel-collapse collapse in">
                                            <div class="panel-body">
                                                <asp:GridView ID="GvStatusKuliah" CssClass="table table-condensed" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h5 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">2. Upload Surat Izin Cuti Kuliah</a>
                                            </h5>
                                        </div>
                                        <div id="collapseTwo" class="panel-collapse collapse in">
                                            <div class="panel-body">
                                                Nama file maksimal 35 huruf <br />
                                                Format file *jpg *jpeg *bmp *png <br />
                                                Ukuran file 200 Kb - 450 Kb <br />
                                                <br />
                                                <p><asp:FileUpload  ID="FPBerkas" runat="server" /></p> 
                                                <p><asp:Button ID="BtnUpload" CssClass="btn btn-danger" runat="server" Text="upload" OnClick="BtnUpload_Click" />&nbsp;<asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                </p> 
                                                <p><asp:Image ID="ImageSuratCuti" runat="server" /></p>
                                                

                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="PanelIzinCuti" runat="server">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree">3. Persetujuan Cuti Kuliah</a>
                                                </h4>
                                            </div>
                                            <div id="collapseThree" class="panel-collapse collapse in ">
                                                <div class="panel-body">
                                                    INFORMASI PENTING !
                                                <p class="auto-style1">Persetujuan Cuti Hanya 1x & Tidak Dapat Diganti</p>
                                                    <asp:RadioButton ID="RbSetuju" runat="server" GroupName="GrbRbCuti" Text="Dizinkan" />&nbsp;<asp:RadioButton ID="RbTolak" runat="server" GroupName="GrbRbCuti" Text="Ditolak" /><br />
                                                    <asp:Button ID="BtnPersetujuan" CssClass="btn btn-success" runat="server" Text="simpan" OnClientClick="return confirm('Anda Yakin Pilihan Persetujuan SUDAH BENAR ?');" OnClick="BtnPersetujuan_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <%--<p><strong>Note:</strong> Click on the linked heading text to expand or collapse accordion panels.</p>  --%>                        
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
