<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="JadwalKuliah3.aspx.cs" Inherits="Portal.WebForm24" EnableEventValidation="false" %>
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
            }

            $('#ctl00_ContentPlaceHolder1_GVJadwal').DataTable({
                'iDisplayLength': 100,
                'aLengthMenu': [[100, 200, 300, -1], [100, 200, 300, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });

        }
    </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVJadwal tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd)
        {
            background-color: #EEF7EE;
        }
    </style>
    <%--<style type="text/css">
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
    <div class="container top-main-form" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
            <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Jadwal Perkuliahan</strong></div>
                    <div class="panel-body">
                    <table class="table-condensed">
                        <tr>
                            <td>
                                Tahun
                            </td>
                            <td>
                                <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control" Width="130px">
                                    <asp:ListItem>Tahun</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                    Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                    LoadingText="Loading" PromptText="Tahun">
                                </ajaxToolkit:CascadingDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Semester
                            </td>
                            <td>
                                <asp:DropDownList ID="DlSemester" runat="server" CssClass="form-control" Width="130px">
                                    <asp:ListItem>Semester</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="BtnJadwal" runat="server" Text="OK" OnClick="BtnJadwal_Click" 
                                    CssClass="btn btn-success" />
                                &nbsp;<asp:Label CssClass="hidden" ID="LbJadwalResult" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </div>
                </div>
                <br />
                <div>
                    <hr />
                    <asp:Panel ID="PanelJadwal" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>List Jadwal Perkuliahan</strong>
                                &nbsp;<asp:Button ID="BtnNewJadwal" OnClick="BtnNewJadwal_Click" runat="server" CssClass="btn btn-danger" Text="Buat Jadwal" />
                            </div>
                            <div class="panel-body">
                                <div class="table table-responsive">
                                    <asp:GridView ID="GVJadwal" runat="server" CssClass="table table-condensed table-bordered table-hover"
                                        OnRowDataBound="GVJadwal_RowDataBound" OnPreRender="GVJadwal_PreRender">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" OnClick="Button1_Click" Text="Add" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Add
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnEdit" runat="server" OnClick="BtnEdit_Click" Text="Edit" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Edit
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" Text="Delete" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Delete
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Quota
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbQuota" runat="server" AutoPostBack="True"
                                                        CssClass="from-control" MaxLength="3" OnTextChanged="TbQuota_TextChanged"
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Label CssClass="hidden" ID="LbThn" runat="server" ForeColor="Transparent"></asp:Label>
                                &nbsp;<asp:Label CssClass="hidden" ID="LbSmstr" runat="server" ForeColor="Transparent" ></asp:Label>
                            </div>
                        </div>
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="PanelEditJadwal" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong> EDIT JADWAL PERKULIAHAN</strong></div>
                            <div class="panel-body">
                                <asp:Panel ID="PanelDetailEditJadwal" runat="server">
                                    <asp:Panel ID="PnlAjaxJadwal" runat="server">
                                        <asp:UpdatePanel ID="UpPnlAjaxJadwal" runat="server">
                                            <ContentTemplate>
                                                <table class="table-bordered table-condensed table">
                                                    <tr>
                                                        <td style="background-color:#E1F0FF; padding-left:10px">
                                                            <strong>MATA KULIAH</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="padding-left:7px">
                                                                <asp:Label CssClass="hidden" ID="LbKodeMakul" runat="server" Font-Size="Medium" 
                                                                    ForeColor="#333399" Style="font-size: 12px"></asp:Label>
                                                                <asp:Label ID="LbMakul" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                                                    Style="font-size: 12px"></asp:Label>
                                                                <asp:Label CssClass="hidden" ID="LbProdi" runat="server" Font-Size="Medium" 
                                                                    ForeColor="Transparent" Style="font-size: 12px"></asp:Label>
                                                            </div>
                                                            <p></p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td style="background-color:#E1F0FF; padding-left:10px">
                                                            <strong>DOSEN PENGAMPU</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td>                                                       <div style="padding-left:7px">
                                                            <asp:Label CssClass="hidden" ID="LbNidn" runat="server" Font-Size="Medium" ForeColor="#333399"
                                                                    Style="font-size: 12px"></asp:Label>
                                                                <asp:Label ID="LbDosen" runat="server" Font-Size="Medium" ForeColor="#333399" Style="font-size: 12px"></asp:Label>
                                                             </div>  
                                                            <asp:Panel ID="PanelDosen" runat="server"> 
                                                                <table class="table-condensed">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList ID="DLProdiDosen" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                                OnSelectedIndexChanged="DLProdiDosen_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <p></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:Panel ID="PanelDetailDosen" runat="server">
                                                                    <table class="table-condensed">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="GVDosen" runat="server" CellPadding="4" CssClass="table-condensed table-bordered"
                                                                                    ForeColor="#333333" GridLines="None" onprerender="GVDosen_PreRender">
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
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td style="background-color:#E1F0FF; padding-left:10px">
                                                            <strong>RUANG KULIAH</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="padding-left:7px">
                                                                <asp:Label ID="LbRuang" runat="server" Font-Size="Medium" ForeColor="#333399" 
                                                                    Style="font-size: 12px"></asp:Label>
                                                                <asp:Label CssClass="hidden" ID="LbNo" runat="server" ForeColor="Transparent" Text="LbNo"></asp:Label>
                                                            </div>    
                                                                <asp:Panel ID="PanelRuang" runat="server">
                                                                <table class="table-condensed">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList ID="DLRuangProdi" runat="server" AutoPostBack="True" 
                                                                                CssClass="form-control" 
                                                                                OnSelectedIndexChanged="DLRuangProdi_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <p></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                                </asp:Panel>
                                                            </asp:Panel>
                                                        </td>
                                                    <tr>
                                                         <td style="background-color:#E1F0FF; padding-left:10px">
                                                             <strong>HARI</strong>
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="table-condensed">
                                                                <tr>
                                                                     <td style="padding-top: 5px; padding-left: 6px">
                                                                        <asp:DropDownList ID="DLKelas" runat="server" CssClass="form-control" 
                                                                            Width="90px">
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
                                                                            Width="90px" onselectedindexchanged="DLHari_SelectedIndexChanged" 
                                                                             AutoPostBack="True">
                                                                            <asp:ListItem Value="-1">Hari</asp:ListItem>
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
                                                         <td style="background-color:#E1F0FF; padding-left:10px">
                                                            <strong>JAM MULAI</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top:10px; padding-left:10px">
                                                            <asp:TextBox ID="TbMulai" runat="server" CssClass="form-control" MaxLength="5" 
                                                                Width="90px"></asp:TextBox>
                                                                <p></p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td style="background-color:#E1F0FF; padding-left:10px">
                                                            <strong>JAM SELESAI</strong></td>
                                                    </tr>
                                                    <tr>
                                                         <td style="padding-top:10px; padding-left:10px">
                                                            <asp:TextBox ID="TbSelesai" runat="server" CssClass="form-control" 
                                                                MaxLength="5" Width="90px"></asp:TextBox>
                                                                <p></p>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel ID="PanelJamMengajar" runat="server">
                                                    <table class="table-bordered table-condensed table">
                                                        <tr>
                                                            <td style="background-color: #E1F0FF; padding-left: 10px">
                                                                <strong>JAM MENGAJAR DOSEN</strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="padding-left:7px; padding-top:5px; padding-right:7px">
                                                                    <asp:GridView ID="GVJamMengajar" CssClass="table-bordered table" runat="server" CellPadding="4"
                                                                        ForeColor="#333333" GridLines="None">
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                                                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                                                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                                                        <SortedDescendingHeaderStyle BackColor="#820000" />
                                                                    </asp:GridView>
                                                                </div>                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelRuangAktif" runat="server">
                                                <table class="table-bordered table-condensed table">
                                                <tr>
                                                    <td style="background-color:#E1F0FF; padding-left:10px"><strong>JADWAL RUANG</strong></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    <div style="padding-left:7px; padding-top:5px; padding-right:7px">

                                                        <p></p>
                                                            <asp:GridView ID="GVRuangAktif" CssClass="table-bordered table" runat="server" CellPadding="4"
                                                                ForeColor="#333333" GridLines="None">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                                                <SortedDescendingHeaderStyle BackColor="#820000" />
                                                            </asp:GridView>
                                                        
                                                        </div>
                                                    </td>
                                                </tr>
                                                </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
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
                            </div>
                            <div class="panel-footer">
                            <asp:Label CssClass="hidden" ID="lbno_jadwal" runat="server" ForeColor="Transparent"></asp:Label>                                
                                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="Update" CssClass="btn btn-success" /> &nbsp;
                                <asp:Button ID="BtnCancel" runat="server" OnClick="BtnCancel_Click" Text="Cancel" CssClass="btn btn-danger" /> 

                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
