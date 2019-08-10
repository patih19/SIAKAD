<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="KrsTahap2.aspx.cs" Inherits="Padu.account.KrsTahap2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/timeline.css" rel="stylesheet" />
    <style type="text/css">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                <div class="col-md-12">
                    <div class="alert alert-success" role="alert">
                       <p><span>PENGISIAN KRS BAGI MAHASISWA YANG TIDAK MEMESAN MATA KULIAH / BELUM KRS</span></p> 
                       <p><span>PERBAIKAN KRS BAGI MAHASISWA YANG SUDAH MENGISI KRS DI TAHAP PERTAMA</span></p> 
                       <p><span>PENGISIAN/PERBAIKAN KRS BAGI MAHASISWA SEMESTER SATU</span></p> 
                    </div>

                    <asp:Panel ID="PanelMhs" runat="server">
                        <table class="table-condensed">
                            <tr>
                                <td colspan="2">
                                    <h5>
                                        <strong>Kartu Rencana Studi (KRS)</strong></h5>
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
                                    <asp:Label ID="LbKdProdi" runat="server"></asp:Label>
                                    &nbsp;-
                                    <asp:Label ID="LbProdi" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tahun</td>
                                <td>
                                    :
                                    <asp:Label ID="LbTahun" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Kartu Rencana Studi (KRS) Tahap KEDUA</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td style="vertical-align:top">
                                        Jenis KRS
                                    </td>
                                    <td colspan="2">
                                <asp:RadioButton ID="RBInput" runat="server" Text="Pengambilan KRS" GroupName="KRS" /> <br />
                                <asp:RadioButton ID="RBEditKRS" runat="server" Text="Edit KRS" GroupName="KRS" /> <br />
                                <%-- <asp:RadioButton ID="RbBatalTambahKRS" runat="server" Text="Batal Tambah KRS" GroupName="KRS" /> <br />--%>
                                <asp:RadioButton ID="RBList" runat="server" Text="Lihat KRS" GroupName="KRS" />
                                    </td>
                                </tr>
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
                                        <asp:DropDownList ID="DLSemester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DLSemester_SelectedIndexChanged"
                                            CssClass="form-control" Width="120px">
                                            <asp:ListItem>Semester</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div runat="server" id="PanelContent" class="panel panel-default">
                        <div class="panel-body table table-responsive">
                            <asp:Panel ID="PanelKRS" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="PanelBdk" runat="server" CssClass="form-control" BackColor="#FFFF99">
                                            <strong>Jumlah maksimal SKS :</strong>
                                            <asp:Label ID="LbMaxSKS" runat="server" Text="" Style="font-weight: 700"></asp:Label>
                                        </asp:Panel>
                                        <p></p>
                                        <strong>Jumlah SKS dipilih :</strong>
                                        <asp:Label ID="LbJumlahSKS" runat="server" Style="font-weight: 700"></asp:Label>
                                        <br />
                                        <asp:GridView ID="GVAmbilKRS" runat="server" CellPadding="4"
                                            ForeColor="#333333" GridLines="None" CssClass="table table-striped"
                                            OnRowDataBound="GVAmbilKRS_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" />
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
                                                        <asp:Label ID="LbSisa" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        Sisa
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                            <SortedDescendingHeaderStyle BackColor="#15524A" />
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
                            <asp:Panel ID="PanelEditKRS" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <br />
                                        <span class="style2"><strong>Batal Tambah/Edit KRS Bisa Dilakukan 8x !!</strong></span><br />
                                        <br />
                                        <strong>Jumlah SKS =</strong>
                                        <asp:Label ID="LbJumlahEditSKS" runat="server" Style="font-weight: 700"></asp:Label>
                                        <br />
                                        <asp:GridView ID="GVEditKRS" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                            CssClass="table table-striped"
                                            OnRowDataBound="GVEditKRS_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CBEdit" runat="server" OnCheckedChanged="CBEdit_CheckedChanged"
                                                            AutoPostBack="True" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        Pilih
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Sisa
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LbSisa" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Button ID="BtnUpdate" runat="server" Text="Update"
                                    OnClick="BtnUpdate_Click" CssClass="btn btn-primary"
                                    OnClientClick="return confirm('Anda Yakin Data Tersebut Benar ?');" />
                            </asp:Panel>
                            <asp:Panel ID="PanelListKRS" runat="server">
                                <div style="color:red"><strong>
                                <asp:Label ID="LbTextValidasi" runat="server" Text=""></asp:Label></strong> </div> <br />
                                <asp:Panel ID="PanelPA" runat="server" CssClass="form-control" BackColor="#FFFF99" >
                                    Pembimbing Akademik : <asp:Label ID="LbDosenPA" runat="server" Text=""></asp:Label>
                                </asp:Panel> 
                                <asp:Panel ID="PanelInfo" runat="server" CssClass="form-control" BackColor="#FFFF99" >                                   
                                    <p>KRS tidak divalidasi akan berdampak tidak dapat mengikuti perkuliahan dan nilai tidak dapat diinput</p>
                                </asp:Panel>
                                <br />
                                <asp:GridView ID="GVListKrs" runat="server" CssClass="table table-striped table-bordered"
                                    CellPadding="4" ForeColor="#333333" GridLines="None"
                                    OnRowDataBound="GVListKrs_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                                <br />
                                <asp:Panel ID="PanelValidasiKRS" runat="server">
                                    <strong>Pesan dosen pembimbing:</strong>
                                    <p></p>
                                    <asp:Repeater ID="RepeaterPesan" runat="server">
                                        <ItemTemplate>
                                            <ul class="timeline">
                                                <li>
                                                    <div class="timeline-badge danger"><i class="glyphicon glyphicon-credit-card"></i></div>
                                                    <div class="timeline-panel">
                                                        <div class="timeline-heading">
                                                            <h5 style="color:#efa700" class="timeline-title"><%# Eval("Tanggal") %></h5>
                                                        </div>
                                                        <div class="timeline-body">
                                                            <p><%# Eval("Pesan") %></p>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <p></p>
                                </asp:Panel>
                                <asp:Button ID="BtnDwnKrs" runat="server" Text="Unduh KRS"
                                    CssClass="btn btn-success" OnClick="BtnDwnKrs_Click" />
                            </asp:Panel>
                        </div>
                    </div>
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
                <br />
            </div>
        </div>
    </div>
</asp:Content>
