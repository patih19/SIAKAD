<%@ Page Title="" Language="C#" MasterPageFile="~/admin/keu_admin.Master" AutoEventWireup="true" CodeBehind="dispen.aspx.cs" Inherits="Keuangan.admin.WebForm4" EnableEventValidation="false" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            font-size: small;
        }
        .style3
        {
            font-size: 16px;
        }
        .style4
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Ajax Script Manager -->
    <ajaxToolkit:ToolkitScriptManager runat="server">
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
                <strong><span class="style3">DISPENSASI PEMBAYARAN
                </span></strong>
                <hr /> 
                <strong>&nbsp;Mahasiswa:</strong>
                <br />
                &nbsp;filter data mahasiswa berdasarkan NPM &nbsp;<asp:Label ID="LbFilterMhs" runat="server"></asp:Label>
                &nbsp;
                <table class="table-condensed">
                    <tr>
                        <td>
                            NPM :
                            <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                            &nbsp;<asp:Button ID="BtnFilter" runat="server" Text="Filter" OnClick="BtnFilter_Click" />
                        </td>
                    </tr>
                    <tr >
                        <td >
                            <asp:RadioButton ID="RBSksAngsuran1" runat="server" Text="SKS &amp; Angsuran 1" 
                                GroupName="angsuran" />
                            <asp:RadioButton ID="RBAngsuran1" runat="server" Text="Cicilan" 
                                GroupName="angsuran"/>
                            <asp:RadioButton ID="RBBebanAwl" runat="server" Text="Beban Awal" 
                                GroupName="angsuran"/>
                        </td>
                    </tr>
                </table>
                <table class="table-condensed table-bordered">
                    <!----------------------- TAHUN NAGKATAN -------------------------->
                    <tr>
                        <td>
                            <asp:Label ID="LbNPM" runat="server" Text="NPM"></asp:Label>
                        </td>
                        <td>
                            <!-- Foto here -->
                            <asp:Label ID="LbNama" runat="server" Text="Nama"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LbProdi" runat="server" Text="Program Studi"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LbClass" runat="server" Text="Kelas"></asp:Label>
                        </td>
                         <td>
                            <asp:Label ID="LbThnAngkatan" runat="server" Text="Tahun Angkatan"></asp:Label>
                        </td>
                    </tr>
                </table>
                <!-- ----------------- Sekat Bayar Angsuran dan SKS -------------- -->
                <asp:Panel ID="PanelSKS_Angsuran" runat="server">
                <hr />
                    <strong>&nbsp;Data Awal Dispensasi:</strong><br />
                    &nbsp;input hanya pada angsuran pertama tiap semester<br />
                    <br />
                    <table class="table-condensed">
                        <tr>
                            <td>
                                <asp:DropDownList ID="DLSem" runat="server">
                                </asp:DropDownList>&nbsp;<asp:Button ID="BtnAngsuran" runat="server" 
                                    Text="View" onclick="BtnAngsuran_Click" />
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDownSemester" runat="server" TargetControlID="DLSem"
                                    Category="DLSemester" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                    LoadingText="Loading" PromptText="Semester">
                                </ajaxToolkit:CascadingDropDown>
                                <asp:GridView ID="GVAngsuran" runat="server" CellPadding="4" CssClass="table-condensed table-bordered" 
                                    ForeColor="#333333" GridLines="None" 
                                    onrowdatabound="GVAngsuran_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" 
                                        HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </td>
                            <td style="vertical-align:top">
                                <strong>Biaya @ SKS :</strong><br />
                                <asp:Label ID="LbHargaSKS" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table class="table-condensed table-bordered">
                        <tr>
                            <td>
                                Semester :
                            </td>
                            <td>
                                <asp:DropDownList ID="DLSemester" runat="server">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDLSemester" runat="server" 
                                    Category="DLSemester" LoadingText="Loading" PromptText="Semester" 
                                    ServiceMethod="semester" ServicePath="~/web_services/ServiceCS.asmx" 
                                    TargetControlID="DLSemester">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Jumlah SKS</td>
                            <td>
                                <asp:TextBox ID="TBSKS" runat="server" MaxLength="2" Width="40px"></asp:TextBox>
                            </td>
                            <td class="style2">
                                <em><span class="style4">input jumlah SKS dalam satu semester</span></em>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Angsuran :
                            </td>
                            <td>
                                <asp:DropDownList ID="DLAngsuran" runat="server">
                                    <asp:ListItem Value="-1">Angsuran</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                Angsuran Pertama atau Kedua
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cicilan :
                            </td>
                            <td>
                                <!-- Foto here -->
                                <asp:DropDownList ID="DLCicilan" runat="server">
                                    <asp:ListItem Value="-1">Cicilan</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                Cicilan Biaya Angsuran
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jumlah Dispensasi (Rp.)
                            </td>
                            <td>
                                <asp:TextBox ID="TBDispen" runat="server" MaxLength="7" Width="85px"></asp:TextBox>
                            </td>
                            <td class="style4">
                                <em>Input Jumlah Dispensasi</em>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BtnPosting" runat="server" Text="Posting" 
                                    OnClick="BtnPosting_Click" ForeColor="#009900" />
                                <br />
                                <br />
                                <asp:Button ID="BtnPostOffline" runat="server" onclick="BtnPostOffline_Click" 
                                    Text="Offline" ForeColor="#E33331" />
                            </td>
                            <td colspan="2">
                                
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LbPostResult" runat="server"></asp:Label>
                </asp:Panel>
                <!-- =================================== Panel Cicilan =================================== -->
                <asp:Panel ID="PanelCicilan" runat="server">
                    <hr />
                    <strong>Input Biaya Dispensasi: </strong>
                    <br />
                    <table class="table-condensed table-bordered">
                        <tr>
                            <td>
                                Semester :
                            </td>
                            <td>
                                <asp:DropDownList ID="DLSemester2" runat="server">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DLSemester2"
                                    Category="DLSemester" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                    LoadingText="Loading" PromptText="Semester">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>

                        <tr>
                            <td>
                                Angsuran :
                            </td>
                            <td>
                                <asp:DropDownList ID="DLAngsuran2" runat="server">
                                    <asp:ListItem Value="-1">Angsuran</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                Angsuran Pertama atau Kedua
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cicilan :
                            </td>
                            <td>
                                <!-- Foto here -->
                                <asp:DropDownList ID="DLCicilan2" runat="server">
                                    <asp:ListItem Value="-1">Cicilan</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                Cicilan Biaya Angsuran
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jumlah Dispensasi (Rp.)
                            </td>
                            <td>
                                <asp:TextBox ID="TbDispen2" runat="server" MaxLength="8" Width="78px"></asp:TextBox>
                            </td>
                            <td class="style4">
                                <em>Input Jumlah Dispensasi</em>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="PostingCicilan" runat="server" onclick="Posting2_Click" 
                                    Text="Posting" ForeColor="#009900" />
                                <br />
                                <br />
                                <asp:Button ID="BtnCicilanOffline" runat="server" 
                                    onclick="BtnCicilanOffline_Click" Text="Offline" ForeColor="#E33331" />
                            </td>
                            <td colspan="2">
                                
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LbPostResult2" runat="server"></asp:Label>
                </asp:Panel >
                <!-- ===================================  Panel Beban Awal  ===================================== -->
                <asp:panel ID="PanelBebanAwal" runat="server">
                    <hr />
                    <table class="table-condensed">
                        <tr>
                            <td>
                                Tagihan Beban Awal:
                                <asp:Button ID="BtnBbnAwal" runat="server" onclick="BtnBbnAwal_Click" 
                                    Text="View" />
                                &nbsp;<asp:GridView ID="GVBebanAwal" runat="server" CellPadding="4" 
                                    CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None">
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
                            </td>
                        </tr>
                    </table>
                    <strong>Input Pembayaran Beban Awal:</strong><br />
                    <table class="table-condensed table-bordered">
                        <tr>
                            <td>
                                Dibayar pada semester :</td>
                            <td>
                                <asp:DropDownList ID="DLSemester3" runat="server">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="DLSemester3"
                                    Category="DLSemester" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                    LoadingText="Loading" PromptText="Semester">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                            <td class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Jumlah (Rp.)
                            </td>
                            <td>
                                <asp:TextBox ID="TbBbnAwal" runat="server" MaxLength="7" Width="85px"></asp:TextBox>
                            </td>
                            <td class="style4">
                                <em>Input Jumlah Pembayaran </em>Beban Awal</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BtnPostBbnAwal" runat="server" onclick="BtnPostBbnAwal_Click" 
                                    Text="Posting" ForeColor="#009900" />
                                <br />
                                <br />
                                <asp:Button ID="BtnAwalOffline" runat="server" onclick="BtnAwalOffline_Click" 
                                    Text="Offline" ForeColor="#E33331" />
                            </td>
                            <td colspan="2">
                                
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LbPostResult3" runat="server"></asp:Label>
                </asp:panel>
            </div>
            <!--  END Contenet  -->
        </div>
        <!-- END CONTENT -->
</div>
</asp:Content>
