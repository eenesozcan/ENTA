<%@ Page Title="Varlık Ayarları" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VarlikAyar.aspx.cs" Inherits="EnvanterTveY.VarlikAyar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/Site.css" rel="stylesheet" />
        <script src="Scripts/KabloHE-js.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
          
    <div class="container-fluid" style="margin-left:10px; margin-top: 20px">
             <div class="row">
                <span class="baslik">Varlık Ayarları</span>      </div>

        <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab4" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="Varlık Lokasyonu" ID="TabPanel1" CssClass="ajax__tab_panel">
                <ContentTemplate>

                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">
                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                                <asp:Button ID="btnvarliklokasyon_ekle" runat="server" ValidationGroup="varlikkaydet"  CssClass="btn btn-success" Text="Varlık Lokasyon Ekle" OnClick="btnvarliklokasyon_ekle_Click" />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_lokasyon_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_lokasyon_liste_RowCommand" Width="500px" >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"         HeaderText="ID"   InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="V_LOKASYON" HeaderText="Varlık Lokasyonu"  />

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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Varlık Tipi" ID="TabPanel2" CssClass="ajax__tab_panel">
                <ContentTemplate>
                                        
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">
                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                                <asp:Button ID="btnvarliktipi_ekle" runat="server"  CssClass="btn btn-success" Text="Varlık Tipi Ekle" OnClick="btnvarliktipi_ekle_Click" />
                                                        </div>
                                                    </div>
                                                </div>
          
                                               <div class="row">
                                                    <div class="table-responsive">

                                                        <asp:GridView ID="grid_tip_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"  Width="500px"  OnRowCommand="grid_tip_liste_RowCommand">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID"          HeaderText="ID"    InsertVisible="False" ReadOnly="True"  />
                                                                <asp:BoundField DataField="V_LOKASYON"  HeaderText="Varlık Lokasyonu"   />
                                                                <asp:BoundField DataField="V_TIP"       HeaderText="Varlık Tİpi" />

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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Varlık Adı" ID="TabPanel3" CssClass="ajax__tab_panel" Visible="true">
                <ContentTemplate>

                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">
                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                                <asp:Button ID="btnvarlikadi_ekle" runat="server" ValidationGroup="varlikkaydet" CssClass="btn btn-success" Text="Varlık Adı Ekle" OnClick="btnvarlikadi_ekle_Click" />
                                                        </div>
                                                    </div>
                                                </div>
          
                                               <div class="row">
                                                    <div class="table-responsive">

                                                        <asp:GridView ID="grid_adi_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı"   Width="700px" OnRowCommand="grid_adi_liste_RowCommand"  >
                                                            <Columns>
                                                                <asp:BoundField DataField="ID"          HeaderText="ID"    InsertVisible="False" ReadOnly="True"  />
                                                                <asp:BoundField DataField="V_LOKASYON"  HeaderText="Varlık Lokasyonu"   />
                                                                <asp:BoundField DataField="V_TIP"       HeaderText="Varlık Tipi"  />
                                                                <asp:BoundField DataField="V_ADI"       HeaderText="Varlık Adı"  />

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




             <!-- The Modal Varlık Lokasyon -->
<div class="modal" id="Modal_Lokasyon_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
               <label for="usr">Lokasyon :</label>
 
               <asp:TextBox ID="txtlokasyon" CssClass="form-control" runat="server" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtlokasyon" ValidationGroup="varlik_lokasyon_kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="Red" />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnlokasyon_kaydet" runat="server" ValidationGroup="varlik_lokasyon_kayit" CssClass="btn btn-success" Text="Lokasyon Kaydet" OnClick="btnlokasyon_kaydet_Click" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

                 <!-- The Modal Varlık Tipi -->
<div class="modal" id="Modal_Tip_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
                                        <label for="usr">Varlık Lokasyonu Seç:</label>
                                        <asp:DropDownList ID="comlokasyon_sec"  DataTextField="ELEMAN_ADI" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="comlokasyon_sec"  ValidationGroup="varlik_tip_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Lokasyon seçiniz." ForeColor="red" />
                                    </div>

           <div class="form-group">
               <label for="usr">Tamir Eleman Çeşit Tür Adı:</label>
 
               <asp:TextBox ID="txttip" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txttip" ValidationGroup="varlik_tip_kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz1" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btntip_kaydet" runat="server" ValidationGroup="varlik_tip_kayit" CssClass="btn btn-success" Text="Varlık Tip Kaydet" OnClick="btntip_kaydet_Click" />

      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

                 <!-- The Modal Varlık Adı -->
<div class="modal" id="Modal_Ad_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
                                        <label for="usr">Varlık Lokasyonu Seç:</label>
                                        <asp:DropDownList ID="comlokasyon_sec1"  DataTextField="V_LOKASYON" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="comlokasyon_sec1_SelectedIndexChanged"  > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="comlokasyon_sec1"  ValidationGroup="varlik_adi_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Lokasyon seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label for="usr">Varlık Tipi Seç:</label>
                                        <asp:DropDownList ID="comtip_sec1"  DataTextField="V_TIP" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="comtip_sec1"  ValidationGroup="varlik_adi_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Tip seçiniz." ForeColor="red" />
                                    </div>
                                   <div class="form-group">
                                       <label for="usr">Varlık Adı:</label>
 
                                       <asp:TextBox ID="txtad" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtad" ValidationGroup="varlik_adi_kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>
                                           <asp:Label ID="lblidcihaz2" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnad_kaydet" runat="server" ValidationGroup="varlik_adi_kayit" CssClass="btn btn-success" Text="Varlık Adı Kaydet"  OnClick="btnad_kaydet_Click" />

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
                  <asp:Label ID="Label1" runat="server" Visible="false" Text=""></asp:Label>
                  <div class="messagealert mt-3 mb-3" id="alert_container_sil">
                  </div>
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

            <div id="YeniMesajDiv">Some text some message..</div>

<div class="loading" align="center">
     <br />
    Yükleniyor. Lütfen bekleyiniz.<br />
    <br />
        <div class="ball"></div>
        <div class="ball1"></div>
</div>
</asp:Content>
