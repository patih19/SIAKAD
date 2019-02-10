<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="UpdateBiodata.aspx.cs" Inherits="Padu.account.WebForm7" EnableEventValidation="false" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: x-small;
            color: #FF3300;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="background: #fafafa">
        <br />
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">AKUN</a> <a
                        href="<%= Page.ResolveUrl("~/account/keuangan") %>" class="list-group-item"><span
                            class="glyphicon glyphicon-transfer"></span>&nbsp;Aktifasi Pembayaran</a>
                    <a href="<%= Page.ResolveUrl("~/account/biodata") %>"
                        class="list-group-item"><span class="glyphicon glyphicon-user"></span>&nbsp;Biodata</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">FASILITAS</a>
                    <a href="<%= Page.ResolveUrl("~/account/Pemesanan") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;Pesan Mata Kuliah (Pra KRS)</a>
                    <a href="<%= Page.ResolveUrl("~/account/KRSNew") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;KRS TAHAP I</a>
                    <a href="<%= Page.ResolveUrl("~/account/KrsTahap2") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;KRS TAHAP II</a>
                    <a href="<%= Page.ResolveUrl("~/account/KHS") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;KHS</a>
                    <a href="<%= Page.ResolveUrl("~/account/KartuUjian") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Kartu Ujian</a>
                    <a href="<%= Page.ResolveUrl("~/account/Transkrip") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Transkrip Nilai</a>
                    <a href="<%= Page.ResolveUrl("~/account/PengajuanCuti") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Pengajuan Cuti</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">SYSTEM</a>
                    <a href="<%= Page.ResolveUrl("~/account/pass") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-lock"></span>&nbsp;Ganti Password</a>
                    <a id="keluar" runat="server" href="#" class="list-group-item"><span class="glyphicon glyphicon-log-out"></span>&nbsp;Logout </a>
                </div>
                <!-- <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">LAMPIRAN</a>
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;Kartu Uijan </a>
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Surat Pernyataan</a> 
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Formulir Pendaftaran</a> 
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-ok">
                        </span>&nbsp;Cek List Berkas</a>
                    <a href="#" class="list-group-item"><span class="glyphicon glyphicon-picture">
                        </span>&nbsp;Upload Foto</a>
                </div> -->
            </div>
            <div class="col-md-9">
                <div class="col-md-12 col-lg-12">
                    <strong>Perbaikan Biodata Mahasiswa</strong><br />
                    <br />
                    <table class="table-condensed table-bordered">
                        <tr>
                            <td>
                                <table class="table-condensed">
                        <tr>
                            <td colspan="2" style="background-color:#C6DFFF">
                                <strong>Identitas</strong>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nama
                            </td>
                            <td>
                                <asp:TextBox ID="TbNama" runat="server" MaxLength="90" CssClass="form-control" Placeholder="Isi Nama Sesuai Ijazah"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nomor Induk Kependudukan <span class="style1"><strong><em>(tanpa penghubung)</em></strong></span></td>
                            <td>
                            <asp:TextBox ID="TbNIK" runat="server" MaxLength="30" CssClass="form-control" Placeholder="Ada pada Kartu Keluarga/Nomor KTP"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Tempat Lahir
                            </td>
                            <td>
                                <asp:TextBox ID="TbTmpLhir" runat="server" Placeholder="Kota/Kab Tempat Lahir"
                                    MaxLength="40" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Tanggal Lahir</td>
                            <td>
                                <asp:TextBox ID="TBTtl" runat="server" placeholder="ex: 1990-02-24 (YY-MM-dd)" 
                                    MaxLength="10" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TBTtl"
                                    Format="yyyy-MM-dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Jenis Kelamin</td>
                            <td>
                                <asp:DropDownList ID="DLGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Jenis Kelamin</asp:ListItem>
                                    <asp:ListItem>Laki-laki</asp:ListItem>
                                    <asp:ListItem>Perempuan</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%--<tr>
                            <td >
                                Warga Negara
                            </td>
                            <td>
                                <asp:DropDownList ID="DLWarga" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Warga Negara</asp:ListItem>
                                    <asp:ListItem>WNI</asp:ListItem>
                                    <asp:ListItem>WNA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td >
                                Agama
                            </td>
                            <td>
                                <asp:DropDownList ID="DLAgama" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Agama</asp:ListItem>
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
                            <td >
                                Golongan Darah</td>
                            <td>
                                <asp:DropDownList ID="DLDarah" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Golongan Darah</asp:ListItem>
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>AB</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem>O</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Provinsi</td>
                            <td>
                                <asp:DropDownList ID="DropDownListProv" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownProv" runat="server" Category="id_provinsi"
                                    TargetControlID="DropDownListProv" ServicePath="~/web_services/ServiceCS.asmx"
                                    ServiceMethod="Prov" PromptText="PROVINSI">
                                </asp:CascadingDropDown>

                            </td>
                        </tr>
                        <tr>
                            <td >
                                Kota/Kabupaten</td>
                            <td>
                                <asp:DropDownList ID="DropDownListKab" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownKotakab" runat="server" TargetControlID="DropDownListKab"
                                    ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Kotakab" ParentControlID="DropDownListProv"
                                    LoadingText="LOADING" PromptText="KOTA/KABUPATEN" Category="id_kotakab">
                                </asp:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Kecamatan</td>
                            <td>
                                <asp:DropDownList ID="DropDownListKec" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownKec" runat="server" TargetControlID="DropDownListKec"
                                    ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Kecamatan" ParentControlID="DropDownListKab"
                                    LoadingText="LOADING" PromptText="KECAMATAN" Category="id_kecamatan">
                                </asp:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Desa/Kelurahan</td>
                            <td>
                                <asp:DropDownList ID="DropDownListDesa" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownDesa" runat="server" TargetControlID="DropDownListDesa"
                                    ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Desa" ParentControlID="DropDownListKec"
                                    LoadingText="LOADING" PromptText="DESA" Category="id_desa">
                                </asp:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Alamat Rumah
                            </td>
                            <td>
                                <asp:TextBox ID="TbAlamat" placeholder="Isi Alamat Rumah Lengkap" 
                                    runat="server" MaxLength="200" TextMode="MultiLine" CssClass="form-control" 
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Kode POS</td>
                            <td>
                                <asp:TextBox ID="TbKdPOS" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                No Handphone
                            </td>
                            <td>
                                <asp:TextBox ID="TBHp" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Email</td>
                            <td>
                                <asp:TextBox ID="TbEmail" placeholder="ex: andi@gmail.com" runat="server" 
                                    MaxLength="120" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left" colspan="2" style="background-color:#C6DFFF">
                                <strong>Pendidikan SMA/SMK/MA/MAK </strong>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nama Sekolah</td>
                            <td>
                                <asp:TextBox ID="TbSekolah" placeholder="NAMA ASAL SEKOLAH LENGKAP"
                                    runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Jurusan
                            </td>
                            <td>
                                <asp:DropDownList ID="DLJurusan" runat="server" CssClass="form-control">
                                <asp:ListItem Value="-1">Jurusan</asp:ListItem>
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
                            <td >
                                Status Sekolah</td>
                            <td>
                                <asp:DropDownList ID="DLStatusSekolah" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Status</asp:ListItem>
                                    <asp:ListItem>Negeri</asp:ListItem>
                                    <asp:ListItem>Swasta</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Tahun Lulus
                            </td>
                            <td>
                                <asp:TextBox ID="TBThnLls" runat="server" MaxLength="4" CssClass="form-control" placeholder="ex: 2013"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left" colspan="2" style="background-color:#C6DFFF">
                                <strong>Keluarga</strong></td>
                        </tr>
                        <tr>
                            <td >
                                Jumlah Adik
                            </td>
                            <td>
                                <asp:TextBox ID="TBAdik" runat="server" MaxLength="1" CssClass="form-control" placeholder="Isi 0 jika tidak ada"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Jumlah Kakak
                            </td>
                            <td>
                                <asp:TextBox ID="TbKakak" runat="server" MaxLength="1" CssClass="form-control" placeholder="Isi 0 jika tidak ada"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nama Ayah
                            </td>
                            <td>
                                <asp:TextBox ID="TbAyah" runat="server" CssClass="form-control" placeholder="Nama Lengkap"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nama Ibu
                            </td>
                            <td>
                                <asp:TextBox ID="TbIbu" runat="server" CssClass="form-control" placeholder="Nama Lengkap"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pendidikan Ayah</td>
                            <td>
                                <asp:DropDownList ID="DLPendidikanAyah" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Pendidikan</asp:ListItem>
                                    <asp:ListItem>SD</asp:ListItem>
                                    <asp:ListItem>SMP</asp:ListItem>
                                    <asp:ListItem>SMA</asp:ListItem>
                                    <asp:ListItem>Diploma</asp:ListItem>
                                    <asp:ListItem>S1/D4</asp:ListItem>
                                    <asp:ListItem>S2</asp:ListItem>
                                    <asp:ListItem>S3</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pendidikan Ibu
                            </td>
                            <td>
                                <asp:DropDownList ID="DLPendidikanIbu" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Pendidikan</asp:ListItem>
                                    <asp:ListItem>SD</asp:ListItem>
                                    <asp:ListItem>SMP</asp:ListItem>
                                    <asp:ListItem>SMA</asp:ListItem>
                                    <asp:ListItem>Diploma</asp:ListItem>
                                    <asp:ListItem>S1/D4</asp:ListItem>
                                    <asp:ListItem>S2</asp:ListItem>
                                    <asp:ListItem>S3</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pekerjaan Ayah
                            </td>
                            <td>
                                <asp:DropDownList ID="DLPekerjaanAyah" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Pekerjaan</asp:ListItem>
                                    <asp:ListItem>Tidak Bekerja</asp:ListItem>
                                    <asp:ListItem>Buruh</asp:ListItem>
                                    <asp:ListItem>Petani/Nelayan</asp:ListItem>
                                    <asp:ListItem>Pegawai Swasta Bukan Guru/Dosen</asp:ListItem>
                                    <asp:ListItem>Karyawan/Wiraswasta/Pedagang</asp:ListItem>
                                    <asp:ListItem>Pensiunan PNS/ABRI</asp:ListItem>
                                    <asp:ListItem>Pensiunan Swasta</asp:ListItem>
                                    <asp:ListItem>Purnawirawan/Veteran</asp:ListItem>
                                    <asp:ListItem>Guru/Dosen Swasta</asp:ListItem>
                                    <asp:ListItem>PNS Bukan Guru/Dosen</asp:ListItem>
                                    <asp:ListItem>Guru/Dosen PNS</asp:ListItem>
                                    <asp:ListItem>Pegawai BUMN/BUMD</asp:ListItem>
                                    <asp:ListItem>TNI/Polri</asp:ListItem>
                                    <asp:ListItem>Lainnya</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pekerjaan Ibu
                            </td>
                            <td>
                                <asp:DropDownList ID="DLPekerjaanIbu" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Pekerjaan</asp:ListItem>
                                    <asp:ListItem>Tidak Bekerja</asp:ListItem>
                                    <asp:ListItem>Buruh</asp:ListItem>
                                    <asp:ListItem>Petani/Nelayan</asp:ListItem>
                                    <asp:ListItem>Pegawai Swasta Bukan Guru/Dosen</asp:ListItem>
                                    <asp:ListItem>Karyawan/Wiraswasta/Pedagang</asp:ListItem>
                                    <asp:ListItem>Pensiunan PNS/ABRI</asp:ListItem>
                                    <asp:ListItem>Pensiunan Swasta</asp:ListItem>
                                    <asp:ListItem>Purnawirawan/Veteran</asp:ListItem>
                                    <asp:ListItem>Guru/Dosen Swasta</asp:ListItem>
                                    <asp:ListItem>PNS Bukan Guru/Dosen</asp:ListItem>
                                    <asp:ListItem>Guru/Dosen PNS</asp:ListItem>
                                    <asp:ListItem>Pegawai BUMN/BUMD</asp:ListItem>
                                    <asp:ListItem>TNI/Polri</asp:ListItem>
                                    <asp:ListItem>Lainnya</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Penghasilan Orang Tua
                            </td>
                            <td>
                                <asp:DropDownList ID="DLPenghasilan" runat="server" CssClass="form-control">
                                    <asp:ListItem>Penghasilan</asp:ListItem>
                                    <asp:ListItem>Kurang dari Rp 1,5 juta</asp:ListItem>
                                    <asp:ListItem>Rp 1,5 juta  - Rp 2,5 juta</asp:ListItem>
                                    <asp:ListItem>Rp 2,5 juta - Rp 3,5 juta</asp:ListItem>
                                    <asp:ListItem>Rp 3,5 juta - Rp 4,5 juta</asp:ListItem>
                                    <asp:ListItem>Rp 4,5 juta - Rp 5,5 juta</asp:ListItem>
                                    <asp:ListItem>Rp 5,5 juta - Rp 7,5 juta</asp:ListItem>
                                    <asp:ListItem>Diatas Rp 7,5 juta</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="BtnSave" runat="server" Text="Simpan" OnClick="BtnSave_Click" 
                                    CssClass="btn btn-success" />
                            </td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
