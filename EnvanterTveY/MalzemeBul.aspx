<%@ Page Title="Malzeme Bul" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MalzemeBul.aspx.cs" Inherits="EnvanterTveY.MalzemeBul" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="Scripts/loading.js"></script>

    <script src="Scripts/KabloHE-js.js"></script>
    <link href="Content/Site.css" rel="stylesheet" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="container-fluid pl-1" style="margin-top: 20px">

        <div class="row">
            <span class="baslik">Malzeme Bul</span>
        </div>
        <br />
        <div class="row">
              <div class="col-md-2 col-xs-12">
                    <asp:TextBox ID="txtserinoara" CssClass="form-control form-control-sm" placeholder="Seri No" runat="server"></asp:TextBox>
              </div>
              <div class="col-md-2 col-xs-12 text-center">
                     <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary btn-sm" Width="130px"  Text="Ara" OnClick="btnara_Click"  />
              </div>
        </div>
    
        <div class="row mr-10 mt-20" >
                    <div class="table-responsive">
                                    <asp:GridView ID="grid" runat="server"  AutoGenerateColumns="False" DataKeyNames="ID"  CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True"  EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="30"  OnRowCommand="grid_RowCommand" OnRowDataBound="grid_RowDataBound" OnPageIndexChanging="grid_PageIndexChanging" OnDataBound="grid_DataBound"  >
                                       <Columns>
                                            <asp:TemplateField Visible="true" HeaderStyle-Width="100px"  HeaderText="Log">
                                            <ItemTemplate > 
                                                <asp:Button ID="btnsec"             runat="server" Text="Log Göster"         CommandName="sec"        CssClass="btn btn-primary btn-sm"             CommandArgument='<%#Eval("ID") %>'      />
                                                
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                                                    <asp:BoundField DataField="ID"                HeaderText="ID"        InsertVisible="False" ReadOnly="True"  />
                                                    <asp:BoundField DataField="TIP"               HeaderText="Malzeme Tipi"   />
                                                    <asp:BoundField DataField="TURU"              HeaderText="Malzeme Türü"   />
                                                    <asp:BoundField DataField="MARKA"             HeaderText="Marka"          />
                                                    <asp:BoundField DataField="MODEL"             HeaderText="Model"          />
                                                    <asp:BoundField DataField="SERI_NO"           HeaderText="Seri No"        />
                                                    <asp:BoundField DataField="DURUM"             HeaderText="Durum"          />
                                                    <asp:BoundField DataField="BOLGE_ADI"         HeaderText="Bölge Adı"      />
                                                    <asp:BoundField DataField="DEPO"              HeaderText="Depo Adı"       />
                                                    <asp:BoundField DataField="KAYIT"             HeaderText="Kayıt Eden"     />
                                                    <asp:BoundField DataField="GUNCELLEME_DURUMU" HeaderText="GD"    Visible="false" />

                                       </Columns>
                                    </asp:GridView>
                                            <asp:Label ID="lblmalzemesayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
                    </div>
        </div>
        <br />
        <br />
                         <div class="row" style=" margin-right:10px">
                            <div class="table-responsive fz-10">
                                            <asp:GridView ID="grid_malzeme_log" runat="server"  ScrollBars="Both" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  >
                                               <Columns>
                                                            <asp:BoundField DataField="ID"            HeaderText="ID"            InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="M_ID"          HeaderText="Malzeme ID"     />
                                                            <asp:BoundField DataField="E_SERINO"      HeaderText="Eski Seri No"   />
                                                            <asp:BoundField DataField="Y_SERINO"      HeaderText="Yeni Seri No"   />
                                                            <asp:BoundField DataField="SEVK_ID"       HeaderText="Sevk ID"        />
                                                            <asp:BoundField DataField="E_DURUM"       HeaderText="Eski Durum"     />
                                                            <asp:BoundField DataField="Y_DURUM"       HeaderText="Yeni Durum"     />
                                                            <asp:BoundField DataField="BOLGE_ADI"     HeaderText="Eski Bölge"     />
                                                            <asp:BoundField DataField="DEPO"          HeaderText="Eski Depo"      />
                                                            <asp:BoundField DataField="YBOLGE_ADI"    HeaderText="Yeni Bölge"     />
                                                            <asp:BoundField DataField="YDEPO"         HeaderText="Yeni Depo"      />
                                                            <asp:BoundField DataField="GEREKCE"       HeaderText="Gerekçe"        />
                                                            <asp:BoundField DataField="ACIKLAMA"      HeaderText="Açıklama"       />
                                                            <asp:BoundField DataField="TURU"          HeaderText="Eski Tür"       />
                                                            <asp:BoundField DataField="YTURU"         HeaderText="Yeni Tür"       />
                                                            <asp:BoundField DataField="TIP"           HeaderText="Eksi Tip"       />
                                                            <asp:BoundField DataField="YTIP"          HeaderText="Yeni Tip"       />
                                                            <asp:BoundField DataField="MARKA"         HeaderText="Eski Marka"     />
                                                            <asp:BoundField DataField="YMARKA"        HeaderText="Yeni Marka"     />
                                                            <asp:BoundField DataField="MODEL"         HeaderText="Eski Model"     />
                                                            <asp:BoundField DataField="YMODEL"        HeaderText="Yeni Model"     />
                                                            <asp:BoundField DataField="KAYIT"         HeaderText="Kayıt"          />

                                                            <asp:TemplateField Visible="false" HeaderStyle-Width="280px"  HeaderText="İşlemler">
                                                                <ItemTemplate > 

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>
                            </div>

                        </div>
                                <asp:Label ID="lblhareketsayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
  
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
