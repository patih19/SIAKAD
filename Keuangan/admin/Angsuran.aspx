﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="Angsuran.aspx.cs" Inherits="Keuangan.admin.WebForm3" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Ajax Script Manager -->
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

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
            <h4>Biaya Angsuran</h4>
                <!-- ================ TABLE FILTER BIAYA =================== -->
                <table class="table-condensed table-bordered">
                    <!----------------------- TAHUN NAGKATAN -------------------------->
                    <tr>
                        <td>
                            Tahun Angkatan :
                        </td>
                        <td>
                            <asp:DropDownList ID="DLThnAngkatan" runat="server">
                                <asp:ListItem Value="-1">Tahun</asp:ListItem>
                                <asp:ListItem>2007/2008</asp:ListItem>
                                <asp:ListItem>2008/2009</asp:ListItem>
                                <asp:ListItem>2009/2010</asp:ListItem>
                                <asp:ListItem>2010/2011</asp:ListItem>
                                <asp:ListItem>2011/2012</asp:ListItem>
                                <asp:ListItem>2012/2013</asp:ListItem>
                                <asp:ListItem>2013/2014</asp:ListItem>
                                <asp:ListItem>2014/2015</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<span class="style1">*</span>
                        </td>
                    </tr>
                    <!----------------------- JURUSAN -------------------------->
                    <tr>
                        <td>
                            Jurusan :
                        </td>
                        <td>
                            <asp:DropDownList ID="DLProgStudi" runat="server">
                                <asp:ListItem Value="-1">Program Studi</asp:ListItem>
                                <asp:ListItem Value="20-201">S1 TEKNIK ELEKTRO</asp:ListItem>
                                <asp:ListItem Value="21-201">S1 TEKNIK MESIN</asp:ListItem>
                                <asp:ListItem Value="21-401">D3 TEKNIK MESIN</asp:ListItem>
                                <asp:ListItem Value="22-201">S1 TEKNIK SIPIL</asp:ListItem>
                                <asp:ListItem Value="54-211">S1 AGROTEKNOLOGI</asp:ListItem>
                                <asp:ListItem Value="60-201">S1 EKONOMI PEMBANGUNAN</asp:ListItem>
                                <asp:ListItem Value="62-401">D3 AKUNTANSI</asp:ListItem>
                                <asp:ListItem Value="63-201">S1 ILMU ADMINISTRASI NEGARA</asp:ListItem>
                                <asp:ListItem Value="88-201">S1 PENDIDIKAN BAHASA DAN SASTRA INDONESIA</asp:ListItem>
                                <asp:ListItem Value="88-203">S1 PENDIDIKAN BAHASA INGGRIS</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<span class="style1">*</span>
                        </td>
                    </tr>
                    <!----------------------- KELAS -------------------------->
                    <tr>
                        <td>
                            Kelas :
                        </td>
                        <td>
                            <asp:DropDownList ID="DLKelas" runat="server">
                                <asp:ListItem Value="-1">Kelas</asp:ListItem>
                                <asp:ListItem>Reguler</asp:ListItem>
                                <asp:ListItem>Non Reguler</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<span class="style1">*</span>
                        </td>
                    </tr>
                    <!----------------------- SEMESTER -------------------------->
                    <tr>
                        <td>
                            Semester :
                        </td>
                        <td>
                            <asp:DropDownList ID="DLSemster" runat="server">
                            </asp:DropDownList>
                            &nbsp;</td>
                        <ajaxToolkit:CascadingDropDown ID="CascadingDLSemester" runat="server" TargetControlID="DLSemster" Category="DLSemester"
                         ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester" LoadingText="Loading" PromptText="Semester">
                        </ajaxToolkit:CascadingDropDown> 
                    </tr>
                    <!---------------------- GELOMBANG ------------------------>
                    <!----------------------- FILTER -------------------------->
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="BtnFilter" runat="server" Text="Filter" 
                                onclick="BtnFilter_Click" />
                        </td>
                    </tr>
                </table>
                <hr />
                <!--======================= GRIDVIEW BIAYA STUDI MAHASISWA ============================== -->
                <h5><strong>List Biaya Angsuran</strong></h5>
                <asp:Label ID="LBResult" runat="server" ></asp:Label>
                <br />
                <asp:GridView ID="GVAngsuran" runat="server" CellPadding="4" 
                    CssClass="table-condensed table-bordered" ForeColor="#333333" 
                    GridLines="None" onrowdatabound="GVAngsuran_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
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
                <br />
            </div>
            <!--  END Contenet  -->
        </div>
        <!-- END CONTENT -->
    </div>
</asp:Content>