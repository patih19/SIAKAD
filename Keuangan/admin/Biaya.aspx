<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="Biaya.aspx.cs" Inherits="Keuangan.admin.WebForm2" EnableEventValidation="false" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style3
        {
            font-size: x-small;
        }
        .style4
        {
            font-size: 16px;
        }
        .style5
        {
            color: #6600FF;
        }
        .style6
        {
            color: #595959;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Ajax Script Manager -->
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
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
                    <a href="<%= Page.ResolveUrl("~/admin/Post.aspx") %>" class="list-group-item"><span class="glyphicon glyphicon-book ">
                        </span>&nbsp;Posting Pembayaran</a>
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
                <span class="style4"><strong>Master Biaya Studi</strong>
            </span>
            <br />
            <br />
                <!-- ================ TABLE FILTER BIAYA =================== -->
                <table class="table-bordered table-condensed">
                    <!----------------------- TAHUN NAGKATAN -------------------------->
                    <tr>
                        <td>
                            Tahun Angkatan :
                        </td>
                        <td>
                            <asp:DropDownList ID="DLThnAngkatan" runat="server">
                                <asp:ListItem Value="-1">Tahun</asp:ListItem>
                                <asp:ListItem>1999/2000</asp:ListItem>
                                <asp:ListItem>2000/2001</asp:ListItem>
                                <asp:ListItem>2006/2007</asp:ListItem>
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
                            &nbsp;<span class="style5">hanya untuk filter</span></td>
                        <ajaxToolkit:CascadingDropDown ID="CascadingDLSemester" runat="server" TargetControlID="DLSemster" Category="DLSemester"
                         ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester" LoadingText="Loading" PromptText="Semester">
                        </ajaxToolkit:CascadingDropDown> 
                    </tr>
                    <!----------------------- FILTER -------------------------->
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="BtnFilter" runat="server" Text="Filter" 
                                onclick="BtnFilter_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <!-- ================ TABLE CRETAE BIAYA =================== -->
                <!-------------------------- TABLE A ------------------------->
                <table class="table-condensed" >
                    <!----------------------- SEMESTER -------------------------->
                    <tr>
                        <td>
                            <span class="style6">Semester :</span>
                        </td>
                        <td>
                            <asp:TextBox ID="TBSemester" runat="server" MaxLength="5" Width="70px"></asp:TextBox> 
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTBSemester" runat="server" Enabled="true"
                                TargetControlID="TBSemester" FilterType="Numbers" FilterInterval="8">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp;<span class="style1">*</span>
                            <span class="style3">ex : 20141
                        </span>
                        </td>
                     </tr>
                </table>
                <!------------------------- TABLE B --------------------------->
                <table class="table-bordered table" >
                    <!----------------------- SEMESTER -------------------------->
                    <tr>
                        <td>
                            <span class="style6">SPP : </span>
                            <asp:TextBox ID="TBSPP" runat="server" MaxLength="7" Width="70px"></asp:TextBox>
                            <!-- Number Only -->
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTBExSPP" runat="server" Enabled="true"
                                TargetControlID="TBSPP" FilterType="Numbers" FilterInterval="8">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <span class="style1">*</span>
                        </td>
                        <td>
                            <span class="style6">BOP : </span>
                            <asp:TextBox ID="TBBOP" runat="server" MaxLength="7" Width="70px"></asp:TextBox>
                              <!-- Number Only -->
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTBExBOP" runat="server" Enabled="true"
                                TargetControlID="TBBOP" FilterType="Numbers" FilterInterval="8">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <span class="style1">*</span>
                        </td>
                        <td>
                            <span class="style6">SKS :</span>
                            <asp:TextBox ID="TBSks" runat="server" MaxLength="7" Width="70px" 
                                BackColor="#FFFFCC" ReadOnly="True">0</asp:TextBox>
                              <!-- Number Only -->
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTBExKmhs" runat="server" Enabled="true"
                                TargetControlID="TBSks" FilterType="Numbers" FilterInterval="8">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td>
                            <span class="style6">Kemhs :</span>
                            <asp:TextBox ID="TBKmhs" runat="server" MaxLength="7" Width="70px"></asp:TextBox>
                              <!-- Number Only -->
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTBExLab" runat="server" Enabled="true"
                                TargetControlID="TBKmhs" FilterType="Numbers" FilterInterval="8">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                         <td>
                            <span class="style6">Lab :</span>
                            <asp:TextBox ID="TbLab" runat="server" MaxLength="7" Width="70px" 
                                 BackColor="#FFFFCC" ReadOnly="True">0</asp:TextBox>
                              <!-- Number Only -->
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                 runat="server" Enabled="true"
                                TargetControlID="TbLab" FilterType="Numbers" FilterInterval="8">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <!----------------------- CREATE BIAYA -------------------------->
                    <tr>
                        <td colspan="6">
                            <asp:Button ID="BtnCreate" runat="server" Text="Create" 
                                onclick="BtnCreate_Click" />
                        </td>
                    </tr>
                </table>
                <hr />
                <!--======================= GRIDVIEW BIAYA STUDI MAHASISWA ============================== -->
                <h5><strong>Tabel Biaya Studi</strong> </h5>
                <asp:Label ID="LBResultFilter" runat="server" ></asp:Label>
                <asp:GridView ID="GVBiayaStudi" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None" CssClass="table-condensed table-bordered" 
                    onrowdatabound="GVBiayaStudi_RowDataBound">
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
