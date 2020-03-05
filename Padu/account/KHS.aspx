<%@ Page Title="" Language="C#" MasterPageFile="~/account/Sipadu.Master" AutoEventWireup="true" CodeBehind="KHS.aspx.cs" Inherits="Padu.account.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
            return false;
        }
    </script>
    <script type="text/javascript">
        function OpenNewWindow() {
            var npm = document.getElementById('<%=LbNpm.ClientID%>');
            var thn = document.getElementById("DLTahun").options[document.getElementById("DLTahun").selectedIndex].value;
            var smstr = document.getElementById("DLSemester").options[document.getElementById("DLSemester").selectedIndex].value;

           // alert("fsdfdfdsfds"+npm.innerHTML);

            //window.open('KHSCard.aspx?Npm=1110501002&Semester=20151&Tahun=2015','_blank',true);

        }
    </script>
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
                    <asp:Panel ID="PanelMhs" runat="server">
                        <table class="table-condensed">
                            <tr>
                                <td colspan="2">
                                    <h5>
                                        <strong>Kartu Hasil Studi (KHS)</strong></h5>
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
                        </table>
                    </asp:Panel>
                    <strong>Kartu Hasil Studi (KHS)</strong><br /><br />

                    <em>pilih tahun kemudian semester</em>
                    <table class="table-condensed table-bordered">
                        <tr>
                            <td>
                                Tahun / Semester
                            </td>
                            <td>
                                <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control" 
                                    Width="120px" AutoPostBack="True" OnSelectedIndexChanged="DLTahun_SelectedIndexChanged">
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
                                    CssClass="form-control" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="DLSemester_SelectedIndexChanged">
                                    <asp:ListItem>Semester</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelListKRS" runat="server">
                    <hr />
                        <table class="table-condensed">
                            <tr>
                                <td>DATA KHS</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GVKHS" runat="server" CellPadding="4" 
                                        CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None" 
                                        OnRowDataBound="GVKHS_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="table-condensed">
                                        <tr>
                                            <td>
                                                <strong>Jumlah SKS</strong></td>
                                            <td>
                                                :
                                                <asp:Label ID="LBSks" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>IP Semester</strong></td>
                                            <td>
                                                :
                                                <asp:Label ID="LbIPS" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="BtnDwnKHS" runat="server" CssClass="btn btn-success" 
                                                    Text="Cetak" onclick="BtnDwnKHS_Click1" /> <%-- OnClientClick="OpenNewWindow()"--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>