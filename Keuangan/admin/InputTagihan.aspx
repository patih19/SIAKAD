<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="InputTagihan.aspx.cs" Inherits="Keuangan.admin.InputTagihan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-main-form" style="background: #fafafa">
        <div class=" row top-buffer">
            <!----------------------- MENU -------------------->
            <div class="col-md-3">
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">AKUN</a> 
                    <a href="<%= Page.ResolveUrl("~/admin/home.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-home "></span>
                        &nbsp;Beranda </a><a id="keluar" runat="server" href="#" class="list-group-item"><span
                            class="glyphicon glyphicon-log-out"></span>&nbsp;Logout </a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">BIAYA PERIODIK</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Biaya.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;Biaya Studi</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Angsuran.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Biaya Angsuran</a>
                    <a href="<%= Page.ResolveUrl("~/admin/SKS.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Edit SKS</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">BIAYA NON PERIODIK</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Biaya_Akhir.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;Biaya Studi Akhir</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Bayar.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-file">
                        </span>&nbsp;Pembayaran</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">PEMBAYARAN</a>
                    <a href="<%= Page.ResolveUrl("~/admin/dispen.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Dispensasi</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Tagihan.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Tagihan</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Masa_Bayar.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Masa Pembayaran</a>
                    <a href="<%= Page.ResolveUrl("~/admin/InputTagihan.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Posting Kekurangan</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Edit_Bayar.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Perbarui Pembayaran</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Beban_Awal.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Beban Awal</a>
                </div>
                <div class="list-group">
                    <a href="#" class="list-group-item" style="background-color: #87cefa">KEAMANAN</a>
                    <a href="<%= Page.ResolveUrl("~/admin/Pass.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-warning-sign ">
                    </span>&nbsp;Ganti Password </a>
                </div>
            </div>
            <!-- END MENU -->

            <!-- CONTENT -->
            <div class="col-md-9">
                <span class="style4"><strong>POSTING KEKURANGAN PEMBAYARAN</strong>
                </span>
                <br />
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Input Tagihan</strong>
                    </div>
                    <div class="panel-body">
                        <p></p>
                        <table class="table table-condensed">
                            <tr>
                                <td>NPM</td>
                                <td>
                                    <asp:TextBox ID="TbNpm" runat="server" Placeholder="Ketik NPM " CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >Semester</td>
                                <td >
                                    <asp:TextBox ID="TbSemester" Placeholder="contoh : 20172 " runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >Biaya</td>
                                <td >
                                    <asp:TextBox ID="TbBiaya" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >Angsuran</td>
                                <td >
                                    <asp:DropDownList ID="DLAngsuran" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Angsuran Ke</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            </table>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="BtnPostKekurangan" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="BtnPostKekurangan_Click" />
                    </div>
                </div>
            </div>
            <!--  END Contenet  -->

        </div>
        <!-- END CONTENT -->
</div>
</asp:Content>
