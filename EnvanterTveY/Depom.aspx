<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Depom.aspx.cs" Inherits="EnvanterTveY.Depom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <script src="Scripts/KabloHE-js.js"></script>


 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    
   <div class="container-fluid" style="margin-left:30px; margin-top: 20px">

    <div class="row">
                <span class="baslik">Depo Yönetimi</span>
    </div>

    <div class="row">

        <div class="col-md-12 col-xs-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:TextBox ID="txtdepoadiara"  runat="server"                    CssClass="form-control"     style="margin-right:35px" placeholder="Depo Arama"  AutoPostBack="true"  ></asp:TextBox>
                    <asp:Button  ID="btndepoara"     runat="server" Text="Ara"         CssClass="btn  btn-primary" style="margin-right:4px"  OnClick="btndepoara_Click" />
                    <asp:Button  ID="btndepoekle"    runat="server" Text="Depo Ekle"   CssClass="btn btn-success"                            OnClick="btndepoekle_Click"  />
            </div>
        </div>
     </div>

    <div class="row">
    <div class="table-responsive">

        <asp:GridView ID="grid_depo_liste" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_depo_liste_RowCommand" Width="1200px"  >
            <Columns>
                <asp:BoundField DataField="ID"             HeaderText="ID"           InsertVisible="False" ReadOnly="True"  />
                <asp:BoundField DataField="BOLGE_ADI"      HeaderText="Bölge Adı"    />
                <asp:BoundField DataField="DEPO"           HeaderText="Depo Adı"     />
                <asp:BoundField DataField="DEPOTANIMI"     HeaderText="Depo Tanımı"  />
                <asp:BoundField DataField="DEPOTURU"       HeaderText="Depo Türü"    />

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
    </asp:UpdatePanel>



         <!-- The Modal Depo kaydet -->
<div class="modal" id="Modal_depo_kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg  modal-dialog-centered" role="document">
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
               <label for="usr">Bölge Seçin:</label>
                   <br />
                  <asp:DropDownList ID="combolge" Width="400px" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" > </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="combolge"  ValidationGroup="kayit12"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Bölgeyi seçiniz." ForeColor="red"  />
           
           </div>
          </div>

         <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Depo Tanımı Seçin:</label>
                   <br />
                    <asp:DropDownList ID="comdepotanimi" Width="400px" DataTextField="DEPOTANIMI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comdepotanimi_SelectedIndexChanged"> </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="comdepotanimi"  ValidationGroup="kayit12"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Depo Tanımını seçiniz." ForeColor="red"  />

           </div>
          </div>
        </div>

        <div class="row">
             <div class="col-md-6">
               <div class="form-group">
                   <label for="usr">Depo Adı:</label>
                   <asp:TextBox ID="txtdepoadi" CssClass="form-control" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtdepoadi" ValidationGroup="kayit12" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Depo Adı bilgisi boş geçilemez." ForeColor="red" />
               </div>
             </div>

             <div class="col-md-6">
               <div class="form-group">
                   <label for="usr">Depo Türü Seçin:</label>
                       <br />
               <asp:DropDownList ID="comdepoturu" Width="400px" DataTextField="DEPOTURU" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" > </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="comdepoturu"  ValidationGroup="kayit2"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Depo Türünü seçiniz." ForeColor="red"  />

               </div>
              </div>

         </div>
                  <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btndepokaydet" runat="server" ValidationGroup="kayit2" CssClass="btn btn-success" Text="Depo Kaydet" OnClick="btndepokaydet_Click"/>

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
                Depo bilgisini silmek istediğinize emin misiniz?
                  <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
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


      <div id="YeniMesajDiv">Some text some message..</div>

<div class="loading" align="center">
<br />
Yükleniyor. Lütfen bekleyiniz.<br />
<br />
<div class="ball"></div>
</div>

</asp:Content>
