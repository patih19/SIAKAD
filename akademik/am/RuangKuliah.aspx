<%@ Page Title="" Language="C#" MasterPageFile="~/am/admin.Master" AutoEventWireup="true" CodeBehind="RuangKuliah.aspx.cs" Inherits="akademik.am.WebForm41" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.3.3.6.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/DataTables/dataTables.bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../Scripts/DataTables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../Scripts/DataTables/dataTables.bootstrap.min.js" type="text/javascript"></script>

    <style type="text/css">
        table#ctl00_ContentPlaceHolder1_GVRuang tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(51, 123, 102); }
        table#ctl00_ContentPlaceHolder1_GVRuang tbody tr:nth-child(odd){ background-color :#fff;}
        table#ctl00_ContentPlaceHolder1_GVRuang tbody tr:nth-child(odd){ background-color :#EEF7EE;}
        
        table#TabelEditRuang tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(51, 123, 102); }
        table#TabelEditRuang tbody tr:nth-child(odd){ background-color :#fff;}
        table#TabelEditRuang tbody tr:nth-child(odd){ background-color :#EEF7EE;}
        
        table#TabelAddRuang tr:hover { background-color :#d9edf7; }
        th { color:White !important; background-color:rgb(51, 123, 102); }
        table#TabelAddRuang tbody tr:nth-child(odd){ background-color :#fff;}
        table#TabelAddRuang tbody tr:nth-child(odd){ background-color :#EEF7EE;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <div class="container top-main-form" style="min-height: 450px; background-color: rgb(242, 242, 255);
        box-shadow: 0px 0px 200px rgba(82, 124, 159, 0.25), 0px 1px 2px rgba(0, 0, 0, 0.19);">
        <div class="row">
            <div class="col-xs-12 col-md-12 col-lg-12">
                <div>
                    <br />
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>MASTER RUANG</strong></div>
                        <div class="panel-body">
                        <table class="table-condensed">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="DLMasterProdi" runat="server" CssClass="form-control" 
                                            AutoPostBack="True" onselectedindexchanged="DLMasterProdi_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Program Studi</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="BtnAddRuang" CssClass="btn btn-success" runat="server" 
                                            Text="Tambah Ruang" onclick="BtnAddRuang_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <asp:Panel ID="PanelDataRuang" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading ui-draggable-handle">
                            <strong>DAFTAR RUANG</strong></div>
                        <div class="panel-body">
                            
                            <asp:GridView ID="GVRuang"  
                                CssClass="table table-condensed table-bordered table-striped table-hover" 
                                runat="server" onrowcreated="GVRuang_RowCreated" 
                                onrowdatabound="GVRuang_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEdit" runat="server" Text="update" onclick="BtnEdit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                        </div>
                    </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="PanelEditRuang" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Update Ruang</strong></div>
                            <div class="panel-body">
                                <table class="table-bordered table" id="TabelEditRuang">
                                    <tr>
                                        <td>
                                            Ruang</td>
                                        <td>
                                            <asp:TextBox ID="TbEditNamaRuang" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: middle">
                                            Kapasitas</td>
                                        <td>
                                            <asp:TextBox ID="TbEditKapasitas" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: middle">
                                            Program Studi</td>
                                        <td>
                                            <asp:DropDownList ID="DLEditProdi" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="-1">Program Studi</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Keterangan</td>
                                        <td>
                                            <asp:RadioButton ID="RbEditAktif" runat="server" GroupName="KeteranganUpdate" 
                                                Text="Aktif" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="RbEditNonAktif" runat="server" 
                                                GroupName="KeteranganUpdate" Text="Non Aktif" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel-footer">
                                <asp:Button CssClass="btn btn-success" ID="BtnEditSave" runat="server" Text="Simpan"
                                    OnClick="BtnEditSave_Click" />
                                &nbsp;<asp:Button CssClass="btn btn-danger" ID="BtnEditBatal" runat="server" Text="Batal"
                                    OnClick="BtnEditBatal_Click" />
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PanelAddRuang" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-heading ui-draggable-handle">
                                <strong>Tambah Ruang</strong></div>
                            <div class="panel-body">
                                <table class="table-bordered table" id="TabelAddRuang">
                                    <tr>
                                        <td>
                                            Ruang</td>
                                        <td>
                                            <asp:TextBox ID="TbAddRuang" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: middle">
                                            Kapasitas</td>
                                        <td>
                                            <asp:TextBox ID="TbAddKapasitas" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: middle">
                                            Program Studi</td>
                                        <td>
                                            <asp:DropDownList ID="DLAddProdi" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="-1">Program Studi</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel-footer">
                                <asp:Button CssClass="btn btn-success" ID="BtnAddSave" runat="server" 
                                    Text="Simpan" onclick="BtnAddSave_Click" />
                                &nbsp;<asp:Button CssClass="btn btn-danger" ID="BtnAddBatal" runat="server" 
                                    Text="Batal" onclick="BtnAddBatal_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
