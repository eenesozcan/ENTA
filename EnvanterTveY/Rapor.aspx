<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rapor.aspx.cs" Inherits="EnvanterTveY.Rapor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <script src="Scripts/loading.js"></script>

    <script src="Scripts/KabloHE-js.js"></script>
    <link href="Content/Site.css" rel="stylesheet" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="container-fluid pl-1" style="margin-top: 20px">

        <div class="row">
            <span class="baslik">Raporlar</span>
        </div>
        <br />
        <div class="row">
                <div class="col-md-2 col-xs-12">
                     <label for="">Rapor Seç</label>
                </div>
                <div class="col-md-4 col-xs-12">
                    <asp:DropDownList ID="comraporsec"  AutoPostBack="true" CssClass="form-control form-control-sm" runat="server" Width="600px" OnSelectedIndexChanged="comraporsec_SelectedIndexChanged"   >
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                     <asp:Label ID="lblsql" runat="server" Visible="false" Text=""></asp:Label>
                    
                </div>
              <div class="col-md-2 col-xs-12 text-center">
                  <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary btn-sm" Width="130px"  Text="Göster" OnClick="btnara_Click"  />
              </div>
        </div>
    

        <br />
        <br />
   <div class="row">
        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" >
                <Columns>
                </Columns>
            </asp:GridView>
        </div>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
   </div>
</ContentTemplate>
</asp:UpdatePanel>


      <div id="YeniMesajDiv">Some text some message..</div>
    <!--
<div class="loading" >
<br />
Yükleniyor. <br />
Lütfen bekleyiniz.<br />
    <br />
    <div  style="text-align:center;">

            <div class="spinner-grow text-primary" style="width: 50px; height: 50px;"></div>
              <div style="width: 50px; height: 50px;" class="spinner-grow text-primary"></div>
              <div style="width: 50px; height: 50px;" class="spinner-grow text-primary"></div>
    </div>
</div>  -->
</asp:Content>
