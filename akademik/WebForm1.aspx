<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="akademik.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="background-color: #F2F2FF;">
        <!-- #FCFCFC -->
        <div class="row">
            <div class=" col-xs-8 col-md-9 col-lg-9 ">
                            <p class="bg-danger">
                    Selamat Datang di Website Seleksi Masuk Universitas Tidar (SM-UNTIDAR)</p>
                <div class="jumbotron alert-danger">
                    dddd
                </div>
                <p class="bg-danger">
                    Selamat Datang di Website Seleksi Masuk Universitas Tidar (SM-UNTIDAR)</p>
                <br />
                <div class=" col-xs-12 col-md-8 col-lg-8 ">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            Statistik Pendaftaran Tiap Program Studi</div>
                        <div class="panel-body">
                            <asp:GridView ID="GVProdi" CssClass="table table-striped" runat="server" CellPadding="4"
                                ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#E2E2E2" Font-Bold="True" ForeColor="Black" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F9F9F9" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class=" col-xs-12 col-md-4 col-lg-4 ">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            Statistik Pendaftaran</div>
                        <div class="panel-body">
                            <table class="table table-striped">
                                <tr>
                                    <td colspan="2">
                                        <strong>PENDAFTAR</strong> &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Jumlah Pendaftar
                                    </td>
                                    <td>
                                        <asp:Label ID="LbPendaftar" runat="server">Jumlah Pendaftar</asp:Label>
                                        &nbsp;orang
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <strong>WILAYAH ASAL SEKOLAH</strong> &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Dalam Kota
                                    </td>
                                    <td>
                                        <asp:Label ID="LbDalamKota" runat="server">Dalam Kota</asp:Label>
                                        &nbsp;orang
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Luar Kota
                                    </td>
                                    <td>
                                        <asp:Label ID="LbLuarKota" runat="server">Luar Kota</asp:Label>
                                        &nbsp;orang
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <strong>JENIS KELAMIN</strong> &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Laki-laki
                                    </td>
                                    <td>
                                        <asp:Label ID="LbLelaki" runat="server">Laki-laki</asp:Label>
                                        &nbsp;orang
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Perempuan
                                    </td>
                                    <td>
                                        <asp:Label ID="LbPerempuan" runat="server">Perempuan</asp:Label>
                                        &nbsp;orang
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <strong>ASAL SEKOLAH</strong> &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        SMK
                                    </td>
                                    <td>
                                        <asp:Label ID="LbSMK" runat="server" Text="SMK"></asp:Label>
                                        &nbsp;orang
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        SMA
                                    </td>
                                    <td>
                                        <asp:Label ID="LbSMA" runat="server" Text="SMA"></asp:Label>
                                        &nbsp;orang
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Content Information -->
            <div class="col-xs-4 col-md-3 col-lg-3">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h4>
                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="https://drive.google.com/file/d/0B9QhYV69qy4bZkpKODdfSVFrN0E/view?usp=sharing"
                                Target="_blank">Pengumuman Hasil SM-UNTIDAR 2015</asp:HyperLink></h4>
                    </div>
                </div>
                <!-- col-md-3 col-lg-3 -->
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h4>
                            HOTLINE KANTOR PMB</h4>
                        <h4>
                            (0293)5590555</h4>
                    </div>
                </div>
                <!-- ========= Pendaftaran ========= -->
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <a href="<%= Page.ResolveUrl("~/tagihan.aspx") %>"><span class="style2">DAFTAR</span></a>
                    </div>
                </div>
                <!-- ========= Login Form ========= -->
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Login
                    </div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="TBNoDaftar" placeholder="Input No Pendaftaran"
                                        runat="server" Width="170px" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqTagihan" runat="server" ErrorMessage="Input No Pendaftaran"
                                        ControlToValidate="TBNoDaftar" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                    <%--<asp:ValidatorCalloutExtender ID="ValUsername" runat="server" TargetControlID="ReqTagihan">
                                    </asp:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox CssClass="form-control" ID="TBPin" placeholder="Input PIN" runat="server"
                                        Width="170px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqPIN" runat="server" ErrorMessage="Input PIN" ControlToValidate="TBPin"
                                        Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                    <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="ReqPIN">
                                    </asp:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--   <cc1:captchacontrol ID="Captcha" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                        CaptchaHeight="55" CaptchaLineNoise="None" CaptchaMinTimeout="12"
                                        CaptchaMaxTimeout="240" FontColor="#529E00" BackColor="White" 
                                        Width="190px" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <em>Ketik ulang 5 huruf yang tampil pada gambar diatas : </em>
                                    <asp:TextBox CssClass="form-control" ID="TBReCaptcha" runat="server" Width="130px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqKode" runat="server" ErrorMessage="Input Kode Keamanan"
                                        ControlToValidate="TBReCaptcha" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                    <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="ReqKode">
                                    </asp:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BtLogin" runat="server" Text="Login" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <hr />
                        Untuk melakukan login, isi nomor Pendaftaran dan PIN (case sensitive) yang tertera
                        pada bukti pembayaran dari Bank</div>
                </div>
                <!-- ========= Download Panduan ========= -->
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Download Panduan</div>
                    <div class="panel-body">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://drive.google.com/file/d/0B9QhYV69qy4bV0NCWkQ3S1BWZlU/view?usp=sharing"
                            Target="_blank">A. Pendaftaran & Pembayaran di Bank Jateng</asp:HyperLink>
                        <br />
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="https://drive.google.com/file/d/0B9QhYV69qy4bSVREaEt6cVY1cEk/view?usp=sharing"
                            Target="_blank">B. Pendaftaran & Pembayaran di Bank BNI</asp:HyperLink>
                    </div>
                </div>
                <!-- ======== Upload Slip Pembayaran ======== -->
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        Upload Bukti Pembayaran</div>
                    <div class="panel-body">
                        Kirim bukti pembayaran / slip pembayaran bagi peserta yang membayar selain di bank
                        Bank Jateng.
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ValUp.aspx" Target="_blank">Upload</asp:HyperLink>
                    </div>
                </div>
            </div>
            <!--  End Menu -->
        </div>
    </div>
</asp:Content>
