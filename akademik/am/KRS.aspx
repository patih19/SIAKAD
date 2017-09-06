<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="KRS.aspx.cs" Inherits="akademik.am.WebForm22"  EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%--<style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }
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
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Kartu Rencana Studi (KRS)</strong></div>
                    <div class="panel-body">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    NPM
                                </td>
                                <td>
                                    <asp:TextBox ID="TBNpm" runat="server" MaxLength="10" Width="130px" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="table-condensed">
                                        <!----------------------- TAHUN NAGKATAN -------------------------->
                                        <tr>
                                            <td>
                                                <asp:Label ID="LbNpm" runat="server">NPM</asp:Label>
                                            </td>
                                            <td>
                                                <!-- Foto here -->
                                                <asp:Label ID="LbNama" runat="server">Nama</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LbProdi" runat="server">Program Studi</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LbKelas" runat="server">Kelas</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LbTahun" runat="server">Tahun</asp:Label>
                                            </td>
                                            <td style="opacity: 0">
                                                <asp:Label ID="LbIdProdi" runat="server" ForeColor="Transparent"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Jenis
                                </td>
                                <td>
                                    <%--<asp:RadioButton ID="RBInputKRS" runat="server" Text="Input KRS" GroupName="KRS" />--%>
                                    &nbsp;<asp:RadioButton ID="RBEditKRS" runat="server" Text="Edit KRS" GroupName="KRS" />
                                    &nbsp;<em>( hanya bisa 1x )
                                        <asp:RadioButton ID="RBList" runat="server" Text="Lihat KRS" GroupName="KRS" />
                                        &nbsp; </em>
                                </td>
                            </tr>
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
                                                <%--<ajaxToolkit:CascadingDropDown ID="CascadingDLTahun" runat="server" TargetControlID="DLTahun"
                                                Category="DLTahun" ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="semester"
                                                LoadingText="Loading" PromptText="Tahun">
                                            </ajaxToolkit:CascadingDropDown>--%>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control">
                                                    <asp:ListItem>Semester</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="BtnFilterMhs" CssClass="btn btn-primary" runat="server" Text="OK"
                            OnClick="BtnFilter_Click" />
                    </div>
                </div>
                <asp:Label ID="LbResult" runat="server" Text=""></asp:Label>
                <asp:Panel ID="PanelKRS" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <hr />
                            <strong>Jumlah SKS =</strong>
                            <asp:Label ID="LbJumlahSKS" runat="server" Style="font-weight: 700"></asp:Label>
                            <br />
                            <asp:GridView ID="GVAmbilKRS" runat="server" CellPadding="4" ForeColor="#333333"
                                GridLines="None" CssClass="table table-striped" OnRowDataBound="GVAmbilKRS_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CBMakul" runat="server" AutoPostBack="true" OnCheckedChanged="CBMakul_CheckedChanged" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Pilih
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="LbSisa" runat="server" Style="font-weight: 700"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Sisa
                                        </HeaderTemplate>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePnlSaceKRS" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnSimpan" runat="server" Text="Simpan" OnClick="BtnSimpan_Click"
                                OnClientClick="return confirm('Anda Yakin Data Tersebut Benar ?');" class="btn btn-default"
                                CssClass="btn btn-primary" />
                            &nbsp;<asp:Label ID="LbPostSuccess" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </asp:Panel>
                <asp:Panel ID="PanelEditKRS" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <hr />
                            <span class="gr"><strong>Batal Tambah/Edit KRS Hanya Bisa Dilakukan 1x !!</strong></span><br />
                            <br />
                            <strong>Jumlah SKS =</strong>
                            <asp:Label ID="LbJumlahEditSKS" runat="server" Style="font-weight: 700"></asp:Label>
                            <br />
                            <asp:GridView ID="GVEditKRS" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                CssClass="table table-striped" OnRowDataBound="GVEditKRS_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CBEdit" runat="server" OnCheckedChanged="CBEdit_CheckedChanged"
                                                AutoPostBack="True" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Pilih
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Sisa
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbSisa" runat="server" Style="font-weight: 700"></asp:Label>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click"
                        CssClass="btn btn-primary" OnClientClick="return confirm('Anda Yakin Data Tersebut Benar ?');" />
                </asp:Panel>
                <asp:Panel ID="PanelListKRS" runat="server">
                    <hr />
                    DATA KRS<asp:GridView ID="GVListKrs" runat="server" CssClass="table table-striped"
                        CellPadding="4" ForeColor="#333333" GridLines="None" 
                        OnRowDataBound="GVListKrs_RowDataBound" ShowFooter="True">
                        <AlternatingRowStyle BackColor="White" />
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
                </asp:Panel>
                <br />
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
                    <ProgressTemplate>
                        <div class="mdl">
                            <div class="center">
                                <img alt="" src="../images/loading135.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
