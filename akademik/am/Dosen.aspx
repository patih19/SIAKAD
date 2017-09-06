<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Dosen.aspx.cs" Inherits="akademik.am.WebForm27" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Modal Pop Up -->
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width:500px;
            overflow:auto;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        .style4
        {
            background-color: #C6DFFF;
        }
        .style5
        {
            background-color: #C6DFFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>DAFTAR DOSEN</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td class="style5">
                                        <strong>Pilih Program Studi</strong></td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style4" colspan="3">
                                        <strong>Cari Dosen</strong></td>
                                        <td></td>
                                        <td class="style4"><strong>Tambah Dosen</strong></td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control" AutoPostBack="True"
                                            OnSelectedIndexChanged="DLProdi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLCari" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="-1">Jenis Pencarian</asp:ListItem>
                                            <asp:ListItem>Nama</asp:ListItem>
                                            <asp:ListItem>NIDN</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbSearch" runat="server" CssClass="form-control" Width="200px" Placeholder="Minimal 4 huruf atau Angka"></asp:TextBox>
                                    </td>
                                    <td class="style4">
                                        <asp:Button ID="BtnCari" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="BtnCari_Click" />
                                    </td>
                                    <td></td>
                                    <td class="style4">
                                        <asp:Button ID="BtnTbhDosen" CssClass="btn btn-danger" runat="server" Text="Add"
                                            OnClick="BtnTbhDosen_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="PanelListDosen" runat="server">
                    <hr />
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <asp:GridView ID="GVDosen" CssClass="table-condensed table-bordered" runat="server"
                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="GVDosen_RowCreated1">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="AKTIF">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEdit" runat="server" CssClass="btn btn-success" Text="Edit" OnClick="BtnEdit_Click" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            EDIT
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CBAktif" runat="server" AutoPostBack="True" OnCheckedChanged="CBAktif_CheckedChanged" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            AKTIF
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <!-- -------- modal Add dosen-------- -->
                <asp:LinkButton ID="lnkFake" runat="server" ForeColor="#F2F2FF"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderAdd" runat="server" PopupControlID="pnlPopup"
                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="BtnClose">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlPopup" runat="server" > <!-- Style="display: none" -->
                    <!-- Style="display: none" -->
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <a id="BtnClose" href="#"><span class="glyphicon glyphicon-eye-close"></span>Close
                            </a>
                        </div>
                        <div class="panel-body">
                            <table class="table-condensed" style="background-color: AliceBlue">
                                <tr>
                                    <td colspan="2" style="background-color: #C6DFFF">
                                        <strong>TAMBAH DOSEN</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        NIDN
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbNIDN" runat="server" CssClass="form-control" MaxLength="10" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nama
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbNama" placeholder="Nama Lengkap Dengan Gelar" runat="server" CssClass="form-control"
                                            MaxLength="75"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Program Studi
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLProdiDosen" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Jenis Kelamin
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLGender" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="-1">Jenis Kelamin</asp:ListItem>
                                            <asp:ListItem>Laki-laki</asp:ListItem>
                                            <asp:ListItem>Perempuan</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Agama
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLAgama" runat="server" CssClass="form-control">
                                            <asp:ListItem>Agama</asp:ListItem>
                                            <asp:ListItem>Islam</asp:ListItem>
                                            <asp:ListItem>Protestan</asp:ListItem>
                                            <asp:ListItem>Katholik</asp:ListItem>
                                            <asp:ListItem>Hindu</asp:ListItem>
                                            <asp:ListItem>Budha</asp:ListItem>
                                            <asp:ListItem>Konghucu</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tgl. Lahir
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TBTglLahir" runat="server" placeholder="ex: 1990-02-24" CssClass="form-control"
                                            MaxLength="10"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TBTglLahir"
                                            Format="yyyy-MM-dd">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Alamat Rumah
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbAlamat" runat="server" CssClass="form-control" MaxLength="200"
                                            placeholder="Alamat Lengkap" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        No Handphone
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TBHp" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbEmail" placeholder="ex: andi@gmail.com" runat="server" MaxLength="50"
                                            Width="180px" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="panel-footer">
                            <asp:Button ID="BtnTambahDosen" runat="server" CssClass="btn btn-primary" OnClick="BtnTambahDosen_Click"
                                Text="Simpan" />
                            &nbsp;<asp:Label ID="LbNPM" runat="server" ForeColor="WhiteSmoke"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
                <!--  --------- End Add dosen ---------  -->

                <!-- -------- modal Edit dosen-------- -->
                <asp:LinkButton ID="lnkFake2" runat="server" ForeColor="#F2F2FF"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderEdit" runat="server" PopupControlID="PanelEdit"
                    TargetControlID="lnkFake2" BackgroundCssClass="modalBackground" CancelControlID="BtnClose2">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="PanelEdit" runat="server" Style="display: none">
                    <!-- Style="display: none" -->
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <a id="BtnClose2" href="#"><span class="glyphicon glyphicon-eye-close"></span>Close
                            </a>
                        </div>
                        <div class="panel-body">
                            <table class="table-condensed" style="background-color: #FEE6B4">
                                <tr>
                                    <td colspan="2" style="background-color: #FFCC66">
                                        <strong>EDIT BIODATA DOSEN</strong>
                                        <asp:Label ID="LbNd" runat="server" BackColor="Transparent" CssClass="hidden" 
                                            ForeColor="Transparent"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nama
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbEditNama" placeholder="Nama Lengkap Dengan Gelar" runat="server" CssClass="form-control"
                                            MaxLength="75"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Program Studi
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLEditProdi" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Jenis Kelamin
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLEditGender" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="gender">Jenis Kelamin</asp:ListItem>
                                            <asp:ListItem>Laki-laki</asp:ListItem>
                                            <asp:ListItem>Perempuan</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Agama
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLEditAgama" runat="server" CssClass="form-control">
                                            <asp:ListItem Value ="-1">Agama</asp:ListItem>
                                            <asp:ListItem>Islam</asp:ListItem>
                                            <asp:ListItem>Protestan</asp:ListItem>
                                            <asp:ListItem>Katholik</asp:ListItem>
                                            <asp:ListItem>Hindu</asp:ListItem>
                                            <asp:ListItem>Budha</asp:ListItem>
                                            <asp:ListItem>Konghucu</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tgl. Lahir
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TBEditTgLahir" runat="server" placeholder="ex: 1990-02-24" CssClass="form-control"
                                            MaxLength="10"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TBEditTgLahir"
                                            Format="yyyy-MM-dd">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Alamat Rumah
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbEditAlamat" runat="server" CssClass="form-control" MaxLength="200"
                                            placeholder="Alamat Lengkap" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        No Handphone
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbEditHp" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbEditEmail" runat="server" CssClass="form-control" 
                                            MaxLength="50" placeholder="ex: andi@gmail.com" Width="180px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="panel-footer">
                            <asp:Button ID="BtnSaveEdit" runat="server" CssClass="btn btn-primary" 
                                Text="Simpan" onclick="BtnSaveEdit_Click" />
                        </div>
                    </div>
                </asp:Panel>
                 <!--  --------- End Edit dosen ---------  -->
            </div>
    </div>
    </div>
</asp:Content>
