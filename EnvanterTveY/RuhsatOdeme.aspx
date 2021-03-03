<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RuhsatOdeme.aspx.cs" Inherits="EnvanterTveY.RuhsatOdeme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <link href="Content/Site.css" rel="stylesheet" />
         <script src="Scripts/KabloHE-js.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
          
    <div class="container-fluid" style="margin-left:10px; margin-top: 20px"">
             <div class="row">
                <span class="baslik">Ruhsat Ödeme Rapor</span>
            </div>


   </div>
         </ContentTemplate>
        </asp:UpdatePanel>



            <div id="YeniMesajDiv">Some text some message..</div>

<div class="loading" align="center">
     <br />
    Yükleniyor. Lütfen bekleyiniz.<br />
    <br />
        <div class="ball"></div>
        <div class="ball1"></div>
</div>

</asp:Content>
