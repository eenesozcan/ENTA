<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenelAyar.aspx.cs" Inherits="EnvanterTveY.GenelAyar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="Scripts/KabloHE-js.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
          
    <div class="container-fluid" style="margin-left:10px; margin-top: 20px">
             <div class="row">
                <span class="baslik">Program Genel Ayarlar</span>
            </div>

        <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab4" runat="server" ActiveTabIndex="1"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="Program Ayarları" ID="TabPanel1" CssClass="ajax__tab_panel">
                <ContentTemplate>

                <div class="container-fluid" style="margin-left:30px">
      <div class="row">
        <div class="col-lg-6">
            <div class="form-group">
                <table style="width: 100%;">
                    <tr>
                            <td>    <label for="">Programın Adı</label></td>
                            <td>    <asp:TextBox ID="txtprograminadi" ValidationGroup="programayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>

                    <tr>
                            <td>    <label for="">Programın Adresi</label></td>
                            <td>    <asp:TextBox ID="txtprograminadresi" ValidationGroup="programayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>
                    <tr> 
                        <td>    &nbsp;</td>
                        <td>    
                            &nbsp;</td>
                    </tr>                    
                 </table>
            </div>
        </div>
          </div>
      <div class="row">
         <div class="col-md-12">
                 <div class="input-group">
                    <span>
                            <asp:Button ID="btnprogramayarkaydet" runat="server" ValidationGroup="programayarkaydet" CssClass="btn btn-success" Text="Program Ayar Kaydet" OnClick="btnprogramayarkaydet_Click" />
                    </span>
                      <asp:Label ID="Label1" runat="server" CssClass="label_durum"></asp:Label>
 
                 </div>
       </div>
     </div>

 
  </div>

 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

             <ajaxToolkit:TabPanel runat="server" HeaderText="Genel Ayarları" ID="TabPanel3" CssClass="ajax__tab_panel">
                <ContentTemplate>

   <div class="container-fluid" style="margin-left:30px">

    <div class="row">
                <!-- <span class="baslik">Bölge EKLE</span> -->
    </div>

    <div class="row">

        <div class="col-md-6 col-xs-12">
            <div class="form-group">
                <table style="width: 100%;">
                    <tr>
                        <td>    <label for="">Giriş</label></td>
                        <td>    
                            <asp:DropDownList ID="comgiris" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" style="margin: 2px;">
                                <asp:ListItem>LDAP</asp:ListItem>
                                <asp:ListItem>LOCAL</asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="comgiris"  ValidationGroup="ayarkaydet"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Kablo Tipini Seçiniz." />
                        </td>
                    </tr>

                    <tr> 
                        <td>    <label for="">Local Giriş</label></td>
                        <td>    <asp:TextBox ID="txtlocalgiris" ValidationGroup="ayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>

                    <tr> 
                        <td>    <label for="">Mail Server</label></td>
                        <td>    <asp:DropDownList ID="commailserver" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" style="margin: 2px;">
                                <asp:ListItem>hasan</asp:ListItem>
                                <asp:ListItem>turksat</asp:ListItem>
                                <asp:ListItem>custom</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>

                    <tr> 
                        <td>    <label for="">SSL</label></td>
                        <td>    
                            <asp:DropDownList ID="comssl" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" style="margin: 2px;">
                                <asp:ListItem Value="True">Evet</asp:ListItem>
                                <asp:ListItem Value="False">Hayır</asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="comssl"  ValidationGroup="ayarkaydet"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Kablo Tipini Seçiniz." />
                        </td>

                    </tr>


                      <tr>
                        <td>    <label for="">Site Yönlendirme</label></td>
                        <td>    
                            <asp:DropDownList ID="comyonlendirme" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" style="margin: 2px;">
                                 <asp:ListItem Value="True">Evet</asp:ListItem>
                                <asp:ListItem Value="False">Hayır</asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="comgiris"  ValidationGroup="ayarkaydet"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Kablo Tipini Seçiniz." />
                        </td>
                    </tr>



                    <tr> 
                        <td>    <label for="">Yönlendirilen Link</label></td>
                        <td>    <asp:TextBox ID="txtyonlink" ValidationGroup="ayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td>    <label for="">Local Giriş </label></td>
                        <td>    
                            <asp:DropDownList ID="comlocalgiris" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" style="margin: 2px;">
                                 <asp:ListItem Value="True">Evet</asp:ListItem>
                                <asp:ListItem Value="False">Hayır</asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="comgiris"  ValidationGroup="ayarkaydet"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Kablo Tipini Seçiniz." />
                        </td>
                    </tr>
                     <tr> 
                        <td>    <label for="">Custom Posta Sunucu</label></td>
                        <td>    <asp:TextBox ID="txtpostasunucu" ValidationGroup="ayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>
                     <tr> 
                        <td>    <label for="">Custom Posta Kullanıcı Adı</label></td>
                        <td>    <asp:TextBox ID="txtpostakullanici" ValidationGroup="ayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>
                     <tr> 
                        <td>    <label for="">Custom Posta Şifre</label></td>
                        <td>    <asp:TextBox ID="txtpostasifre" ValidationGroup="ayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>
                     <tr> 
                        <td>    <label for="">Custom Posta Port</label></td>
                        <td>    <asp:TextBox ID="txtpostaport" ValidationGroup="ayarkaydet"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                    </tr>
                </table>

            </div>
        </div>

                                                                                <div class="col-md-3 col-xs-12"> 
                                                                                     <div class="form-group">
                                                                                       <label for=""></label>
                                                                                         <label for=""></label>
                                                                                     </div>
                                                                               </div>
                                                                                        <div class="col-md-3 col-xs-12"> 
                                                                                     <div class="form-group">
                                                                                       <label for=""></label>
                                                                                     </div>
                                                                               </div>
                                                                                                <div class="col-md-3 col-xs-12"> 
                                                                                     <div class="form-group">
                                                                                       <label for=""></label>
                                                                                     </div>
                                                                               </div>
                                                                                                <div class="col-md-3 col-xs-12"> 
                                                                                     <div class="form-group">
                                                                                       <label for=""></label>
                                                                                     </div>
                                                                               </div>
                                                                                                <div class="col-md-3 col-xs-12"> 
                                                                                     <div class="form-group">
                                                                                       <label for=""></label>
                                                                                     </div>
                                                                               </div>

        <div class="col-md-6 col-xs-12 float-right">
             <label for=""></label>
              <div class="input-group">
                  <span>

                                                  <asp:Button ID="btnayarkaydet" runat="server" ValidationGroup="ayarkaydet" CssClass="btn btn-success" OnClick="btnayarkaydet_Click" Text="Ayar Kaydet" />

                      </span>
              </div>
         </div>

     </div>
        </div>  

 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

    <ajaxToolkit:TabPanel runat="server" HeaderText="Com Doldur" ID="TabPanel2" CssClass="ajax__tab_panel">
        <ContentTemplate>

                <div class="container-fluid" style="margin-left:30px">
                    <div class="row">
                    <div class="col-lg-4">
                        <div class="form-group">
                            <table style="width: 100%;">
                                <tr>
                                        <td>    <label for="">Com Adı</label></td>
                                        <td>    <asp:TextBox ID="txtcomadi" ValidationGroup="comdoldur"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                                </tr>
                                <tr>
                                        <td>    <label for="">Seçenek</label></td>
                                        <td>    <asp:TextBox ID="txtsecenek" ValidationGroup="comdoldur"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                                </tr>
                                <tr>
                                        <td>    <label for="">Değer</label></td>
                                        <td>    <asp:TextBox ID="txtdeger" ValidationGroup="comdoldur"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                                </tr>       
                                <tr>
                                        <td>    <label for="">Sıra</label></td>
                                        <td>    <asp:TextBox ID="txtsira" ValidationGroup="comdoldur"  CssClass="form-control" runat="server" style="margin: 2px;" onkeypress="return sadecerakam(event)"></asp:TextBox></td>
                                </tr>                    
                                </table>
                        </div>
                    </div>
            </div>  
                    <div class="row">
                        <div class="col-md-1">
                                <div class="input-group">
                                <span>
                                        <asp:Button ID="btncomdoldurkaydet" runat="server" ValidationGroup="comdoldur" CssClass="btn btn-success" Text="Kaydet"  OnClick="btncomdoldurkaydet_Click" />
                                </span>
 
                                </div>
                    </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                        <div class="table-responsive" >
                                    <asp:GridView ID="grid_comdoldur" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"  Width="900px" ShowHeaderWhenEmpty="True" DataKeyNames="ID" EmptyDataText="Kayıt bulunamadı"  AllowPaging="True" PageSize="10" OnRowCommand="grid_comdoldur_RowCommand" OnPageIndexChanging="grid_comdoldur_PageIndexChanging"  >
                                        <Columns>
                                                    <asp:BoundField DataField="ID"         HeaderText="ID"         />
                                                    <asp:BoundField DataField="COM_ADI"    HeaderText="Com Adı"    />
                                                    <asp:BoundField DataField="SECENEK"    HeaderText="Seçenek"    />
                                                    <asp:BoundField DataField="DEGER"      HeaderText="Değer"      />
                                                    <asp:BoundField DataField="SIRA"       HeaderText="Sıra"       />

                                                    <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                        <ItemTemplate > 
                                                            <asp:Button ID="btnsil"            runat="server" Text="Sil"               CommandName="sil"      CssClass="btn btn-danger btn-sm"   CommandArgument='<%#Eval("ID") %>' />

                                                        </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblcomdoldur" runat="server" CssClass="label_durum"></asp:Label>
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

         <!-- Modal Sil-->
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
        Kayıt sililecektir. Emin misiniz?
          <asp:Label ID="lblislem" runat="server" Visible="false" Text=""></asp:Label>
          <div class="messagealert mt-3 mb-3" id="alert_container_sil">
          </div>
      </div>

      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>
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
