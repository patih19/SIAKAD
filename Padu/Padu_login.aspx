<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Padu_login.aspx.cs" Inherits="Padu.Padu_login" EnableViewStateMac="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            function preventBack() { window.history.forward(); }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
    </script>
    <!-- Page Load -->
    <script type="text/javascript">
        $(window).load(function () {
            $("#pageloaddiv").fadeOut(1000);
        });
    </script>

    <!-- CSS Class -->
    <style type="text/css">
        #pageloaddiv
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 1000;
            background: url('images/loading135.gif') no-repeat center center;
            background-color:Gray;
            filter:alpha(opacity=20);
            opacity:0.5;
        }
        .style4
        {
            color: #808080;
        }
    </style>
</head>
<body>
 <form id="form1" runat="server">
     <!-- ----------------------- Marque ------------------------- -->
        <div style=" background-color: #333399">
            <marquee direction="left" style="color: #FFFFFF; font-size: large;"><div>Aktifasi Pada Sistem SIPADU Hanya Untuk <strong>Biaya Semester & Skripsi</strong>, Aktifasi <strong>Biaya Akhir (KP, KKN, PPL, TA & Wisuda)</strong>   Dapat Menghubungi Langsung Di Bagian Pelayanan Keuangan </div></marquee>
        </div>
    <!-- -------------- End Margue -------------------------------- -->
    <!-- loading div to show loading image -->
    <div id="pageloaddiv"></div>
    <!--  end load -->
    <div class="container">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
            <br />
            <br />
                <div class="panel panel-info">
                    <div class="panel-heading">
                        LOGIN SIPADU</div>
                    <div class="panel-body">
                        <div class="account-wall">
                            <div class="form-signin">
                                <asp:TextBox CssClass="form-control" placeholder="NPM" ID="TextBoxUsername" runat="server"></asp:TextBox>
                                <br />
                                <asp:TextBox CssClass="form-control" placeholder="Password" ID="TextBoxPasswd" runat="server"
                                    TextMode="Password"></asp:TextBox>
                                <br />
                                <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Sign in"
                                    OnClick="Button1_Click" />
                                &nbsp;<br /><br />
                                <asp:Label ID="LoginResult" runat="server"></asp:Label>
                                <br />
                                <br />
                                <span class="style4">SISTEM INFORMASI TERPADU MAHASISWA<br />
                                    Copyright &copy; 2014, Puskominfo Untidar</span></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
