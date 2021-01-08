<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SevkAyar.aspx.cs" Inherits="EnvanterTveY.SevkAyar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <link href="Content/Site.css" rel="stylesheet" />
        <script src="Scripts/KabloHE-js.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
          
    <div class="container-fluid" style="margin-left:10px; margin-top: 20px">
             <div class="row">
                <span class="baslik">Sevk Ayarları</span>
            </div>

        <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab4" runat="server" ActiveTabIndex="1"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="Sevk Durum" ID="TabPanel1" CssClass="ajax__tab_panel">
                <ContentTemplate>

   <div class="container-fluid" style="margin-left:30px">
        <div class="row">
        </div>
             <div class="row">

                        <div class="col-md-12 col-xs-12 ">
                            <div class="form-group">
                                <div class="input-group">
                                            <asp:Button ID="btnsevkdurumekle" runat="server" ValidationGroup="sevkdurumkaydet"  CssClass="btn btn-success" Text="Sevk Durum Ekle" OnClick="btnsevkdurumekle_Click" />
                                </div>
                             </div>
                         </div>
          
                        <div class="row">
                            <div class="table-responsive">

                                <asp:GridView ID="grid_sevkdurum_liste" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_sevkdurum_liste_RowCommand" Width="500px" >
                                    <Columns>
                                        <asp:BoundField DataField="ID"      HeaderText="ID"           InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="S_DURUM" HeaderText="Sevk Durum"   />
                                <asp:TemplateField Visible="true"           HeaderStyle-Width="150px"  HeaderText="İşlemler">
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
<div class="loading" align="center">
     <br />
    Yükleniyor. Lütfen bekleyiniz.<br />
    <br />
        <div class="ball"></div>
        <div class="ball1"></div>
</div>

             <!-- The Modal Sevk Durum Kaydet -->
<div class="modal" id="Modal_SevkDurum_Kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
               <label for="usr">Sevk Durum Adı:</label>
 
               <asp:TextBox ID="txtsevkdurum" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtsevkdurum" ValidationGroup="sevkdurumkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Sevk Durum bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnsevkdurumkaydet" runat="server" ValidationGroup="sevkdurumkayit" CssClass="btn btn-success" Text="Malzme Durum Kaydet" OnClick="btnsevkdurumkaydet_Click" />

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
                  <asp:Button ID="btnsil" CssClass="btn btn-danger"  runat="server" Text="Sil" OnClick="btnsil_Click"/>
                  <asp:Label ID="lblidsil" Visible="false" runat="server" Text="Label"></asp:Label>
              </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

</asp:Content>
