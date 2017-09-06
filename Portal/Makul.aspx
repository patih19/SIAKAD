<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="Makul.aspx.cs" Inherits="Portal.WebForm16" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_GVMakul').DataTable({
                'iDisplayLength': 200,
                'aLengthMenu': [[200, 300, 400, 500, 600, 700, -1], [200, 300, 400, 500, 600, 700, "All"]],
                language: {
                    search: "Pencarian :",
                    searchPlaceholder: "Ketik Kata Kunci"
                }
            });
        });
    </script>
    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVMakul tr:hover
        {
            background-color: #d9edf7;
        }
        th
        {
            color: White !important;
            background-color: rgb(51, 123, 102);
        }
        table#ctl00_ContentPlaceHolder1_GVMakul tbody tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table#ctl00_ContentPlaceHolder1_GVMakul tbody tr:nth-child(odd)
        {
            background-color: #EEF7EE;
        }
    </style>
    <style type="text/css">
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Daftar Mata Kuliah</strong> 
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="GVMakul" runat="server" 
                            CssClass="table table-bordered" OnRowCreated="GVMakul_RowCreated" OnRowDataBound="GVMakul_RowDataBound"
                            onprerender="GVMakul_PreRender">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkEdit" runat="server" OnClick="LnkEdit_Click">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <!-- -------- latihan modal -------- -->
            <asp:LinkButton ID="lnkFake" runat="server" CssClass="hidden"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlPopup"
                TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="BtnClose">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" Width="60%">
                <!-- Style="display: none" -->
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <a id="BtnClose" href="#"><span class="glyphicon glyphicon-eye-close"></span>Close
                        </a>
                    </div>
                    <div class="panel-body">
                        <table class="table">
                            <tr>
                                <td>
                                    Mata Kuliah *</td>
                                <td>
                                    <asp:TextBox ID="TbMakul" runat="server" Placeholder="Isi Nama Mata Kuliah" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tatap Muka *</td>
                                <td>
                                    <asp:TextBox ID="TbTtpMuka" runat="server" Placeholder="Isi Angka" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Praktikum *</td>
                                <td>
                                    <asp:TextBox ID="TbPrak" runat="server"  Placeholder="Isi Angka" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Laporan *</td>
                                <td>
                                    <asp:TextBox ID="TbLaporan" runat="server" Placeholder="Isi Angka" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SKS Mata Kuliah *</td>
                                <td>
                                    <asp:TextBox ID="TbSKS" runat="server" CssClass="form-control" 
                                        Placeholder="Isi Angka"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pilihan/Wajib *</td>
                                <td>
                                    <asp:DropDownList ID="DlStatusMK" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Jenis Mata Kuliah</asp:ListItem>
                                        <asp:ListItem Value="A">Wajib</asp:ListItem>
                                        <asp:ListItem Value="B">Pilihan</asp:ListItem>
                                        <asp:ListItem Value="S">TA/Skripsi/Tesis/Disertasi</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Simpan"
                            OnClientClick="return confirm('Anda Yakin Data Sudah Benar ?');" OnClick="BtnUpdate_Click" />
                        &nbsp;<asp:Label ID="LbIdMakul" runat="server" CssClass="hidden" ></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <!--  --------- End Modal ---------  -->


            <!-- -------- latihan modal -------- -->
            <asp:LinkButton ID="lnkFake2" runat="server" CssClass="hidden"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlPopup2"
                TargetControlID="lnkFake2" BackgroundCssClass="modalBackground" CancelControlID="BtnClose2">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopup2" runat="server" Width="60%">
                <!-- Style="display: none" -->
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <a id="BtnClose2" href="#"><span class="glyphicon glyphicon-eye-close"></span>Close
                        </a>
                    </div>
                    <div class="panel-body">
                        <table class="table">
                            <tr>
                                <td>
                                    Kode Makul*</td>
                                <td>
                                    <asp:TextBox ID="TbKdMakul2" runat="server" Placeholder="Isi Kode Makul" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Mata Kuliah *</td>
                                <td>
                                    <asp:TextBox ID="TbMakul2" runat="server" Placeholder="Isi Nama Mata Kuliah" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    Semester *</td>
                                <td>
                                    <asp:TextBox ID="TbSemester2" runat="server" Placeholder="Semeseter (1-8)" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tatap Muka *</td>
                                <td>
                                    <asp:TextBox ID="TbTtpMuka2" runat="server" Placeholder="Isi Angka" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Praktikum *</td>
                                <td>
                                    <asp:TextBox ID="TbPrak2" runat="server"  Placeholder="Isi Angka" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Laporan *</td>
                                <td>
                                    <asp:TextBox ID="TbLaporan2" runat="server" Placeholder="Isi Angka" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SKS Mata Kuliah *</td>
                                <td>
                                    <asp:TextBox ID="TbSKS2" runat="server" CssClass="form-control" 
                                        Placeholder="Isi Angka"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pilihan/Wajib *</td>
                                <td>
                                    <asp:DropDownList ID="DlStatusMK2" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">Jenis Mata Kuliah</asp:ListItem>
                                        <asp:ListItem Value="A">Wajib</asp:ListItem>
                                        <asp:ListItem Value="B">Pilihan</asp:ListItem>
                                        <asp:ListItem Value="S">TA/Skripsi/Tesis/Disertasi</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="BtnUpdate2" CssClass="btn btn-primary" runat="server" Text="Simpan"
                            OnClientClick="return confirm('Anda Yakin Data Sudah Benar ?');" OnClick="BtnUpdate2_Click" />
                        &nbsp;<asp:Label ID="LbIdMakul2" runat="server" CssClass="hidden" ></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <!--  --------- End Modal ---------  -->

        </div>
    </div>
</asp:Content>
