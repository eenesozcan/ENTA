<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepoAyar.aspx.cs" Inherits="EnvanterTveY.DepoAyar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <link href="Content/Site.css" rel="stylesheet" />
        <script src="Scripts/KabloHE-js.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>

   <div class="container-fluid" style="margin-left:10px; margin-top: 20px">
             <div class="row">
                <span class="baslik">Malzeme ve Depo Ayarları</span>
            </div>

        <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab4" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="Malzeme Tipi" ID="TabPanel1" CssClass="ajax__tab_panel">
            <ContentTemplate>

    <div class="container-fluid" style="margin-left:30px">
            <div class="row">
            </div>
                <div class="row">

                    <div class="col-md-12 col-xs-12 ">
                        <div class="form-group">
                            <div class="input-group">
                                        <asp:Button ID="btntipekle" runat="server" ValidationGroup="tipkaydet"  CssClass="btn btn-success" Text="Tip Ekle" OnClick="btntipekle_Click"  />
                            </div>
                            </div>
                        </div>
          
                    <div class="row">
                        <div class="table-responsive">

                            <asp:GridView ID="grid_tip_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="500px" OnRowCommand="grid_tip_liste_RowCommand"     >
                                <Columns>
                                    <asp:BoundField DataField="ID"  HeaderText="ID"  SortExpression="ID" InsertVisible="False" ReadOnly="True"  />
                                    <asp:BoundField DataField="TIP" HeaderText="Tip" SortExpression="TIP" />
                            <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                <ItemTemplate > 
                                            <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                            <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                        </div>
                </div>   
    </div>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Malzeme Türü" ID="TabPanel2" CssClass="ajax__tab_panel">
            <ContentTemplate>

                    <div class="container-fluid" style="margin-left:30px">
                                <div class="row">
                                </div>
                                <div class="row">

                        <div class="col-md-12 ">
                            <div class="form-group">
                                <div class="input-group">
                                        <asp:Button ID="btncinsekle" runat="server" ValidationGroup="cinskaydet" CssClass="btn btn-success" Text="Malzeme Türü Ekle"  OnClick="btncinsekle_Click1"   />
                                </div>
                            </div>
                        </div>
          
                        <div class="row">
                            <div class="table-responsive">

                                <asp:GridView ID="grid_cins_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"  Width="800px" OnRowCommand="grid_cins_liste_RowCommand"   >
                                    <Columns>
                                        <asp:BoundField DataField="ID"                HeaderText="ID"                        InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="TIP"               HeaderText="Malzeme Tipi"              />
                                        <asp:BoundField DataField="TURU"              HeaderText="Malzeme Türü"              />
                                        <asp:BoundField DataField="MAKS_TAMIR_SURESI" HeaderText="Maks.Tamir Süresü (Saat)"  />
                            <asp:TemplateField Visible="true" HeaderStyle-Width="200px"  HeaderText="İşlemler">
                                <ItemTemplate > 
                                            <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                            <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </div>
                        </div>

                        </div>           


                </div>                    

            </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Malzeme Durumları" ID="TabPanel3" CssClass="ajax__tab_panel">
                <ContentTemplate>
      <div class="container-fluid" style="margin-left:30px">
        <div class="row">
        </div>
             <div class="row">

                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <div class="input-group">
                                            <asp:Button ID="btnmalzemedurumekle" runat="server" ValidationGroup="malzemedurumkaydet" CssClass="btn btn-success" Text="Malzeme Durumu Ekle" OnClick="btnmalzemedurumekle_Click" />
                                    </div>
                                </div>
                            </div>
          
                           <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="grid_malzemedurum_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"  Width="500px" OnRowCommand="grid_malzemedurum_liste_RowCommand"  >
                                        <Columns>
                                            <asp:BoundField DataField="ID"    HeaderText="ID"     InsertVisible="False" ReadOnly="True"  />
                                            <asp:BoundField DataField="DURUM" HeaderText="Durum"  />
                                <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                   <ItemTemplate > 
                                                <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                   </ItemTemplate>
                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                            </div>

        </div>   
      </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Malzeme Marka" ID="TabPanel4" CssClass="ajax__tab_panel">
                <ContentTemplate>

      <div class="container-fluid" style="margin-left:30px">
        <div class="row">
        </div>
             <div class="row">

                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <div class="input-group">
                                            <asp:Button ID="btnmarkamodelekle" runat="server" ValidationGroup="btnmarkamodelekle" CssClass="btn btn-success" Text="Marka Ekle" OnClick="btnmarkamodelekle_Click" />
                                    </div>
                                </div>
                            </div>
          
                           <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="grid_markamodel_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"  Width="750px" OnRowCommand="grid_markamodel_liste_RowCommand"  >
                                        <Columns>
                                            <asp:BoundField DataField="ID"    HeaderText="ID"    InsertVisible="False" ReadOnly="True"  />
                                            <asp:BoundField DataField="TIP"   HeaderText="Malzeme Tipi"  />
                                            <asp:BoundField DataField="TURU"  HeaderText="Malzeme Türü"  />
                                            <asp:BoundField DataField="MARKA" HeaderText="Marka"         />

                                <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                   <ItemTemplate > 
                                                <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                   </ItemTemplate>
                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                            </div>

        </div>   
  </div>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Malzeme Model" ID="TabPanel5" CssClass="ajax__tab_panel">
                <ContentTemplate>

      <div class="container-fluid" style="margin-left:30px">
        <div class="row">
        </div>
             <div class="row">

                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <div class="input-group">
                                            <asp:Button ID="btnmodelekle" runat="server" ValidationGroup="btnmodelekle" CssClass="btn btn-success" Text="Model Ekle"  OnClick="btnmodelekle_Click" />
                                    </div>
                                </div>
                            </div>
          
                           <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="grid_model_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"  Width="750px" OnRowCommand="grid_model_liste_RowCommand"   >
                                        <Columns>
                                            <asp:BoundField DataField="ID"    HeaderText="ID"    InsertVisible="False" ReadOnly="True"  />
                                            <asp:BoundField DataField="TIP"   HeaderText="Tipi"  />
                                            <asp:BoundField DataField="TURU"  HeaderText="Türü"  />
                                            <asp:BoundField DataField="MARKA" HeaderText="Marka"  />
                                            <asp:BoundField DataField="MODEL" HeaderText="Model"  />
                                <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                   <ItemTemplate > 
                                                <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                   </ItemTemplate>
                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                            </div>
        </div>   
  </div>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Depo Tanımı" ID="TabPanel6" CssClass="ajax__tab_panel">
                <ContentTemplate>

     <div class="container-fluid" style="margin-left:30px">
        <div class="row">
        </div>
             <div class="row">

                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <div class="input-group">
                                            <asp:Button ID="btndepotanimiekle" runat="server" ValidationGroup="depoturkayit" CssClass="btn btn-success" Text="Depo Tanımı Ekle" OnClick="btndepotanimiekle_Click"  />
                                    </div>
                                </div>
                            </div>
          
                           <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="grid_depotanimi_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="600px"  OnRowCommand="grid_depotanimi_liste_RowCommand"  >
                                        <Columns>
                                            <asp:BoundField DataField="ID"                        HeaderText="ID"          InsertVisible="False" ReadOnly="True"  />
                                            <asp:BoundField DataField="DEPOTANIMI"                HeaderText="Depo Tanımı"  />
                                            <asp:BoundField DataField="GARANTILI_MALZEME_KABUL"   HeaderText="Garantili Malzeme Kabul"  />
                                            <asp:BoundField DataField="GARANTISIZ_MALZEME_KABUL"  HeaderText="Garantisiz Malzeme Kabul"  />
                                <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                   <ItemTemplate > 
                                                <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                   </ItemTemplate>
                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                            </div>
        </div>   
  </div>
  
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Depo Türü" ID="TabPanel7" CssClass="ajax__tab_panel">
                <ContentTemplate>

     <div class="container-fluid" style="margin-left:30px">
        <div class="row">
        </div>
             <div class="row">

                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <div class="input-group">
                                            <asp:Button ID="btndepoturekle" runat="server" ValidationGroup="depoturkayit" CssClass="btn btn-success" Text="Depo Türü Ekle" OnClick="btndepoturekle_Click" />
                                    </div>
                                </div>
                            </div>
          
                           <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="grid_depotur_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_depotur_liste_RowCommand" Width="600px"  >
                                        <Columns>
                                            <asp:BoundField DataField="ID"          HeaderText="ID"          InsertVisible="False" ReadOnly="True"  />
                                            <asp:BoundField DataField="DEPOTANIMI"  HeaderText="Depo Tanımı"  />
                                            <asp:BoundField DataField="DEPOTURU"    HeaderText="Depo Türü"    />
                                <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                   <ItemTemplate > 
                                                <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                   </ItemTemplate>
                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                            </div>
        </div>   
  </div>
  
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

        </ajaxToolkit:TabContainer>
   </div>
         </ContentTemplate>
        </asp:UpdatePanel>


          <div id="YeniMesajDiv">Some text some message..</div>

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
</div>  

             <!-- The Modal Model Kaydet -->
<div class="modal" id="Modal_Model_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>

                             <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik5" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-3" id="alert_container_yeni5">
              </div>

           <div class="row">
            <div class="col-md-6">

                <div class="form-group">
                                        <label for="usr">Malzeme Tip Seç:</label>
                                        <asp:DropDownList ID="commodel_tip"  DataTextField="TIP" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="commodel_tip_SelectedIndexChanged"  OnDataBound="commodel_tip_DataBound"  > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="commodel_tip"  ValidationGroup="modelkayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme Tipi seçiniz." ForeColor="red"  />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Malzeme Türü Seç:</label>
                                        <asp:DropDownList ID="commodel_tur"  DataTextField="TURU" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="commodel_tur_SelectedIndexChanged"  OnDataBound="commodel_tur_DataBound"  > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="commodel_tur"  ValidationGroup="modelkayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme Türü seçiniz." ForeColor="red" />
                                    </div>

                                    <div class="form-group">
                                        <label for="usr">Malzeme Marka Seç:</label>
                                        <asp:DropDownList ID="commarka_sec"  DataTextField="MARKA" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="commarka_sec"  ValidationGroup="modelkayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme Marka seçiniz." ForeColor="red"  />
                                    </div>

          <div class="form-group">
               <label for="usr">Malzeme Modeli:</label>
 
               <asp:TextBox ID="txtmodel" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtmodel" ValidationGroup="modelkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Model bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz5" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnmodelkaydet" runat="server" ValidationGroup="modelkayit" CssClass="btn btn-success" Text="Model Kaydet" OnClick="btnmodelkaydet_Click"  />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>



             <!-- The Modal Marka ve Model Kaydet -->
<div class="modal" id="Modal_MarkaModel_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>

                             <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik4" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-3" id="alert_container_yeni4">
              </div>

           <div class="row">
            <div class="col-md-6">


                                    <div class="form-group">
                                        <label for="usr">Malzeme Tip Seç:</label>
                                        <asp:DropDownList ID="commarkamodel_tipsec"  DataTextField="TIP" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="commarkamodel_tipsec_SelectedIndexChanged"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="commarkamodel_tipsec"  ValidationGroup="markamodelkayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme Tipi seçiniz." ForeColor="red"  />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Malzeme Türü Seç:</label>
                                        <asp:DropDownList ID="commarkamodel_tursec"  DataTextField="TURU" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="commarkamodel_tursec"  ValidationGroup="markamodelkayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme Türü seçiniz." ForeColor="red" />
                                    </div>
                                     

          <div class="form-group">
               <label for="usr">Malzeme Markası:</label>
 
               <asp:TextBox ID="txtmarka" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtmarka" ValidationGroup="markamodelkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Marka bilgisi boş geçilemez." />
           </div>

               </div>
                                           <asp:Label ID="lblidcihaz4" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnmarkamodelkaydet" runat="server" ValidationGroup="markamodelkayit" CssClass="btn btn-success" Text="Marka Kaydet" OnClick="btnmarkamodelkaydet_Click"  />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

             <!-- The Modal Malzeme Durum Kaydet -->
<div class="modal" id="Modal_MalzemeDurum_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>

                             <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik3" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-3" id="alert_container_yeni3">
              </div>

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Malzeme Durum Adı:</label>
 
               <asp:TextBox ID="txtmalzemedurum" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtmalzemedurum" ValidationGroup="malzemedurumkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme Durum bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz3" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnmalzemedurumkaydet" runat="server" ValidationGroup="malzemedurumkayit" CssClass="btn btn-success" Text="Malzme Durum Kaydet" OnClick="btnmalzemedurumkaydet_Click" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

             <!-- The Modal Tip Kaydet -->
<div class="modal" id="Modal_Tip_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Tip Adı:</label>
 
               <asp:TextBox ID="txttip" CssClass="form-control" runat="server" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txttip" ValidationGroup="tipkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Tip bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btntipkaydet" runat="server" ValidationGroup="tipkayit" CssClass="btn btn-success" Text="Tip Kaydet" OnClick="btntipkaydet_Click" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

                 <!-- The Modal Depo Tanımı Kaydet -->
<div class="modal" id="Modal_DepoTanimi_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>

                             <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik6" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-3" id="alert_container_yeni6">
              </div>

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Depo Tanımı:</label>
 
               <asp:TextBox ID="txtdepotanimi" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtdepotanimi" ValidationGroup="tanimkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Depo Tanım bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz6" runat="server" Visible="false" Text=""></asp:Label>

            </div>
              <div class="row" style="margin-top:0px">
                <div class="col-md-12" > 
                    <asp:CheckBox class="text rounded p-9 label-garanti-secim bg-success" ID="chc_garantili" runat="server" Text="Garantili Malzeme Kabul" AutoPostBack="true" OnCheckedChanged="chc_garantili_CheckedChanged"   Visible="true" />
                </div>



              </div>
              <div class="row" style="margin-top:0px">
                 <div class="col-md-12" > 
                    <asp:CheckBox class="text rounded p-9 label-garanti-secim bg-success" ID="chc_garantisiz" runat="server" Text="Garantisiz Malzeme Kabul" AutoPostBack="true"  OnCheckedChanged="chc_garantisiz_CheckedChanged"   Visible="true" />
                </div>
              </div>


           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btndepotanimikaydet" runat="server" ValidationGroup="tanimkayit" CssClass="btn btn-success" Text="Depo Tanım Kaydet" OnClick="btndepotanimikaydet_Click"   />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

                 <!-- The Modal Depo Tür Kaydet -->
<div class="modal" id="Modal_DepoTur_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
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

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Depo Tanımı:</label>
 
               <asp:DropDownList ID="comdepotanimi"  DataTextField="DEPOTANIMI" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"  > </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="comdepotanimi"  ValidationGroup="turkayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Depo Tanımı seçiniz." ForeColor="red"  />
                                 
           </div>
               </div>

            </div>

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Depo Türü:</label>
 
               <asp:TextBox ID="txtdepotur" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtdepotur" ValidationGroup="turkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Depo Türü bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz2" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btndepoturkaydet" runat="server" ValidationGroup="depoturkayit" CssClass="btn btn-success" Text="Depo Tür Kaydet" OnClick="btndepoturkaydet_Click"/>

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

         <!-- The Modal -->
<div class="modal fade" id="ModalSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

             <!-- Modal Header -->
              <div class="modal-header">
                 <h4 class="modal-title">Kayıt Silinecek</h4>
                 <button type="button" class="close" data-dismiss="modal">&times;</button>
              </div>

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>

              <!-- Modal body -->
              <div class="modal-body">
                                    <div class="messagealert mt-3 mb-3" id="alert_container_sil">                  </div>
                Silmek istediğinize emin misiniz?
                  <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
                  <asp:Label ID="Label1" runat="server" Visible="false" Text=""></asp:Label>

              </div>

              <!-- Modal footer -->
              <div class="modal-footer">
                  <button type="button"   class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                  <asp:Button ID="btnsil" CssClass="btn btn-danger"  runat="server" Text="Sil" OnClick="btnsil_Click" />
                  <asp:Label ID="lblidsil" Visible="false" runat="server" Text="Label"></asp:Label>
              </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

                 <!-- The Modal Malzeme Tür Kaydet -->
<div class="modal" id="Modal_Cins_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

                             <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik1" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-3" id="alert_container_yeni1">
              </div>

           <div class="row">
            <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="usr">Malzeme Tip Seç:</label>
                                        <asp:DropDownList ID="comtipsec"  DataTextField="TIP" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="comtipsec"  ValidationGroup="cinskayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme Tipi seçiniz." ForeColor="red" />
                                    </div>

               <div class="form-group">
                   <label for="usr">Malzeme Tür Adı:</label>
 
                   <asp:TextBox ID="txtcins" CssClass="form-control" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtcins" ValidationGroup="cinskayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Cins bilgisi boş geçilemez." />
               </div>
               <div class="form-group">
                   <label for="usr">Malzeme Maksimum Tamir Süresü (Saat):</label>
 
                   <asp:TextBox ID="txtmakssure" CssClass="form-control" placeholder="saat"  onkeypress="return sadecerakam(event)" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtmakssure" ValidationGroup="cinskayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Cins bilgisi boş geçilemez." />
               </div>

            </div>
                                           <asp:Label ID="lblidcihaz1" runat="server" Visible="false" Text=""></asp:Label>
            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btncinskaydet" runat="server" ValidationGroup="cinskayit" CssClass="btn btn-success" Text="Malzeme Türü Kaydet" OnClick="btncinskaydet_Click" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

</asp:Content>