<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="keu-login.aspx.cs" Inherits="Keuangan.keu_login" EnableViewStateMac="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/Login.css" rel="stylesheet" type="text/css" />
        <script type = "text/javascript" >
            history.pushState(null, null, 'keu-login.aspx');
            window.addEventListener('popstate', function (event) {
                history.pushState(null, null, 'keu-login.aspx');
            });
    </script>
     <style type="text/css">
        .style4
        {
            color: #808080;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <br /> <br />
	<section id="content">
			<h1 class="style1">Login Form</h1>
			<div class="panel panel-success" >
            <!--    <div class="panel-heading">
                    <h3 class="panel-title">
                        <strong><i>LOGIN FORM</i></strong></h3>
                </div> -->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12 col-xs-12">
                            <br />
                            <div class="breadcrumb" >
                                <div class="form-group">
                                    <span >Username :</span>
                                    <asp:TextBox ID="TextBoxUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <span>Password :</span>
                                    <asp:TextBox ID="TextBoxPasswd" runat="server" CssClass="form-control" 
                                        TextMode="Password"></asp:TextBox>
                                </div>
                                <asp:ImageButton ID="ImageButtonLogin" runat="server" 
                                    ImageUrl="~/images/login-btn.png" onclick="ImageButtonLogin_Click" 
                                    Height="31px" Width="79px" /> <br />
                                <asp:Label ID="LoginResult" runat="server" ></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        <div class="button">
            <h4>
                <strong><i>Sistem Informasi Pembayaran</i></strong></h4> <br />
        <span class="style4">
                         Copyright &copy; 2014, Puskominfo Untidar</span></div>
        </div><!-- button -->
	</section><!-- content -->
</div><!-- container -->
    </form>
</body>
</html>
