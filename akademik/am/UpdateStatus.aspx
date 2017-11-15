<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="UpdateStatus.aspx.cs" Inherits="akademik.am.UpdateStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .style2
        {
            color: #FF3300;
        }
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container top-atas" style="min-height: 450px; background-color: rgba(36, 134, 17, 0.06); box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelBdk" CssClass=" panel panel-default" runat="server" BackColor="#FFFF99">
                            <p></p>
                            <ul>
                                <li>Update status mahasiswa dilakukan setelah proses KRS selesai dilaksanan pada semester aktif berjalan</li>
                                <li>Update status mahasiswa dilakukan maksimal satu minggu setelah masa KRS berakhir</li>
                                <li>Mahasiswa yang tidak mengajukan cuti kuliah akan dicatat sebagai mahasiswa Non Aktif</li>
                            </ul>
                            <p></p>
                        </asp:Panel>

                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Status Update Data Mahasiswa</strong>
                            </div>
                            <div class="panel-body">
                                <asp:Label ID="LbMsgUpdate" runat="server" Style="font-weight: 700"></asp:Label>
                                <%--<asp:Button ID="BtnUpdateStatus" runat="server" OnClick="BtnUpdateStatus_Click" Text="Update" />--%>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-body">
                                <asp:GridView ID="GvSemUpdate" CssClass="table table-condensed" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="mdl">
                            <div class="center">
                                <img alt="" src="../images/loading135.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>
</asp:Content>
