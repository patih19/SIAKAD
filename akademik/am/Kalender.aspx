<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="Kalender.aspx.cs" Inherits="akademik.am.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            color: #FF3300;
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
    <div class="row">
        <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
            <div>
                <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </ajaxToolkit:ToolkitScriptManager>
                <asp:Panel ID="PnlAddKeg1" runat="server">
                    &nbsp;<table class="table-bordered table-condensed">
                        <tr>
                            <td colspan="2" style="background-color: #FFCD58">
                                Input Kegiatan Semester Gasal
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenis Kegiatan
                            </td>
                            <td>
                                <asp:DropDownList ID="DLJenisKeg" runat="server" CssClass="form-control">
                                    <asp:ListItem>Jenis Kegiatan</asp:ListItem>
                                    <asp:ListItem Value="PMB">Masa Penerimaan Mahasiswa Baru</asp:ListItem>
                                    <asp:ListItem Value="KRSMABA">Masa Pengisian KRS Mahasiswa Baru</asp:ListItem>
                                    <asp:ListItem Value="KRSNONMABA">Masa Pengisian KRS Mahasiswa Lama</asp:ListItem>
                                    <asp:ListItem Value="BatalTambah">Masa Batal Tambah Makul</asp:ListItem>
                                    <asp:ListItem Value="UTS">Masa Ujian Tengah Semester</asp:ListItem>
                                    <asp:ListItem Value="UAS">Masa Ujian Akhir Semester</asp:ListItem>
                                    <asp:ListItem Value="Nilai">Masa Input Nilai</asp:ListItem>
                                    <asp:ListItem Value="KHS">Masa Penerbitan KHS</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Nama Kegiatan
                            </td>
                            <td>
                                <asp:TextBox ID="TbKeg" runat="server" CssClass="form-control" MaxLength="90"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal Mulai
                            </td>
                            <td>
                                <asp:TextBox ID="TbMulai" runat="server" CssClass="form-control" MaxLength="10" ReadOnly="True"
                                    Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TbMulai"
                                    Format="yyyy-MM-dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal Selesai
                            </td>
                            <td>
                                <asp:TextBox ID="TbSelesai" runat="server" CssClass="form-control" MaxLength="10"
                                    ReadOnly="True" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TbSelesai"
                                    Format="yyyy-MM-dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenjang</td>
                            <td>
                                <asp:DropDownList ID="DLJenjang" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">jenjang</asp:ListItem>
                                    <asp:ListItem>S1</asp:ListItem>
                                    <asp:ListItem>S2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Tahun </td>
                            <td>
                                <asp:TextBox ID="TbTahun" runat="server" CssClass="form-control" ReadOnly="true" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="BtnOK" runat="server" Text="OK" OnClick="BtnOK_Click" />
                                &nbsp;<asp:Button ID="BtnCancel" runat="server" Text="Cancel" />
                                &nbsp;<span class="style21"><em>( Batalkan jika TAHUN tidak sesuai ...)</em></span>
                            </td>
                        </tr>
                    </table>
                    <hr />
                </asp:Panel>
                <asp:Panel ID="PnlAddKeg2" runat="server">
                    <br />
                    <table class="table-bordered table-condensed">
                        <tr>
                            <td colspan="2" style="background-color: #FFCD58">
                                Input Kegiatan Semester Genap
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenis Kegiatan
                            </td>
                            <td>
                                <asp:DropDownList ID="DLJenisKeg2" runat="server" CssClass="form-control">
                                    <asp:ListItem>Jenis Kegiatan</asp:ListItem>
                                    <asp:ListItem Value="PenawaranMakul">Penawaran Mata Kuliah</asp:ListItem>
                                    <asp:ListItem Value="KRSMABA">Masa Pengisian KRS Mahasiswa Baru</asp:ListItem>
                                    <asp:ListItem Value="KRSNONMABA">Masa Pengisian KRS Mahasiswa Lama</asp:ListItem>
                                    <asp:ListItem Value="BatalTambah">Masa Batal Tambah Makul</asp:ListItem>
                                    <asp:ListItem Value="UTS">Masa Ujian Tengah Semester</asp:ListItem>
                                    <asp:ListItem Value="UAS">Masa Ujian Akhir Semester</asp:ListItem>
                                    <asp:ListItem Value="Nilai">Masa Input Nilai</asp:ListItem>
                                    <asp:ListItem Value="KHS">Masa Penerbitan KHS</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Nama Kegiatan
                            </td>
                            <td>
                                <asp:TextBox ID="TbKeg2" runat="server" CssClass="form-control" MaxLength="90"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal Mulai
                            </td>
                            <td>
                                <asp:TextBox ID="TbMulai2" runat="server" CssClass="form-control" MaxLength="10"
                                    ReadOnly="True" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TbMulai2"
                                    Format="yyyy-MM-dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal Selesai
                            </td>
                            <td>
                                <asp:TextBox ID="TbSelesai2" runat="server" CssClass="form-control" Width="100px"
                                    ReadOnly="True" MaxLength="10"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="yyyy-MM-dd"
                                    TargetControlID="TbSelesai2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenjang</td>
                            <td>
                                <asp:DropDownList ID="DLJenjang2" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">jenjang</asp:ListItem>
                                    <asp:ListItem>S1</asp:ListItem>
                                    <asp:ListItem>S2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Tahun </td>
                            <td>
                                <asp:TextBox ID="TbTahun2" runat="server" CssClass="form-control" ReadOnly="true" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="BtnOKGenap" runat="server" Text="OK" OnClick="BtnOKGenap_Click" />
                                &nbsp;<span class="style21"><em>( Batalkan jika TAHUN tidak sesuai ...)</em></span>
                            </td>
                        </tr>
                    </table>
                    <hr />
                </asp:Panel>
                <asp:Panel ID="PnlNewKalender" runat="server">
                    <span class="style4"><em>Warning : Input dapat dilakukan jika jadwal kegiatan akademik
                        paling akhir pada tahun sebelumnya sudah berakhir !!</em></span><br />
                    <table class="table-bordered table-condensed">
                        <tr>
                            <td colspan="2" style="background-color: #FFCD58">
                                Input Kalendar Akademik Baru
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tahun
                            </td>
                            <td>
                                <asp:TextBox ID="TbNewTahun" runat="server" CssClass="form-control" MaxLength="4"
                                    Width="70px"></asp:TextBox>
                                &nbsp;<strong><em><span class="style21">(contoh : 2014)</span></em></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;<asp:Button ID="BtnOkNewYear" runat="server" OnClick="Button4_Click" 
                                    Text="OK" />
                                &nbsp;<asp:Button ID="BtnBatal" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <hr />
                </asp:Panel>
                <asp:Panel ID="PnlEditKegiatan" runat="server">
                    <table class="table-bordered table-condensed">
                        <tr>
                            <td colspan="2" style="background-color: #FFCD58">
                                Ubah Kegiatan Akademik
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenis Kegiatan
                            </td>
                            <td>
                                <asp:DropDownList ID="DLJenisKeg3" runat="server" CssClass="form-control">
                                    <asp:ListItem>Jenis Kegiatan</asp:ListItem>
                                    <asp:ListItem Value="PenawaranMakul">Penawaran Mata Kuliah</asp:ListItem>
                                    <asp:ListItem Value="KRSMABA">Masa Pengisian KRS Mahasiswa Baru</asp:ListItem>
                                    <asp:ListItem Value="KRSNONMABA">Masa Pengisian KRS Mahasiswa Lama</asp:ListItem>
                                    <asp:ListItem Value="BatalTambah">Masa Batal Tambah Makul</asp:ListItem>
                                    <asp:ListItem Value="UTS">Masa Ujian Tengah Semester</asp:ListItem>
                                    <asp:ListItem Value="UAS">Masa Ujian Akhir Semester</asp:ListItem>
                                    <asp:ListItem Value="Nilai">Masa Input Nilai</asp:ListItem>
                                    <asp:ListItem Value="KHS">Masa Penerbitan KHS</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Nama Kegiatan
                            </td>
                            <td>
                                <asp:TextBox ID="TbKeg3" runat="server" CssClass="form-control" MaxLength="90"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal Mulai
                            </td>
                            <td>
                                <asp:TextBox ID="TbMulai3" runat="server" CssClass="form-control" MaxLength="10"
                                    ReadOnly="True" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TbMulai3"
                                    Format="yyyy-MM-dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal Selesai
                            </td>
                            <td>
                                <asp:TextBox ID="TbSelesai3" runat="server" CssClass="form-control" Width="100px"
                                    ReadOnly="True" MaxLength="10"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="yyyy-MM-dd"
                                    TargetControlID="TbSelesai3">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenjang</td>
                            <td>
                                <asp:Label ID="LbJenjang" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LbNoKeg" runat="server" BackColor="Transparent" ForeColor="Transparent"></asp:Label>
                                &nbsp; </td>
                            <td>
                                <asp:Button ID="BtnSvEdit" runat="server" OnClick="Button3_Click" Text="OK" />
                                &nbsp;<asp:Button ID="Button2" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <hr />
                </asp:Panel>
                <asp:Label ID="Label1" runat="server" style="font-weight: 700" 
                    Text="KALENDER AKADEMIK"></asp:Label>
                <br />
                <br />
                <table class="table-bordered table-condensed">
                    <tr style="background-color: rgba(21, 167, 21, 0.59);">
                        <td>
                            <strong>Jadwal Kegiatan Semester Gasal</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GVGasal" runat="server" CellPadding="4" CssClass="table-condensed table-bordered"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVGasal_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Add
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btn_del" runat="server" Text="Del"  OnClientClick="return confirm('Anda Yakin Jadwal Dihapus ?');" OnClick="Btn_del_Click" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Hapus
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Ubah
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEditKeg" runat="server" OnClick="BtnEditKeg_Click" Text="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <asp:Panel ID="PnlNewGasal" runat="server">
                                <asp:Button ID="BtNewGasal" runat="server" Text="Add" OnClick="BtNewGasal_Click" />
                                <span class="style21"><em>&nbsp;Buat Kegiatan Baru</em></span></asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="background-color: rgba(21, 167, 21, 0.59);">
                        <td>
                            <strong>Jadwal Kegiatan Semester Genap</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GVGenap" runat="server" CellPadding="4" CssClass="table-condensed table-bordered"
                                ForeColor="#333333" GridLines="None" OnRowDataBound="GVGenap_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click1" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Add
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btn_del" runat="server" Text="Del"  OnClientClick="return confirm('Anda Yakin Jadwal Dihapus ?');" OnClick="Btn_del_Click1" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Hapus
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Ubah
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEditKeg2" runat="server" OnClick="BtnEditKeg2_Click" Text="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <asp:Panel ID="PnlNewGenap" runat="server">
                                <asp:Button ID="BtNewGenap" runat="server" Text="Add" OnClick="BtNewGenap_Click" />
                                &nbsp;<span class="style21"><em>Buat Kegiatan Baru</em></span></asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="background-color: #CCCCFF">
                        <td>
                            <strong>Buat Tahun Kalender Baru</strong>
                            <asp:Button ID="BtnNewKal" runat="server" Text="Create" OnClick="BtnNewKal_Click" />
                            &nbsp;<br />
                        </td>
                    </tr>
                </table>
                <br />
                <span class="gr"><strong>CATATAN :</strong><br />
                Pada Awal Semester 
                Harus Didefinisikan :
                <br />
                1. Jadwal KRS Mahasiswa Baru
                <br />
                2. Jadwal KRS Mahasiswa Lama
                <br/>
                3. Jadwal Batal Tambah KRS
                <br />
                4. Jadwal Input Nilai </span>
            </div>
            <br />
        </div>
        <br />
    </div>
    </div>
</asp:Content>
