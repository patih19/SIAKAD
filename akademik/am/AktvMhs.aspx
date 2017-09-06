<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="AktvMhs.aspx.cs" Inherits="akademik.am.WebForm29" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
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
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
            <br />
                <div id="Tabs" role="tabpanel">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li><a href="#satu"  role="tab" data-toggle="tab">Rekap Prodi</a></li>
                        <li><a href="#dua"  role="tab" data-toggle="tab">Rekap Mahasiswa</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content" style="padding: 15px 15px;background-color:White;">
                        <div role="tabpanel" class="tab-pane active" id="satu">
                            <!-- Isi Tab Pertama -->
                            <div class="panel panel-default">
                                <div class="panel-heading ui-draggable-handle">
                                    <strong>Rekap Aktifitas Perkuliahan Program Studi</strong></div>
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
                                                <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DLProdi_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%-- <div class="panel-footer">
                                     &nbsp;<asp:Button ID="BtnAktvMhs" runat="server" Text="Buat Rekap" OnClick="BtnAktvMhs_Click"
                                            CssClass="btn btn-primary" />
                                        <asp:Label ID="LbJadwalResult" runat="server"></asp:Label> 
                                    </div> --%>
                            </div>
                            <asp:GridView ID="GVAktvMhs" runat="server" CssClass=" table table-condensed table-bordered"
                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                onrowcreated="GVAktvMhs_RowCreated" 
                                onrowdatabound="GVAktvMhs_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            &nbsp;<asp:LinkButton ID="LnkEdit" runat="server" onclick="LnkEdit_Click">Edit</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Edit
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
                                                    NPM</td>
                                                <td>
                                                    <asp:Label ID="LbNPM" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Nama</td>
                                                <td>
                                                    <asp:Label ID="LbNama" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Semester</td>
                                                <td>
                                                    <asp:Label ID="LbSemester" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    IPK *</td>
                                                <td>
                                                    <asp:TextBox ID="TbIPK" runat="server" Placeholder="Isi Angka" 
                                                        CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    SKS Total *</td>
                                                <td>
                                                    <asp:TextBox ID="TbSKSTotal" runat="server" Placeholder="Isi Angka" 
                                                        CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="panel-footer">
                                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
                                            OnClientClick="return confirm('Anda Yakin Data Sudah Benar ?');" 
                                            onclick="BtnUpdate_Click" />
                                        &nbsp;<asp:Label ID="LbIdMakul" runat="server" CssClass="hidden"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <!--  --------- End Modal ---------  -->

                        </div>
                        <div role="tabpanel" class="tab-pane" id="dua">
                            <!-- Isi Tab Kedua -->
                                <div class="panel panel-default">
                                    <div class="panel-heading ui-draggable-handle">
                                        <strong>Aktivitas Kuliah Mahasiswa</strong> </div>
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
                                                                <asp:DropDownList ID="DLTahun2" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <ajaxToolkit:CascadingDropDown ID="CascadingDLTahun2" TargetControlID="DLTahun2" runat="server"
                                                                    Category="DLTahun2" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                                    LoadingText="Loading" PromptText="Tahun">
                                                                </ajaxToolkit:CascadingDropDown>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                                    <asp:ListItem>Semester</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    NPM
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TBNPM" runat="server" CssClass="form-control" Placeholder="Isi NPM Mahasiswa"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LbSrcMhs" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="panel-footer">
                                        &nbsp;<asp:Button ID="Button1" runat="server" Text="Update" OnClick="BtnAktvMhs_Click"
                                            CssClass="btn btn-primary" />
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <asp:GridView ID="GridView1" runat="server" CssClass="table-condensed table-bordered"
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
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
                </div>

            </div>
            <br />
        </div>
    </div>
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "satu";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>
    </strong>
    </div>
</asp:Content>