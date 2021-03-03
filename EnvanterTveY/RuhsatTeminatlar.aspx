<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RuhsatTeminatlar.aspx.cs" Inherits="EnvanterTveY.Teminatlar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
<script src="Scripts/loading.js"></script>

    <script src="Scripts/KabloHE-js.js"></script>
    <link href="Content/Site.css" rel="stylesheet" />

   <script>
    $(function () {
        $('[id*=txtDate_baslangic1]').datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd/mm/yyyy",
            language: "tr"
        });
        });

        function dateshow() {
            var txt = $("txtDate_baslangic1");
            alert(txt.val);
            //document.getElementById('txtdate').click();
        }

    </script>

     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css"/>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <div class="container-fluid"  style="margin-top: 20px">

        <div class="row">
            <span class="baslik">Teminatlar</span>

        </div>

        <div class="row">
                <div class="col-md-2 col-xs-12">
                    <label for="">Bölge</label>
                    <asp:DropDownList ID="combolgeara"  AutoPostBack="true" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" >
                    </asp:DropDownList>
                </div>

                <div class="col-md-2 col-xs-12">
                </div>
                        <div class="col-md-4 col-xs-12 text-right">
                            <label for=""></label>
                              <div class="">
                                  <span class="">
                                      <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary" Width="130px"  Text="Ara" OnClick="btnara_Click"      />
                                      <asp:Button ID="btnteminatekle"      runat="server" Text="Teminat Ekle"        CssClass="btn btn-success " Width="130px" OnClick="btnteminatekle_Click"      />
                                    </span>
                              </div>
                        </div>
         </div>

    <div class="row" style="margin-top:20px">
        <div class="table-responsive margin-body">
                        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="50" OnRowCommand="grid_RowCommand" OnPageIndexChanging="grid_PageIndexChanging"      >
                           <Columns>

                                        <asp:BoundField DataField="ID"             HeaderText="ID"          InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="IL"             HeaderText="İl"               />
                                        <asp:BoundField DataField="BOLGE_ADI"      HeaderText="Bölge"            />
                                        <asp:BoundField DataField="RUHSAT_VEREN"   HeaderText="Teminatın Verildiği Yer"             />
                                        <asp:BoundField DataField="TARIH"          HeaderText="Teminat Tarihi"      />
                                        <asp:BoundField DataField="SURE"           HeaderText="Teminat Süresi"          />
                                        <asp:BoundField DataField="TUTAR"          HeaderText="Teminat Tutarı"      />
                                        <asp:BoundField DataField="KALAN"          HeaderText="Kalan Tutar"      />

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="300px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="goruntule"            runat="server" Text="Görüntüle"        CommandName="goruntule"       CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="guncelle"             runat="server" Text="Güncelle"         CommandName="guncelle"        CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"               runat="server" Text="Sil"              CommandName="sil"             CssClass="btn btn-danger btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                                <asp:Label ID="lblgridsay" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
        </div>
        
    </div>

    </div>
</ContentTemplate>
</asp:UpdatePanel>




         <!-- Modal Sil-->
<div class="modal fade" id="ModalSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Silme İşlemi</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">

        Kayıt silinecek. Emin misiniz?
          <asp:Label ID="lblislem_sil" runat="server" Visible="false" Text=""></asp:Label>

          <div class="messagealert mt-3 mb-3" id="alert_container_sil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnsil" CssClass="btn btn-danger" ValidationGroup="sil"   runat="server" Text="Sil"  OnClick="btnsil_Click"     />
                             <asp:Label ID="lblidsil" Visible="false"  runat="server" Text="Label"></asp:Label>

      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>



     <!-- Modal Teminat Ekle -->
<div class="modal fade" id="ModalTeminat"  role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable modal-dialog-centered" role="document" style="max-width: 55%;">
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


<asp:Panel ID="panel_kayit_guncelle"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
        <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                <label class="text-primary" for="">İl</label>
                        </div>
                        <div class="col-md-4">
                                <asp:DropDownList ID="comil_kayit"  DataTextField="IL" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comil_kayit_SelectedIndexChanged"        > </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="comil_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>

                        <div class="col-md-2">
                                <label class="text-primary"  for="">Teminat Süresi (Ay)</label>
                        </div>
                        <div class="col-md-4">
                                <asp:TextBox ID="txtteminatsuresi_kayit" CssClass="form-control" onkeypress="return onlyNumberAndDot(event)" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtteminatsuresi_kayit" ValidationGroup="kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red"  Text="Boş geçilemez."  />

                        </div>
        </div>

        <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Bölge</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="combolge_kayit"  DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combolge_kayit_SelectedIndexChanged"          > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="combolge_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>

                        <div class="col-md-2">
                                <label class="text-primary"  for="">Teminat Tarihi</label>
                        </div>
                        <div class="col-md-4">
                                <asp:TextBox ID="txtteminattarihi_kayit" CssClass="form-control" onkeypress="return onlyNumberAndDot(event)" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtteminattarihi_kayit" ValidationGroup="kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red"  Text="Boş geçilemez."  />
                        </div>
        </div>

        <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Teminat Teslim Yeri</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="comteminatinverilen_kayit"  DataTextField="RUHSAT_VEREN" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"                > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="comteminatinverilen_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
                        <div class="col-md-2">
                                <label class="text-primary"  for="">Teminat Tutarı</label>
                        </div>
                        <div class="col-md-4">
                                <asp:TextBox ID="txttutar_kayit" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event)" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txttutar_kayit" ValidationGroup="kayit" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red"  Text="Boş geçilemez."  />
                        </div>

        </div>
</asp:Panel>

<asp:Panel ID="panel_goruntule"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
	<div class="card bg-light">
        <div class="row"style="margin-top:10px">

                        <div class="col-md-3">
                            <label class="text-primary" for="">İl</label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblil_goster" runat="server" ></asp:Label>
                        </div>					
                        <div class="col-md-3">
                            <label class="text-primary" for="">Teminat Süresi</label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblsure_goster" runat="server" ></asp:Label>
                        </div>							
		    </div>

            <div class="row"style="margin-top:10px">
                            <div class="col-md-3">
                                <label class="text-primary" for="">Bölge</label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblbolge_goster" runat="server" ></asp:Label>
                            </div>					
                            <div class="col-md-3">
                                <label class="text-primary" for="">Teminatın Tarihi</label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbltarih_goster" runat="server" ></asp:Label>
                            </div>	

            </div>

            <div class="row"style="margin-top:10px">
                            <div class="col-md-3">
                                <label class="text-primary" for="">Verileceği Yer</label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblteminatverilenyer_goster" runat="server" ></asp:Label>
                            </div>					
                            <div class="col-md-3">
                                <label class="text-primary" for="">Teminat Tutarı</label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbltutar_goster" runat="server" ></asp:Label>
                            </div>	

            </div>
            <div class="row"style="margin-top:10px">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                            </div>					
                            <div class="col-md-3">
                                <label class="text-primary" for="">Teminat Kalan Tutar</label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblkalantutar_goster" runat="server" ></asp:Label>
                            </div>	

            </div>

        </div>


                <div class="row" style="margin-top:20px;overflow: auto;height: 500px;">
        <div class="table-responsive margin-body">
                        <asp:GridView ID="grid_teminat_ruhsat" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="50"  OnRowCommand="grid_teminat_ruhsat_RowCommand"  OnSelectedIndexChanging="grid_teminat_ruhsat_SelectedIndexChanging"          >
                           <Columns>

                                        <asp:BoundField DataField="ID"                HeaderText="ID"          InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="IL"                HeaderText="İl"     Visible="false"          />
                                        <asp:BoundField DataField="BOLGE_ADI"         HeaderText="Bölge"            />
                                        <asp:BoundField DataField="ILCE"              HeaderText="Teminatın Verildiği Yer"             />
                                        <asp:BoundField DataField="RUHSATNO"          HeaderText="Ruhsat No"      />
                                        <asp:BoundField DataField="T_PROJE_TIPI"      HeaderText="Proje Tipi"          />
                                        <asp:BoundField DataField="BASLANGIC_TARIHI"  HeaderText="Ruhsat Tarihi"      />
                                        <asp:BoundField DataField="TEMINAT_TOPLAM"    HeaderText="Teminata Konu Tutar"      />

                                        <asp:TemplateField Visible="false" HeaderStyle-Width="400px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="goruntule"            runat="server" Text="Görüntüle"        CommandName="goruntule"       CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"               runat="server" Text="Sil"              CommandName="sil"             CssClass="btn btn-danger btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                                <asp:Label ID="lblgrid_teminat_ruhsatsay" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
        </div>
        
    </div>

</asp:Panel>


                                  <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>


            
      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
        <asp:Button ID="btnkaydet" runat="server" ValidationGroup="kayit" CssClass="btn btn-success" Text="Teminat Kaydet" OnClick="btnkaydet_Click"       />
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
