<%@ Page Title="Bölge Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bolge.aspx.cs" Inherits="EnvanterTveY.Bolge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    
   <div class="container-fluid" style="margin-left:30px; margin-top: 20px">

    <div class="row">
                <span class="baslik">Bölge Yönetimi</span>
    </div>

    <div class="row">

        <div class="col-md-12 col-xs-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:TextBox ID="txtbadi"       runat="server"                    CssClass="form-control"     style="margin-right:35px" placeholder="Bölge Arama"  AutoPostBack="true"  ></asp:TextBox>
                    <asp:Button  ID="btnbolgeara"   runat="server" Text="Ara"         CssClass="btn  btn-primary" style="margin-right:4px" OnClick="btnbolgeara_Click" />
                    <asp:Button  ID="btnbolgeekle"  runat="server" Text="Bölge Ekle"  CssClass="btn btn-success" OnClick="btnbolgeekle_Click"  />
            </div>
        </div>
     </div>

    <div class="row">
    <div class="table-responsive">

        <asp:GridView ID="grid_bolge_liste" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_bolge_liste_RowCommand" Width="600px"  >
            <Columns>
                <asp:BoundField DataField="ID"        HeaderText="ID" InsertVisible="False" ReadOnly="True"  />
                <asp:BoundField DataField="IL"        HeaderText="İl Adı"  />
                <asp:BoundField DataField="BOLGE_ADI" HeaderText="Bölge Adı"  />
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

            <script src="Scripts/KabloHE-js.js"></script>

    <div id="YeniMesajDiv">Some text some message..</div>

         <!-- The Modal Bölge kaydet -->
<div class="modal" id="Modal_bolge_kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
               <label for="usr">İl Adı Seçin:</label>
                   <br />
                    <asp:DropDownList ID="comil" DataSourceID="SqlDataSource1" DataTextField="IL" DataValueField="ID" CssClass="form-control" runat="server" ></asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KabloHE %>" SelectCommand="SELECT [ID], [IL] FROM [IL]"></asp:SqlDataSource>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="comil" ValidationGroup="kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl Adı bilgisi boş geçilemez." /> 
                     </div>
               </div>
            </div>

           <div class="row">
            <div class="col-md-6">
           <div class="form-group">
               <label for="usr">Bölge Adı:</label>
 
               <asp:TextBox ID="txtbolgeadi" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtbolgeadi" ValidationGroup="kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Bölge Adı bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnbolgekaydet" runat="server" ValidationGroup="kayit" CssClass="btn btn-success" Text="Bölge Kaydet" OnClick="btnbolgekaydet_Click" />

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
                Bölge bilgisini silmek istediğinize emin misiniz?
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



</asp:Content>

