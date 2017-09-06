<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="FJadwalKuliah.aspx.cs" Inherits="akademik.am.WebForm40" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.3.3.6.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVJadwal tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(51, 123, 102); }
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd){ background-color :#fff;}
        table#ctl00_ContentPlaceHolder1_GVJadwal tbody tr:nth-child(odd){ background-color :#EEF7EE;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>Jadwal Perkuliahan (FEEDER)</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        Tahun / Semester
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="DLTahun" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" TargetControlID="DLTahun" runat="server"
                                                        Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                        LoadingText="Loading" PromptText="Tahun">
                                                    </ajaxToolkit:CascadingDropDown>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DlSemester" runat="server" CssClass="form-control">
                                                        <asp:ListItem>Semester</asp:ListItem>
                                                        <asp:ListItem>1</asp:ListItem>
                                                        <asp:ListItem>2</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Program Studi
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="panel-footer">
                            &nbsp;<asp:Button ID="BtnJadwal" runat="server" Text="OK" OnClick="BtnJadwal_Click"
                                CssClass="btn btn-primary" />
                            <asp:Label ID="LbJadwalResult" runat="server"></asp:Label>
                        </div>
                    </div>                    
                    <asp:Panel ID="PanelJadwalKuliah" runat="server">
                        <hr />
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                Daftar Mata Kuliah</div>
                            <div class="panel-body">
                                <asp:Panel ID="PanelJadwal" runat="server">
                                    <asp:GridView ID="GVJadwal" runat="server" CssClass="table table-condensed table-bordered table-striped table-hover"
                                        OnRowDataBound="GVJadwal_RowDataBound" OnPreRender="GVJadwal_PreRender">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Add
                                                </HeaderTemplate>
                                            </asp:TemplateField>
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
                                                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Delete
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="clearfix">
                                    </div>
                                    <asp:Label ID="LbThn" runat="server" ForeColor="Transparent"></asp:Label>
                                    &nbsp;<asp:Label ID="LbSmstr" runat="server" ForeColor="Transparent"></asp:Label>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="PanelEditJadwal" runat="server">
                                    <asp:Label ID="Label3" runat="server" Style="font-weight: 700; font-size: small;
                                        font-family: Arial, Helvetica, sans-serif;" Text="EDIT MATA KULIAH (FEEDER)"></asp:Label>
                                    <br />
                                    <br />
                                    <table class="table-bordered table-condensed">
                                        <tr>
                                            <td style="vertical-align: top">
                                                Mata Kuliah <em><span class="style1">&nbsp;</span></em>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;<asp:Label ID="LbProdi" runat="server" Font-Size="Medium" ForeColor="#333399"
                                                    Style="font-size: 14px"></asp:Label>
                                                &nbsp;<asp:Label ID="LbKodeMakul" runat="server" Font-Size="Medium" ForeColor="#333399"
                                                    Style="font-size: 14px"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="LbMakul" runat="server" Font-Size="Medium" ForeColor="#333399" Style="font-size: 14px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">
                                                Dosen Pengajar
                                            </td>
                                            <td>
                                                <asp:Panel ID="PanelDosen" runat="server">
                                                    <table class="table-condensed">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LbNidn" runat="server" Font-Size="Medium" ForeColor="#333399" Style="font-size: 14px"></asp:Label>
                                                                &nbsp;<asp:Label ID="LbDosen" runat="server" Font-Size="Medium" ForeColor="#333399"
                                                                    Style="font-size: 14px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="DLProdiDosen" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                    OnSelectedIndexChanged="DLProdiDosen_SelectedIndexChanged">
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
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel ID="PanelDetailDosen" runat="server">
                                                        <br />
                                                        <table class="table-condensed">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GVDosen" runat="server" CellPadding="4" CssClass="table-condensed table-bordered"
                                                                        ForeColor="#333333" GridLines="None">
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
                                            <td>
                                                Pembagian Kelas
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DLKelas" runat="server" CssClass="form-control" Width="90px">
                                                    <asp:ListItem>Kelas</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbno_jadwal" runat="server" ForeColor="Transparent"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnSave" runat="server" Text="Update" OnClick="BtnSave_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <div class="panel-footer">
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <br />
            </div>
            <br />
        </div>
    </div>

    <script type="text/javascript">
        $('#ctl00_ContentPlaceHolder1_GVJadwal').DataTable({
            'iDisplayLength': 25,
            'aLengthMenu': [[25, 50, 100, 200, 300, 400, 500, 600, 700, 1000, 1250, 1500, 2000, 4000, 5000, -1], [25, 50, 100, 200, 300, 400, 500, 600, 700, 1000, 1250, 1500, 2000, 4000, 5000, "All"]],
            language: {
                search: "Pencarian :",
                searchPlaceholder: "Ketik Kata Kunci"
            }
        });
    </script>

</asp:Content>
