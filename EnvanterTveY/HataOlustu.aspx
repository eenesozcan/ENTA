<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HataOlustu.aspx.cs" Inherits="EnvanterTveY.App_Start.HataOlustu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%:lblprgramadi.Text %></title>
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="images/sakarya_logo1.png" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
   
 <div class="page" id="home">
  
	<div id="hata-sayfasi" >
    <div class="hata-resim"></div>
    <div class="hata-icerik">
    
	        <table style="width: 100%; height: auto;">
                  
                    <tr align="center" style="height: 170px;  width:100%;"  >
                        <td align="left" colspan="2">
                        
                        <asp:Label ID="Label3" runat="server" Text="Sanırım bir yerlerde hata oluştu. !!! "  
                            Font-Size="18pt"  Font-Bold="True" ForeColor="black" ></asp:Label>
                    </td>
                    </tr>
                    <tr align="center" style="height: 30px;  width:100%;"  >
                        <td align="left" width="150px">
                        
                        <asp:Label ID="Label1" runat="server" Text="Hata Mesajı" 
                            Font-Size="10pt"  Font-Bold="True" ForeColor="Black" ></asp:Label>
                    </td>
                     <td align="left">
                        
                        <asp:Label ID="lblhata" runat="server" 
                            Font-Size="10pt"  Font-Bold="True" ForeColor="Black" ></asp:Label>
                    </td>
                </tr>
                   <tr align="center" style="height: 30px;  width:100%;"  >
                        <td align="left" >
                        
                        <asp:Label ID="Label4" runat="server" Text="Açıklama" 
                            Font-Size="10pt"  Font-Bold="True" ForeColor="Black" ></asp:Label>
                    </td>
                     <td align="left">
                        
                        <asp:Label ID="lblaciklama" runat="server" 
                            Font-Size="10pt"  Font-Bold="True" ForeColor="Black" ></asp:Label>
                    </td>
                </tr>
                <tr align="center" style="height: 30px;  width:100%;"  >
                        <td align="left" >
                        
                        <asp:Label ID="Label2" runat="server" Text="Hata Saati" 
                            Font-Size="10pt"  Font-Bold="True" ForeColor="Black" ></asp:Label>
                    </td>
                     <td align="left">
                        
                        <asp:Label ID="lblsaat" runat="server" 
                            Font-Size="10pt"  Font-Bold="True" ForeColor="Black" ></asp:Label>
                    </td>
                </tr>
                <tr align="center" style="height: 50px;  width:100%;"   >
                     <td align="left" colspan="1" valign="bottom">
                        
                         <asp:HyperLink ID="linksayfa"  CssClass="btn btn-primary"   Font-Bold="True"  
                             runat="server"  Width="200px"
                             NavigateUrl="javascript:history.go(-1);">Önceki sayfaya geri git</asp:HyperLink>
                        </td>

                         <td align="left" colspan="1" valign="bottom" style="padding-left: 30px">
                        
                         <asp:HyperLink ID="HyperLink1" Width="200px"  CssClass="btn  btn-primary"   Font-Bold="True"  
                             runat="server" 
                             NavigateUrl="Default.aspx">Anasayfa</asp:HyperLink>
                        </td>
                </tr>
                <tr align="center" style="height: 150px;  width:100%;"   >
                     <td align="left" colspan="2" valign="bottom">
                        
                        <asp:Label ID="Label5" runat="server" Text="İşlemlerinizi kontrol ederek yenileyiniz. Eğer aynı hatayı almaya devam ediyorsanız hata tarihi ile sistem yöneticinize başvurunuz. " 
                            Font-Size="10pt"  Font-Bold="False" ForeColor="Gray" ></asp:Label>
                        </td>
                </tr>
                </table>

                </div>
                <br />
        
                <br />
                        <asp:Label ID="lblprgramadi" runat="server"     ></asp:Label>    
	</div>

	</div>
    </form>
</body>
</html>

