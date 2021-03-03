<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RuhsatTalep.aspx.cs" Inherits="EnvanterTveY.RuhsatTalep" %>
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

        <script type="text/javascript">
            

        function FileOpen() {
            document.getElementById("MainContent_FileUpload1").click();
            return false;
        }

        function FileChange(oFile) {
            document.getElementById("MainContent_btndosyasec").value = oFile.value.split('\\')[oFile.value.split('\\').length - 1];
            document.getElementById("MainContent_lblfile").innerText =oFile.value;
        }


        function splitpath(paths) {
        var st = paths.split("\\");
        return st[st.length - 1];
        }
        function updatemyname() {
        var varfile = document.getElementById("Uploadfile");
        document.getElementById("fileone").value = splitpath(varfile.value);
        }

    </script>

<script>
$(document).ready(function(){
    $('[data-toggle="popover"]').popover({
        placement : 'top'
    });
});
</script>


     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css"/>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>



            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="container-fluid"  style="margin-top: 20px">

        <div class="row">
            <span class="baslik">Ruhsatlar</span>

        </div>
       
        <div class="row">
                <div class="col-md-1 col-xs-2">
                    <label class="text-primary" for="">Bölge</label>
                </div>

                <div class="col-md-2 col-xs-2">
                    <asp:DropDownList ID="combolgeara"  AutoPostBack="true" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" >
                    </asp:DropDownList>
                </div>
                <div class="col-md-1 col-xs-2">
                    <label class="text-primary" for="">Ruhsat Durumu</label>
                </div>
                <div class="col-md-2 col-xs-2">
                    <asp:DropDownList ID="comdurumara"  AutoPostBack="True" DataTextField="DURUM" DataValueField="ID" CssClass="form-control form-control-sm " runat="server"  >
                    </asp:DropDownList>
                </div>

                        <div class="col-md-4 col-xs-2 text-right">
                              <div class="">
                                  <span class="">
                                      <asp:Button ID="btnara" runat="server"   CssClass="btn  btn-primary"  Width="130px"  Text="Ara"  OnClick="btnara_Click"   />
                                      <asp:Button ID="btnruhsatekle"      runat="server" Text="Ruhsat Ekle"        CssClass="btn btn-success " Width="130px"  OnClick="btnruhsatekle_Click"  />
                                    </span>
                              </div>
                        </div>
         </div>

    <div class="row" style="margin-top:20px">
        <div class="table-responsive margin-body">
                        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="30"  OnRowCommand="grid_RowCommand"  OnPageIndexChanging="grid_PageIndexChanging"  OnRowDataBound="grid_RowDataBound"   >
                           <Columns>

                                        <asp:BoundField DataField="ID"                HeaderText="ID"          InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="RUHSATNO"          HeaderText="Ruhsat No"        />
                                        <asp:BoundField DataField="PROJENO"           HeaderText="Proje No"         />
                                        <asp:BoundField DataField="DURUM"             HeaderText="Durum"        HtmlEncode="False"   />
                                        <asp:BoundField DataField="IL"                HeaderText="İl"               />
                                        <asp:BoundField DataField="BOLGE_ADI"         HeaderText="Bölge"            />
                                        <asp:BoundField DataField="ILCE"              HeaderText="İlçe"             />
                                        <asp:BoundField DataField="MAHALLE"           HeaderText="Mahalle"          />
                                        <asp:BoundField DataField="CADDESOKAK"        HeaderText="Cadde/Sokak"      />
                                        <asp:BoundField DataField="ADRES_DETAY"       HeaderText="Adres Detay"      />
                                        <asp:BoundField DataField="T_PROJE_TIPI"      HeaderText="T Proje Tipi"     />
                                        <asp:BoundField DataField="A_PROJE_TIPI"      HeaderText="A Proje Tipi"     />
                                        <asp:BoundField DataField="BASLANGIC_TARIHI"  HeaderText="Başlangıç Tarihi" />
                                        <asp:BoundField DataField="BITIS_TARIHI"      HeaderText="Bitiş Tarihi"     />
                                        <asp:BoundField DataField="TEMINAT"           HeaderText="Ödeme Şekli"  HtmlEncode="False"     />
                                        <asp:BoundField DataField="GUNCELLEME_DURUMU" HeaderText="GD"             Visible="false" />

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="300px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="goruntule"            runat="server" Text="Görüntüle"        CommandName="goruntule"       CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="islemler"             runat="server" Text="Kabul"            CommandName="kabul"           CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="kesifbilgi"           runat="server" Text="Bilgi"      CommandName="kesifbilgi"      CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="btnsil"               runat="server" Text="Sil"              CommandName="sil"             CssClass="btn btn-danger btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                           </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                                <asp:Label ID="lblruhsatsayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
        </div>
        
    </div>

    </div>
</ContentTemplate>
</asp:UpdatePanel>


         <!-- Modal Ruhsat Sil - Kabul Kayıt Oluştur ve Tamamla-->
<div class="modal fade" id="ModalSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
                              <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblmodalyenibaslik2" runat="server" Text="Label"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                              </div>

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">

          <asp:Label ID="lblislem_sil" runat="server" Visible="false" Text=""></asp:Label>

          <div class="messagealert mt-3 mb-3" id="alert_container_sil">
          </div>


                <asp:Panel ID="Panel_Sil"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                        Kayıt silinecek. Emin misiniz?
                </asp:Panel>

                <asp:Panel ID="Panel_Kabul_Olustur"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                       <label class="text-primary" for="">Kabul kaydı oluşturulsun mu?</label>
                          <asp:Label ID="lblislem_ruhsatkabul" runat="server" Visible="false" Text=""></asp:Label>


                </asp:Panel>

                <asp:Panel ID="Panel_Tamamlandi_Olustur"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                       <label class="text-primary" for="">Ruhsat süreci tamamlandı mı?</label>

                </asp:Panel>

      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
       <!--  <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button> -->
          <asp:Button ID="btnsilkapat" CssClass="btn btn-secondary"   runat="server" Text="Kapat" OnClick="btnsilkapat_Click"      />
          <asp:Button ID="btnsil"               CssClass="btn btn-danger"  ValidationGroup="sil"   runat="server" Text="Sil"  OnClick="btnsil_Click"  />
          <asp:Button ID="btnkabulolustur"      CssClass="btn btn-success" runat="server" Text="Evet K"  OnClick="btnkabulolustur_Click"      />
          <asp:Button ID="btntamamlandiolustur" CssClass="btn btn-success" runat="server" Text="Evet T"  OnClick="btntamamlandiolustur_Click"     />

                             <asp:Label ID="lblruhsatidsil" Visible="false"  runat="server" Text="Label"></asp:Label>
                             <asp:Label ID="lblruhsatid"    Visible="false"  runat="server" Text="Label"></asp:Label>

      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

         <!-- Modal Ruhsat İşlem Aktar-->
<div class="modal fade" id="Modalislem" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
            <div class="modal-header">
            <h4 class="modal-title">
                <asp:Label ID="lblmodalyenibaslik1" runat="server" Text="Labelagfg"></asp:Label></h4>
            <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">
                   
          <div class="messagealert mt-3 mb-3" id="alert_container_ruhsatislem">
          </div>



<asp:Panel ID="Panel_Kesif_Bilgi"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
keşif bilgi keşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgi
    keşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgi
    keşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgikeşif bilgi
</asp:Panel>


      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                             <asp:Label ID="lblruhsatislem" Visible="false"  runat="server" Text="Label"></asp:Label>

      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

     <!-- Modal Ruhsat Ekle -->
<div class="modal fade" id="ModalRuhsatTalep" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
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
    <asp:Panel ID="Panel_Ruhsat_Olustur"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">


        <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab6" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="Ruhsat Bilgi" ID="TabPanel1" CssClass="ajax__tab_panel">
                <ContentTemplate>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Ruhsat No</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtruhsatno_kayit" CssClass="form-control" runat="server"></asp:TextBox>
						</div>
             
                        <div class="col-md-2">
                                        <label class="text-primary"  for="">Proje No</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtprojeno_kayit" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">İl</label>
                        </div>
                        <div class="col-md-4">
                                <asp:DropDownList ID="comil_kayit"  DataTextField="IL" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comil_kayit_SelectedIndexChanged"    > </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="comil_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
             
                        <div class="col-md-2">
                                        <label class="text-primary"  for="">Mahalle</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="commahalle_kayit"  DataTextField="MAHALLE" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="commahalle_kayit_SelectedIndexChanged"       > </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Bölge</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="combolge_kayit"  DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="combolge_kayit_SelectedIndexChanged"     > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="combolge_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>

                        <div class="col-md-2">
                                        <label class="text-primary"  for="">Cadde/Sokak</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="comcdsk_kayit"  DataTextField="CADDESOKAK" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"       > </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">İlçe</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="comilce_kayit"  DataTextField="ILCE" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="comilce_kayit_SelectedIndexChanged"         > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="comilce_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Adres Detay</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtadresdetay" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
<hr />
                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Ruhsat Yetkilisi</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="comruhsatveren"  DataTextField="RUHSAT_VEREN" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="comruhsatveren_SelectedIndexChanged"              > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="comruhsatveren"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Tarife Dönemi</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="comdonem"  DataTextField="DONEM" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="comdonem_SelectedIndexChanged"        > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="comdonem"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
                    </div>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Türksat Proje Tipi</label>
                        </div>
                        <div class="col-md-4">
                                <asp:DropDownList ID="comtprojetipi_kayit"  DataTextField="T_PROJE_TIPI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"    > </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="comtprojetipi_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
                        <div class="col-md-2">
                                        <label class="text-primary"  for="">Aykome Proje Tipi</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="comaprojetipi_kayit"  DataTextField="A_PROJE_TIPI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="comaprojetipi_kayit_SelectedIndexChanged"        > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="comaprojetipi_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
                   </div>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                <label class="text-primary" for="">Başlangıç Tarihi</label>
                        </div>
                        <div class="col-md-4">
                                <asp:TextBox ID="txtDate_baslangic" onkeypress="return onlyNumberAndDot(event)"  CssClass="form-control"  runat="server"></asp:TextBox>

                                    <!--             <div class="input-group input-group-lg date" id="datetimepicker1" data-target-input="nearest">
                                                    <input type="text" id="txtDate_baslangic1" class="form-control datetimepicker-input" data-target="#datetimepicker1" />
                                                    <div class="input-group-append" data-target="#datetimepicker1" data-toggle="datetimepicker">
                                                        <div class="input-group-text "  onclick="dateshow();"> <svg aria-hidden="true" height="25px" focusable="false" data-prefix="far" data-icon="calendar-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" class="svg-inline--fa fa-calendar-alt fa-w-14 fa-5x"><path fill="currentColor" d="M148 288h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm108-12v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 96v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm192 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96-260v352c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V112c0-26.5 21.5-48 48-48h48V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h128V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h48c26.5 0 48 21.5 48 48zm-48 346V160H48v298c0 3.3 2.7 6 6 6h340c3.3 0 6-2.7 6-6z" class=""></path></svg> </div>
                                                    </div>     
                                                </div> -->
                        </div>
                        <div class="col-md-2">
                                <label class="text-primary"  for="">Bitiş Tarihi</label>
                        </div>
                        <div class="col-md-4">
                                <asp:TextBox ID="txtDate_bitis" onkeypress="return onlyNumberAndDot(event)" CssClass="form-control" runat="server"></asp:TextBox>

                                   <!--              <div class="input-group input-group-lg date" id="datetimepicker2" data-target-input="nearest">
                                                    <input type="text" id="txtDate_bitis" class="form-control datetimepicker-input" data-target="#datetimepicker1" />
                                                    <div class="input-group-append" data-target="#datetimepicker1" data-toggle="datetimepicker">
                                                        <div class="input-group-text "  onclick="dateshow();"> <svg aria-hidden="true" height="25px" focusable="false" data-prefix="far" data-icon="calendar-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" class="svg-inline--fa fa-calendar-alt fa-w-14 fa-5x"><path fill="currentColor" d="M148 288h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm108-12v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 96v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm192 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96-260v352c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V112c0-26.5 21.5-48 48-48h48V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h128V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h48c26.5 0 48 21.5 48 48zm-48 346V160H48v298c0 3.3 2.7 6 6 6h340c3.3 0 6-2.7 6-6z" class=""></path></svg> </div>
                                                    </div>     
                                                </div> -->
                        </div>
                   </div>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                    <label class="text-primary" for="">Açıklama</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtaciklama" CssClass="form-control" TextMode ="MultiLine" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3 align-self-end">
                            <!--
                                        <label class="text-primary" for="usr">Durumu:</label>
                                        <label class="switch">
                                            <asp:CheckBox ID="chcaktif_pasif" runat="server"  Checked="true" AutoPostBack="True"       />
                                            <span class="slider round" style="scale: 0.7;"></span> 
                                        </label>       
                                        <asp:Label ID="lbl_ruhsat_aktif_pasif" style="font-size:14px"  runat="server" Visible="false"    ></asp:Label>
                                -->
                        </div>
                        <div class="col-md-3 align-self-end">
                                  <asp:Button ID="btnruhstkaydet" runat="server" ValidationGroup="kayit" CssClass="btn btn-success" Text="Ruhsat Kaydet" OnClick="btnruhstkaydet_Click"  />
                        </div>
                   </div>

                    <br />
                    <div class="row" style="margin-left:10px">


                             <!--   <label for="usr">Ruhsat Durumu :  </label>
                                  <asp:CheckBox ID="chcaktif_pasif1" runat="server" Style="margin-left:4px" Text="" Checked="true"   />      
                            <asp:Label ID="chckontrol" runat="server" Style="margin-left:4px" Visible="True" Text=""></asp:Label> -->
                    </div>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Metraj" ID="TabPanel2" CssClass="ajax__tab_panel">
                <ContentTemplate>
                    
           <!--  <div class="col-md-12  row align-middle text-left searchBg p-20 mb-3 pt-3 ml-0 pl-3 border" style="background-color:#6c757d !important"  >      -->
            <div class="row"style="margin-top:10px">
                  <div class="col-md-3 col-xl-3 ">                 
                        <div class="form-group">
                        <label for="usr">Aykome Kazı Türü:</label>                                            
                                <asp:DropDownList ID="comkazituru"  DataTextField="KAZI_TURU" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="comkazituru_SelectedIndexChanged"           > </asp:DropDownList> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="comkazituru"  ValidationGroup="metrajekle"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />

                        </div>
                  </div>
                  <div class="col-md-3 col-xl-3 ">
                        <div class="form-group">   
                            <label for="usr">Kazı Tipi:</label>
                                <asp:DropDownList ID="comkazitipi_sec"  DataTextField="KAZI_TIPI" DataValueField="ID" Enabled="false" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" Width="130px"  OnSelectedIndexChanged="comkazitipi_sec_SelectedIndexChanged"          > </asp:DropDownList>                    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="comkazitipi_sec"  ValidationGroup="metrajekle"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div> 
                  </div>                                                                    
                  <div class="col-md-1 col-xl-1 ">                 
                        <div class="form-group">
                        <label for="usr">Uzunluk:</label>                                            
                                    <asp:TextBox ID="txtkaziuzunluk" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event)" runat="server" Width="60px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtkaziuzunluk" ValidationGroup="metrajekle" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red"  Text="Boş geçilemez."  />
                        </div>
                  </div>
                  <div class="col-md-1 col-xl-1 ">                 
                        <div class="form-group">
                        <label for="usr">Genişlik:</label>                                            
                                    <asp:TextBox ID="txtkazigenislik" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event)" runat="server" Width="60px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtkazigenislik" ValidationGroup="metrajekle" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red" Text="Boş geçilemez."/>
                        </div>
                  </div>
                  <div class="col-md-2 col-xl-2 ">                 
                        <div class="form-group">
                        <label for="usr">Derinlik:</label>                                            
                                    <asp:TextBox ID="txtkaziderinlik" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event)" runat="server" Width="60px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtkaziderinlik" ValidationGroup="metrajekle" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red" Text="Boş geçilemez."/>
                        </div>
                  </div>

                  <div class="col-md-2 col-xl-2 ">
                            <label for="usr">&nbsp</label>
                      <br />
                        <div class="input-group">
                            <span class="input-group-btn mr-20">
                            <asp:Button ID="btnmetrajekle" runat="server"  ValidationGroup="metrajekle"  CssClass="btn  btn-primary  btn-sm" Text="Metraj Ekle"  OnClick="btnmetrajekle_Click"         />
                            </span>
                        </div>
                   </div> 
              </div>                    
                  <div class="table-responsive margin-body">
                                    <asp:GridView ID="grid_metraj" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="5" OnRowCommand="grid_metraj_RowCommand"    OnPageIndexChanging="grid_metraj_PageIndexChanging"     >
                                       <Columns>
                                                    <asp:BoundField DataField="ID"                    HeaderText="ID"                            InsertVisible="False" ReadOnly="True"  />
                                                    <asp:BoundField DataField="UZUNLUK"               HeaderText="Uzunluk"     />
                                                    <asp:BoundField DataField="GENISLIK"              HeaderText="Genişlik"     />
                                                    <asp:BoundField DataField="DERINLIK"              HeaderText="Derinlik"     />
                                                    <asp:BoundField DataField="KAZI_TURU"             HeaderText="Kazı Türü"            />
                                                    <asp:BoundField DataField="KAZI_TIPI"             HeaderText="Kazı Tipi"            />

                                                    <asp:TemplateField Visible="true" HeaderStyle-Width="80px"  HeaderText="İşlemler">
                                                        <ItemTemplate > 
                                                            <asp:Button ID="btnsil"               runat="server" Text="Sil"              CommandName="sil"             CssClass="btn btn-danger btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                                        </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                       </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                            <asp:Label ID="lblmetrajsayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
                    </div>
                  


                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Ruhsat Bedeli" ID="TabPanel3" CssClass="ajax__tab_panel">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>



                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Alan (Zemin) Tahrip Bedeli</label>
                                                    <asp:TextBox ID="txt_alan_tahrip_bedeli" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txt_alan_tahrip_bedeli_TextChanged"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txt_alan_tahrip_bedeli"  ValidationGroup="odeme_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Keşif ve Kontrol Bedeli</label>
                                                    <asp:TextBox ID="txt_kesif_kontrol_bedeli" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txt_kesif_kontrol_bedeli_TextChanged"  ></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txt_kesif_kontrol_bedeli"  ValidationGroup="odeme_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Altyapı Ruhsat Bedeli</label>
                                                    <asp:TextBox ID="txt_altyapi_ruhsat_bedeli" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txt_altyapi_ruhsat_bedeli_TextChanged"    ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txt_altyapi_ruhsat_bedeli"  ValidationGroup="odeme_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Altyapı Kazı İzin Harcı</label>
                                                    <asp:TextBox ID="txt_kazi_izin_harci" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txt_kazi_izin_harci_TextChanged"    ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txt_kazi_izin_harci"  ValidationGroup="odeme_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">KDV</label>
                                                    <asp:TextBox ID="txt_kdv" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txt_kdv_TextChanged"    ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txt_kdv"  ValidationGroup="odeme_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">TOPLAM RUHSAT BEDELİ</label>
                                                    <asp:TextBox ID="txt_toplam" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txt_toplam"  ValidationGroup="odeme_kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="switch">
                                                <asp:CheckBox ID="chc_nakit_teminat" runat="server"  Checked="false" AutoPostBack="True"  OnCheckedChanged="chc_nakit_teminat_CheckedChanged"                 />
                                                <span class="slider round" style="scale: 0.7;"></span> 
                                            </label>       
                                             <asp:Label ID="lbl_nakit_teminat" style="font-size:14px"  runat="server" Text="Nakit" ></asp:Label>
                                        </div>
                                        <div class="col-md-6">

                                        </div>
                                    </div>

                <asp:Panel ID="Panel_Teminat"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" ID="lbl_txt_teminata_konu_tutar" for="usr">Teminata Konu Tutar</label>
                                                    <asp:TextBox ID="txt_teminata_konu_tutar" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" ID="lbl_com_teminat_sec" for="usr">Teminat Seç</label>
                                                <asp:DropDownList ID="com_teminat_sec"  DataTextField="AA" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="com_teminat_sec_SelectedIndexChanged"            > </asp:DropDownList> 

                                            </div>
                                        </div>
                                    </div>
                </asp:Panel>




                                    <div class="row"style="margin-top:20px">
                                        <div class="col-md-6">
                                            </div>
                                        <div class="col-md-2">
                                              <asp:Button ID="btnhesapla"     runat="server"  CssClass="btn btn-primary" Text="Otomatik Hesap Yap"  OnClick="btnhesapla_Click"           />
                                            </div>
                                        <div class="col-md-1">

                                            </div>
                                        <div class="col-md-2">
                                              <asp:Button ID="btnodemekaydet" runat="server" ValidationGroup="kayit" CssClass="btn btn-success" Text="Ödeme Kaydet" OnClick="btnodemekaydet_Click"    />
                                            </div>
                                        <div class="col-md-1">

                                            </div>
                                    </div>
                                    <br />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Harita" ID="TabPanel4"  CssClass="ajax__tab_panel">
                <ContentTemplate>

                    <div class="row" style="margin-left:10px">

                         <div class="col-md-2">
                                    <label class="text-primary" for="">Dosya Yükle</label>
                        </div>
                        <div class="col-md-10">
                                    <div id="kmzyukle" class="row" runat="server" >
                                        <div class="form-group">
                                            <label for="usr"></label>
                                                <asp:FileUpload ID="FileUpload1" CssClass="btn btn-outline-success text- " Width="260px"  Font-Size="12px"  runat="server" />
                                                <asp:Button ID="btnkmzyukle" runat="server"  CssClass="btn btn-success" Text="kmz Yükle"  OnClick="btnkmzyukle_Click"     />

                                            <label for="usr"> 
                                            </label>
                                                <label for="usr">
                                                    <asp:Label ID="lbldurumdosya" runat="server" CssClass="text-warning" Text=""></asp:Label></label>
                                            </div>
                                    </div>
                        </div>
                    </div>
                <div class="row" style="margin-left:10px">
                 <div class="col-md-12">

                  <div class="table-responsive margin-body" style="width: 600px;">
                                    <asp:GridView ID="grid_kmz" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="5" Width="450px"  OnRowCommand="grid_kmz_RowCommand" OnPageIndexChanging="grid_kmz_PageIndexChanging"       >
                                       <Columns>
                                                    <asp:BoundField DataField="ID"                    HeaderText="ID"                            InsertVisible="False" ReadOnly="True"  />
                                                    <asp:BoundField DataField="DOSYA_ADI"               HeaderText="Uzunluk"     />

                                                    <asp:TemplateField Visible="true" HeaderStyle-Width="500px"  HeaderText="İşlemler">
                                                        <ItemTemplate > 
                                                            <asp:Button ID="btnharita"            runat="server" Text="Haritada Göster"  CommandName="harita"             CssClass="btn btn-primary btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                            <asp:Button ID="btnindir"             runat="server" Text="İndir"            CommandName="indir"             CssClass="btn btn-primary btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                            <asp:Button ID="btnsil"               runat="server" Text="Sil"              CommandName="sil"             CssClass="btn btn-danger btn-sm"                   CommandArgument='<%# Container.DataItemIndex %>' />
                                                
                                                        </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                       </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                            <asp:Label ID="lblkmzsayisi" class="text-info" runat="server" Visible="true" Text=""></asp:Label>
                    </div>
  
                                                <asp:Label ID="lblkmz" CssClass="text-secondary"  runat="server" Text=""></asp:Label>




                        </div>
                    </div> 
                    
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Resimler" ID="TabPanel5"  CssClass="ajax__tab_panel">
                <ContentTemplate>


                    
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Otomatik Hesap" ID="TabPanel6" Visible="false" CssClass="ajax__tab_panel">
                <ContentTemplate>
                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>



                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Alan (Zemin) Tahrip Bedeli</label>
                                                    <asp:Label ID="lbl_alan_tahrip_bedeli" runat="server" Visible="true" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Keşif ve Kontrol Bedeli</label>
                                                    <asp:Label ID="lbl_kesif_kontrol_bedeli" runat="server" Visible="true" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Altyapı Ruhsat Bedeli</label>
                                                    <asp:Label ID="lbl_altyapi_ruhsat_bedeli" runat="server" Visible="true" Text=""></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Altyapı Kazı İzin Harcı</label>
                                                    <asp:Label ID="lbl_kazi_izin_harci" runat="server" Visible="true" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">KDV</label>
                                                    <asp:Label ID="lbl_kdv" runat="server" Visible="true" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">TOPLAM RUHSAT BEDELİ</label>
                                                    <asp:Label ID="lbl_toplam" runat="server" Visible="true" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary"  for="usr">Teminata Konu Tutar</label>
                                                    <asp:Label ID="lbl_teminata_konu_tutar" runat="server" Visible="true" Text=""></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="text-primary" for="usr">Teminat Seç</label>
                                                    <asp:Label ID="lbl_teminat_adi" runat="server" Visible="true" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row"style="margin-top:20px">
                                        <div class="col-md-9">
                                            </div>
                                        <div class="col-md-2">

                                              <asp:Button ID="btn_otomatik_hesap" runat="server"  CssClass="btn btn-success" Text="Otomatik Hesap Yap"     />

                                            </div>
                                        <div class="col-md-1">

                                            </div>
                                    </div>
                                    <br />

                        </ContentTemplate>
                    </asp:UpdatePanel>
 
 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
                       
        </ajaxToolkit:TabContainer>       

   </asp:Panel>

    <asp:Panel ID="Panel_Ruhsat_Bilgi"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">

        <ajaxToolkit:TabContainer ID="TabContainer2"  CssClass="Tab6" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%" >

		    <ajaxToolkit:TabPanel runat="server" HeaderText="Ruhsat Keşif" ID="TabPanel7" CssClass="ajax__tab_panel">
                <ContentTemplate>

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
                            <asp:GridView ID="grid_metraj_kesif" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="50"          >
                               <Columns>

                                                    <asp:BoundField DataField="ID"                    HeaderText="ID"                            InsertVisible="False" ReadOnly="True"  />
                                                    <asp:BoundField DataField="UZUNLUK"               HeaderText="Uzunluk"     />
                                                    <asp:BoundField DataField="GENISLIK"              HeaderText="Genişlik"     />
                                                    <asp:BoundField DataField="DERINLIK"              HeaderText="Derinlik"     />
                                                    <asp:BoundField DataField="KAZI_TURU"             HeaderText="Kazı Türü"            />
                                                    <asp:BoundField DataField="KAZI_TIPI"             HeaderText="Kazı Tipi"            />

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
                                    <asp:Label ID="lblgrid_say" class="text-info" runat="server" Visible="false" Text=""></asp:Label>
            </div>
        
        </div>

	            </ContentTemplate>
            </ajaxToolkit:TabPanel>

		    <ajaxToolkit:TabPanel runat="server" HeaderText="Ruhsat Kabul" ID="TabPanel8" CssClass="ajax__tab_panel">
                <ContentTemplate>



	            </ContentTemplate>
            </ajaxToolkit:TabPanel>

	    </ajaxToolkit:TabContainer>   

   </asp:Panel>

                                  <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lblidmetraj" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lblidodeme" runat="server" Visible="false" Text=""></asp:Label>

                                     <!-- Metraj tabı için -->
                                  <asp:Label ID="lbl_ruhsat_yetkilisi" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lbl_donem" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lbl_aykome_kazi_turu" runat="server" Visible="false" Text=""></asp:Label>

                                  <asp:Label ID="lbl_il" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lbl_kazi_turu" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lbl_kazi_tipi" runat="server" Visible="false" Text=""></asp:Label>

            
      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>










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
