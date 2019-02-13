<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="MhsAktif.aspx.cs" Inherits="Portal.WebForm18" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
            background-color: #FFFFFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Filter Data Mahasiswa AKTIF</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td class="style4">
                                        <strong>Program Studi</strong>
                                    </td>
                                    <td class="style5">
                                    </td>
                                    <td class="style4">
                                        <strong>Angkatan</strong>
                                    </td>
                                    <td class="style4">
                                        <strong>Gender</strong>
                                    </td>
                                    <td class="style4">
                                        <strong>Agama</strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style4">
                                        <strong>Sebagian Nama / NPM</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style5">
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbAngkatan" runat="server" CssClass="form-control" MaxLength="9"
                                            Placeholder="ex: 2015/2016" AutoPostBack="True" OnTextChanged="TbAngkatan_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList CssClass="form-control" ID="DLSex" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="DLSex_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Gender</asp:ListItem>
                                            <asp:ListItem>Laki-laki</asp:ListItem>
                                            <asp:ListItem>Perempuan</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList CssClass="form-control" ID="DLReligi" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="DLReligi_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Agama</asp:ListItem>
                                            <asp:ListItem>Islam</asp:ListItem>
                                            <asp:ListItem>Katholik</asp:ListItem>
                                            <asp:ListItem>Protestan</asp:ListItem>
                                            <asp:ListItem>Hindu</asp:ListItem>
                                            <asp:ListItem>Budha</asp:ListItem>
                                            <asp:ListItem>Konghucu</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox CssClass="form-control" MaxLength="20" ID="TbSrcNama" runat="server"
                                            Placeholder="Min 4 huruf" AutoPostBack="True" OnTextChanged="TbSrcNama_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="LbKet" runat="server" CssClass="hidden"></asp:Label>
                        </div>
                        <div class="panel-footer">
                            <em>pilih program studi dan salah satu jenis pencarian atau ketik sebagian nama atau
                            NPM</em></div>
                    </div>
                </div>
                <hr />
                <asp:Label ID="LbKetSelect"
                    runat="server" CssClass="hidden"></asp:Label>
                <div class="table table-responsive" >
                    <asp:GridView ID="GVMhs" runat="server" CellPadding="4"
                        ForeColor="#333333" GridLines="None"
                        CssClass="table-condensed table-bordered" PageSize="100"
                        OnRowCreated="GVMhs_RowCreated">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    BIODATA
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="BtnLihat" runat="server" CssClass="btn btn-success" Text="Lihat" OnClick="BtnLihat_Click" />
                                </ItemTemplate>
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
                <asp:Repeater ID="Repeater1" runat="server" EnableTheming="True">
                    <ItemTemplate>
                        <asp:LinkButton ID="PageButton" runat="server" Text='<%#Eval("Text")%>' CommandArgument='<%#Eval("Value")%>'
                            Enabled='<%#Eval("Enabled")%>' OnClick="PageButton_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
            </div>
            <br />
            <!-- -------- latihan modal -------- -->
             <asp:LinkButton ID="lnkFake" runat="server" ForeColor="#F2F2FF"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                PopupControlID="pnlPopup" TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="BtnClose">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Vertical" Height="98%" Style="display: none"> <!-- Style="display: none" -->
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <a id="BtnClose" href="#"><span class="glyphicon glyphicon-eye-close"></span>
                        Close </a>
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed" style="background-color:AliceBlue">
                            <tr>
                                <td colspan="2" style="background-color: #C6DFFF">
                                    <strong>IDENTITAS</strong></td>
                            </tr>
                            <tr>
                                <td>
                                    Nama
                                </td>
                                <td>
                                    <asp:TextBox ID="TbNama" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tempat Lahir
                                </td>
                                <td>
                                    <asp:TextBox ID="TbTmpLhir" placeholder="kota/kabupaten" runat="server" CssClass="form-control"  MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tanggal Lahir
                                </td>
                                <td>
                                    <asp:TextBox ID="TBTtl" runat="server" placeholder="ex: 1990-02-24" CssClass="form-control"  MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TBTtl"
                                        Format="yyyy-MM-dd">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Jenis Kelamin
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLGender" runat="server"  CssClass="form-control">
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
                                    <asp:DropDownList ID="DLAgama" runat="server" CssClass="form-control" >
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
                                    Golongan Darah
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLDarah" runat="server" CssClass="form-control" >
                                        <asp:ListItem Value="darah">Golongan Darah</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>AB</asp:ListItem>
                                        <asp:ListItem>B</asp:ListItem>
                                        <asp:ListItem>O</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Provinsi
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListProv" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>
                                    <ajaxToolkit:CascadingDropDown ID="CascadingDropDownProv" runat="server" Category="id_provinsi"
                                        TargetControlID="DropDownListProv" ServicePath="~/web_services/ServiceCS.asmx"
                                        ServiceMethod="Prov" PromptText="PROVINSI" >
                                    </ajaxToolkit:CascadingDropDown>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Kota/Kabupaten
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListKab" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>
                                    <ajaxToolkit:CascadingDropDown ID="CascadingDropDownKotakab" runat="server" TargetControlID="DropDownListKab"
                                        ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Kotakab" ParentControlID="DropDownListProv"
                                        LoadingText="LOADING" PromptText="KOTA/KABUPATEN" Category="id_kotakab">
                                    </ajaxToolkit:CascadingDropDown>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Kecamatan
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListKec" runat="server" CssClass="form-control" >
                                    </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDownKec" runat="server" TargetControlID="DropDownListKec"
                                    ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Kecamatan" ParentControlID="DropDownListKab"
                                    LoadingText="LOADING" PromptText="KECAMATAN" Category="id_kecamatan">
                                </ajaxToolkit:CascadingDropDown>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Alamat Rumah
                                </td>
                                <td>
                                    <asp:TextBox ID="TbAlamat" placeholder="Alamat Lengkap" runat="server" MaxLength="110"
                                        TextMode="MultiLine" Width="300px" CssClass="form-control" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Kode POS
                                </td>
                                <td>
                                    <asp:TextBox ID="TbKdPOS" runat="server" MaxLength="5" CssClass="form-control" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No Handphone
                                </td>
                                <td>
                                    <asp:TextBox ID="TBHp" runat="server" MaxLength="12" CssClass="form-control" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Email
                                </td>
                                <td>
                                    <asp:TextBox ID="TbEmail" placeholder="ex: andi@gmail.com" runat="server" MaxLength="100"
                                       CssClass="form-control" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" colspan="2" style="background-color: #C6DFFF">
                                    <strong>PENDIDIKAN SMA/SMK/MA </strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nama Sekolah
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSekolah" placeholder="ex: SMK Negeri 1 Magelang" runat="server"
                                        MaxLength="40" Width="300px" CssClass="form-control" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Jurusan
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLJurusan" runat="server" CssClass="form-control" >
                                        <asp:ListItem>Jurusan</asp:ListItem>
                                        <asp:ListItem>IPA</asp:ListItem>
                                        <asp:ListItem>IPS</asp:ListItem>
                                        <asp:ListItem>Bahasa</asp:ListItem>
                                        <asp:ListItem>----------</asp:ListItem>
                                        <asp:ListItem>Bangunan</asp:ListItem>
                                        <asp:ListItem>Elektro</asp:ListItem>
                                        <asp:ListItem>TKJ</asp:ListItem>
                                        <asp:ListItem>Rekayasa Perangkat Lunak</asp:ListItem>
                                        <asp:ListItem>Listrik</asp:ListItem>
                                        <asp:ListItem>Mesin</asp:ListItem>
                                        <asp:ListItem>Otomotif</asp:ListItem>
                                        <asp:ListItem>Akuntansi</asp:ListItem>
                                        <asp:ListItem>Administrasi Perkantoran</asp:ListItem>
                                        <asp:ListItem>Pemasaran</asp:ListItem>
                                        <asp:ListItem>Tata Boga</asp:ListItem>
                                        <asp:ListItem>Perhotelan</asp:ListItem>
                                        <asp:ListItem>Kecantikan</asp:ListItem>
                                        <asp:ListItem>Tata Busana</asp:ListItem>
                                        <asp:ListItem>Lain-lain</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status Sekolah
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLStatusSekolah" runat="server" CssClass="form-control" >
                                        <asp:ListItem>Status</asp:ListItem>
                                        <asp:ListItem>Negeri</asp:ListItem>
                                        <asp:ListItem>Swasta</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tahun Lulus
                                </td>
                                <td>
                                    <asp:TextBox ID="TBThnLls" runat="server" MaxLength="4" Width="60px" CssClass="form-control" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" colspan="2" style="background-color: #C6DFFF" CssClass="form-control" >
                                    <strong>KELUARGA</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Jumlah Adik
                                </td>
                                <td>
                                    <asp:TextBox ID="TBAdik" runat="server" MaxLength="1" Width="60px" CssClass="form-control" ></asp:TextBox>
                                    &nbsp;<span >Jika tidak memiliki isikan 0</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Jumlah Kakak
                                </td>
                                <td>
                                    <asp:TextBox ID="TbKakak" runat="server" MaxLength="1" Width="60px" CssClass="form-control" ></asp:TextBox>
                                    &nbsp;<span >Jika tidak memiliki isikan 0</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nama Ayah
                                </td>
                                <td>
                                    <asp:TextBox ID="TbAyah" runat="server" Width="200px" CssClass="form-control" MaxLength="75"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nama Ibu
                                </td>
                                <td>
                                    <asp:TextBox ID="TbIbu" runat="server" MaxLength="70" Width="200px" CssClass="form-control" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pendidikan Ayah
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLPendidikanAyah" runat="server" CssClass="form-control" >
                                        <asp:ListItem>Pendidikan</asp:ListItem>
                                        <asp:ListItem>SD</asp:ListItem>
                                        <asp:ListItem>SMP</asp:ListItem>
                                        <asp:ListItem>SMA</asp:ListItem>
                                        <asp:ListItem>Diploma</asp:ListItem>
                                        <asp:ListItem>S1/D4</asp:ListItem>
                                        <asp:ListItem>S2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pendidikan Ibu
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLPendidikanIbu" runat="server" CssClass="form-control" >
                                        <asp:ListItem>Pendidikan</asp:ListItem>
                                        <asp:ListItem>SD</asp:ListItem>
                                        <asp:ListItem>SMP</asp:ListItem>
                                        <asp:ListItem>SMA</asp:ListItem>
                                        <asp:ListItem>Diploma</asp:ListItem>
                                        <asp:ListItem>S1/D4</asp:ListItem>
                                        <asp:ListItem>S2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pekerjaan Ayah
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLPekerjaanAyah" runat="server" CssClass="form-control" >
                                        <asp:ListItem>Pekerjaan</asp:ListItem>
                                        <asp:ListItem>Wiraswasta</asp:ListItem>
                                        <asp:ListItem>Buruh</asp:ListItem>
                                        <asp:ListItem>Petani</asp:ListItem>
                                        <asp:ListItem>Pedagang</asp:ListItem>
                                        <asp:ListItem>TNI/POLRI</asp:ListItem>
                                        <asp:ListItem>PNS</asp:ListItem>
                                        <asp:ListItem>Pensiunan</asp:ListItem>
                                        <asp:ListItem>Ibu Rumah Tangga</asp:ListItem>
                                        <asp:ListItem>Lain-lain</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pekerjaan Ibu
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLPekerjaanIbu" runat="server" CssClass="form-control" >
                                        <asp:ListItem>Pekerjaan</asp:ListItem>
                                        <asp:ListItem>Wiraswasta</asp:ListItem>
                                        <asp:ListItem>Buruh</asp:ListItem>
                                        <asp:ListItem>Petani</asp:ListItem>
                                        <asp:ListItem>Pedagang</asp:ListItem>
                                        <asp:ListItem>TNI/POLRI</asp:ListItem>
                                        <asp:ListItem>PNS</asp:ListItem>
                                        <asp:ListItem>Pensiunan</asp:ListItem>
                                        <asp:ListItem>Ibu Rumah Tangga</asp:ListItem>
                                        <asp:ListItem>Lain-lain</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Penghasilan Orang Tua
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLPenghasilan" runat="server" CssClass="form-control" >
                                        <asp:ListItem>Penghasilan</asp:ListItem>
                                        <asp:ListItem Value="Kurang dari Rp 1,5 juta">Kurang dari Rp 1,5 juta</asp:ListItem>
                                        <asp:ListItem>Rp 1,5 juta  - Rp 2,5 juta</asp:ListItem>
                                        <asp:ListItem>Rp 2,5 juta - Rp 3,5 juta</asp:ListItem>
                                        <asp:ListItem>Rp 3,5 juta - Rp 4,5 juta</asp:ListItem>
                                        <asp:ListItem>Rp 4,5 juta - Rp 5,5 juta</asp:ListItem>
                                        <asp:ListItem>Rp 5,5 juta - Rp 7,5 juta</asp:ListItem>
                                        <asp:ListItem>Diatas Rp 7,5 juta</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-footer">
                    </div>
                </div>
            </asp:Panel>
            <!--  --------- End Modal ---------  -->
        </div>
    </div>
    
    <script type="text/javascript">
        function Hidepopup() {
            $find("ModalPopupExtender1").hide();
            return false;
        }
    </script>
</asp:Content>
