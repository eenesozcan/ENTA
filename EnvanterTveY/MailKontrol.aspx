<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MailKontrol.aspx.cs" Inherits="EnvanterTveY.MailKontrol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="page" id="home">

	<div id="content_tam" style="font-size:10px;" >
    
        <table style="width: 100%;"  >
    <tr align="center">
        <td>
    
            &nbsp;</td>
        <td>
        
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
        
            &nbsp;</td>
    </tr>
    <tr align="left">
        <td colspan="2">
    
        
            <asp:Label ID="Label2" runat="server" CssClass="label1" Text="Bekleyenler" ></asp:Label>
        
        </td>
        <td>
        </td>
        <td>
        
            &nbsp;</td>
    </tr>
    </table>
    
    <table style="width: 100%">
    <tr style="height: auto; width:100%; ">
        <td colspan="4";  style="width:100%; "  >
        
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="table table-striped table-bordered table-condensed table-hover" 
             AutoGenerateColumns="true">
                
            </asp:GridView>
        </td>
    
    </tr>
    </table>
    
    <table style="width: 100%; height: 200px;" >
        <tr align="left"; >
        <td align="left"; class="style1">
        
            <asp:Label ID="Label4" runat="server" CssClass="label"></asp:Label>
        </td>
                <td class="style1">
        
                    &nbsp;</td>    
    </tr>

        <tr align="left"; >
        <td align="left"; class="style1">
        
            &nbsp;</td>

                <td class="style1">
        
                    &nbsp;</td>    
    </tr>

        <tr align="left"; >
        <td align="left"; class="style1">
        
            <asp:Label ID="Label3" runat="server" CssClass="label" Text="Tüm Mail" ></asp:Label>
        </td>
                <td class="style1">
                    &nbsp;</td>    
    </tr>

        <tr align=center >
        <td colspan="2">
        
            <asp:GridView ID="GridView2" runat="server"  CellPadding="4" CssClass="table table-striped table-bordered table-condensed table-hover"  SelectedRowStyle-CssClass="mGrid selectedRowStyle"
                AutoGenerateColumns="true" 
                 CellSpacing="2"  
                 >
                
            </asp:GridView>
            </td>
    </tr>
</table>
<br />
            <asp:Label ID="Label1" runat="server" CssClass="label"></asp:Label>
<br />

    </div>
    </div>

</asp:Content>
