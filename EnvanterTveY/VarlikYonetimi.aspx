<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VarlikYonetimi.aspx.cs" Inherits="EnvanterTveY.VarlikYonetimi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/loading.js"></script>

    <script src="Scripts/KabloHE-js.js"></script>
    <link href="Content/Site.css" rel="stylesheet" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="container-fluid" style="margin-left:30px; margin-top: 20px">

        
        <div class="row">
            <span class="baslik">Varlık Yönetimi</span>
        </div>

        <div class="row">
                <div class="col-md-2 col-xs-12">
                    <label for="">Bölge</label>
                    <asp:DropDownList ID="combolge_ara" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                    <label for="">Lokasyon</label>
                    <asp:DropDownList ID="comlokasyon_ara" DataTextField="V_LOKASYON" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comlokasyon_ara_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                    <label for="">Tip</label>
                    <asp:DropDownList ID="comtip_ara" DataTextField="V_TIP" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comtip_ara_SelectedIndexChanged"   >
                    </asp:DropDownList>
                </div>

        </div>
        <div class="row">

                        <div class="col-md-2 col-xs-12">
                                <label for="">Varlık Adı</label>
                                <asp:DropDownList ID="comvarlikadi_ara" DataTextField="V_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server">
                                </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-xs-12">
                              <label for="">Varlık Kodu</label>
                              <asp:TextBox ID="txtvarlikkodu_ara" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-xs-12">
                            <label for="">Durum</label>
                            <asp:DropDownList ID="comdurum_ara" DataTextField="DURUM" DataValueField="ID" CssClass="form-control form-control-sm" runat="server">
                                <asp:ListItem>Seç</asp:ListItem>
                                <asp:ListItem Value="True">Aktif</asp:ListItem>
                                <asp:ListItem Value="False">Pasif</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-xs-12 text-center">
                            
                                  <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary btn-sm" Width="130px"  Text="Ara" OnClick="btnara_Click"  />
                            <asp:Button ID="btnvarlikekle"      runat="server" Text="Varlık Ekle"        CssClass="btn btn-success btn-sm" Width="130px" Style="margin-top:3px"  OnClick="btnvarlikekle_Click"    />

                        </div>

        </div>
    
    <div class="row" style="margin-top:20px">
        <div class="table-responsive">
                        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="30" OnRowCommand="grid_RowCommand" OnRowDataBound="grid_RowDataBound" OnPageIndexChanging="grid_PageIndexChanging"     >
                           <Columns>
                                        <asp:BoundField DataField="ID"               HeaderText="ID"         InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="IL"               HeaderText="İl Adı"     />
                                        <asp:BoundField DataField="BOLGE_ADI"        HeaderText="Bölge Adı"  />
                                        <asp:BoundField DataField="V_LOKASYON"       HeaderText="Lokasyonu"  />
                                        <asp:BoundField DataField="V_TIP"            HeaderText="Tipi"       />
                                        <asp:BoundField DataField="V_ADI"            HeaderText="Adı"        />
                                        <asp:BoundField DataField="V_KODU"           HeaderText="KODU"       />
                                        <asp:BoundField DataField="V_DURUM"          HeaderText="Durumu"     />
                                        <asp:BoundField DataField="V_ADRES"          HeaderText="Adres"      />

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="280px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="islemler"             runat="server" Text="İşlemler"         CommandName="islemler"        CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnguncelle"          runat="server" Text="Güncelle"         CommandName="guncelle"        CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"               runat="server" Text="Sil"              CommandName="sil"             CssClass="btn btn-danger btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
           
                        </asp:GridView>
                                <asp:Label ID="lblmalzemesayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
        </div>
      </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>

     <!-- Modal Varlık Ekle -->
<div class="modal fade" id="ModalVarlikEkle" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content">

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

                    <div class="row"style="margin-top:10px">
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">İl</label>
                                        </div>
                                        <div class="col-md-4">
                                             <asp:DropDownList ID="comil_kayit"  DataTextField="IL" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comil_kayit_SelectedIndexChanged"  > </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="comil_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>
             
                                        <div class="col-md-2">
                                                     <label class="text-primary"  for="">Varlık Tipi</label>
                                        </div>
                                        <div class="col-md-4">
                                             <asp:DropDownList ID="comtip_kayit"  DataTextField="V_TIP" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comtip_kayit_SelectedIndexChanged"    > </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="comtip_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                     </div>
                    </div>

                    <div class="row"style="margin-top:10px">
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">Bölge</label>
                                        </div>
                                        <div class="col-md-4">
                                           <asp:DropDownList ID="combolge_kayit"  DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="combolge_kayit_SelectedIndexChanged" > </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="combolge_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>

                                        <div class="col-md-2">
                                                     <label class="text-primary"  for="">Varlık Adı</label>
                                        </div>
                                        <div class="col-md-4">
                                             <asp:DropDownList ID="comvarlikadi_kayit"  DataTextField="V_ADI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"  > </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="comvarlikadi_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>
                    </div>

                    <div class="row"style="margin-top:10px">
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">Varlık Lokasyonu</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="comlokasyon_kayit"  DataTextField="V_LOKASYON" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comlokasyon_kayit_SelectedIndexChanged"   > </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="comlokasyon_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>
                                        <div class="col-md-2">
                                                     <label class="text-primary"  for="">Varlık Kodu</label>
                                        </div>
                                        <div class="col-md-4">
                                          <asp:TextBox ID="txtvarlikkodu_kayit" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtvarlikkodu_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" />
                                        </div>
                    </div>


<asp:Panel ID="panel_dolap"  CssClass="panel panel-primary" DefaultButton="" runat="server">
<hr />
                    <div class="row">
  
                                    <div class="form-group">
                                        <label for="usr">&nbsp;&nbsp;&nbsp;&nbsp;En :&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Boy :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Yükseklik : </label>
                                        <table class="table">
                                              <th scope="col"><asp:TextBox ID="txten_kayit"        CssClass="form-control" Width="90px" placeholder="50,5"  runat="server"></asp:TextBox></th>
                                              <th scope="col"><asp:TextBox ID="txtboy_kayit"       CssClass="form-control" Width="90px" placeholder="50,5"  runat="server"></asp:TextBox></th>
                                              <th scope="col"><asp:TextBox ID="txtyukseklik_kayit" CssClass="form-control" Width="90px" placeholder="80,5"  runat="server"></asp:TextBox></th>
                                        </table>
                                    </div>
                        
                                    <div class="form-group">
                                        <label for="usr">&nbsp;&nbsp;&nbsp;Enlem :&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Boylam : </label>
                                        <table class="table">
                                              <th scope="col"><asp:TextBox ID="txtenlem_kayit"  CssClass="form-control" Width="150px" placeholder="Örnek : 30.0000000"  runat="server"></asp:TextBox></th>
                                              <th scope="col"><asp:TextBox ID="txtboylam_kayit" CssClass="form-control" Width="150px" placeholder="Örnek 30.0000000"  runat="server"></asp:TextBox></th>
                                        </table>
                                    </div>

                    </div>

                        <div class="row">
                              <div class="col-md-6">
                                        <label for="usr">Adres :</label>
                                        <asp:TextBox ID="txtadres_kayit" CssClass="form-control" TextMode ="MultiLine" runat="server"></asp:TextBox>
                                  </div>
                              <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="usr">Dolap Konum :</label>
                                            <asp:DropDownList ID="comdolapkonum_kayit"  DataTextField="SECENEK" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        </div>
                                           <div class="form-group">
                                            <label for="usr">Dolap Kira Bölgesi :</label>
                                            <asp:DropDownList ID="comdolapkira_kayit"  DataTextField="SECENEK" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        </div>
                                  </div>
                            </div>
</asp:Panel>

<asp:Panel ID="panel_bina"  CssClass="panel panel-primary" DefaultButton="" runat="server">
<hr />
                    <div class="row"style="margin-top:10px">
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">İlçe</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="comilce_kayit"  DataTextField="ILCE" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="comilce_kayit_SelectedIndexChanged"    > </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="comilce_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">Cadde/Sokak</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="comcdsk_kayit"  DataTextField="CADDESOKAK" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comcdsk_kayit_SelectedIndexChanged"        > </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="comcdsk_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>

                    </div>
                    <div class="row"style="margin-top:10px">
                                        <div class="col-md-2">
                                                    <label class="text-primary" for="">Mahalle</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="commahalle_kayit"  DataTextField="MAHALLE" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="commahalle_kayit_SelectedIndexChanged"     > </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="commahalle_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>
                                        <div class="col-md-2">
                                                     <label class="text-primary"  for="">Bina No</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="combinano_kayit"  DataTextField="BINANO" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="combinano_kayit_SelectedIndexChanged"        > </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="combinano_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                        </div>
                    </div>	

</asp:Panel>

                    <br />
                    <div class="row">
                                <label for="usr">Varlık Durumu :  </label>
                                  <asp:CheckBox ID="chcaktif_pasif" runat="server" Style="margin-left:4px" Text="" Checked="true" OnCheckedChanged="chcaktif_pasif_CheckedChanged"  />      
                            <asp:Label ID="chckontrol" runat="server" Style="margin-left:4px" Visible="True" Text=""></asp:Label>
                    </div>
         
                            <asp:Label ID="lblmahalle" runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="lblcaddesokak" runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="lblbinano" runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="lblilce" runat="server" Visible="true" Text=""></asp:Label>

                                  <asp:Label ID="lblidadres" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

  </div>
            
      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>

          <asp:Button ID="btnvarlikkaydet" runat="server" ValidationGroup="kayit" CssClass="btn btn-success" Text="Varlık Kaydet" OnClick="btnvarlikkaydet_Click" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

             <!-- Modal Malzeme_Varlık Ekle -->
<div class="modal fade" id="ModalMalzemeVarlikEkle" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
                              <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik2" runat="server" Text="Label"></asp:Label></h4>

                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

                <!-- Modal body -->
       <div class="modal-body">
        
                    <div class="messagealert mt-3 mb-3" id="alert_container_malzemeekle">
                    </div>

                        <div class="row" style="margin-top:10px">
                            <div class="col-md-3">
                                                <asp:TextBox ID="txtserinoara"  placeholder="Seri No Arama"  ForeColor="Blue" CssClass="form-control form-control-sm" runat="server" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtserinoara"  ValidationGroup="serinoara"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" />
                             </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                  <asp:Button ID="btnmalzemeekle" runat="server" ValidationGroup="serinoara" CssClass="btn btn-success" Text="Malzeme Ekle" OnClick="btnmalzemeekle_Click"    />
                            </div>
                            <div class="col-md-3">
                            </div>
                       </div>

                    <asp:Label ID="lblsevkdurumkontrol_durum"      runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="lblidcihaz_islemler"      runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="lbltamirdepokontrol_durum"      runat="server" Visible="false" Text=""></asp:Label>

        </div>

							<div class="row  margin-body" >
                             <div class="row scroll-y" >
                                <div class="table-responsive fz-10 margin-body scrool-x-unset h-30vh  scroll-x">
                                                <asp:GridView ID="grid_varlik_malzeme_baglama" runat="server" ScrollBars="verticle" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_varlik_malzeme_baglama_RowCommand"       >
                                                   <Columns>
                                                                <asp:BoundField DataField="ID"                      HeaderText="ID"               />
                                                                <asp:BoundField DataField="V_ID"                    HeaderText="Varlık ID"        />
                                                                <asp:BoundField DataField="V_KODU"                  HeaderText="Varlık Kodu"      />
                                                                <asp:BoundField DataField="M_ID"                    HeaderText="Malzeme ID"       />
                                                                <asp:BoundField DataField="MARKA"                   HeaderText="Marka"            />
                                                                <asp:BoundField DataField="MODEL"                   HeaderText="Model"            />
                                                                <asp:BoundField DataField="SERI_NO"                 HeaderText="Seri No"          />
                                                                <asp:BoundField DataField="V_M_BAGLANTI_DURUMU"     HeaderText="Bağlantı Durumu"  Visible="false"  />
                                                                <asp:BoundField DataField="V_M_BAGLANTI_TARIHI"     HeaderText="Bağlantı Tarihi"  />
                                                                <asp:BoundField DataField="V_M_AYRILMA_TARIHI"      HeaderText="Ayrılma Tarihi"   Visible="false"  />
                                                                <asp:BoundField DataField="KAYIT"                   HeaderText="Kayıt"            />

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
            
      <!-- Modal footer -->
      <div class="modal-footer">
      
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
    
      </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>


     <!-- Modal Sil-->
<div class="modal fade" id="ModalSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Malzeme Sil</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">
        Malzeme silinecek. Emin Misiniz?
          <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
          <asp:Label ID="Label1" runat="server" Visible="false" Text=""></asp:Label>
          <div class="messagealert mt-3 mb-3" id="alert_container_sil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnsil" CssClass="btn btn-danger"  runat="server" Text="Sil" OnClick="btnsil_Click" />
                             <asp:Label ID="lblidsil" Visible="False" runat="server" Text="Label"></asp:Label>

      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>


         <!-- Modal Malzeme Varlık Bağlantısı Sil-->
<div class="modal fade" id="ModalMalzemeSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Malzeme Varlık Bağlantısı Sil</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">

                <div class="col-md-12">
                    <label for="usr">Malzemenin Varlık Bağlantısı Silindikten Sonraki Durumunu Seç:</label>
                    <asp:DropDownList ID="comyenidurumsec_varlikbaglantisil" DataTextField="SECENEK" DataValueField="ID" CssClass="form-control form-control-sm" runat="server"> </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="comyenidurumsec_varlikbaglantisil"  ValidationGroup="sil"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Yeni Durum seçiniz." ForeColor="red" InitialValue="0"  />
                </div>
          <br />
                <div class="col-md-12">
                   <asp:TextBox ID="txtgerekce_varlikbaglantisil" ValidationGroup="sil" CssClass="form-control form-control-sm" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </div>

        Kayıt silinecek. Emin misiniz?
          <asp:Label ID="Label2" runat="server" Visible="false" Text=""></asp:Label>
          <asp:Label ID="Label3" runat="server" Visible="false" Text=""></asp:Label>
          <div class="messagealert mt-3 mb-3" id="alert_container_silmalzeme">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnmalzemesil" CssClass="btn btn-danger" ValidationGroup="sil"   runat="server" Text="Sil" OnClick="btnmalzemesil_Click"   />
                             <asp:Label ID="lblidsil1" Visible="False"  runat="server" Text="Label"></asp:Label>

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
