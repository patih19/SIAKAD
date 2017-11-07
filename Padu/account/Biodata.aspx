<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="Biodata.aspx.cs" Inherits="Padu.account.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="background: #fafafa">
        <br />
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">AKUN</a> <a
                        href="<%= Page.ResolveUrl("~/account/keuangan") %>" class="list-group-item"><span
                            class="glyphicon glyphicon-transfer"></span>&nbsp;Aktifasi Pembayaran</a>
                    <a href="#" class="list-group-item"><span
                        class="glyphicon glyphicon-picture "></span>&nbsp;Upload Foto</a>
                    <a href="<%= Page.ResolveUrl("~/account/biodata") %>"
                        class="list-group-item"><span class="glyphicon glyphicon-user"></span>&nbsp;Biodata</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">FASILITAS</a>
                    <a href="<%= Page.ResolveUrl("~/account/KRS") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-check"></span>&nbsp;KRS</a>
                    <a href="<%= Page.ResolveUrl("~/account/KHS") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;KHS</a>
                    <a href="<%= Page.ResolveUrl("~/account/KartuUjian") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Kartu Ujian</a>
                    <a href="<%= Page.ResolveUrl("~/account/Transkrip") %>" class="list-group-item"><span
                        class="glyphicon glyphicon-file"></span>&nbsp;Transkrip Nilai</a>
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
                <div class="col-md-12">
                    <asp:Panel ID="PanelMhs" runat="server">
                        <table class="table-condensed">
                            <tr>
                                <td colspan="2">
                                    <h5>
                                        <strong>Biodata Mahasiswa</strong></h5>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nama
                                </td>
                                <td>
                                    :
                                    <asp:Label ID="LbNama" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    NPM
                                </td>
                                <td>
                                    :
                                    <asp:Label ID="LbNpm" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Kelas
                                </td>
                                <td>
                                    :
                                    <asp:Label ID="LbKelas" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Program Studi
                                </td>
                                <td>
                                    :
                                    <asp:Label ID="LbProdi" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <strong>Biodata Mahasiswa</strong><br />
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
                                :
                                <asp:Label ID="LbName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                NIK</td>
                            <td>
                                :
                                <asp:Label ID="LbNIK" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Tempat / Tanggal Lahir
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbTmpLahir" runat="server"></asp:Label>
                                &nbsp;/
                                <asp:Label ID="LbTglLhair" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Jenis Kelamin
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbGender" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Agama
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbAgama" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Golongan Darah</td>
                            <td>
                                :
                                <asp:Label ID="LbDarah" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Provinsi</td>
                            <td>
                                :
                                <asp:Label ID="LbProv" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Kota/Kabupaten</td>
                            <td>
                                :
                                <asp:Label ID="LbKotaKab" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Kecamatan</td>
                            <td>
                                :
                                <asp:Label ID="LbKec" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Desa/Kelurahan</td>
                            <td>
                                :
                                <asp:Label ID="LbDesa" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Alamat Rumah
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbAlamat" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Kode Pos</td>
                            <td>
                                :
                                <asp:Label ID="LbKdPOS" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                No Handphone
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbHp" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Email</td>
                            <td>
                                :
                                <asp:Label ID="LbEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left" colspan="2" style="background-color:#C6DFFF">
                                <strong>Pendidikan Menengah Atas</strong>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nama Sekolah
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbSekolah" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Jurusan
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbJurusan" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Status Sekolah</td>
                            <td>
                                :
                                <asp:Label ID="LbStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Tahun Lulus
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbThnLulus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left" colspan="2" style="background-color:#C6DFFF">
                                <strong>Latar Belakang Keluarga</strong></td>
                        </tr>
                        <tr>
                            <td >
                                Jumlah Adik
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbAdik" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Jumlah Kakak
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbKakak" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nama Ayah
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbNamaAyah" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Nama Ibu
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbNamaIbu" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pendidikan Ayah
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbPendidikanAyah" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pendidikan Ibu
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbPendidikanIbu" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pekerjaan Ayah
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbPekerjaanAyah" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Pekerjaan Ibu
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbPekerjaanIbu" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Penghasilan Orang Tua
                            </td>
                            <td>
                                :
                                <asp:Label ID="LbPenghasilan" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <!-- <tr>
                            <td class="text-left" colspan="2" style="background-color:#C6DFFF">
                                <strong>Foto</strong>
                            </td>
                        </tr> -->
                        <!-- <tr>
                            <td colspan="2" >
                                <asp:Image ID="Image1" runat="server" Height="175px" Width="146px" />
                            </td>
                        </tr> -->
                        <tr>
                            <td colspan="2" >
                                <br />
                                <asp:Button ID="BtnUpdate" runat="server" Text="Update Data" 
                                    onclick="BtnUpdate_Click" CssClass="btn btn-success" />
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
