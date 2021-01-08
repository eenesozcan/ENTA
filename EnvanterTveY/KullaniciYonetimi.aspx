<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KullaniciYonetimi.aspx.cs" Inherits="EnvanterTveY.KullaniciYonetimi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
        <script src="Scripts/loading.js"></script>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script src="Scripts/KabloHE-js.js"></script>

    <div class="container-fluid" style="margin-top: 20px">

    <div class="row">
        <span class="baslik">Kullanıcı Yönetimi</span>
    </div>
    <div class="row">

        <div class="col-md-3 col-xs-12 ">
             <div class="form-group">
              <label for="">Rol  </label>
              <asp:DropDownList ID="comrol" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" DataSourceID="sqlRol" DataTextField="ROL" DataValueField="ID">
              </asp:DropDownList>

             </div>
        </div>
        <div class="col-md-3 col-xs-12">
            <div class="form-group">
                <label for="">Kullanıcı Adı</label>
                    <asp:TextBox ID="txtkullaniciadiarama" CssClass="form-control" runat="server"></asp:TextBox>
               </div>
        </div>
        <div class="col-md-3 col-xs-12">
             <label for="">İl  </label>
              <asp:DropDownList ID="comilara" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" DataSourceID="sqlil" DataTextField="IL" DataValueField="ID">
              </asp:DropDownList>

        </div>
        <div class="col-md-3 col-xs-12 text-right">
             <label for=""></label>
              <div class="">
                  
                <asp:Button ID="btnkullanicilistele" runat="server"  CssClass="btn  btn-primary " Text="Listele" OnClick="btnkullanicilistele_Click"   />
                <asp:Button ID="btnyenikullaniciekle" runat="server" Text="Kullanici Ekle"  CssClass="btn btn-success" OnClick="btnyenikullaniciekle_Click"  />
                <asp:Button ID="btnrolekle" runat="server" Text="Rol İşlemleri"  CssClass="btn btn-success" OnClick="btnrolekle_Click"   />
        </div>


    </div>
    </div>
    <div class="row">

    <div class="table-responsive">

        <asp:GridView ID="gridkullanici" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" OnRowCommand="gridkullanici_RowCommand"  >
            <Columns>

                <asp:BoundField DataField="ID"                  HeaderText="ID"             SortExpression="ID" />
                    <asp:BoundField DataField="TC"              HeaderText="T.C."           SortExpression="KULLANICI_ADI" />
                    <asp:BoundField DataField="ISIM"            HeaderText="İsim Soyisim"   SortExpression="KULLANICI_ADI" />
                    <asp:BoundField DataField="KULLANICI_ADI"   HeaderText="Kullanıcı Adı"  SortExpression="KULLANICI_ADI" />
                    <asp:BoundField DataField="EMAIL"           HeaderText="E-Posta"        SortExpression="KULLANICI_ADI" />
                    <asp:BoundField DataField="TELEFON"         HeaderText="Telefon"        SortExpression="KULLANICI_ADI" />
                    <asp:BoundField DataField="DURUM"           HeaderText="Durum"          SortExpression="DURUM" />
                    <asp:BoundField DataField="ROL"             HeaderText="Rol"            SortExpression="ROL" />
                    <asp:BoundField DataField="IL"              HeaderText="İl"             SortExpression="FIRMA_ADI" />
                    <asp:BoundField DataField="BOLGE_ADI"       HeaderText="Bölge"          SortExpression="FIRMA_ADI" />
                    <asp:BoundField DataField="FIRMA_ADI"       HeaderText="Firma"          SortExpression="FIRMA_ADI" />
      

                 <asp:TemplateField Visible="true" HeaderStyle-Width="230px"  HeaderText="İşlemler">
                                            <ItemTemplate > 
                                                <asp:Button ID="guncelle"    runat="server" Text="Güncelle"      CommandName="guncelle"    CssClass="btn btn-success btn-sm"  CommandArgument='<%# Container.DataItemIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
            </Columns>
        </asp:GridView>
  
        <asp:Label ID="lblsayi" CssClass="text-primary" runat="server" Text=""></asp:Label>        
        </div>
    </div>
    </div>

              <div id="YeniMesajDiv">Some text some message..</div>


         </ContentTemplate>
        </asp:UpdatePanel>
    
       <!-- ===================================================================================================================== -->

     <!-- The Modal-Kullanıcı Ekle -->
<div class="modal" id="ModalKullaniciEkle">
  <div class="modal-dialog modal-lg">
    <div class="modal-content" style="margin-top:50px">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Yeni Kullanıcı Ekle</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

      <!-- Modal body -->
      <div class="modal-body">
        
           <div class="messagealert mt-3 mb-5" id="alert_container_kul_ekle">
              </div>

           <div class="row">
            <div class="col-md-3">
               <label for="usr">TC Kimlik No</label>
            </div>
            <div class="col-md-3">
                  <asp:TextBox ID="txtTC" MaxLength="11" CssClass="form-control" runat="server"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"   ControlToValidate="txtTC" ErrorMessage="*" ValidationExpression="^(?=.*\d)[\d]{11}" ValidationGroup="kullaniciekle"></asp:RegularExpressionValidator>
            </div>
               <div class="col-md-3">
               <label for="usr">Eposta</label>

            </div>
               <div class="col-md-3">
                  <asp:TextBox ID="txteposta" CssClass="form-control" runat="server"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator155"  runat="server" ControlToValidate="txteposta" 
                                ErrorMessage="Geçersiz adres!" SetFocusOnError="True" ForeColor="red"   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ValidationGroup="kullaniciekle"></asp:RegularExpressionValidator>
            </div>
           </div>

          <div class="row">
            <div class="col-md-3">
               <label for="usr">Kullanıcı Adı</label>
            </div>
            <div class="col-md-3">
                  <asp:TextBox ID="txtkullaniciadi" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtkullaniciadi" ErrorMessage="*" ValidationGroup="kullaniciekle"></asp:RequiredFieldValidator>
                </div>
               <div class="col-md-3">
               <label for="usr">Telefon</label>

            </div>
               <div class="col-md-3">
                  <asp:TextBox ID="txttelefon" CssClass="form-control" runat="server"></asp:TextBox>
                  
            </div>
           </div>

           <div class="row">
            <div class="col-md-3">
               <label for="usr">İsim Soyisim</label>
            </div>
            <div class="col-md-3">
                  <asp:TextBox ID="txtisim" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtisim" ErrorMessage="*" ValidationGroup="kullaniciekle"></asp:RequiredFieldValidator>
                </div>
            <div class="col-md-3">
               <label for="usr">Rol</label>
            </div>
            <div class="col-md-3">
                   <asp:DropDownList ID="comrolekle" runat="server" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" 
                                 DataSourceID="sqlrol" DataTextField="ROL" DataValueField="ID" 
                                 Width="150px">
                             </asp:DropDownList>
                 <label for="usr"></label>
            </div>
           </div>

          <div class="row align-middle text-left ml-20 p-20    pt-3   " >
                        <label for="" style="font-size:14px;  color:red; ">Aşağıda istenen bilgiler kullanıcı verileri içindir. Yetkilendirme için kayıt işleminden sonra Güncelle butonuna basarak YETKİ işlemlerinin tamamlanması gerekmektedir.</label>
            </div>

          <div class="row">
                <div class="col-md-3">
               <label for="usr">İl</label>
            </div>
               <div class="col-md-3">
                            <asp:DropDownList ID="comilekle" runat="server" AppendDataBoundItems="True" 
                                 AutoPostBack="True" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" DataSourceID="sqlil" 
                                 DataTextField="IL" DataValueField="ID"  
                                  Width="150px" OnDataBound="comilekle_DataBound" OnSelectedIndexChanged="comilekle_SelectedIndexChanged">
                                 <asp:ListItem Value="0">-</asp:ListItem>
                             </asp:DropDownList>  
                   <label for="usr"></label>
            </div>
              
           </div>
           
                 <div class="row">
                <div class="col-md-3">
               <label for="usr">Bölge</label>
            </div>
               <div class="col-md-3">
                            <asp:DropDownList ID="combolgeekle" runat="server" AppendDataBoundItems="True" 
                                 CssClass="form-control mb-2 mr-sm-2 mb-sm-0" DataSourceID="sqlbolge" DataTextField="BOLGE_ADI" 
                                 DataValueField="ID" Width="150px">
                                 <asp:ListItem Value="0">-</asp:ListItem>
                             </asp:DropDownList>
                    <label for="usr"></label>
            </div>
            <div class="col-md-3">
               <label for="usr">Firma</label>
            </div>
            <div class="col-md-3">
                  <asp:DropDownList ID="comfirmaekle" runat="server" AppendDataBoundItems="True" 
                                 CssClass="form-control mb-2 mr-sm-2 mb-sm-0" DataSourceID="sqlfirma" DataTextField="FIRMA_ADI" 
                                 DataValueField="ID" Width="150px">
                                 <asp:ListItem Value="0">-</asp:ListItem>
                             </asp:DropDownList>
                 <label for="usr"></label>
            </div>
              
           </div>
           
           </div>
    <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnyetkiver" runat="server" ValidationGroup="" CssClass="btn btn-success disabled"  Visible="true" OnClick="btnyetkiver_Click" Text="Yetkiler"  />
          <asp:Button ID="btnkaydet" runat="server" ValidationGroup="kullaniciekle" CssClass="btn btn-success" Text="Kaydet" OnClick="btnkaydet_Click" />
      </div>
         <asp:Label ID="lblid1" Visible="false" runat="server" Text="Label"></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

     <!-- ===================================================================================================================== -->
    
     <!-- The Modal-Kullanıcı Güncelle -->
<div class="modal fade" id="ModalKullaniciGuncelle" tabindex="-1" role="dialog" aria-labelledby="ModalKullaniciGuncelleLabel" aria-hidden="true">>
  <div class="modal-dialog modal-lg" role="document"">
    <div class="modal-content" style="margin-top:50px">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">
            <asp:Label ID="lblmodalkullaniciadi" runat="server" Text=""></asp:Label>
             </h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

      <!-- Modal body -->
      <div class="modal-body">
          <asp:UpdatePanel ID="UpdatePanel4" runat="server">
              <ContentTemplate>
                    <div class="messagealert mt-3 mb-5" id="alert_container_guncelle">
                    </div>

                  <asp:TabContainer ID="TabContainer1" CssClass="Tab6" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%"  >
                      <asp:TabPanel runat="server" Height="100%" HeaderText="Bilgi Güncelleme" ID="TabPanel1" CssClass="ajax__tab_panel">
                          <ContentTemplate>
                              <asp:Panel runat="server">
      <div class="row">
            <div class="col-md-2">
               <label for="usr">TC Kimlik No</label>
            </div>
            <div class="col-md-4">
                  <asp:TextBox ID="txttc2" MaxLength="11" CssClass="form-control" runat="server"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"   ControlToValidate="txttc2" ErrorMessage="*" ValidationExpression="^(?=.*\d)[\d]{11}" ValidationGroup="guncelle"></asp:RegularExpressionValidator>
            </div>
               <div class="col-md-2">
               <label for="usr">Eposta</label>

            </div>
               <div class="col-md-4">
                  <asp:TextBox ID="txteposta2" CssClass="form-control" runat="server"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator3"  runat="server" ControlToValidate="txteposta2" 
                                ErrorMessage="Geçersiz adres!" SetFocusOnError="True" ForeColor="red"   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ValidationGroup="guncelle"></asp:RegularExpressionValidator>
            </div>
           </div>

          <div class="row">
            <div class="col-md-2">
               <label for="usr">Kullanıcı Adı</label>
            </div>
            <div class="col-md-4">
                  <asp:TextBox ID="txtkullaniciadi2" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtkullaniciadi2" ErrorMessage="*" ValidationGroup="guncelle"></asp:RequiredFieldValidator>
                </div>
               <div class="col-md-2">
               <label for="usr">Telefon</label>

            </div>
               <div class="col-md-4">
                  <asp:TextBox ID="txttelefon2" CssClass="form-control" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttelefon2" ErrorMessage="*" ValidationGroup="guncelle"></asp:RequiredFieldValidator>
                
            </div>
           </div>

           <div class="row">
            <div class="col-md-2">
               <label for="usr">İsim</label>
            </div>
            <div class="col-md-4">
                  <asp:TextBox ID="txtisim2" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtisim2" ErrorMessage="*" ValidationGroup="guncelle"></asp:RequiredFieldValidator>
                </div>
            <div class="col-md-2">
               <label for="usr">Rol</label>
            </div>
            <div class="col-md-4">
                   <asp:DropDownList ID="comrol2" runat="server" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" 
                                 DataSourceID="sqlrol" DataTextField="ROL" DataValueField="ID" 
                                 Width="100%">
                             </asp:DropDownList>
                 <label for="usr"></label>
            </div>
           </div>

          <div class="row align-middle text-left ml-20 p-20  pt-3  " style="margin-left:20px">
                        <label for="" style="font-size:14px; color:red; ">Aşağıda istenen bilgiler kullanıcı verileri içindir. Yetkilendirme için yukarıda bulunan tab'lardan Bölge ve Depo yetkileri verilmelidir.</label>
            </div>

          <div class="row">
                <div class="col-md-2">
               <label for="usr">İl</label>
            </div>
               <div class="col-md-4">
                            <asp:DropDownList ID="comil2" runat="server" AppendDataBoundItems="True" 
                                 AutoPostBack="True" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" DataSourceID="sqlil" 
                                 DataTextField="IL" DataValueField="ID"  
                                  Width="100%" OnDataBound="comil2_DataBound" OnSelectedIndexChanged="comil2_SelectedIndexChanged"   >
                                 <asp:ListItem Value="0">-</asp:ListItem>
                             </asp:DropDownList>  
                   <label for="usr"></label>
            </div>
              
           </div>
           
                 <div class="row">
                <div class="col-md-2">
               <label for="usr">Bölge</label>
            </div>
               <div class="col-md-4">
                            <asp:DropDownList ID="combolge2" runat="server" AppendDataBoundItems="True" 
                                 CssClass="form-control mb-2 mr-sm-2 mb-sm-0" DataSourceID="sqlbolge" DataTextField="BOLGE_ADI" 
                                 DataValueField="ID" Width="100%">
                                 <asp:ListItem Value="0">-</asp:ListItem>
                             </asp:DropDownList>
                    <label for="usr"></label>
            </div>
            <div class="col-md-2">
               <label for="usr">Firma</label>
            </div>
            <div class="col-md-4">
                  <asp:DropDownList ID="comfirma2" runat="server" AppendDataBoundItems="True" 
                                 CssClass="form-control mb-2 mr-sm-2 mb-sm-0" DataSourceID="sqlfirma" DataTextField="FIRMA_ADI" 
                                 DataValueField="ID" Width="100%">
                                 <asp:ListItem Value="0">-</asp:ListItem>
                             </asp:DropDownList>
                 <label for="usr"></label>
            </div>
              
           </div>

            <div class="row">
                <div class="col-md-3">
               <label for="usr"></label>
            </div>
               <div class="col-md-3">
                        
                    <label for="usr"></label>
            </div>
            <div class="col-md-3">
               <label for="usr">Durumu</label>
            </div>
            <div class="col-md-3">
                <asp:CheckBox ID="chcdurum" CssClass="text" runat="server" />
                 <label for="usr"></label>
            </div>
              
           </div>

                                   <div class="row">
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2 text-right">
                                            <asp:Button ID="btnkullaniciguncelle" runat="server" ValidationGroup="guncelle" CssClass="btn btn-success" Text="Kaydet" OnClick="btnkullaniciguncelle_Click" />
                                      </div>
                                   </div>
</asp:Panel>
                          </ContentTemplate>
                      </asp:TabPanel>       
                       <asp:TabPanel runat="server" Height="300px" HeaderText="Bölge Yetkileri" ID="TabPanel7" CssClass="ajax__tab_panel" >
                          <ContentTemplate>
                               <asp:Panel runat="server">
                              <div class="row">
                                  <div class="col-md-5">
                                      <asp:ListBox ID="lstbolge1" runat="server"  Width="400px"  CssClass="form-control wd-max-100" datasourceid="SqlDataSource3" DataTextField="BOLGE_ADI" DataValueField="ID" OnDataBound="lstbolge1_DataBound" SelectionMode="Multiple" Height="200px" ></asp:ListBox>
                                  </div>
                                  <div class="col-md-2" style="margin-top:40px">
                                            <asp:Button ID="btnbolgeekle" runat="server" CssClass="btn btn-primary mt-10"  Text="Ekle" OnClick="btnbolgeekle_Click" />
                                            <br />
                                            <asp:Button ID="btnbolgeekle0" runat="server" CssClass="btn btn-primary mt-10" Text="Çıkar" Style="margin-top:5px"  OnClick="btnbolgeekle0_Click" />
                                            
                                  </div>
                                  <div class="col-md-5">
                                      <asp:ListBox ID="lstbolge2" SelectionMode="Multiple" Width="400px" Height="200px"  CssClass="form-control wd-max-100 " runat="server"></asp:ListBox>
                                  </div>

                              </div>
                                    <div class="row ml-20">
                                        <asp:CheckBox ID="chctumbolge2" runat="server" style="margin-left:20px" CssClass="text-primary" Text="Tüm Bölgeleri Görsün" />

                                  </div>
                                 <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:KabloHE %>" SelectCommand="SELECT [ID], [BOLGE_ADI] FROM [BOLGE] ORDER BY [BOLGE_ADI]"></asp:SqlDataSource>
                                   <div class="row mt-10">
                                      
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-6 text-right">
                                        <asp:Button ID="btnilekle" runat="server" ValidationGroup="rolyetkibölgekaydet" CssClass="btn btn-success" Text="Bölge Yetkilerini Kaydet" OnClick="btnilekle_Click" />
                                      </div>
                                     </div>
                                   </asp:Panel>
                          </ContentTemplate>
                      </asp:TabPanel>

                       <asp:TabPanel runat="server" Height="300px" HeaderText="Depo Yetkileri" ID="TabPanel8" CssClass="ajax__tab_panel" >
                          <ContentTemplate>
                               <asp:Panel runat="server">
                              <div class="row">
                                  <div class="col-md-5">
                                      <asp:ListBox ID="lstdepo1" runat="server"  Width="400px"  CssClass="form-control wd-max-100"  DataTextField="DEPO" DataValueField="ID" OnDataBound="lstdepo1_DataBound" SelectionMode="Multiple" Height="200px" ></asp:ListBox>
                                  </div>
                                  <div class="col-md-2" style="margin-top:40px">
                                            <asp:Button ID="btndepoekle" runat="server" CssClass="btn btn-primary mt-10"  Text="Ekle" OnClick="btndepoekle_Click" />
                                            <br />
                                            <asp:Button ID="btndepoekle0" runat="server" CssClass="btn btn-primary mt-10" Text="Çıkar" Style="margin-top:5px"  OnClick="btndepoekle0_Click" />
                                            
                                  </div>
                                  <div class="col-md-5">
                                      <asp:ListBox ID="lstdepo2" SelectionMode="Multiple" Width="400px" Height="200px"  CssClass="form-control wd-max-100 " runat="server"></asp:ListBox>
                                  </div>

                              </div>
                                    <div class="row ml-20">

                                  </div>
                                 <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:KabloHE %>" SelectCommand="SELECT [ID], [DEPO] FROM [DEPO] ORDER BY [DEPO]"></asp:SqlDataSource>
                                   <div class="row mt-10">
                                      
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-2"></div>
                                      <div class="col-md-6 text-right">
                                        <asp:Button ID="btndepokaydet" runat="server" ValidationGroup="rolyetkidepokaydet" CssClass="btn btn-success" Text="Depo Yetkilerini Kaydet" OnClick="btndepokaydet_Click" />
                                      </div>
                                     </div>
                                   </asp:Panel>
                          </ContentTemplate>
                      </asp:TabPanel>

                        <asp:TabPanel runat="server" Height="300px" HeaderText="Şifre Değiştir" ID="TabPanel2" CssClass="ajax__tab_panel" >
                          <ContentTemplate>
                               <asp:Panel runat="server">
                              <div class="row">
                                  <div class="col-md-3">
                                        <label for="usr">Yeni Şifreyi Giriniz</label>
                                  </div>
                                  <div class="col-md-3">
                                            <asp:TextBox ID="txtyenisifre" CssClass="form-control" runat="server"></asp:TextBox>
                                  </div>
                                  <div class="col-md-3">
                                             <asp:Button ID="btnsifreguncelle" runat="server" ValidationGroup="sifreguncelle" CssClass="btn btn-success" Text="Şifre Güncelle" OnClick="btnsifreguncelle_Click" />
                                  </div>

                              </div>
                                   </asp:Panel>
                          </ContentTemplate>
                      </asp:TabPanel>   

                  </asp:TabContainer>
                
          </ContentTemplate>
          </asp:UpdatePanel>
      
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
      </div>
         <asp:Label ID="lblidguncelle" Visible="false"  CssClass="gizli"   runat="server" Text="1"></asp:Label>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

     <!-- ===================================================================================================================== -->
        
     <!-- The Modal-Rol Ekle -->
<div class="modal" id="ModalRol">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content" style="margin-top:50px">

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Roller</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

      <!-- Modal body -->
      <div class="modal-body">

            <div class="messagealert mt-3 mb-5" id="alert_container_rol">
            </div>

             <asp:Panel runat="server">
                        <div class="row">
                            <div class="col-md-4">
                                <label for="usr">Kayıtlı Roller</label>
                            </div>
                            <div class="col-md-4">
                                    <asp:DropDownList ID="comrolkaydet" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" Width="200px" DataSourceID="sqlRol" DataTextField="ROL" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="comrolkaydet_SelectedIndexChanged" >              </asp:DropDownList>
                             </div>
                    </div>
</br>

                        <div class="row">
                            <div class="col-md-4">
                                <label for="usr">Yeni Rol Giriş</label>
                            </div>
                            <div class="col-md-4">
                                    <asp:TextBox ID="txtroladi" CssClass="form-control" Width="200px" runat="server"></asp:TextBox>
                            </div>
                    </div>
                     <div class="row">
                         
                         <br />
                         <br />
                     </div>

                     <div class="row ml-10 mt-10 mr-10 mb-10" style="margin-top:15px">
                         <asp:TabContainer ID="TabContainer2" CssClass="Tab6" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%"  >
                              
                  </asp:TabContainer>

                     </div>

              </asp:Panel>
      
      </div>
         
      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                                        <asp:Button ID="btnrolkaydet" runat="server" ValidationGroup="rolkaydet" CssClass="btn btn-success" Text="Rol Kaydet" OnClick="btnrolkaydet_Click" />
      </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
  </div>
</div>

     <!-- ===================================================================================================================== -->

      <!-- The Modal-Sil -->
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
        Kullanıcı Silinecek Emin Misiniz?
          <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
          <asp:Label ID="lblidsil" runat="server" Visible="false" Text=""></asp:Label>
          <div class="messagealert mt-3 mb-3" id="alert_container_sil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
          <asp:Button ID="btnsil" CssClass="btn btn-danger"  OnClick="btnsil_Click" runat="server" Text="Sil" />
      </div>

                 </ContentTemplate>
        </asp:UpdatePanel>

    </div>
  </div>
</div>

         <!-- ===================================================================================================================== -->
    
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
     
<asp:SqlDataSource ID="sqlRol" runat="server" ConnectionString="<%$ ConnectionStrings:KabloHE %>" ></asp:SqlDataSource>

      <asp:SqlDataSource ID="sqlKullanici" runat="server" ConnectionString="<%$ ConnectionStrings:KabloHE %>" ProviderName="<%$ ConnectionStrings:KabloHE.ProviderName %>"></asp:SqlDataSource>

     <asp:SqlDataSource ID="sqlil" runat="server" 
                ConnectionString="<%$ ConnectionStrings:KabloHE %>" 
                ></asp:SqlDataSource>
    
     <asp:SqlDataSource ID="sqlbolge" runat="server" 
                ConnectionString="<%$ ConnectionStrings:KabloHE %>" 
                ></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlfirma" runat="server" 
         ConnectionString="<%$ ConnectionStrings:KabloHE %>" 
                ></asp:SqlDataSource>
</asp:Content>
