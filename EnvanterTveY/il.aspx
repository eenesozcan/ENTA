<%@ Page Title="İl Yönetimi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="il.aspx.cs" Inherits="EnvanterTveY.il" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/KabloHE-js.js"></script>

        

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>

            <div class="container-fluid" style="margin-left:30px; margin-top: 20px" >

                    <div class="row">
                        <span class="baslik">İl Yönetimi</span>
                    </div>

                <div class="row">
                            <div class="col-md-12 col-xs-12">
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtadi"    runat="server"                 CssClass="form-control"        style="margin-right:35px" placeholder="İl Arama"  ></asp:TextBox>
                                        <asp:Button  ID="btnilara"  runat="server" Text="Ara"      CssClass="btn  btn-primary "   style="margin-right:4px"  OnClick="btnilara_Click" />
                                        <asp:Button  ID="btnilekle" runat="server" Text="İl Ekle"  CssClass="btn btn-success"                               OnClick="btnilekle_Click" />
                                    </div>
                                </div>
                            </div>
                </div>

                    <div class="row">
                        <div class="table-responsive">
                        <asp:GridView ID="grid_il_liste" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" OnRowCommand="grid_il_liste_RowCommand" Width="600px"  >
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID"     SortExpression="ID" InsertVisible="False" ReadOnly="True"  />
                                <asp:BoundField DataField="IL" HeaderText="İl Adı" SortExpression="DONEM" />
                    

                                                                       
                                        <asp:TemplateField Visible="true" HeaderStyle-Width="300px"  HeaderText="İşlemler">
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

    </ContentTemplate>
</asp:UpdatePanel>
           
    <div id="YeniMesajDiv">Some text some message..</div>

         <!-- The Modal İl kaydet -->
<div class="modal" id="Modal_il_kaydet" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
               <label for="usr">İl Adı:</label>
 
               <asp:TextBox ID="txtiladi" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtiladi" ValidationGroup="ilkayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl Adı bilgisi boş geçilemez." />
           </div>
               </div>
                                           <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>

            </div>
           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnilkaydet" runat="server" ValidationGroup="ilkayit" CssClass="btn btn-success" Text="İl Kaydet" OnClick="btnilkaydet_Click" />

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
                İl bilgisini silmek istediğinize emin misiniz?
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



</asp:Content>

