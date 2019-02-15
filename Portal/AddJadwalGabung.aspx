<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="AddJadwalGabung.aspx.cs" Inherits="Portal.AddJadwalGabung" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>

    <script type="text/jscript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {

                $('#ctl00_ContentPlaceHolder1_GVDosen').DataTable({
                    'iDisplayLength': 20,
                    'aLengthMenu': [[20, 40, -1], [20, 40, "All"]],
                    language: {
                        search: "Pencarian :",
                        searchPlaceholder: "Ketik Kata Kunci"
                    }
                });

                $('#ctl00_ContentPlaceHolder1_GVMakul').DataTable({
                    'iDisplayLength': 40,
                    'aLengthMenu': [[40, 50, 80, -1], [40, 50, 80, "All"]],
                    language: {
                        search: "Pencarian :",
                        searchPlaceholder: "Ketik Kata Kunci"
                    }
                });

                $('#ctl00_ContentPlaceHolder1_GVMakul2').DataTable({
                    'iDisplayLength': 40,
                    'aLengthMenu': [[40, 50, 80, -1], [40, 50, 80, "All"]],
                    language: {
                        search: "Pencarian :",
                        searchPlaceholder: "Ketik Kata Kunci"
                    }
                });

            }
        }
    </script>

<%--    <style type="text/css">
        .mdl
        {
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
        .center
        {
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
        .center img
        {
            height: 95px;
            width: 95px;
        }
    </style>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color:rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="alert alert-danger" role="alert">
                    <p>
                        <span>Mohon Dicermati Kembali Pengisian MATA KULIAH, JENIS KELAS DAN QUOTA </span>
                    </p>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>PENGGABUNGAN JADWAL PERKULIAHAN</strong>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="PanelSPI" runat="server">
                            <asp:UpdatePanel ID="UpPnlSPI" runat="server">
                                <ContentTemplate>
                                    <div class="col-xs-12 col-md-12 col-lg-6">
                                        <table class="table-bordered table-condensed table">
                                        <tr>
                                            <td style="background-color:#E1F0FF; padding-left:10px">
                                                <strong>MATA KULIAH</strong></td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    <div style="padding-left: 7px">
                                                        <asp:Label CssClass="hidden" ID="LbProdi" runat="server" Font-Size="Medium"
                                                            Style="font-size: 12px"></asp:Label>

                                                        <asp:Label CssClass="hidden" ID="LbKodeMakul" runat="server" Font-Size="Medium"
                                                            ForeColor="#333399" Style="font-size: 12px"></asp:Label>
                                                        <asp:Label ID="LbMakul" runat="server" Font-Size="Medium" ForeColor="#333399"
                                                            Style="font-size: 12px"></asp:Label>
                                                    </div>
                                                    <asp:Panel ID="PanelMakul" runat="server">
                                                        <table class="table-condensed">
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="DLProdiMakul" runat="server" AutoPostBack="True" 
                                                                        CssClass="form-control" 
                                                                        OnSelectedIndexChanged="DLProdiMakul_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="background-color:#FFFFEC">
                                                            <asp:Panel ID="PanelDetailMakul" runat="server">
                                                                <div class="table table-responsive">
                                                                    <div class="row">
                                                                        <div class="col-xs-12 col-md-12 col-lg-12">
                                                                            <asp:GridView ID="GVMakul" runat="server" CellPadding="4" CssClass=" table-bordered table-condensed"
                                                                                DataKeyNames="Kode" ForeColor="#333333" GridLines="None" OnPreRender="GVMakul_PreRender">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="CBMakul" runat="server" AutoPostBack="True" OnCheckedChanged="CBMakul_CheckedChanged" />
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
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </asp:Panel>
                                                    <p></p>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td  style="background-color:#E1F0FF; padding-left:10px">
                                                <strong>DOSEN PENGAMPU</strong></td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    <div style="padding-left:7px">
                                                    <asp:Label CssClass="hidden" ID="LbNidn" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                                        Style="font-size: 12px"></asp:Label>
                                                    <asp:Label ID="LbDosen" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                                        Style="font-size: 12px"></asp:Label>
                                                    </div>
                                                    <asp:Panel ID="PanelDosen" runat="server">
                                                        <table class="table-condensed">
                                                            <tr>
                                                                <td>
                                                                    <div style="background-color: #FFFFEC">
                                                                        <asp:DropDownList ID="DLProdiDosen" runat="server" AutoPostBack="True"
                                                                            CssClass="form-control"
                                                                            OnSelectedIndexChanged="DLProdiDosen_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="background-color:#FFFFEC">
                                                            <div class="table table-responsive">
                                                                <div class="row">
                                                                    <div class="col-xs-12 col-md-12 col-lg-12">
                                                                        <asp:Panel ID="PanelDetailDosen" runat="server">
                                                                            <table class="table-condensed">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView ID="GVDosen" runat="server" CellPadding="4" CssClass=" table-condensed table-bordered"
                                                                                            ForeColor="#333333" GridLines="None" OnPreRender="GVDosen_PreRender">
                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="CbDosen" runat="server" AutoPostBack="True" OnCheckedChanged="CbDosen_CheckedChanged" />
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
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <p></p>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td  style="background-color:#E1F0FF; padding-left:10px"">
                                                <strong>RUANG</strong></td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    <div style="padding-left:7px">
                                                    <asp:Label ID="LbRuang" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                                        Style="font-size: 12px"></asp:Label>
                                                    <asp:Label CssClass="hidden" ID="LbNo" runat="server" Font-Size="Medium"
                                                        Style="font-size: 12px"></asp:Label>
                                                        </div>
                                                    <asp:Panel ID="PanelRuang" runat="server">
                                                        <table class="table-condensed">
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="DLRuangProdi" runat="server" AutoPostBack="True" 
                                                                        CssClass="form-control" 
                                                                        OnSelectedIndexChanged="DLRuangProdi_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="background-color:#FFFFEC">
                                                        <asp:Panel ID="PanelDetailRuang" runat="server">
                                                            <table class="table-condensed">
                                                                <tr>
                                                                    <td>
                                                                            <asp:GridView ID="GVRuang" runat="server" CellPadding="4"
                                                                                CssClass="table-condensed table-bordered" ForeColor="#333333" GridLines="None"
                                                                                OnRowDataBound="GVRuang_RowDataBound">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="CbRuang" runat="server" AutoPostBack="True"
                                                                                                OnCheckedChanged="CbRuang_CheckedChanged" />
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </asp:Panel>
                                                        </div>
                                                    </asp:Panel>
                                                    <p></p>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td  style="background-color:#E1F0FF; padding-left:10px"">
                                                <strong>KELAS/HARI</strong> 
                                                <asp:Label ID="LbSemester" CssClass="hidden" runat="server" Text="" ForeColor="Transparent"></asp:Label></td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="padding-top: 5px; padding-left: 6px">
                                                                <asp:DropDownList ID="DLKelas" runat="server" CssClass="form-control" Width="90px" AutoPostBack="True" OnSelectedIndexChanged="DLKelas_SelectedIndexChanged">
                                                                    <asp:ListItem>Kelas</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding-top: 5px; padding-left: 6px">
                                                                <asp:DropDownList ID="DLHari" runat="server" CssClass="form-control" 
                                                                    Width="90px" AutoPostBack="True" OnSelectedIndexChanged="DLHari_SelectedIndexChanged">
                                                                    <asp:ListItem>Hari</asp:ListItem>
                                                                    <asp:ListItem>Senin</asp:ListItem>
                                                                    <asp:ListItem>Selasa</asp:ListItem>
                                                                    <asp:ListItem>Rabu</asp:ListItem>
                                                                    <asp:ListItem>Kamis</asp:ListItem>
                                                                    <asp:ListItem>Jumat</asp:ListItem>
                                                                    <asp:ListItem>Sabtu</asp:ListItem>
                                                                    <asp:ListItem>Minggu</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <p></p>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td  style="background-color:#E1F0FF; padding-left:10px">
                                                    <strong>JAM MULAI</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top:10px; padding-left:10px">
                                                    <asp:TextBox ID="TbMulai" runat="server" CssClass="form-control" MaxLength="5" 
                                                        Placeholder="Contoh 07:00" Width="110px" AutoPostBack="True" OnTextChanged="TbMulai_TextChanged"></asp:TextBox>
                                                    <p></p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#E1F0FF; padding-left:10px"">
                                                    <strong>JAM SELESAI</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top:10px; padding-left:10px">
                                                    <asp:TextBox ID="TbSelesai" runat="server" CssClass="form-control" 
                                                        MaxLength="5" Placeholder="Contoh 08:40" Width="110px" AutoPostBack="True" OnTextChanged="TbSelesai_TextChanged"></asp:TextBox>
                                                <p></p>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #E1F0FF; padding-left: 10px">
                                                    <strong>JENIS KELAS</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 10px; padding-left: 10px">
                                                    <asp:DropDownList ID="DLJenisKelas" runat="server" CssClass="form-control"
                                                        Width="150px">
                                                        <asp:ListItem>Jenis Kelas</asp:ListItem>
                                                        <asp:ListItem>Reguler</asp:ListItem>
                                                        <asp:ListItem>Non Reguler</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <p></p>
                                                </td>
                                            </tr>
                                                <tr>
                                                    <td style="background-color:#E1F0FF; padding-left:10px">
                                                        <strong>QUOTA</strong></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top:10px; padding-left:10px">
                                                        <asp:TextBox ID="TbQuota" runat="server" CssClass="form-control" MaxLength="2" 
                                                            Width="80px" TextMode="Number"></asp:TextBox>
                                                            <p></p>
                                                    </td>
                                                </tr>
                                            </table>
                                     </div>
                                    <div class="col-xs-12 col-md-12 col-lg-6">
                                        <table class="table-bordered table-condensed table">
                                            <tr>
                                                <td style="background-color:#E1F0FF; padding-left:10px">
                                                    <strong>MATA KULIAH</strong></td>
                                            </tr>
                                                <tr>
                                                    <td>
                                                        <div style="padding-left:7px">
                                                        <asp:Label CssClass="hidden" ID="LbProdi2" runat="server" Font-Size="Medium"
                                                            Style="font-size: 12px"></asp:Label>

                                                        <asp:Label CssClass="hidden" ID="LbKodeMakul2" runat="server" Font-Size="Medium"
                                                            ForeColor="#333399" Style="font-size: 12px"></asp:Label>
                                                        <asp:Label ID="LbMakul2" runat="server" Font-Size="Medium" ForeColor="#333399"
                                                            Style="font-size: 12px"></asp:Label>
                                                            </div>
                                                        <asp:Panel ID="PanelMakul2" runat="server">
                                                            <table class="table-condensed">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="DLProdiMakul2" runat="server" AutoPostBack="True" 
                                                                            CssClass="form-control" 
                                                                            OnSelectedIndexChanged="DLProdiMakul2_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="background-color:#FFFFEC">
                                                                <asp:Panel ID="PanelDetailMakul2" runat="server">
                                                                    <div class="table table-responsive">
                                                                        <div class="row">
                                                                            <div class="col-xs-12 col-md-12 col-lg-12">
                                                                                <asp:GridView ID="GVMakul2" runat="server" CellPadding="4" CssClass=" table-bordered table-condensed"
                                                                                    DataKeyNames="Kode" ForeColor="#333333" GridLines="None" OnPreRender="GVMakul2_PreRender">
                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="CBMakul2" runat="server" AutoPostBack="True" OnCheckedChanged="CBMakul2_CheckedChanged" />
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
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                                <p></p>
                                                            </div>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            <tr>
                                                <td  style="background-color:#E1F0FF; padding-left:10px">
                                                    <strong>DOSEN PENGAMPU</strong></td>
                                            </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label CssClass="hidden" ID="LbNidn2" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="LbDosen2" runat="server" Text=""></asp:Label>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            <tr>
                                                <td  style="background-color:#E1F0FF; padding-left:10px"">
                                                    <strong>RUANG</strong></td>
                                            </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LbRuang2" runat="server" Font-Size="Medium" ForeColor="#333399"
                                                            Style="font-size: 12px"></asp:Label>
                                                        <asp:Label CssClass="hidden" ID="LbNo2" runat="server" Font-Size="Medium"
                                                            Style="font-size: 12px"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            <tr>
                                                <td  style="background-color:#E1F0FF; padding-left:10px"">
                                                    <strong>KELAS/HARI</strong> 
                                                    <asp:Label ID="Label8" CssClass="hidden" runat="server" Text="" ForeColor="Transparent"></asp:Label></td>
                                            </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="padding-top: 5px; padding-left: 6px">
                                                                    <asp:Label ID="LbKelas" CssClass="form-control" runat="server" Text="Kelas"></asp:Label>
                                                                </td>
                                                                <td style="padding-top: 5px; padding-left: 6px">
                                                                    <asp:Label ID="LbHari" CssClass="form-control" runat="server" Text="Hari"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <p></p>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td  style="background-color:#E1F0FF; padding-left:10px">
                                                        <strong>JAM MULAI</strong></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top:10px; padding-left:10px">
                                                        <asp:Label CssClass="form-control"  Width="110px" ID="LbMulai" runat="server" Text="00:00"></asp:Label>
                                                        <p></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#E1F0FF; padding-left:10px"">
                                                        <strong>JAM SELESAI</strong></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top:10px; padding-left:10px">
                                                        <asp:Label CssClass="form-control"  Width="110px" ID="LbSelesai" runat="server" Text="00:00"></asp:Label>
                                                    <p></p>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #E1F0FF; padding-left: 10px">
                                                        <strong>JENIS KELAS</strong></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top: 10px; padding-left: 10px">
                                                    <asp:DropDownList ID="DLJenisKelas2" runat="server" CssClass="form-control"
                                                        Width="150px" AutoPostBack="True">
                                                        <asp:ListItem>Jenis Kelas</asp:ListItem>
                                                        <asp:ListItem>Reguler</asp:ListItem>
                                                        <asp:ListItem>Non Reguler</asp:ListItem>
                                                    </asp:DropDownList>
                                                        <p></p>
                                                    </td>
                                                </tr>
                                                    <tr>
                                                        <td style="background-color:#E1F0FF; padding-left:10px">
                                                            <strong>QUOTA</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top:10px; padding-left:10px">
                                                            <asp:TextBox ID="TbQuota2" runat="server" CssClass="form-control" MaxLength="2" 
                                                                Width="80px" TextMode="Number"></asp:TextBox>
                                                                <p></p>
                                                        </td>
                                                    </tr>
                                                </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <asp:UpdateProgress ID="UpProgSPI" runat="server">
                            <ProgressTemplate>
                                <div class="mdl">
                                    <div class="center">
                                        <img src="images/loading135.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:Panel ID="PanelUpdate" runat="server">
                            <%--<asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click"
                                    CssClass="btn btn-success" />--%>
                        </asp:Panel>
                    </div>
                    <div class="panel-footer">
                            <asp:Label CssClass="hidden" ID="lbno_jadwal" runat="server" ForeColor="Transparent"></asp:Label>
                            <asp:Panel ID="PanelTambah" runat="server">
                                <asp:Button ID="BtnSave" runat="server" Text="Tambah" OnClick="BtnSave_Click" CssClass="btn btn-success" />
                                &nbsp;<asp:Label ID="LbAlert" runat="server" Text="PERIKSALAH KEMBALI SEBELUM DISIMPAN" Font-Size="Small" ForeColor="#FF3300"></asp:Label>
                            </asp:Panel>
                    </div>
                </div>
                <br />
                <asp:Panel runat="server" ID="PanelListJadwal">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>LIST JADWAL PERKULIAHAN</strong>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="GVJadwal" runat="server" CssClass="table-condensed table-bordered"
                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVJadwal_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_Click" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_Click" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Delete
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
