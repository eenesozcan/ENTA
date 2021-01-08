<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HataDosyasiOku.aspx.cs" Inherits="EnvanterTveY.App_Start.HataDosyasiOku" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="page" id="home">
        <div id="content">
            <br />  

            <table style="width: 100%; height: 100%;">
                    <tr style="  width:100%;">
                    <td align="left">
    
                        <asp:DropDownList ID="DropDownList1"  AutoPostBack="true" runat="server" 
                                OnDataBound="DropDownList1_DataBound" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    </tr>
                        <tr style="  width:100%;">
                            <td>
                                <asp:TextBox ID="txthata" runat="server" CssClass="hata_text" 
                                    TextMode="MultiLine" Height="400px" Width="80%"></asp:TextBox>
                            </td> 
                        </tr>        
                        <tr style=" width:100%;">
                            <td>             
                            </td>            
                        </tr>
            </table>
        </div>
    </div>

</asp:Content>
