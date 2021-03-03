<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RuhsatKabul.aspx.cs" Inherits="EnvanterTveY.RuhsatKabul" %>
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


     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css"/>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="container-fluid"  style="margin-top: 20px">

        <div class="row">
            <span class="baslik">Ruhsat Kabul</span>

        </div>

        <div class="row">
                <div class="col-md-2 col-xs-12">
                    <label for="">Bölge</label>
                    <asp:DropDownList ID="combolgeara"  AutoPostBack="true" DataTextField="BOLGE_ADI" DataValueField="ID" CssClass="form-control form-control-sm" runat="server" >
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                    <label for="">Tipi</label>
                    <asp:DropDownList ID="comtipara" DataTextField="TIP" DataValueField="ID" CssClass="form-control form-control-sm " runat="server" AutoPostBack="True"  >
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                    <label for="">Durum</label>
                    <asp:DropDownList ID="comdurumara" DataTextField="SECENEK" DataValueField="ID" CssClass="form-control form-control-sm" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-12">
                </div>
                        <div class="col-md-4 col-xs-12 text-right">
                            <label for=""></label>
                              <div class="">
                                  <span class="">
                                      <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary" Width="130px"  Text="Ara"  OnClick="btnara_Click"   />
                                      <asp:Button ID="btnruhsatekle"      runat="server" Text="Ruhsat Ekle"        CssClass="btn btn-success " Width="130px"  OnClick="btnruhsatekle_Click"  />
                                    </span>
                              </div>
                        </div>
         </div>

    <div class="row" style="margin-top:20px">
        <div class="table-responsive margin-body">
                        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="10"  OnRowCommand="grid_RowCommand"  OnPageIndexChanging="grid_PageIndexChanging"    >
                           <Columns>

                                        <asp:BoundField DataField="ID"                HeaderText="ID"          InsertVisible="False" ReadOnly="True"  />
                                        <asp:BoundField DataField="RUHSATNO"          HeaderText="Ruhsat No"        />
                                        <asp:BoundField DataField="PROJENO"           HeaderText="Proje No"         />
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
                                        <asp:BoundField DataField="DURUM"             HeaderText="Durum"          HtmlEncode="false"  />
                                        <asp:BoundField DataField="GUNCELLEME_DURUMU" HeaderText="GD"             Visible="false" />

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="280px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="btnguncelle"          runat="server" Text="Güncelle"         CommandName="guncelle"        CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
                                                <asp:Button ID="islemler"             runat="server" Text="Kabul"            CommandName="kabul"           CssClass="btn btn-success btn-sm"                  CommandArgument='<%# Container.DataItemIndex %>' />
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

         <!-- Modal Ruhsat Kabul Aktar-->
<div class="modal fade" id="ModalKabul">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Ruhsat Kabul Aktar</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">

        Kabul kaydı oluşturulsun mu??
          <asp:Label ID="lblislem_ruhsatkabul" runat="server" Visible="false" Text=""></asp:Label>

          <div class="messagealert mt-3 mb-3" id="alert_container_ruhsatkabul">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnkabulolustur" CssClass="btn btn-success" ValidationGroup="sil"   runat="server" Text="Evet"  OnClick="btnkabulolustur_Click"      />
                             <asp:Label ID="lblruhsatidkabul" Visible="false"  runat="server" Text="Label"></asp:Label>

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
                            <!--  <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="commahalle_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" /> -->
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
                        <!--     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="comcdsk_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />  -->
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
                            <asp:DropDownList ID="comaprojetipi_kayit"  DataTextField="A_PROJE_TIPI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"        > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="comaprojetipi_kayit"  ValidationGroup="kayit"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
                        </div>
                   </div>

                    <div class="row"style="margin-top:10px">
                        <div class="col-md-2">
                                <label class="text-primary" for="">Başlangıç Tarihi</label>
                        </div>
                        <div class="col-md-4">
                                <asp:TextBox ID="txtDate_baslangic" CssClass="form-control"  runat="server"></asp:TextBox>

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
                                <asp:TextBox ID="txtDate_bitis" CssClass="form-control" runat="server"></asp:TextBox>

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

                   </div>
<hr />
         <div class="text-right ">
              <asp:Button ID="btnruhstkaydet" runat="server" ValidationGroup="kayit" CssClass="btn btn-success" Text="Ruhsat Kaydet" OnClick="btnruhstkaydet_Click"  />
        </div>
                    <div class="row" style="margin-left:10px">
                                <label for="usr">Ruhsat Durumu :  </label>
                                  <asp:CheckBox ID="chcaktif_pasif" runat="server" Style="margin-left:4px" Text="" Checked="true"   />      
                            <asp:Label ID="chckontrol" runat="server" Style="margin-left:4px" Visible="True" Text=""></asp:Label>
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
                                <asp:DropDownList ID="comkazituru"  DataTextField="KAZI_TURU" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"          > </asp:DropDownList> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="comkazituru"  ValidationGroup="metrajekle"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />

                        </div>
                  </div>
                                                                    
                  <div class="col-md-1 col-xl-1 ">                 
                        <div class="form-group">
                        <label for="usr">Metraj:</label>                                            
                                    <asp:TextBox ID="txtkaziuzunluk" CssClass="form-control" onkeypress="return sadecerakam(event)" runat="server" Width="60px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtkaziuzunluk" ValidationGroup="metrajekle" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red"  Text="Boş geçilemez."  />

                        </div>
                  </div>
                  <div class="col-md-2 col-xl-2 ">                 
                        <div class="form-group">
                        <label for="usr">Genişlik:</label>                                            
                                    <asp:TextBox ID="txtkazigenislik" CssClass="form-control" onkeypress="return sadecerakam(event)" runat="server" Width="60px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtkazigenislik" ValidationGroup="metrajekle" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" ForeColor="red" Text="Boş geçilemez."/>
                        </div>
                  </div>
                  <div class="col-md-3 col-xl-3 ">
                        <div class="form-group">   
                            <label for="usr">Kazı Tipi:</label>
                                <asp:DropDownList ID="comkazitipi_sec"  DataTextField="KAZI_TIPI" DataValueField="ID" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" Width="130px"         > </asp:DropDownList>                    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="comkazitipi_sec"  ValidationGroup="metrajekle"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." ForeColor="red" InitialValue="0" />
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
                                    <asp:GridView ID="grid_metraj" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="5" OnPageIndexChanging="grid_metraj_PageIndexChanging"     >
                                       <Columns>
                                                    <asp:BoundField DataField="ID"                    HeaderText="ID"                            InsertVisible="False" ReadOnly="True"  />
                                                    <asp:BoundField DataField="UZUNLUK"               HeaderText="Uzunluk"     />
                                                    <asp:BoundField DataField="GENISLIK"              HeaderText="Genişlik"     />
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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Harita" ID="TabPanel3" CssClass="ajax__tab_panel">
                <ContentTemplate>

                    <div class="row" style="margin-left:10px">

                         <div class="col-md-2">
                                    <label class="text-primary" for="">Dosya Yükle</label>
                        </div>
                        <div class="col-md-6">
                                    <div id="kmzyukle" class="row" runat="server" >
                                        <div class="form-group">
                                            <label for="usr"></label>
                                                <asp:FileUpload ID="FileUpload1" CssClass="btn btn-outline-success text- " Width="260px"  Font-Size="12px"  runat="server" />
                                            <label for="usr"> 
                                            </label>
                                                <label for="usr">
                                                    <asp:Label ID="lbldurumdosya" runat="server" CssClass="text-warning" Text=""></asp:Label></label>
                                            </div>
                                    </div>
                                    <div id="kmzindir" class="row" runat="server">
                                        <div class="form-group">
                                            <label for="usr">
                                                <asp:Label ID="lblkmz" CssClass="text-secondary"  runat="server" Text=""></asp:Label>
                                            </label>
                                            <label for="usr"> 
                                                <asp:Button ID="btnharita" runat="server"  CssClass="btn btn-primary" Text="Haritada Göster"  OnClick="btnharita_Click"   />
                                                <asp:Button ID="btnindir" runat="server"   CssClass="btn btn-secondary" Text="İndir"  OnClick="btnindir_Click"     />
                                                <asp:Button ID="btnkmzsil" runat="server"  CssClass="btn btn-danger" Text="Sil"  OnClick="btnkmzsil_Click"    />
                                            </label>
                                            </div>

                                    </div>
                        </div>
                    </div> 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Fotoğraf" ID="TabPanel4" CssClass="ajax__tab_panel">
                <ContentTemplate>
 
 
 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" HeaderText="Diğer" ID="TabPanel5" CssClass="ajax__tab_panel">
                <ContentTemplate>
  
  
  
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

        </ajaxToolkit:TabContainer>       




         
                            <asp:Label ID="lblmahalle"    runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="lblcaddesokak" runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="lblbinano"     runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="lblilce"       runat="server" Visible="true" Text=""></asp:Label>

                                  <asp:Label ID="lblidadres" runat="server" Visible="false" Text=""></asp:Label>


                                  <asp:Label ID="lblidcihaz" runat="server" Visible="false" Text=""></asp:Label>
                                  <asp:Label ID="lblidmetraj" runat="server" Visible="false" Text=""></asp:Label>


            
      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
      </div>
        
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>




         <!-- Modal Ruhsat Sil-->
<div class="modal fade" id="ModalSil">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Ruhsat Talep Sil</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>

      <!-- Modal body -->
      <div class="modal-body">

        Kayıt silinecek. Emin misiniz?
          <asp:Label ID="lblislem_ruhsatsil" runat="server" Visible="false" Text=""></asp:Label>

          <div class="messagealert mt-3 mb-3" id="alert_container_ruhsatsil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnsil" CssClass="btn btn-danger" ValidationGroup="sil"   runat="server" Text="Sil"  OnClick="btnsil_Click"  />
                             <asp:Label ID="lblruhsatidsil" Visible="false"  runat="server" Text="Label"></asp:Label>

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
