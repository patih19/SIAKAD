<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="MhsCuti.aspx.cs" Inherits="akademik.am.WebForm19" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../css/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>--%>

    <!-- Modal Pop Up -->
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 500px;
            overflow: auto;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        .style4
        {
            background-color: rgba(198, 223, 255, 0.48);
        }
        .style5
        {
            background-color: rgba(17, 155, 218, 0.54);
        }
        .style6
        {
            direction: ltr;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div id="Tabs" role="tabpanel">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li><a href="#personal"  role="tab" data-toggle="tab">Aktif/Drop Out/Non-Aktif</a></li>
                        <li><a href="#employment"  role="tab" data-toggle="tab">Cuti/Lulus</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content" style="padding: 8px; background-color: White;">
                        <div role="tabpanel" class="tab-pane active" id="personal">
                            <!-- <asp:Label ID="Label2" runat="server" CssClass="hidden"></asp:Label> -->
                            <asp:Label ID="Label1" runat="server" CssClass="hidden"></asp:Label>
                            <table class="table-condensed">
                                <tr>
                                    <td class="style5" colspan="2">
                                        <strong>PENCARIAN</strong>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style5" colspan="2">
                                        <strong>PENCARIAN LANJUT</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>NPM/Sebagian Nama</strong>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbSrcNama2" runat="server" CssClass="form-control" Placeholder="Nama Minimal 4 Huruf"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <strong>Angkatan </strong>(wajib disi)
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbAngkatan" runat="server" CssClass="form-control" MaxLength="9"
                                            Placeholder="ex: 2015/2016" OnTextChanged="TbAngkatan_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <asp:Button ID="BtnSrc2" runat="server" CssClass="btn btn-primary" Text="Filter"
                                            OnClick="BtnSrc2_Click" />
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <strong>Program Studi </strong>(wajib disi)
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <strong>Status</strong>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="-1">Status</asp:ListItem>
                                            <asp:ListItem Value="A">Aktif</asp:ListItem>
                                            <asp:ListItem Value="D">Drop-Out</asp:ListItem>
                                            <asp:ListItem Value="G">Double Degree</asp:ListItem>
                                            <asp:ListItem Value="K">Keluar</asp:ListItem>
                                            <asp:ListItem Value="N">Non-Aktif</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <strong>Gender</strong>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList CssClass="form-control" ID="DLSex" runat="server" OnSelectedIndexChanged="DLSex_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Gender</asp:ListItem>
                                            <asp:ListItem>Laki-laki</asp:ListItem>
                                            <asp:ListItem>Perempuan</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <strong>NPM/Sebagian Nama</strong>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbSrcNama" runat="server" CssClass="form-control" Placeholder="Min. 4 Huruf"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:Button ID="BtnSrc" runat="server" CssClass="btn btn-primary" Text="Filter" OnClick="BtnSrc_Click" />
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="employment">
                            <table class="table-condensed">
                                <tr>
                                    <td class="style5" colspan="2">
                                        <strong>PENCARIAN LANJUT</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Program Studi </strong>(wajib disi)
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLProdi3" runat="server" CssClass="form-control">
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
                                            <asp:ListItem Value="All">ALL</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Status </strong>(wajib disi)
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList ID="DLStatus3" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="-1">Status</asp:ListItem>
                                            <asp:ListItem Value="C">Cuti</asp:ListItem>
                                            <asp:ListItem Value="L">Lulus</asp:ListItem>
                                            <asp:ListItem Value="D">Drop-Out</asp:ListItem>
                                            <asp:ListItem Value="G">Double Degree</asp:ListItem>
                                            <asp:ListItem Value="K">Keluar</asp:ListItem>
                                            <asp:ListItem Value="N">Non-Aktif</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Semester Lulus</strong>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbSemLls" runat="server" CssClass="form-control" MaxLength="5" Placeholder="ex: 20151"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Tahun Angkatan</strong>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="TbThnAngkatan" runat="server" CssClass="form-control" Placeholder="Tahun Masuk ex: 2011/2012"
                                            MaxLength="9"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <strong>Gender</strong>
                                    </td>
                                    <td class="style4">
                                        <asp:DropDownList CssClass="form-control" ID="DLSex3" runat="server">
                                            <asp:ListItem Value="-1">Gender</asp:ListItem>
                                            <asp:ListItem>Laki-laki</asp:ListItem>
                                            <asp:ListItem>Perempuan</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <asp:Button ID="BtnSrcLLs" runat="server" CssClass="btn btn-primary" Text="Filter"
                                            OnClick="BtnSrcLLs_Click" />
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <hr />
                <asp:Label ID="LbKetSelect" runat="server" CssClass="hidden"></asp:Label>
                <asp:GridView ID="GVMhs" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                    CssClass="table-condensed table-bordered" PageSize="150" 
                    OnRowCreated="GVMhs_RowCreated">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Status
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="BtnLihat" runat="server" CssClass="btn btn-success" Text="Update"
                                    OnClick="BtnLihat_Click" />
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
             <asp:LinkButton ID="lnkFake" runat="server"  CssClass="hidden"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                PopupControlID="pnlPopup" TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="BtnClose">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Vertical" Height="98%" Width="50%" Style="display: none" > <!--  Style="display: none" -->
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <a id="BtnClose" href="#"><span class="glyphicon glyphicon-eye-close"></span>Close
                        </a>
                    </div>
                    <div class="panel-body">
                        <asp:Label ID="LbNPM" runat="server" CssClass="hidden"></asp:Label>
                        <asp:Label ID="LbStatus" runat="server" CssClass="hidden"></asp:Label>
                        <div style="text-align:center">---- ========== <strong>PERUBAHAN STATUS MAHASISWA</strong> =========== ---</div>
                        <hr />
                        Menjadi Mahasiswa <strong>Aktif/DO/Non Aktif</strong> 
                        <table class="table-condensed" style="background-color: #FEE6B4">
                            <tr>
                                <td class="text-left" style="background-color: #FFCC66" colspan="2">
                                    <strong>MAHASISWA Aktif/Drop Out/Non-Aktif </strong>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    Status Mahasiswa
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLStatusAktif" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Status</asp:ListItem>
                                        <asp:ListItem Value="A">Aktif</asp:ListItem>
                                        <asp:ListItem Value="D">Drop-Out</asp:ListItem>
                                        <asp:ListItem Value="K">Keluar</asp:ListItem>
                                        <asp:ListItem Value="N">Non-Aktif</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnAktif" CssClass="btn btn-danger" runat="server" Text="Simpan"
                                        OnClientClick="return confirm('Anda Yakin Data Sudah Benar ?');" OnClick="BtnUpdateAktif_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <hr />
                        Menjadi Mahasiswa <strong>Cuti</strong>
                        <table class="table-condensed"style="background-color: rgba(120, 206, 92, 0.32);">
                            <tr>
                                <td class="text-left"  style="background-color: rgba(33, 195, 45, 0.37);">
                                    <strong>MAHASISWA CUTI </strong>&nbsp;
                                </td>
                                <td class="text-left"  style="background-color: rgba(33, 195, 45, 0.37);">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    Status Mahasiswa
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLStatusCuti" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Status</asp:ListItem>
                                        <asp:ListItem Value="C">Cuti</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Semester Cuti
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSmstrCuti" runat="server" CssClass="form-control" 
                                        Placeholder="ex : 20151" TextMode="Number" MaxLength="5"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnCuti" CssClass="btn btn-success" runat="server" Text="Simpan"
                                        OnClientClick="return confirm('Anda Yakin Data Sudah Benar ?');" OnClick="BtnUpdateCuti_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <hr />
                        Menjadi Mahasiswa <strong>Lulus</strong>
                        <table class="table-condensed" style="background-color: AliceBlue">
                            <tr>
                                <td colspan="4" style="background-color: #C6DFFF">
                                    <strong>MAHASISWA LULUS</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="DLStatus2" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Status</asp:ListItem>
                                        <asp:ListItem Value="L">Lulus</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" colspan="4" style="background-color: #C6DFFF">
                                    <strong>KETERANGAN </strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tanggal Lulus
                                </td>
                                <td>
                                    <asp:TextBox ID="TbTglLulus" runat="server" CssClass="form-control" Placeholder="YYYY-MM-dd"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TbTglLulus"
                                        Format="yyyy-MM-dd">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Nomor Ijazah
                                </td>
                                <td>
                                    <asp:TextBox ID="TbNoIjazah" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Jumlah SKS
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSKS" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    Jalur Kelulusan
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLJalur" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Jalur Kelulusan</asp:ListItem>
                                        <asp:ListItem Value="N">Non Skripsi</asp:ListItem>
                                        <asp:ListItem Value="S">Skripsi-TA-Tesis-Disertasi</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IPK
                                </td>
                                <td>
                                    <asp:TextBox ID="TbIPK" runat="server" MaxLength="4" Placeholder="ex : 3.50" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    Pelaksanaan
                                </td>
                                <td>
                                    <asp:DropDownList ID="DLPelaksanaan" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Pelaksanaan</asp:ListItem>
                                        <asp:ListItem Value="I">Individu</asp:ListItem>
                                        <asp:ListItem Value="K">Kelompok</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nomor S.K.
                                </td>
                                <td>
                                    <asp:TextBox ID="TBNoSK" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    Tahun Lulus
                                </td>
                                <td>
                                    <asp:TextBox ID="TbThnLls" runat="server" CssClass="form-control" Placeholder="ex : 2015/2016"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tanggal S.K.
                                </td>
                                <td>
                                    <asp:TextBox ID="TbTglSK" runat="server" CssClass="form-control" Placeholder="YYYY-MM-dd"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TbTglSK"
                                        Format="yyyy-MM-dd">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Semester Lulus
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSmstrLls" runat="server" CssClass="form-control" Placeholder="ex : 20151" TextMode="Number"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Judul
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="TbJudul" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Simpan"
                                        OnClientClick="return confirm('Anda Yakin Data Sudah Benar ?');" OnClick="BtnUpdate_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
            <!--  --------- End Modal ---------  -->
        </div>
    </div>


    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>
</asp:Content>


