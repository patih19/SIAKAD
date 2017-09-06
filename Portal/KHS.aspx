<%@ Page Title="" Language="C#" MasterPageFile="~/TU.Master" AutoEventWireup="true" CodeBehind="KHS.aspx.cs" Inherits="Portal.WebForm21" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <strong>REKAP PENGAMBILAN SKS</strong></div>
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
                                                <asp:ListItem>Tahun</asp:ListItem>
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                                <asp:ListItem>2018</asp:ListItem>
                                                <asp:ListItem>2019</asp:ListItem>
                                                <asp:ListItem>2020</asp:ListItem>
                                                <asp:ListItem>2021</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DLSemester" runat="server" CssClass="form-control">
                                                <asp:ListItem>Semester</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Program Studi</td>
                            <td>
                                    <asp:DropDownList ID="DLProdi" runat="server" CssClass="form-control" 
                                        Width="400px">
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
                    </div>
                    <div class="panel-footer">
                                <asp:Button ID="BtnOK" runat="server" onclick="BtnOK_Click" Text="OK" 
                                    CssClass="btn btn-primary" />
                    </div>
                </div>
                <asp:Panel ID="PanelRekapSKS" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            Daftar Mahasiswa Aktif KRS</div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tr>
                                    <td>
                                        <span class="style111">Program Studi </span>
                                    </td>
                                    <td>
                                        &nbsp;:
                                        <asp:Label ID="LbIdProdi" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="LbProdi" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tahun / Semester
                                    </td>
                                    <td>
                                        &nbsp;:
                                        <asp:Label ID="LbTahun" runat="server"></asp:Label>
                                        &nbsp;/
                                        <asp:Label ID="LbSemester" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="GVRekapSKS" runat="server" CssClass="table table-bordered"
                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                onrowcreated="GVRekapSKS_RowCreated">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="HKS">
                                        <HeaderTemplate>
                                            KHS
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkKHS" runat="server" onclick="LnkKHS_Click">Lihat</asp:LinkButton>
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
                </asp:Panel>
            </div>
            <br />
            <br />
            <!-- -------- latihan modal -------- -->
            <asp:LinkButton ID="lnkFake" runat="server" CssClass="hidden"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlPopup"
                TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="BtnClose">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" Width="50%">
                <!-- Style="display: none" -->
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <a id="BtnClose" href="#"><span class="glyphicon glyphicon-eye-close"></span>Close
                        </a>
                    </div>
                    <div class="panel-body">
                        <table>
                            <tr>
                                <td>
                                    NPM
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LbNPM" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nama
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LbNama" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Semester
                                </td>
                                <td>
                                    &nbsp;:&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LbSem" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GVKHS" runat="server" 
                            CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" 
                            GridLines="None" onrowdatabound="GVKHS_RowDataBound">
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
                        <strong>Jumlah SKS :</strong>
                        <asp:Label ID="LBSks" runat="server"></asp:Label>
                        <br />
                        <strong>IP Semester :</strong>
                        <asp:Label ID="LbIPS" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <!--  --------- End Modal ---------  -->
        </div>
    </div>
</asp:Content>
