<%@ Page Title="" Language="C#" MasterPageFile="~/pasca/Pasca.Master" AutoEventWireup="true" CodeBehind="UpdateBiodata.aspx.cs" Inherits="Padu.pasca.UpdateBiodata" EnableEventValidation="false" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="content-header">
        <h1>Biodata Mahasiswa</h1>
    </div>
    <div id="content-container">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading ui-draggable-handle">
                        <strong>Form Input Biodata</strong></div>
                    <div class="panel-body">

                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Nomor Induk Kependudukan (NIK)</label>
                                <asp:TextBox ID="TbNIK" CssClass="form-control" runat="server"></asp:TextBox>
                                <small id="SmTbNIK" class="form-text text-muted">Nama sesuai Ijazah.</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Tempat Lahir</label>
                                <asp:TextBox ID="TbTmpLahir" CssClass="form-control" runat="server"></asp:TextBox>
                                <small id="SmTbTmpLahir" class="form-text text-muted">Kota/Kabupaten Tempat Lahir</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Tanggal Lahir</label>
                                <asp:TextBox ID="TBTtl" CssClass="form-control" runat="server"></asp:TextBox> 
                                <small id="SmTBTtl" class="form-text text-muted">Contoh : 1985-02-23 (tahun-bulan-tanggal)</small>                              
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Jenis Kelamin</label>
                                <asp:DropDownList ID="DLGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Jenis Kelamin</asp:ListItem>
                                    <asp:ListItem>Laki-laki</asp:ListItem>
                                    <asp:ListItem>Perempuan</asp:ListItem>
                                </asp:DropDownList>
                                <small id="SmDLGender" class="form-text text-muted">Jenis Kelamin</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Agama</label>
                                <asp:DropDownList ID="DLAgama" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Agama</asp:ListItem>
                                    <asp:ListItem>Islam</asp:ListItem>
                                    <asp:ListItem>Protestan</asp:ListItem>
                                    <asp:ListItem>Katholik</asp:ListItem>
                                    <asp:ListItem>Hindu</asp:ListItem>
                                    <asp:ListItem>Budha</asp:ListItem>
                                    <asp:ListItem>Konghucu</asp:ListItem>
                                </asp:DropDownList>   
                                <small id="SmDLAgama" class="form-text text-muted">Pilih Agama</small>                            
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Provinsi</label>
                                <asp:DropDownList ID="DropDownListProv" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownProv" runat="server" Category="id_provinsi"
                                    TargetControlID="DropDownListProv" ServicePath="~/web_services/ServiceCS.asmx"
                                    ServiceMethod="Prov" PromptText="PROVINSI">
                                </asp:CascadingDropDown>
                                <small id="SmDropDownListProv" class="form-text text-muted">Provinsi Tempat Tinggal</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Kota/Kabupaten</label>
                                <asp:DropDownList ID="DropDownListKab" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownKotakab" runat="server" TargetControlID="DropDownListKab"
                                    ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Kotakab" ParentControlID="DropDownListProv"
                                    LoadingText="LOADING" PromptText="KOTA/KABUPATEN" Category="id_kotakab">
                                </asp:CascadingDropDown>
                                <small id="SmDropDownListKab" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Kecamatan</label>
                                <asp:DropDownList ID="DropDownListKec" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownKec" runat="server" TargetControlID="DropDownListKec"
                                    ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Kecamatan" ParentControlID="DropDownListKab"
                                    LoadingText="LOADING" PromptText="KECAMATAN" Category="id_kecamatan">
                                </asp:CascadingDropDown>
                                <small id="SmDropDownListKec" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Desa/Kelurahan</label>
                                <asp:DropDownList ID="DropDownListDesa" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:CascadingDropDown ID="CascadingDropDownDesa" runat="server" TargetControlID="DropDownListDesa"
                                    ServicePath="~/web_services/ServiceCS.asmx" ServiceMethod="Desa" ParentControlID="DropDownListKec"
                                    LoadingText="LOADING" PromptText="DESA" Category="id_desa">
                                </asp:CascadingDropDown>
                                <small id="SmDropDownListDesa" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Alamat Lengkap</label>
                                <asp:TextBox ID="TbAlamat" placeholder="Alamat Rumah Lengkap ..." 
                                    runat="server" MaxLength="200" TextMode="MultiLine" CssClass="form-control" 
                                    ></asp:TextBox>
                                <small id="SmTbAlamat" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Kode POS</label>
                               <asp:TextBox ID="TbKdPOS" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                <small id="SmTbKdPOS" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">No Handphone</label>
                                <asp:TextBox ID="TBHp" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                <small id="SmTBHp" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Email</label>
                               <asp:TextBox ID="TbEmail" placeholder="email aktif " runat="server" 
                                    MaxLength="120" CssClass="form-control"></asp:TextBox>
                                <small id="SmTbEmail" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                            <asp:Button ID="BtnSvBiodata" CssClass="btn btn-primary" runat="server" Text="Simpan" OnClick="BtnSvBiodata_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
