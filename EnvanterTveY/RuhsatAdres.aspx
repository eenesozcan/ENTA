<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RuhsatAdres.aspx.cs" Inherits="EnvanterTveY.RuhsatAdres" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <link href="Content/Site.css" rel="stylesheet" />
         <script src="Scripts/KabloHE-js.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
          
    <div class="container-fluid" style="margin-left:10px; margin-top: 20px"">
             <div class="row">
                <span class="baslik">Ruhsat Adres Yönetimi</span>
            </div>

        <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab4" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="İl" ID="TabPanel1" CssClass="ajax__tab_panel">
                <ContentTemplate>

                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                        <asp:TextBox ID="txtadi"    runat="server"                 CssClass="form-control"        style="margin-right:35px" placeholder="İl Arama"  ></asp:TextBox>
                                        <asp:Button  ID="btnilara"  runat="server" Text="Ara"      CssClass="btn  btn-primary "   style="margin-right:4px"  OnClick="btnilara_Click"     />
                                                                <asp:Button ID="btnil_ekle" runat="server" ValidationGroup="adres_il_kaydet"  CssClass="btn btn-success" Text="İl Ekle"  Visible="false"  />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_il_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"  >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"         HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"         HeaderText="İL"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="false"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="false"  CommandArgument='<%# Container.DataItemIndex %>' />
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
			
            <ajaxToolkit:TabPanel runat="server" HeaderText="Bölge" ID="TabPanel2" CssClass="ajax__tab_panel">
                <ContentTemplate>

                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                                <asp:TextBox ID="txtbadi"       runat="server"                    CssClass="form-control"     style="margin-right:35px" placeholder="Bölge Arama"  AutoPostBack="true"  ></asp:TextBox>
                                                                <asp:Button  ID="btnbolgeara"   runat="server" Text="Ara"         CssClass="btn  btn-primary" style="margin-right:4px" OnClick="btnbolgeara_Click"   />
                                                                <asp:Button ID="btnbolge_ekle" runat="server" ValidationGroup="adres_bolge_kaydet"  CssClass="btn btn-success" Text="Bölge Ekle"  Visible="false"    />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_bolge_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"     >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"         HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"         HeaderText="İL"  />
                                                            <asp:BoundField DataField="BOLGE_ADI"  HeaderText="BÖLGE"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="false"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="false"  CommandArgument='<%# Container.DataItemIndex %>' />
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

            <ajaxToolkit:TabPanel runat="server" HeaderText="İlçe" ID="TabPanel3" CssClass="ajax__tab_panel">
                <ContentTemplate>

                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                                <asp:Button ID="btnilce_ekle" runat="server" ValidationGroup="adres_ilce_kaydet"  CssClass="btn btn-success" Text="İlçe Ekle" OnClick="btnilce_ekle_Click"     />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_ilce_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px" OnRowCommand="grid_ilce_liste_RowCommand"   >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"         HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"         HeaderText="İL"  />
                                                            <asp:BoundField DataField="BOLGE_ADI"  HeaderText="BÖLGE"  />
                                                            <asp:BoundField DataField="ILCE"       HeaderText="İLÇE"  />

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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Mahalle" ID="TabPanel4" CssClass="ajax__tab_panel">
                <ContentTemplate>
                                        
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">
                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                                <asp:Button ID="btnmahalle_ekle" runat="server"  CssClass="btn btn-success" Text="Mahalle Ekle" OnClick="btnmahalle_ekle_Click"  />
                                                        </div>
                                                    </div>
                                                </div>
          
                                               <div class="row">
                                                    <div class="table-responsive">

                                                        <asp:GridView ID="grid_mahalle_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"  Width="900px"  OnRowCommand="grid_mahalle_liste_RowCommand"    >
                                                            <Columns>
                                                            <asp:BoundField DataField="ID"        HeaderText="ID"   InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"        HeaderText="İL"  />
                                                            <asp:BoundField DataField="BOLGE_ADI" HeaderText="BÖLGE" />
                                                            <asp:BoundField DataField="ILCE"      HeaderText="İLÇE"  />
                                                            <asp:BoundField DataField="MAHALLE"   HeaderText="MAHALLE"  />

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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Cadde/Sokak Adı" ID="TabPanel5" CssClass="ajax__tab_panel" Visible="true">
                <ContentTemplate>

                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">
                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                                <asp:Button ID="btncaddesokak_ekle" runat="server" ValidationGroup="caddesokak_kaydet" CssClass="btn btn-success" Text="Cadde/Sokak Ekle"  OnClick="btncaddesokak_ekle_Click"    />
                                                        </div>
                                                    </div>
                                                </div>
          
                                               <div class="row">
                                                    <div class="table-responsive">

                                                        <asp:GridView ID="grid_caddesokak_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"   Width="950px" OnRowCommand="grid_caddesokak_liste_RowCommand"      >
                                                            <Columns>
                                                            <asp:BoundField DataField="ID"         HeaderText="ID"   InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"         HeaderText="İL"  />
                                                            <asp:BoundField DataField="BOLGE_ADI"  HeaderText="BÖLGE"  />
                                                            <asp:BoundField DataField="ILCE"       HeaderText="İLÇE"  />
                                                            <asp:BoundField DataField="MAHALLE"    HeaderText="MAHALLE"  />
                                                            <asp:BoundField DataField="CADDESOKAK" HeaderText="CADDE / SOKAK"  />

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

         <!-- The Modal İl kaydet -->
<div class="modal" id="Modal_il_kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>

                             <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik_il" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-3" id="alert_container_il">
              </div>

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">İl Adı:</label>
 
               <asp:TextBox ID="txtiladi" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtiladi" ValidationGroup="adres_il_kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl Adı bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="Label2" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnilkaydet" runat="server" ValidationGroup="adres_il_kayit" CssClass="btn btn-success" Text="İl Kaydet"  Visible="false" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

         <!-- The Modal Bölge kaydet -->
<div class="modal" id="Modal_bolge_kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>

                             <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik_bolge" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-3" id="alert_container_bolge">
              </div>

              <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">İl Adı Seçin:</label>
                   <br />
                        <asp:DropDownList ID="comil_sec1"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"     > </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="comil_sec1"  ValidationGroup="adres_bolge_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                     </div>
               </div>
            </div>

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Bölge Adı:</label>
 
               <asp:TextBox ID="txtbolgeadi" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtbolgeadi" ValidationGroup="adres_bolge_kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Bölge Adı bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="Label4" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnbolgekaydet" runat="server" ValidationGroup="adres_bolge_kayit" CssClass="btn btn-success" Text="Bölge Kaydet" Visible="false" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>


             <!-- The Modal ilçe -->
<div class="modal" id="Modal_ilce_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
                                        <label for="usr">İl Seç:</label>
                                        <asp:DropDownList ID="comil_sec"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comil_sec_SelectedIndexChanged"  > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="comil_sec"  ValidationGroup="adres_ilce_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>

                                   <div class="form-group">
                                        <label for="usr">Bölge Seç:</label>
                                        <asp:DropDownList ID="combolge_sec"  DataTextField="BOLGE_ADI" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combolge_sec_SelectedIndexChanged"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="combolge_sec"  ValidationGroup="adres_ilce_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Bölge seçiniz." ForeColor="red" />
                                    </div>

           <div class="form-group">
               <label for="usr">İlçe Adı :</label>
 
               <asp:TextBox ID="txtilce" CssClass="form-control" runat="server" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtilce" ValidationGroup="adres_ilce_kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="Red" />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnilce_kaydet" runat="server" ValidationGroup="adres_ilce_kayit" CssClass="btn btn-success" Text="İlçe Kaydet" OnClick="btnilce_kaydet_Click"  />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

                 <!-- The Modal Mahalle -->
<div class="modal" id="Modal_Mahalle_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
                                        <label for="usr">İl:</label>
                                        <asp:DropDownList ID="comil_sec_mahalle"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comil_sec_mahalle_SelectedIndexChanged"      > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="comil_sec_mahalle"  ValidationGroup="adres_mahalle_kayit1"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">İlçe:</label>
                                        <asp:DropDownList ID="combolge_sec_mahalle"  DataTextField="BOLGE_ADI" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="combolge_sec_mahalle_SelectedIndexChanged"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="combolge_sec_mahalle"  ValidationGroup="adres_mahalle_kayit1"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Bölge seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">İlçe:</label>
                                        <asp:DropDownList ID="comilce_sec"  DataTextField="ILCE" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="comilce_sec"  ValidationGroup="adres_mahalle_kayit1"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İlçe seçiniz." ForeColor="red" />
                                    </div>

           <div class="form-group">
               <label for="usr">Mahalle :</label>
 
               <asp:TextBox ID="txtmahalle" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtmahalle" ValidationGroup="adres_mahalle_kayit1" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblcomsecici_mahalle" runat="server" Visible="false" Text=""></asp:Label>
                                           <asp:Label ID="lblidcihaz1" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnmahalle_kaydet" runat="server" ValidationGroup="adres_mahalle_kayit1" CssClass="btn btn-success" Text="Mahalle Kaydet"  OnClick="btnmahalle_kaydet_Click"    />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

                 <!-- The Modal Cadde Sokak -->
<div class="modal" id="Modal_CaddeSokak_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
                                        <label for="usr">İl:</label>
                                        <asp:DropDownList ID="comil_sec_caddesokak"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="comil_sec_caddesokak_SelectedIndexChanged"      > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="comil_sec_caddesokak"  ValidationGroup="adres_caddesokak_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">İlçe:</label>
                                        <asp:DropDownList ID="combolge_sec_caddesokak"  DataTextField="BOLGE_ADI" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="combolge_sec_caddesokak_SelectedIndexChanged"       > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="combolge_sec_caddesokak"  ValidationGroup="adres_caddesokak_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Bölge seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">İlçe:</label>
                                        <asp:DropDownList ID="comilce_sec_caddesokak"  DataTextField="ILCE" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="comilce_sec_caddesokak_SelectedIndexChanged"    > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="comilce_sec_caddesokak"  ValidationGroup="adres_caddesokak_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İlçe seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Mahalle Seç:</label>
                                        <asp:DropDownList ID="commahalle_sec"  DataTextField="MAHALLE" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="commahalle_sec_SelectedIndexChanged"    > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="commahalle_sec"  ValidationGroup="adres_caddesokak_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Mahalle seçiniz." ForeColor="red" />
                                    </div>
                                   <div class="form-group">
                                       <label for="usr">Cadde/Sokak Adı:</label>
 
                                       <asp:TextBox ID="txtcaddesokak" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtcaddesokak" ValidationGroup="adres_caddesokak_kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>
                                           <asp:Label ID="lblidcihaz2" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btncaddesokak_kaydet" runat="server" ValidationGroup="adres_caddesokak_kayit" CssClass="btn btn-success" Text="Cadde/Sokak Kaydet" OnClick="btncaddesokak_kaydet_Click"   />

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
                Silmek istediğinize emin misiniz?
                  <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
                  <div class="messagealert mt-3 mb-3" id="alert_container_sil">
                  </div>
              </div>

              <!-- Modal footer -->
              <div class="modal-footer">
                  <button type="button"   class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                  <asp:Button ID="btnsil" CssClass="btn btn-danger"  runat="server" Text="Sil" OnClick="btnsil_Click"   />
                  <asp:Label ID="lblidsil" Visible="false" runat="server" Text="Label"></asp:Label>
              </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

            <div id="YeniMesajDiv">Some text some message..</div>

<div class="loading" align="center">
     <br />
    Yükleniyor. Lütfen bekleyiniz.<br />
    <br />
        <div class="ball"></div>
        <div class="ball1"></div>
</div>
</asp:Content>
