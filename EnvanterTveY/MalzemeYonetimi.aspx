<%@ Page Title="Malzeme Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MalzemeYonetimi.aspx.cs" Inherits="EnvanterTveY.MalzemeYonetimi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/loading.js"></script>

    <script src="Scripts/KabloHE-js.js"></script>
    <link href="Content/Site.css" rel="stylesheet" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="container-fluid"  style="margin-top: 20px">

        <div class="row">
            <span class="baslik">Malzeme Yönetimi</span>
        </div>

        <div class="row">
                <div class="col-md-2 col-xs-12">
                     <label for="">Bölge</label>
                    <asp:DropDownList ID="combolgeara"  AutoPostBack="true" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="combolgeara_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                    <label for="">Tipi</label>
                              <asp:DropDownList ID="comtipara" DataTextField="TIP" DataValueField="ID" CssClass="form-control form-control-sm " runat="server" AutoPostBack="True"  OnSelectedIndexChanged="comtipara_SelectedIndexChanged">
                              </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                     <label for="">Durum</label>
                              <asp:DropDownList ID="comdurumara" DataTextField="SECENEK" DataValueField="ID" CssClass="form-control form-control-sm" runat="server">
                              </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                </div>
                <div class="col-md-2 col-xs-12">
                </div>
                <div class="col-md-2 col-xs-12 text-center">
                    <br />
                </div>
         </div>

                    <div class="row">
                        <div class="col-md-2 col-xs-12">
                            <label for="">Depo</label>
                            <asp:DropDownList ID="comdepoara"  DataTextField="DEPO" DataValueField="ID" CssClass="form-control form-control-sm" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-xs-12">
                              <label for="">Türü</label>
                              <asp:DropDownList ID="comturara" DataTextField="TURU" DataValueField="ID" CssClass="form-control form-control-sm" runat="server">
                              </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-xs-12">
                              <label for="">Seri No</label>
                              <asp:TextBox ID="txtserinoara" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-xs-12">
                        </div>
                        <div class="col-md-4 col-xs-12 text-right">
                            <label for=""></label>
                              <div class="">
                                  <span class="">
                                      <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary" Width="130px"  Text="Ara" OnClick="btnara_Click"   />
                                      <asp:Button ID="btnmalzemeekle"      runat="server" Text="Malzeme Ekle"        CssClass="btn btn-success " Width="130px"   OnClick="btnmalzemeekle_Click"  />
                                    </span>
                              </div>
                        </div>
                    </div>
    
    <div class="row" style="margin-top:20px">
        <div class="table-responsive margin-body">
                        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="20"  OnRowCommand="grid_RowCommand" OnRowDataBound="grid_RowDataBound"  OnPageIndexChanging="grid_PageIndexChanging"   >
                           <Columns>
                                        <asp:BoundField DataField="ID"                HeaderText="ID"                            InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="TIP"               HeaderText="Malzeme Tipi"     />
                                        <asp:BoundField DataField="TURU"              HeaderText="Malzeme Türü"     />
                                        <asp:BoundField DataField="MARKA"             HeaderText="Marka"            />
                                        <asp:BoundField DataField="MODEL"             HeaderText="Model"            />
                                        <asp:BoundField DataField="SERI_NO"           HeaderText="Seri No"          />
                                        <asp:BoundField DataField="DURUM"             HeaderText="Durum"       HtmlEncode="false" />
                                        <asp:BoundField DataField="BOLGE_ADI"         HeaderText="Bölge Adı"        />
                                        <asp:BoundField DataField="DEPO"              HeaderText="Depo Adı"         />
                                        <asp:BoundField DataField="GARANTI"           HeaderText="Garanti Durumu"   />
                                        <asp:BoundField DataField="KAYIT"             HeaderText="Kayıt Eden"     Visible="False"  />
                                        <asp:BoundField DataField="GUNCELLEME_DURUMU" HeaderText="GD"             Visible="false" />

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="280px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="islemler"             runat="server" Text="İşlemler"         CommandName="islemler"        CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnguncelle"          runat="server" Text="Güncelle"         CommandName="guncelle"        CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"               runat="server" Text="Sil"              CommandName="sil"             CssClass="btn btn-danger btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                                <asp:Label ID="lblmalzemesayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
        </div>
        
    </div>
  
    </div>
</ContentTemplate>
</asp:UpdatePanel>

     <!-- Modal Malzeme -->
<div class="modal fade" id="ModalMalzeme" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content" style="margin-top:50px">

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

                    <div class="row">
                            <div class="col-md-6">
                            <div class="form-group">
                                <label for="usr">Malzeme Tipi: (*)</label>
                                <asp:DropDownList ID="commalzemetip"  DataTextField="TIP" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="commalzemetip_SelectedIndexChanged"> </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="commalzemetip"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme tipini seçiniz." ForeColor="red" InitialValue="0" />
                            </div>
                            <div class="form-group">
                                <label for="usr">Malzeme Türü (*)</label>
                                <asp:DropDownList ID="commalzemetur"  DataTextField="TURU" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="commalzemetur_SelectedIndexChanged"> </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="commalzemetur"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme türünü seçiniz." ForeColor="red" InitialValue="0" />
                            </div>
                               <div class="form-group">
                                <label for="usr">Marka (*)</label>
                                <asp:DropDownList ID="commalzememarka"  DataTextField="MARKA" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="commalzememarka_SelectedIndexChanged" > </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="commalzememarka"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme türünü seçiniz." ForeColor="red" InitialValue="0" />
                            </div>

                            <div class="form-group">
                                <label for="usr">Model (*)</label>
                                <asp:DropDownList ID="commalzememodel"  DataTextField="MODEL" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" > </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="commalzememodel"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Malzeme türünü seçiniz." ForeColor="red" InitialValue="0" />
                            </div>
                            <div class="form-group">
                                <label for="usr">Dalga Boyu</label>
                                <asp:DropDownList ID="comdalgaboyu"  DataTextField="DALGABOYU" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" > </asp:DropDownList>
                                
                            </div>
                                </div>
            
                            <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="usr">Seri No (*) </label> <span  ID="seri_no_span" runat="server" class="text-danger fz-10">Aralarına virgül koyarak birden fazla girebilirsiniz</span>
                                        <asp:TextBox ID="txtserino" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtserino"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Garanti Bitiş Tarihi </label>
                                        <asp:TextBox ID="txtgarantibitistarihi" CssClass="form-control" placeholder="Örnek tarih : 01.01.2023"  runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Stok Kodu </label>
                                        <asp:TextBox ID="txtstokkodu" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Bölge Adı (*)</label>
                                        <asp:DropDownList ID="combolge"  DataTextField="BOLGE_ADI" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combolge_SelectedIndexChanged" > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="combolge"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Bölgeyi seçiniz." ForeColor="red" InitialValue="0" />
                                    
                                         </div>

                                    <div class="form-group">
                                        <label for="usr">Depo Adı (*)</label>
                                        <asp:DropDownList ID="comdepo"   DataTextField="DEPO" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"> </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12"  ControlToValidate="comdepo"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Depo bilgisini seçiniz." ForeColor="red" InitialValue="0" />
                                    
                                       </div>

                                    <div class="form-group">
                                       <asp:DropDownList ID="comdurum"  DataTextField="TIP" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" Visible="false"> </asp:DropDownList>
                                   
                                    </div>
                            </div>
         
                            <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblguncellemedurumu" runat="server" Visible="true" Text=""></asp:Label>
                </div>
</div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnmalzemekaydet" runat="server" ValidationGroup="kayit" CssClass="btn btn-success" Text="Malzeme Kaydet" OnClick="btnmalzemekaydet_Click" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>


         <!-- Modal İşlemler -->
<div class="modal fade" id="ModalIslemler" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered min-h60" role="document">
    <div class="modal-content " style="margin-top:50px">

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik3" runat="server" Text="Malzeme İşlemleri"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

                               <!-- Modal body -->
                <div class="modal-body min-h60 ">
                    <div class="row">

                            <asp:Label ID="lblsevkdurumkontrol_durum"   class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblvarlikdurumkontrol_durum" class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lbltamirdepokontrol_durum"   class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblidcihaz_islemler"         class="text-danger" runat="server" Visible="false" Text=""></asp:Label>

                            <asp:Label ID="lblebolge_durum"        class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lbledepo_durum"         class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblybolge_durum"        class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                            <asp:Label ID="lblydepo_durum"         class="text-danger" runat="server" Visible="false" Text=""></asp:Label>
                    <div class="messagealert mt-3 mb-3" id="alert_container_yeni3">
                        </div>
            <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab6" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="Varlık İşlemleri" ID="TabPanel1" CssClass="ajax__tab_panel">
                <ContentTemplate>

                  <div class="container-fluid" >
                  
                             <div class="row f-bold" >
                                <div class="col-md-2">
                                            <label class="text-primary" for="">Malzeme Türü</label>
                                </div>
                                <div class="col-md-3">
                                            <label class="text-primary" for="">Marka</label>
                                </div>
                                <div class="col-md-3">
                                            <label class="text-primary" for="">Model</label>
                                </div>
                                <div class="col-md-2">
                                            <label class="text-primary" for="">Seri No</label>
                                </div>
                                <div class="col-md-2">
                                            <label class="text-primary" for="">Durum</label>
                                </div>
                            </div> 
                            <div class="row" >
                                <div class="col-md-2">
                                            <asp:Label ID="lbltur_varlik" runat="server"    Text=""></asp:Label>
                                </div>
                                <div class="col-md-3">
                                            <asp:Label ID="lblmarka_varlik" runat="server"  Text=""></asp:Label>
                                </div>
                                <div class="col-md-3">
                                            <asp:Label ID="lblmodel_varlik" runat="server"  Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                            <asp:Label ID="lblserino_varlik" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                            <asp:Label ID="lblbolge" runat="server"  Text=""></asp:Label>
                                            <asp:Label ID="lbldurum_varlik" runat="server"  Text=""></asp:Label>
                                </div>
                            </div>
                      <hr />
                                <div class="row" style="margin-top:10px">
                                    <div class="col-md-2">
                                         <asp:DropDownList ID="comlokasyon_kayit"  DataTextField="V_LOKASYON" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"  AutoPostBack="True"  OnSelectedIndexChanged="comlokasyon_kayit_SelectedIndexChanged" > </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="comlokasyon_kayit"  ValidationGroup="varlik"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Varlık seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="col-md-2">
                                         <asp:DropDownList ID="comtip_kayit"  DataTextField="V_TIP" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comtip_kayit_SelectedIndexChanged" > </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="comtip_kayit"  ValidationGroup="varlik"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Varlık seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="col-md-2">
                                         <asp:DropDownList ID="comvarlikadi_kayit"  DataTextField="V_ADI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comvarlikadi_kayit_SelectedIndexChanged" > </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="comvarlikadi_kayit"  ValidationGroup="varlik"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Varlık seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="col-md-2">
                                         <asp:DropDownList ID="comvarlikkod_kayit"  DataTextField="V_KODU" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comvarlikkod_kayit_SelectedIndexChanged"> </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="comvarlikkod_kayit"  ValidationGroup="varlik"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Varlık seçiniz." ForeColor="red" />
                                    </div>
                                <div class="col-md-2">
                                  <asp:Button ID="btnvarlikeslestir"       runat="server" ValidationGroup="varlik" CssClass="btn btn-success" Text="Varlık Eşleştir" OnClick="btnvarlikeslestir_Click" />
                                </div>
                                <div class="col-md-2">
                                             <asp:Label ID="lblvarlikid_varlikhareket"         runat="server" Visible="true" Text=""></asp:Label>
                                            <asp:Label ID="lblvarlikid"         runat="server" Visible="true" Text=""></asp:Label>
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
                                            <asp:Label ID="lblsevkdurum"         runat="server" Visible="true" Text=""></asp:Label>
                                </div>
                            </div>

                              <div class="row  margin-body" >
                                <div class="table-responsive fz-10">
                                                <asp:GridView ID="grid_varlik_malzeme_baglama" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_varlik_malzeme_baglama_RowCommand"   >
                                                   <Columns>
                                                                <asp:BoundField DataField="ID"                      HeaderText="ID"                 />
                                                                <asp:BoundField DataField="V_ID"                    HeaderText="Varlık ID"          />
                                                                <asp:BoundField DataField="V_KODU"                  HeaderText="Varlık Kodu"        />
                                                                <asp:BoundField DataField="M_ID"                    HeaderText="Malzeme ID"         />
                                                                <asp:BoundField DataField="MARKA"                   HeaderText="Marka"              />
                                                                <asp:BoundField DataField="MODEL"                   HeaderText="Model"              />
                                                                <asp:BoundField DataField="SERI_NO"                 HeaderText="Seri No"            />
                                                                <asp:BoundField DataField="V_M_BAGLANTI_DURUMU"     HeaderText="Bağlantı Durumu"   Visible="false"  />
                                                                <asp:BoundField DataField="V_M_BAGLANTI_TARIHI"     HeaderText="Bağlantı Tarihi"    />
                                                                <asp:BoundField DataField="V_M_AYRILMA_TARIHI"      HeaderText="Ayrılma Tarihi"    Visible="false"  />
                                                                <asp:BoundField DataField="KAYIT"                   HeaderText="Kayıt"              />

                                                                <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                                    <ItemTemplate > 
                                                                        <asp:Button ID="btnsil"         runat="server" Text="Sil"             CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                   </Columns>
                                                </asp:GridView>
                                            <asp:Label ID="lblmalzemesayisi_varlik_malzeme"         runat="server" Visible="true" Text=""></asp:Label>

                                </div>
                            </div>
                  </div>
 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Durum Değişikliği" ID="TabPanel2" CssClass="ajax__tab_panel">
                <ContentTemplate>

                             
                  <div class="container-fluid" >
                                    <div class="row f-bold " >
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">Garanti Durum / Bitiş Tarihi</label>
                                        </div>
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">Mevcut Durum</label>
                                        </div>
                                        <div class="col-md-3">
                                                    <label class="text-primary" for="">Yeni Durum</label>
                                        </div>
                                        <div class="col-md-4">
                                                    <label class="text-primary" for="">Gerekçe</label>
                                        </div>

                                    </div> 
                                    <div class="row" style="margin-top:10px">
                                        <div class="col-md-2">
                                                    <asp:Label ID="lblgarantidurum" runat="server"    Text="" Visible="true"></asp:Label>
                                                    <asp:Label ID="lblgarantibitistarihi"   runat="server"    Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                                    <asp:Label ID="lblmevcutdurum_durumid" runat="server"    Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblmevcutdurum_durum"   runat="server"    Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                                     <asp:DropDownList ID="comyenidurum_durum" DataTextField="SECENEK" DataValueField="ID" CssClass="form-control form-control-sm" runat="server"> </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="comyenidurum_durum"  ValidationGroup="durumdegistir"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Yeni Durum seçiniz." ForeColor="red"  />

                                        </div>
                                        <div class="col-md-4">
                                                    <asp:TextBox ID="txtgerekce_durum" ValidationGroup="durumdegistir" CssClass="form-control form-control-sm" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                        </div>

                                    </div>
                                  <div class="row" style="margin-top:10px">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-2">
                                                    <asp:Button ID="btndurumdegistir_durum" runat="server" ValidationGroup="durumdegistir"  CssClass="btn btn-success" Text="Durum Değiştir" OnClick="btndurumdegistir_durum_Click"  />

                                        </div>
                                       </div>
                  </div>     

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Hareketler" ID="TabPanel3" CssClass="ajax__tab_panel">
                <ContentTemplate>
                    <asp:Panel runat="server" >

                    
                  <div class="container-fluid " style="  "  >
                         <div class="row scroll-y" >
                            <div class="table-responsive fz-10 margin-body scrool-x-unset h-40vh scroll-y scroll-x">
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
                        </asp:Panel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel runat="server" HeaderText="Durum Değişiklik Hareketleri" ID="TabPanel4" CssClass="ajax__tab_panel">
                    <ContentTemplate>
                          <div class="row  " >
                            <div class="table-responsive  fz-10 margin-body scrool-x-unset h-40vh scroll-y scroll-x " >
                                            <asp:GridView ID="grid_ariza" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="M_ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="50"   >
                                               <Columns>

                                                            <asp:BoundField DataField="M_ID"              HeaderText="Malzeme ID"           Visible="false"             InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="BOLGE_ADI"         HeaderText="Ait Olduğu Bölge"      />
                                                            <asp:BoundField DataField="E_DURUM"           HeaderText="Malzeme Eski Durum"    />
                                                            <asp:BoundField DataField="Y_DURUM"           HeaderText="Malzeme Yeni Durum"    />
                                                            <asp:BoundField DataField="GEREKCE"           HeaderText="Gerekçe"               />
                                                            <asp:BoundField DataField="ACIKLAMA"          HeaderText="Açıklama"              />
                                                            <asp:BoundField DataField="KAYIT"             HeaderText="Kayıt"                 />

                                               </Columns>
                                            </asp:GridView>
                            </div>
                         </div>
                                                <asp:Label ID="lblhareketsayisi_ariza" class="text-info" runat="server" Visible="true" Text=""></asp:Label>

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel runat="server" HeaderText="Varlık Değişiklik Hareketleri" ID="TabPanel5" CssClass="ajax__tab_panel">
                    <ContentTemplate>
                   <asp:Button ID="btnhareketgor" runat="server"   CssClass="btn btn-info" Text="Hareket Gör" OnClick="btnhareketgor_Click"   />

                          <div class="row  ">
                            <div class="table-responsive  fz-10  margin-body scrool-x-unset h-40vh scroll-y scroll-x " >
                                            <asp:GridView ID="grid_varlik_hareket" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="V_ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="50"   >
                                               <Columns>

                                                            <asp:BoundField DataField="V_ID"         HeaderText="Varlık ID"     />
                                                            <asp:BoundField DataField="M_ID"         HeaderText="Malzeme ID"    />
                                                            <asp:BoundField DataField="V_LOKASYON"   HeaderText="V.Lokasyon"    Visible="false"/>
                                                            <asp:BoundField DataField="V_TIP"        HeaderText="V.Tipi"        Visible="false" />
                                                            <asp:BoundField DataField="V_ADI"        HeaderText="V.Adı"         Visible="false"/>
                                                            <asp:BoundField DataField="V_KODU"       HeaderText="V.Kodu"        Visible="false"/>
                                                            <asp:BoundField DataField="IL"           HeaderText="İl"            Visible="false" />
                                                            <asp:BoundField DataField="BOLGE_ADI"    HeaderText="Bölge"         Visible="false"/>
                                                            <asp:BoundField DataField="V_DURUM"      HeaderText="V.Durum"       Visible="false" />
                                                            <asp:BoundField DataField="ENLEM"        HeaderText="Enlem"         Visible="false" />
                                                            <asp:BoundField DataField="BOYLAM"       HeaderText="Boylam"        Visible="false" />
                                                            <asp:BoundField DataField="ADRES"        HeaderText="Adres"         Visible="false" />
                                                            <asp:BoundField DataField="LOG_ACIKLAMA" HeaderText="Açıklama"      />
                                                            <asp:BoundField DataField="KAYIT"        HeaderText="Kayıt"         />

                                               </Columns>
                                            </asp:GridView>
                            </div>
                         </div>
                                                <asp:Label ID="lblhareketsayisi_varlikhareket" class="text-info" runat="server" Visible="true" Text=""></asp:Label>

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

        </ajaxToolkit:TabContainer>                

                    </div>
  </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="xxxx" CssClass="btn btn-success"  runat="server" Text="XXX" Visible="false"  />

      </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

         <!-- Modal Malzeme Sil-->
<div class="modal fade" id="ModalMalzemeSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Malzeme Sil1</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">

        Kayıt silinecek. Emin misiniz?
          <asp:Label ID="lblislem_malzemesil" runat="server" Visible="false" Text=""></asp:Label>

          <div class="messagealert mt-3 mb-3" id="alert_container_malzemesil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnmalzemesil" CssClass="btn btn-danger" ValidationGroup="sil"   runat="server" Text="Sil" OnClick="btnmalzemesil_Click"   />
                             <asp:Label ID="lblmalzemeidsil" Visible="false"  runat="server" Text="Label"></asp:Label>

      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

         <!-- Modal Malzeme Varlık Bağlantısı Sil-->
<div class="modal fade" id="ModalSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Malzeme Varlık Bağlantısı Sil</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">

                <div class="col-md-12">
                    <label for="usr">Malzemenin Varlık Bağlantısı Silindikten Sonraki Durumunu Seç:</label>
                    <asp:DropDownList ID="comyenidurumsec_varlikbaglantisil" DataTextField="SECENEK" DataValueField="ID" CssClass="form-control form-control-sm" runat="server"> </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="comyenidurumsec_varlikbaglantisil"  ValidationGroup="sil"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Yeni Durum seçiniz." ForeColor="red" InitialValue="0"  />
                </div>
          <br />
                <div class="col-md-12">
                   <asp:TextBox ID="txtgerekce_varlikbaglantisil" ValidationGroup="sil" CssClass="form-control form-control-sm" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </div>

        Kayıt silinecek. Emin misiniz?
          <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
          <asp:Label ID="Label1" runat="server" Visible="false" Text=""></asp:Label>
          <div class="messagealert mt-3 mb-3" id="alert_container_sil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnsil" CssClass="btn btn-danger" ValidationGroup="sil"   runat="server" Text="Sil" OnClick="btnsil_Click" />
                             <asp:Label ID="lblidsil" Visible="False"  runat="server" Text="Label"></asp:Label>

      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

    <asp:SqlDataSource ID="sqlcomdepo" runat="server" ConnectionString="<%$ ConnectionStrings:KabloHE %>" ></asp:SqlDataSource>

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

</asp:Content>
