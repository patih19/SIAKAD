<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="Portal.Log" EnableViewStateMac="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Portal Login</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <section class="container">
            <asp:Image ID="Image1" runat="server"
            ImageUrl="~/images/Untidar Kemriting.png" />
            <br />
        <br />
            <div class="login">
                <h1>
                    Login Portal Akademik</h1>
                <p>
                    <asp:TextBox ID="TbUser" placeholder="Username Operator Program Studi" runat="server"></asp:TextBox></p>
                <p>
                    <asp:TextBox ID="TBPasswd" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox></p>
                <p class="remember_me">
                    <label>
                        <input type="checkbox" name="remember_me" id="Checkbox1">
                        Remember me on this computer
                    </label>
                </p>
                <p class="submit">
                    <asp:Button ID="BtnLogin" runat="server" Text="Login" 
                        onclick="BtnLogin_Click" /></p>
            </div>
            <div class="login-help">
                <p>
                    Forgot your password? <a href="<%= Page.ResolveUrl("~/Log.aspx") %>">Click here to reset it</a>.</p>
            </div>
        </section>
        <section class="about">
           <!-- <p class="about-links">
                <a href="http://www.cssflow.com/snippets/login-form" target="_parent">View Article</a>
                <a href="http://www.cssflow.com/snippets/login-form.zip" target="_parent">Download</a>
            </p> -->
            <p class="about-author">
                &copy; 2014&ndash;2015, TIM IT PUSKOMINFO UNTIDAR
            </p>
                <!-- <a href="http://www.cssflow.com/mit-license" target="_blank">MIT License</a>
                Original PSD by <a href="http://www.premiumpixels.com/freebies/clean-simple-login-form-psd/"
                    target="_blank">Orman Clark</a> -->
        </section>
    </div>
    </form>
</body>
</html>
