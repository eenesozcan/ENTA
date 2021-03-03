<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Giris.aspx.cs" Inherits="EnvanterTveY.Giris" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  
    <script src="../Scripts/jquery-3.5.1.js"></script>
    <link href="Content/giris.css" rel="stylesheet" />

    <meta http-equiv="refresh" content="300">
     <link id="Link2" runat="server" rel="icon" href="images/turksat-logo2.png" type="image/ico" /> 
    <style>

        .login-container {
            margin-top: 5%;
            margin-bottom: 5%;
        }
        .login-form-1 {
    padding: 5%;
        padding-right: 5%;
        padding-left: 5%;
    box-shadow: 0 5px 8px 0 rgba(0, 0, 0, 0.2), 0 9px 26px 0 rgba(0, 0, 0, 0.19);
}

    </style>
 </head>
<body>
    
<link href='https://fonts.googleapis.com/css?family=Source+Sans+Pro' rel='stylesheet' type='text/css'>

    <form id="form1" runat="server">
    <div>
        <img src="/images/turksat-logo2.png" style="width:350px;height: 100px" alt="Kablo">

            <br />

        <asp:Panel ID="Panel1" DefaultButton="Button1" runat="server">
       
         <h4>  <asp:Label ID="lblprogramadi" runat="server" Text="Label"></asp:Label> </h4>
        
        <asp:TextBox ID="txtkullaniciadi" placeholder="Kullanıcı Adı"  autocomplete="off" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>

            <asp:TextBox ID="txtsifre"  placeholder="Şifre"  autocomplete="off" runat="server" 
            TextMode="Password"  AutoCompleteType="Disabled" ></asp:TextBox> 
            
            <div style="text-align: left">
                <asp:Label ID="lblgirisdurum" runat="server" 
                CssClass="label_durum_hata"></asp:Label>   
            </div>
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="sifreunuttum" 
                                onclick="Button2_Click">Şifremi Unuttum</asp:LinkButton>
         <asp:Panel ID="Panel_captcha" runat="server" Height="50px">
                                
                <table style="width: 400px; height: 50px; vertical-align:middle; ">
                <tr style="height: 50px">
                <td width="150px" height="50px">
                    <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" 
                        CaptchaHeight="40" CaptchaLength="5" CaptchaMaxTimeout="240" 
                        CaptchaMinTimeout="5" CaptchaWidth="150" FontColor="#D20B0C" 
                        NoiseColor="#B1B1B1" />
                    </td>
                    <td width="50px" height="50px">
                <asp:ImageButton ID="ImageButton1" CssClass="textbox" ImageUrl="~/Images/buton/refresh.png" runat="server" CausesValidation="false" Width="40px" />
                </td>
                <td width="100px" height="50px">
                    <asp:TextBox ID="txtCaptcha" runat="server" autocomplete="off" 
                        AutoCompleteType="Disabled" CssClass="textbox" Width="100px" Height="30px"></asp:TextBox>
                </td>
                            
                </tr>
                </table>
         </asp:Panel>

        <table>
            <tr>
            <td width="195px">
            <asp:Button  class="button" ID="Button1" runat="server" Text="Giriş" onclick="Button1_Click" DefaultButton="Button1" />
              </td>
            <td>
       
              </td>
            </tr>
        </table>

           </asp:Panel>
    </div>
    </form>


</body>
</html>
