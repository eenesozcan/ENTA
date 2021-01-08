<%@ Page Title="Sevk Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SevkYonetimi.aspx.cs" Inherits="EnvanterTveY.SevkYonetimi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <script src="Scripts/loading.js"></script>

    <link href="Content/Site.css" rel="stylesheet" />
    <script src="Scripts/KabloHE-js.js"></script>

        <div class="row" style="margin-left:30px ;margin-top: 20px">
            <span class="baslik">Sevk Yönetimi</span>
        </div>
                <br />
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

<div class="container-fluid" >

        <div class="row">
                <div class="col-md-2 col-xs-12">
                    <label for="">Kaynak Bölge</label>
                    <asp:DropDownList ID="comkaynakbolgeara"  DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server"   >
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                    <label for="">Hedef Bölge</label>
                    <asp:DropDownList ID="comhedefbolgeara"  DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server"  >
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                    <label for="">Sevk Durumu</label>
                    <asp:DropDownList ID="comsevkdurumara"  DataTextField="SEVK_DURUM" DataValueField="ID" CssClass="form-control form-control-sm" runat="server"  Visible="true" >
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12 ">
                    <label for="">Sevk ID</label>
                    <asp:TextBox ID="txtsevkidara"  placeholder="Seri No"  onkeypress="return sadecerakam(event)"  CssClass="form-control form-control-sm" runat="server" ></asp:TextBox>
                </div>
                <div class="col-md-4 col-xs-12 text-right">
                    <br />
                            <asp:Button ID="btnara"      runat="server"  CssClass="btn  btn-primary" Width="130px"  Text="Ara"          OnClick="btnara_Click" />
                            <asp:Button ID="btnsevkekle" runat="server"  CssClass="btn  btn-success" Width="130px"  Text="Sevk Oluştur" OnClick="btnsevkekle_Click"  />
                </div>
        </div>
                    <asp:Label ID="surecdurumid" runat="server"  Visible="false" Text=1></asp:Label>
    <br />
    <div class="row  "  >
        <div class="table-responsive margin-body">
                        <asp:GridView ID="grid_sevk_listele" runat="server" AllowPaging="true"  PageSize="20"  AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_sevk_listele_RowCommand" OnRowDataBound="grid_sevk_listele_RowDataBound"  OnPageIndexChanging="grid_sevk_listele_PageIndexChanging"  >
                           <Columns>
                                        <asp:BoundField DataField="ID"            HeaderText="ID"                 InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="BOLGE_ADI"     HeaderText="Kaynak Bölge"               />
                                        <asp:BoundField DataField="DEPO"          HeaderText="Kaynak Depo"                />
                                        <asp:BoundField DataField="HBOLGE_ADI"    HeaderText="Hedef Bölge"                />
                                        <asp:BoundField DataField="HDEPO"         HeaderText="Hedef Depo"                 />
                                        <asp:BoundField DataField="S_DURUM"       HeaderText="Sevk Durum"                HtmlEncode="false" />
                                        <asp:BoundField DataField="KAYIT"         HeaderText="Kayıt Eden/Kayıt Tarihi"    />
                                        <asp:BoundField DataField="SEVKTARIHI"    HeaderText="Sevk Eden/Kayıt Tarihi"    Visible="false"  />
                                        <asp:BoundField DataField="TESLIMTARIHI"  HeaderText="Teslim Eden/Teslim Tarihi" Visible="false" />
                                        <asp:BoundField DataField="IADETARIHI"    HeaderText="İade Eden/İade Tarihi"     Visible="false" />
                                        <asp:TemplateField Visible="true" HeaderStyle-Width="280px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                
                                                <asp:Button ID="btndetay"       runat="server" Text="Detay"         CommandName="detay"       CssClass="btn btn-info btn-sm"     CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnislemler"    runat="server" Text="İşlemler"      CommandName="islemler"    CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnguncelle"    runat="server" Text="Güncelle"      CommandName="guncelle"    CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnhareket"     runat="server" Text="Hareket"       CommandName="hareket"     CssClass="btn btn-info btn-sm"     CommandArgument='<%# Container.DataItemIndex %>'  Visible="false"/>
                                                <asp:Button ID="btnsil"         runat="server" Text="Sil"           CommandName="sil"         CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                           </Columns>
                              <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                            <asp:Label ID="lblsevksayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
        </div>
      </div>

    </div>
    </ContentTemplate>
</asp:UpdatePanel>

         <!-- Modal Malzeme Ekle -->
<div class="modal fade" id="ModalMalzemeEkle" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik1" runat="server" Text="Label"></asp:Label></h4>
                &emsp;
                &emsp;
                <asp:Label ID="lblsevkdurumtext" runat="server" Text=""></asp:Label>

                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

                <!-- Modal body -->
                <div class="modal-body">
        
                    <div class="messagealert mt-3 mb-3" id="alert_container_sevket">
                        </div>

      <div class="row  md-12 d-flex align-items-end" >
            <div class="col-md-10">
                
                <span class="label-depo text rounded p-10 durum-sevk-transfer">
                        <asp:Label ID="lblkaynakbolge" runat="server" Text="" Visible="false" ></asp:Label>
                    
                        <asp:Label ID="lblkaynakdepo" runat="server" Text=""></asp:Label>
                    </span>

                    &emsp; 
                <span class="">
                    <asp:Image runat="server" ImageUrl="~/Images/deliverywhite.gif" Height="50px" />
                </span>
            
                &emsp;
                <span class="label-depo text rounded p-10 durum-sevk-transfer">
                        <asp:Label ID="lblhedefbolge" runat="server" Text="" Visible="false"></asp:Label>
                    
                        <asp:Label ID="lblshedefdepo" runat="server" Text=""></asp:Label>
                 </span>
            </div>
       
            <div class="col-md-2 text-center">
          <asp:Button ID="btnhareket"           runat="server"  CssClass="btn btn-info btn-sm"  Text="Hareket"    OnClick="btnhareket_Click"  />
          
            </div>
        </div>

    <div class="row" style="margin-top:0px">
                <div class="col-md-4" > 
                    <asp:CheckBox class="text rounded p-9 label-garanti-secim bg-success" ID="chc_garantili" runat="server" Text="Garantili Malzeme Eklenebilir" AutoPostBack="true"  OnCheckedChanged="chc_garantili_CheckedChanged"  Visible="true" />
                </div>
                <div class="col-md-4" > 
                    <asp:CheckBox class="text rounded p-9 label-garanti-secim bg-success" ID="chc_garantisiz" runat="server" Text="Garantisiz Malzeme Eklenebilir" AutoPostBack="true" OnCheckedChanged="chc_garantisiz_CheckedChanged" Visible="true" />
                </div>
                    <asp:Label ID="lblgarantili_mlz_kbl" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblgarantisiz_mlz_kbl" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblhedefdepoid" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lbldepotanimkontrol" runat="server" Text="" Visible="false"></asp:Label>

    </div>
                    <hr>

        <div class="row" style="margin-top:10px">
            <div class="col-md-3">
                                <asp:TextBox ID="txtserinoara"  placeholder="Seri No Arama"  ForeColor="Blue" CssClass="form-control form-control-sm" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtserinoara"  ValidationGroup="serinoara"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" />
             </div>

        <div class="col-md-1">

        </div>
        <div class="col-md-4">
          <asp:Button ID="btnmalzemeara"      runat="server" ValidationGroup="serinoara" CssClass="btn btn-primary" Text="Bul"           OnClick="btnmalzemeara_Click"     Visible="false" />
          <asp:Button ID="btnsevkmalzemeekle" runat="server" ValidationGroup="serinoara" CssClass="btn btn-success" Text="Malzeme Ekle"  OnClick="btnsevkmalzemeekle_Click" />
                    
        </div>
        <div class="col-md-3">
                    
        </div>

    </div>
    <div class="row" style="margin-top:10px">
        <div class="col-md-1">
                    <asp:Label ID="lblmalzemeid"         runat="server" Visible="false" Text=""></asp:Label>
        </div>
        <div class="col-md-2">
                    <asp:Label ID="lblmalzememarka"      runat="server" Visible="false" Text=""></asp:Label>
        </div>
        <div class="col-md-2">
                    <asp:Label ID="lblmalzememodel"      runat="server" Visible="false" Text=""></asp:Label>
        </div>
        <div class="col-md-2">
                    <asp:Label ID="lblmalzemedurum"      runat="server" Visible="false" Text=""></asp:Label>
        </div>
        <div class="col-md-2">
                    <asp:Label ID="lblmalzemesevkdurumu" runat="server" Visible="false" Text=""></asp:Label>
        </div>
        <div class="col-md-2">
                    <asp:Label ID="lblsevkdurum"         runat="server" Visible="false" Text=""></asp:Label>
        </div>
    </div>

      <div class="row  " style="overflow-y:auto; height:280px;">
        <div class="table-responsive margin-body fz-10" >
                        <asp:GridView ID="grid_sevk_malzeme_sec" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="20" OnPageIndexChanging="grid_sevk_malzeme_sec_PageIndexChanging"  OnRowCommand="grid_sevk_malzeme_sec_RowCommand" OnRowDataBound="grid_sevk_malzeme_sec_RowDataBound"    >
                           <Columns>
                                        <asp:BoundField DataField="ID"           HeaderText="ID"              />
                                        <asp:BoundField DataField="MARKA"        HeaderText="Marka"           />
                                        <asp:BoundField DataField="MODEL"        HeaderText="Model"           />
                                        <asp:BoundField DataField="SERI_NO"      HeaderText="Seri No"         />
                                        <asp:BoundField DataField="DURUM"        HeaderText="Durumu"          />
                                        <asp:BoundField DataField="GARANTI"      HeaderText="Garanti Durumu"  />
                                        <asp:BoundField DataField="KAYIT"        HeaderText="Kayıt"           Visible="false" />


                                        <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="btnarizaaciklama"  runat="server" Text="Arıza Bilgi"  CommandName="ariza"    CssClass="btn btn-primary btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"            runat="server" Text="Sil"               CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />

                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
                        </asp:GridView>
        </div>
    </div>
                            <asp:Label ID="lblmalzemesayisi"    runat="server" Visible="true" Text=""  class="text-danger" ></asp:Label>
                            <asp:Label ID="lblidsevk"           runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblkaynakbolgeid"    runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblkullanicibolgeid" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblhedefbolgeid"     runat="server" Visible="false" Text=""></asp:Label>

  </div>

      <!-- Modal footer -->
      <div class="modal-footer">
          <asp:Button ID="btnsevket"            runat="server"  CssClass="btn btn-success"  Text="Sevk Başlat"     OnClick="btnsevket_Click"  />
          <asp:Button ID="btnsevkteslimal"      runat="server"  CssClass="btn btn-success"  Text="Teslim Al"       OnClick="btnsevkteslimal_Click"  Visible="false"  />
          <asp:Button ID="btnsevkiadeet"        runat="server"  CssClass="btn btn-danger"   Text="İade Et"         OnClick="btnsevkiadeet_Click"    Visible="false"  />
          <asp:Button ID="btnsevkiptal"         runat="server"  CssClass="btn btn-danger"   Text="Sevk İptal"      OnClick="btnsevkiptal_Click" Visible="false" />
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
      
      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

     <!-- Modal Sevk Oluştur -->
<div class="modal fade" id="ModalSevkOlustur" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content ">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

                <!-- Modal body -->
                <div class="modal-body">
        
                    <div class="messagealert mt-3 mb-3" id="alert_container_yeni">
                        </div>

    <div class="row" style="margin-top:10px">
        <div class="col-md-2">
                    <label class="text-primary" for="">Sevk No</label>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblsurecno"  class="text-danger"  runat="server" Text="Sevk No"></asp:Label>
        </div>
        <div class="col-md-1">
        </div>
        <div class="col-md-2">
                     <label class="text-primary"  for="">Kargo Bilgileri</label>
        </div>
        <div class="col-md-4">
                     <asp:TextBox ID="txtkargo" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="row" style="margin-top:10px">
        <div class="col-md-2">
                     <label class="text-primary" for="">Kaynak Bölge</label>
        </div>
        <div class="col-md-4">
                    <asp:DropDownList ID="comkaynakbolgesec" Width="250px" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comkaynakbolgesec_SelectedIndexChanged"> </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="comkaynakbolgesec"  ValidationGroup="sevkolustur"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Kaynak Bölgeyi seçiniz." ForeColor="red"  />
        </div>

        <div class="col-md-2">
                     <label class="text-primary"  for="">Açıklama</label>
        </div>
        <div class="col-md-4">
                     <asp:TextBox ID="txtaciklama"  CssClass="form-control form-control-sm" runat="server" Height="32px" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>

    <div class="row" style="margin-top:10px">
        <div class="col-md-2">
                    <label class="text-primary" for="">Kaynak Depo</label>
        </div>
        <div class="col-md-4">
                    <asp:DropDownList ID="comkaynakdeposec" Width="250px" DataTextField="DEPO" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comkaynakdeposec_SelectedIndexChanged">  </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="comkaynakdeposec"  ValidationGroup="sevkolustur"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Kaynak Depoyu seçiniz." ForeColor="red"  />
        </div>

        <div class="col-md-2">
                     <label class="text-primary"  for=""></label>
        </div>
        <div class="col-md-4">
                    <asp:CheckBox class="text rounded p-10 label-garanti-secim bg-warning" Font-Size="12px" ID="chcgarantilimalzemekabul" runat="server" Text="&nbsp;&nbsp;&nbsp;Garantili Malzeme Eklenebilir" AutoPostBack="false"   Visible="true" />
        </div>
    </div>

    <div class="row" style="margin-top:10px">
        <div class="col-md-2">
                    <label class="text-primary" for="">Hedef Bölge</label>
        </div>
        <div class="col-md-4">
                    <asp:DropDownList ID="comhedefbolgesec" Width="250px" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comhedefbolgesec_SelectedIndexChanged">   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="comhedefbolgesec"  ValidationGroup="sevkolustur"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Hedef Bölgeyi seçiniz." ForeColor="red"  />
        </div>
        <div class="col-md-2">
                     <label class="text-primary"  for=""></label>
        </div>
        <div class="col-md-4">
                    <asp:CheckBox class="text rounded p-10 label-garanti-secim bg-warning" Font-Size="12px" ID="chcgarantisizmalzemekabul" runat="server" Text="&nbsp;&nbsp;&nbsp;Garantisiz Malzeme Eklenebilir" AutoPostBack="false"    Visible="true" />
        </div>
    </div>

    <div class="row" style="margin-top:10px">
        <div class="col-md-2">
                    <label class="text-primary" for="">Hedef Depo</label>
        </div>
        <div class="col-md-3">
                    <asp:DropDownList ID="comhedefdeposec" Width="250px" DataTextField="DEPO" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comhedefdeposec_SelectedIndexChanged" >   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="comhedefdeposec"  ValidationGroup="sevkolustur"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Hedef Depoyu seçiniz." ForeColor="red"  />
                    <asp:Label ID="garantili_kabul_kayit" runat="server" Text="" ></asp:Label>
                    <asp:Label ID="garantisiz_kabul_kayit" runat="server" Text="" ></asp:Label>
        </div>
    </div>
                            <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

                </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnsevkkaydet" runat="server" ValidationGroup="sevkolustur" CssClass="btn btn-success" Text="Sevk Kaydet" OnClick="btnsevkkaydet_Click"  />
          <asp:Button ID="btnislemler" runat="server"   CssClass="btn btn-info" Text="İşlemler"  OnClick="btnislemler_Click"  />
      </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

         <!-- Modal Tamir Depodan Malzeme Ekle -->
<div class="modal fade" id="ModalTamir_MalzemeEkle" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered " role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik2" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

                <!-- Modal body -->
                <div class="modal-body">
        
                    <div class="messagealert mt-3 mb-3" id="alert_container_yeni2">
                        </div>
    <div class="row  mt-10 d-flex align-items-end" >
        <div class="col-md-9">
            <asp:Label ID="lblsevkdurumtexttamir" runat="server" Text=""></asp:Label>
            &emsp;
            <span class="label-depo text rounded p-10 durum-sevk-transfer">
                    <asp:Label ID="lblkaynakbolge_tamir" runat="server" Text="" Visible="false"></asp:Label>
                
                    <asp:Label ID="lblkaynakdepo_tamir" runat="server" Text=""></asp:Label>
                </span>

                &emsp; 
            <span class="">
                <asp:Image runat="server" ImageUrl="~/Images/deliverywhite.gif" Height="50px" />
            </span>
            
            &emsp;
            <span class="label-depo text rounded p-10 durum-sevk-transfer">
                    <asp:Label ID="lblhedefbolge_tamir" runat="server" Text="" Visible="false"></asp:Label>
                
                    <asp:Label ID="lblshedefdepo_tamir" runat="server" Text=""></asp:Label>
             </span>
        </div>
       
        <div class="col-md-3 text-right">
            <asp:Button ID="btnsevkmalzemeexcel_tamirsevkolustur"  runat="server"  CssClass="btn btn-info btn-sm"  Text="Excel"       Visible="true"  />
            <asp:Button ID="btnhareket_tamirsevkolustur"           runat="server"  CssClass="btn btn-info btn-sm"  Text="Hareket"  OnClick="btnhareket_tamirsevkolustur_Click"     />
                    <asp:Label ID="lblhedefdepokontrol" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblkaynakdepoid_tamir" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lbldepotanimkontrol_tamir" runat="server" Text="" Visible="false"></asp:Label>

        </div>
    </div>
                  
<asp:Panel ID="panel_malzeme_ekle"  CssClass="panel panel-primary" DefaultButton="btnsevkmalzemeekle1_tamirsevkolustur" runat="server">
        <div class="row" style="margin-top:10px">
            <div class="col-md-12">
                <table class="table table-bordered fz-10 text-white">
                  <thead>
                    <tr class="bg-info">
                      <th scope="col">Seçerek Ekleme</th>
                      <th scope="col">Bölgenin Tüm Malzemelerini Ekleme</th>
                      <th scope="col">Seri Numarasına Göre Ekleme</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>
                  <asp:Button ID="btnsevkmalzemeekle1_tamirsevkolustur" runat="server" CssClass="btn btn-primary btn-sm" Text="Seç" ToolTip="Tamir işlemi görmüş malzemeleri tek tek listeye ekle."  OnClick="btnsevkmalzemeekle1_tamirsevkolustur_Click"  />

                      </td>
                      <td>
                              <div class="row">
                                <div class="col-md-6">
                                      <asp:DropDownList ID="combolgesec_tamirsevkolustur" DataTextField="BOLGE_ADI" DataValueField="ID" Width="180px" CssClass="form-control form-control-sm" runat="server"> </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                      <asp:Button ID="btnsevkmalzemeekle_tamirsevkolustur"  runat="server" CssClass="btn btn-success btn-sm"  Text="Ekle" ToolTip="Tamir işlemi görmüş tüm malzemeleri listeye ekle."  OnClick="btnsevkmalzemeekle_tamirsevkolustur_Click"  />
                                </div>
                              </div>
                      </td>
                      <td>
                              <div class="row">
                                <div class="col-md-6">
                                      <asp:TextBox ID="txtserino_tamirsevkolustur"  placeholder="Seri No Arama"  ForeColor="Blue" CssClass="form-control form-control-sm" runat="server" ></asp:TextBox>

                                </div>
                                <div class="col-md-1">
                                      <asp:Button ID="btnsevkmalzemeekle_1_tamirsevkolustur"  runat="server" CssClass="btn btn-success btn-sm"  Text="Ekle" ToolTip="Tamir işlemi görmüş seri numaralı malzemeyi listeye ekle."  OnClick="btnsevkmalzemeekle_1_tamirsevkolustur_Click"   />
                                </div>
                              </div>
                      </td>
                    </tr>
                  </tbody>
                </table>
            </div>
       </div>
     </asp:Panel>


      <div class="row  "  style="  height:280px;">
          
        <div class="table-responsive margin-body fz-10" >
                        <asp:GridView ID="grid_sevk_malzeme_sec_tamirsevkolustur" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="10"  OnRowDataBound="grid_sevk_malzeme_sec_tamirsevkolustur_RowDataBound" OnRowCommand="grid_sevk_malzeme_sec_tamirsevkolustur_RowCommand" OnPageIndexChanging="grid_sevk_malzeme_sec_tamirsevkolustur_PageIndexChanging"    >
                           <Columns>
                                        <asp:BoundField DataField="ID"           HeaderText="ID"            />
                                        <asp:BoundField DataField="BOLGE_ADI"    HeaderText="Bölge"         />
                                        <asp:BoundField DataField="MARKA"        HeaderText="Marka"         />
                                        <asp:BoundField DataField="MODEL"        HeaderText="Model"         />
                                        <asp:BoundField DataField="SERI_NO"      HeaderText="Seri No"       />
                                        <asp:BoundField DataField="DURUM"        HeaderText="Durumu"        />
                                        <asp:BoundField DataField="KAYIT"        HeaderText="Kayıt"         />

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="btnarizaaciklama"  runat="server" Text="Arıza Bilgi"       CommandName="ariza"    CssClass="btn btn-primary btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"            runat="server" Text="Sil"               CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%#Eval("ID") %>' />

                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
                        </asp:GridView>
        </div>
    </div>
                            <asp:Label ID="lblmalzemesayisi_tamirsevkolustur" class="text-danger" runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="lblmalzemedurum_tamirsevkolustur" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblmalzemeid_tamirsevkolustur" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="tamiregelensevkid" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblsevkdurum_tamirsevkolustur" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblgarantibitistarihi"   runat="server" Visible="false"    Text=""></asp:Label>


  </div>

      <!-- Modal footer -->
      <div class="modal-footer">
          <asp:Button ID="btnsevket_tamirsevkolustur"            runat="server"  CssClass="btn btn-success"  Text="Sevk Başlat" OnClick="btnsevket_tamirsevkolustur_Click"   />
          <asp:Button ID="btnsevkteslimal_tamirsevkolustur"      runat="server"  CssClass="btn btn-success"  Text="Teslim Al"   OnClick="btnsevkteslimal_tamirsevkolustur_Click"    Visible="false"  />
          <asp:Button ID="btnsevkiadeet_tamirsevkolustur"        runat="server"  CssClass="btn btn-danger"   Text="İade Et"     OnClick="btnsevkiadeet_tamirsevkolustur_Click"    Visible="false"  />
          <asp:Button ID="btnsevkiptal_tamirsevkolustur"         runat="server"  CssClass="btn btn-danger"   Text="Sevk İptal"    OnClick="btnsevkiptal_tamirsevkolustur_Click" Visible="false"  />
          
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
      
      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

             <!-- Modal Tamir Depodan Malzeme Seç-->
<div class="modal fade" id="ModalTamir_MalzemeSec" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik3" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="gizle" data-dismiss="modal">&times;</button>
                              </div>

                <!-- Modal body -->
                <div class="modal-body">
        
                    <div class="messagealert mt-3 mb-3" id="alert_container_yeni3">
                        </div>

    <div class="row" style="margin-top:10px">
        <div class="col-md-3">
                    <label class="text-primary" for="">Bölge Seç</label>
		    <asp:DropDownList ID="combolgesec_malzemesec" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combolgesec_malzemesec_SelectedIndexChanged"> </asp:DropDownList>
        </div>
        <div class="col-md-3">
                    <label class="text-primary" for="">Malzeme Durumu Seç</label>
		    <asp:DropDownList ID="comdurumsec_malzemesec" DataTextField="DURUM" DataValueField="DURUM" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comdurumsec_malzemesec_SelectedIndexChanged"> </asp:DropDownList>
        </div>        
    </div> 
                    <hr>


      <div class="row  margin-body" style="overflow-y:auto; height:280px;">
        <div class="table-responsive fz-10" >
                        <asp:GridView ID="grid_tamir_malzeme_sec_tamirsevkolustur" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="M_ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="20" OnRowDataBound="grid_tamir_malzeme_sec_tamirsevkolustur_RowDataBound" OnRowCommand="grid_tamir_malzeme_sec_tamirsevkolustur_RowCommand"  OnPageIndexChanging="grid_tamir_malzeme_sec_tamirsevkolustur_PageIndexChanging"    >
                           <Columns>

                                        <asp:BoundField DataField="M_ID"                HeaderText="Malzeme ID"      InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="BOLGE_ADI"         HeaderText="Ait Olduğu Bölge"  />
                                        <asp:BoundField DataField="TIP"               HeaderText="Malzeme Tipi"      />
                                        <asp:BoundField DataField="TURU"              HeaderText="Malzeme Türü"      />
                                        <asp:BoundField DataField="MARKA"             HeaderText="Marka"             />
                                        <asp:BoundField DataField="MODEL"             HeaderText="Model"             />
                                        <asp:BoundField DataField="M_SERINO"           HeaderText="Seri No"          />
                                        <asp:BoundField DataField="DURUM"             HeaderText="Durum"             />
                                        <asp:BoundField DataField="DEPO"              HeaderText="Depo Adı"          Visible="false" />
                                        <asp:BoundField DataField="KAYIT"             HeaderText="Kayıt Eden"        Visible="false" />
                                        <asp:BoundField DataField="GUNCELLEME_DURUMU" HeaderText="GD"                Visible="false" />


                                        <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="btnekle"            runat="server" Text="Ekle"               CommandName="ekle"      CssClass="btn btn-success btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />

                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
                        </asp:GridView>
                            <asp:Label ID="lblmalzemesayisi_malzemesec" class="text-danger" runat="server" Visible="true" Text=""></asp:Label>

        </div>
    </div>
                            <asp:Label ID="lblidmalzeme" class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblmalzemedurumid" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lbldurummalzeme" runat="server" Visible="false" Text=""></asp:Label>

                            <asp:Label ID="lblmodalmalzemesecyonlendir" class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lbltest" class="text-danger" runat="server" Visible="true" Text=""></asp:Label>

  </div>

      <!-- Modal footer -->
      <div class="modal-footer">
          <button type="button" class="gizle" data-dismiss="modal">Kapat</button>
        <asp:Button ID="btnmodalmalzemsectemirkapat" CssClass="btn btn-secondary"  runat="server" Text="Kapat" OnClick="btnmodalmalzemsectemirkapat_Click" />
      
      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

             <!-- Modal Arıza Durum Takip-->
<div class="modal fade" id="ModalAriza" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content ">

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header ">
                                <h4 class="modal-title ">
                                    <asp:Label ID="lblmodalyenibaslik5" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="gizle" data-dismiss="modal">&times;</button>
                              </div>

                <!-- Modal body -->
                <div class="modal-body">
        
                    <div class="messagealert mt-3 mb-3" id="alert_container_yeni5">
                        </div>
                    <hr>

      <div class="row  margin-body" style="overflow-y:auto; height:280px;">
        <div class="table-responsive fz-10" >
                        <asp:GridView ID="grid_ariza" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="M_ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="50"   >
                           <Columns>

                                        <asp:BoundField DataField="M_ID"              HeaderText="Malzeme ID"           Visible="false"             InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="BOLGE_ADI"         HeaderText="Ait Olduğu Bölge"     />
                                        <asp:BoundField DataField="E_DURUM"           HeaderText="Malzeme Eski Durum"   />
                                        <asp:BoundField DataField="Y_DURUM"           HeaderText="Malzeme Yeni Durum"   />
                                        <asp:BoundField DataField="GEREKCE"           HeaderText="Gerekçe"              />
                                        <asp:BoundField DataField="ACIKLAMA"          HeaderText="Açıklama"             />
                                        <asp:BoundField DataField="KAYIT"             HeaderText="Kayıt"                />

                           </Columns>
                        </asp:GridView>
                            <asp:Label ID="lblhareketsayisi_ariza" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
        </div>
     </div>
                            <asp:Label ID="lblidsevk_ariza" class="text-info" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblmodalarizayonlendir" class="text-info" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblidcihaz_ariza" class="text-info" runat="server" Visible="false" Text=""></asp:Label>

  </div>

      <!-- Modal footer -->
      <div class="modal-footer">
          <button type="button" class=" gizle" data-dismiss="modal">Kapat</button>
            <asp:Button ID="btnarizamodalkapat"    runat="server"  CssClass="btn btn-secondary"  Text="Kapat"    OnClick="btnarizamodalkapat_Click"  />
      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

             <!-- Modal Hareket -->
<div class="modal fade" id="ModalHareket" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik6" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="gizle" data-dismiss="modal">&times;</button>
                              </div>

                <!-- Modal body -->
  <div class="modal-body">
        <div class="messagealert mt-3 mb-3" id="alert_container_yeni6"> </div>
                     
                  <div class="container-fluid" style="margin-left:10px"  >
                         <div class="row" style="overflow-y:auto; height:280px; margin-right:10px">
                            <div class="table-responsive fz-10">
                                            <asp:GridView ID="grid_sevk_log" runat="server"  ScrollBars="Both" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  >
                                               <Columns>
                                                            <asp:BoundField DataField="ID"               HeaderText="ID"               InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="SEVK_ID"          HeaderText="Sevk ID"         />
                                                            <asp:BoundField DataField="BOLGE_ADI"        HeaderText ="Kaynak Bölge"   />
                                                            <asp:BoundField DataField="DEPO"             HeaderText="Kaynak Depo"     />
                                                            <asp:BoundField DataField="HBOLGE_ADI"       HeaderText="Hedef Bölge"     />
                                                            <asp:BoundField DataField="HDEPO"            HeaderText="Hedef Depo"      />
                                                            <asp:BoundField DataField="S_DURUM"          HeaderText="Sevk Durumu"     />
                                                            <asp:BoundField DataField="KAYIT"            HeaderText="Kayıt Bilgisi"   />
                                                            <asp:BoundField DataField="SEVK"             HeaderText="Sevk Bilgisi"    />
                                                            <asp:BoundField DataField="TESLIM"           HeaderText="Teslim Bilgisi"  />
                                                            <asp:BoundField DataField="IADE"             HeaderText="İade Bilgisi"    />
                                                            <asp:BoundField DataField="SILEN"            HeaderText="Silme Bilgisi"   />
                                                            <asp:BoundField DataField="ACIKLAMA"         HeaderText="Eski Marka"      />

                                                            <asp:TemplateField Visible="false" HeaderStyle-Width="280px"  HeaderText="İşlemler">
                                                                <ItemTemplate > 

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>
                            </div>
                        </div>
                                <asp:Label ID="lblmodalyonlendirhareket" class="text-info" runat="server" Visible="false" Text=""></asp:Label>
                                <asp:Label ID="lblhareketsayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
                  </div>
  </div>

      <!-- Modal footer -->
      <div class="modal-footer">
          <button type="button" class="gizle" data-dismiss="modal">Kapat</button>
            <asp:Button ID="btnhareketkapat"    runat="server"  CssClass="btn btn-secondary"  Text="Kapat"    OnClick="btnhareketkapat_Click"  />
      
      </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

         <!-- Modal Sil-->
<div class="modal fade" id="ModalSil" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Kayıt Silinecek</h4>
        <button type="button" class="gizle" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">
        Kayıt sililecektir. Emin misiniz?
          <asp:Label ID="lblmodalsilyonlendir" runat="server" Visible="false" Text=""></asp:Label>
          <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
          <div class="messagealert mt-3 mb-3" id="alert_container_sil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class=" gizle" data-dismiss="modal">Kapat</button>
        <asp:Button ID="btnmodalsilkapat" CssClass="btn btn-secondary"  runat="server" Text="Kapat" OnClick="btnmodalsilkapat_Click" />
        <asp:Button ID="btnsil" CssClass="btn btn-danger"  runat="server" Text="Sil" OnClick="btnsil_Click" />
        <asp:Label ID="lblidsil" Visible="false" runat="server" Text="Label"></asp:Label>

      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

      <div id="YeniMesajDiv">Some text some message..</div>

<div class="loading" >
<br />
Yükleniyor. Lütfen bekleyiniz.<br />
<br />
<div  style="text-align:center;">

        <div class="spinner-grow text-primary" style="width: 50px; height: 50px;"></div>
          <div style="width: 50px; height: 50px;" class="spinner-grow text-primary"></div>
          <div style="width: 50px; height: 50px;" class="spinner-grow text-primary"></div>
</div>
</div>                

</asp:Content>
