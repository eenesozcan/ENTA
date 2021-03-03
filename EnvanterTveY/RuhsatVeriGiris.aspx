<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RuhsatVeriGiris.aspx.cs" Inherits="EnvanterTveY.RuhsatVeriGiris" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="Scripts/loading.js"></script>

    <script src="Scripts/KabloHE-js.js"></script>
    <link href="Content/Site.css" rel="stylesheet" />



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <div class="container-fluid"  style="margin-top: 20px">

        <div class="row">
            <span class="baslik">Ruhsat Veri Girişi</span>
        </div>

        <div class="row">

        <ajaxToolkit:TabContainer ID="TabContainer1"  CssClass="Tab6" runat="server" ActiveTabIndex="0"  CssTheme="None"  Width="100%" >

            <ajaxToolkit:TabPanel runat="server" HeaderText="Aykome Proje Tipi" ID="TabPanel1" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Aykome_Proje_Tipi_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Aykome_Proje_Tipi"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px"    OnClick="btnara_Aykome_Proje_Tipi_Click"        />
                                                        <asp:Button  ID="btn_Aykome_Proje_Tipi_ekle"  runat="server" Text="Ekle"        CssClass="btn btn-success " Width="130px" OnClick="btn_Aykome_Proje_Tipi_ekle_Click"     />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Aykome_Proje_Tipi" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"  OnRowCommand="grid_Aykome_Proje_Tipi_RowCommand"  OnPageIndexChanging="grid_Aykome_Proje_Tipi_PageIndexChanging"     >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"               HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="A_PROJE_TIPI"     HeaderText="Aykome Proje Tipi"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Türksat Proje Tipi" ID="TabPanel2" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Turksat_Proje_Tipi_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Turksat_Proje_Tipi"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px"   OnClick="btnara_Turksat_Proje_Tipi_Click"            />
                                                        <asp:Button  ID="btn_Turksat_Proje_Tipi_ekle" runat="server" Text="Ekle"        CssClass="btn btn-success " Width="130px"   OnClick="btn_Turksat_Proje_Tipi_ekle_Click"        />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Turksat_Proje_Tipi" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"  OnRowCommand="grid_Turksat_Proje_Tipi_RowCommand"  OnSelectedIndexChanging="grid_Turksat_Proje_Tipi_SelectedIndexChanging"         >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"               HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="T_PROJE_TIPI"     HeaderText="Türksat Proje Tipi"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Kazı Birimi" ID="TabPanel3" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Kazi_Birim_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Kazi_Birim"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px" OnClick="btnara_Kazi_Birim_Click"                  />
                                                        <asp:Button  ID="btn_Kazi_Birim_ekle" runat="server" Text="Ekle"        CssClass="btn btn-success " Width="130px"       OnClick="btn_Kazi_Birim_ekle_Click"    />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Kazi_Birim" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"  OnRowCommand="grid_Kazi_Birim_RowCommand"  OnSelectedIndexChanging="grid_Kazi_Birim_SelectedIndexChanging"             >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"               HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="BIRIM"     HeaderText="Kazı Birimi"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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


            <ajaxToolkit:TabPanel runat="server" HeaderText="Ruhsat Veren İdare" ID="TabPanel4" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Ruhsat_Veren_Idare_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Ruhsat_Veren_Idare"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px"   OnClick="btnara_Ruhsat_Veren_Idare_Click"                  />
                                                        <asp:Button  ID="btn_Ruhsat_Veren_Idare_ekle"       runat="server" Text="Ekle"     CssClass="btn btn-success " Width="130px"      OnClick="btn_Ruhsat_Veren_Idare_ekle_Click"         />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Ruhsat_Veren_Idare" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"  OnRowCommand="grid_Ruhsat_Veren_Idare_RowCommand"  OnSelectedIndexChanging="grid_Ruhsat_Veren_Idare_SelectedIndexChanging"                 >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"               HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"            HeaderText="İl"  />
                                                            <asp:BoundField DataField="RUHSAT_VEREN"  HeaderText="Ruhsat Veren İdare"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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
			
            <ajaxToolkit:TabPanel runat="server" HeaderText="Ruhsat Dönemi" ID="TabPanel5" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Ruhsat_Donemi_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Ruhsat_Donemi"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px"   OnClick="btnara_Ruhsat_Donemi_Click"                      />
                                                        <asp:Button  ID="btn_Ruhsat_Donemi_ekle"       runat="server" Text="Ekle"     CssClass="btn btn-success " Width="130px"      OnClick="btn_Ruhsat_Donemi_ekle_Click"          />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Ruhsat_Donemi" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"  OnRowCommand="grid_Ruhsat_Donemi_RowCommand"  OnSelectedIndexChanging="grid_Ruhsat_Donemi_SelectedIndexChanging"                 >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"               HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"            HeaderText="İl"  />
                                                            <asp:BoundField DataField="RUHSAT_VEREN"  HeaderText="Ruhsat Veren İdare"  />
                                                            <asp:BoundField DataField="DONEM"         HeaderText="Dönem"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Kazı Türü" ID="TabPanel6" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Kazi_Turu_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Kazi_Turu"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px"  OnClick="btnara_Kazi_Turu_Click"             />
                                                        <asp:Button  ID="btn_Kazi_Turu_ekle"       runat="server" Text="Ekle"     CssClass="btn btn-success " Width="130px"     OnClick="btn_Kazi_Turu_ekle_Click"          />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Kazi_Turu" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="700px"   OnRowCommand="grid_Kazi_Turu_RowCommand" OnSelectedIndexChanging="grid_Kazi_Turu_SelectedIndexChanging"                   >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"            HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"            HeaderText="İl"  />
                                                            <asp:BoundField DataField="RUHSAT_VEREN"  HeaderText="Ruhsat Veren İdare"  />
                                                            <asp:BoundField DataField="DONEM"         HeaderText="Dönem"  />
                                                            <asp:BoundField DataField="KAZI_TURU"     HeaderText="Kazı Türü"  />

                                                    <asp:TemplateField Visible="false" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Kazı Tipi ve Tarifeler" ID="TabPanel7" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Kazi_Tipi_Tarife_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Kazi_Tipi_Tarife"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px"  OnClick="btnara_Kazi_Tipi_Tarife_Click"              />
                                                        <asp:Button  ID="btn_Kazi_Tipi_Tarife_ekle"       runat="server" Text="Ekle"     CssClass="btn btn-success " Width="130px"   OnClick="btn_Kazi_Tipi_Tarife_ekle_Click"          />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Kazi_Tipi_Tarife" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="90%"   OnRowCommand="grid_Kazi_Tipi_Tarife_RowCommand"     OnPageIndexChanging="grid_Kazi_Tipi_Tarife_PageIndexChanging"               >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"            HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"            HeaderText="İl"  />
                                                            <asp:BoundField DataField="RUHSAT_VEREN"  HeaderText="Ruhsat Veren İdare"  />
                                                            <asp:BoundField DataField="DONEM"         HeaderText="Dönem"  />
                                                            <asp:BoundField DataField="KAZI_TURU"     HeaderText="Kazı Türü"  />
                                                            <asp:BoundField DataField="KAZI_TIPI"     HeaderText="Kazı Tipi"  />
                                                            <asp:BoundField DataField="UCRET"         HeaderText="Birim Fiyatı"  />
                                                            <asp:BoundField DataField="BIRIM"         HeaderText="Birimi"  />
                                                            <asp:BoundField DataField="KDV"           HeaderText="KDV"  />

                                                    <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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

            <ajaxToolkit:TabPanel runat="server" HeaderText="Ruhsat Tarife" ID="TabPanel8" CssClass="ajax__tab_panel">
                <ContentTemplate>
                       <div class="container-fluid" style="margin-left:30px">
                            <div class="row">
                            </div>
                                 <div class="row">

                                            <div class="col-md-12 col-xs-12 ">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_Ruhsat_Tarife_ara"        runat="server"                 CssClass="form-control"        style="margin-right:35px"   placeholder="Ara"  ></asp:TextBox>
                                                        <asp:Button  ID="btnara_Ruhsat_Tarife"         runat="server" Text="Ara"      CssClass="btn  btn-primary"    style="margin-right:4px"    OnClick="btnara_Ruhsat_Tarife_Click"                />
                                                        <asp:Button  ID="btn_Ruhsat_Tarife_ekle"       runat="server" Text="Ekle"     CssClass="btn btn-success " Width="130px"       OnClick="btn_Ruhsat_Tarife_ekle_Click"           />
                                                    </div>
                                                 </div>
                                            </div>
          
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grid_Ruhsat_Tarife" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" DataKeyNames="ID"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" Width="90%"    OnRowCommand="grid_Ruhsat_Tarife_RowCommand"  OnPageIndexChanging="grid_Ruhsat_Tarife_PageIndexChanging"                >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID"                             HeaderText="ID"  InsertVisible="False" ReadOnly="True"  />
                                                            <asp:BoundField DataField="IL"                             HeaderText="İl"  />
                                                            <asp:BoundField DataField="RUHSAT_VEREN"                   HeaderText="Ruhsat Veren İdare"  />
                                                            <asp:BoundField DataField="DONEM"                          HeaderText="Dönem"  />
                                                            <asp:BoundField DataField="KAZI_TURU"                      HeaderText="Kazı Türü"  />
                                                            <asp:BoundField DataField="KESIF_KONTROL_BEDELI"           HeaderText="Keşif ve Kontrol Bedeli"  />
                                                            <asp:BoundField DataField="RUHSAT_BEDELI"                  HeaderText="Altyapı Ruhsat Bedeli"  />
                                                            <asp:BoundField DataField="KAZI_IZIN_HARCI"                HeaderText="Altyapı Kazı İzin Harcı"  />
                                                            <asp:BoundField DataField="TEMINATA_DAHIL_OLMA_ALTLIMITI"  HeaderText="Teminata Dahil Olma Alt Limiti"  />
                                                            <asp:BoundField DataField="TEMINATA_DAHIL_OLMA_YUZDESI"    HeaderText="Teminat Kullanım Yüzdesi"  />
                                                            <asp:BoundField DataField="KDV"                            HeaderText="KDV"  />

                                                    <asp:TemplateField Visible="true" HeaderStyle-Width="150px"  HeaderText="İşlemler">
                                                       <ItemTemplate > 
                                                                    <asp:Button ID="btnguncelle" runat="server" Text="Güncelle"  CommandName="guncelle" CssClass="btn btn-success btn-sm" Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
                                                                    <asp:Button ID="btnsil"      runat="server" Text="Sil"       CommandName="sil"      CssClass="btn btn-danger btn-sm"  Visible="true"  CommandArgument='<%# Container.DataItemIndex %>' />
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

    </div>

</ContentTemplate>
</asp:UpdatePanel>










                 <!-- The Modal Ruhsat Veri Giriş -->
<div class="modal" id="ModalRuhsatVeri" tabindex="-1" role="dialog" aria-labelledby="quickview" aria-hidden="true">
  <div class="modal-dialog  modal-dialog-centered" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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

<asp:Panel ID="Panel_Aykome_Proje_Tipi"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
           <div class="row">
            <div class="col-md-6">
                                   <div class="form-group">
                                       <label class="text-primary" for="usr">Aykome Proje Tipi:</label>
 
                                       <asp:TextBox ID="txt_aproje_tipi" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txt_aproje_tipi" ValidationGroup="Aykome_Proje_Tipi" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>

            </div>

           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnkaydet_Aykome_Proje_Tipi" runat="server" ValidationGroup="Aykome_Proje_Tipi" CssClass="btn btn-success" Text="Kaydet" OnClick="btnkaydet_Aykome_Proje_Tipi_Click"     />
                </div>
            </div>

</asp:Panel>

<asp:Panel ID="Panel_Turksat_Proje_Tipi"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
           <div class="row">
            <div class="col-md-6">
                                   <div class="form-group">
                                       <label class="text-primary" for="usr">Türksat Proje Tipi:</label>
 
                                       <asp:TextBox ID="txt_tproje_tipi" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator30" ControlToValidate="txt_tproje_tipi" ValidationGroup="Turksat_Proje_Tipi" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>

            </div>
           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnkaydet_Turksat_Proje_Tipi" runat="server" ValidationGroup="Turksat_Proje_Tipi" CssClass="btn btn-success" Text="Kaydet"  OnClick="btnkaydet_Turksat_Proje_Tipi_Click"    />
                </div>
            </div>
</asp:Panel>

<asp:Panel ID="Panel_Kazi_Birimi"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">

           <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="text-primary" for="usr">Kazı Birimi:</label>
 
                        <asp:TextBox ID="txt_birim" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txt_birim" ValidationGroup="Kazi_Birimi" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                    </div>
                 </div>
            </div>
           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnkaydet_Ruhsat_Veren_Kazi_Birimi" runat="server" ValidationGroup="Kazi_Birimi" CssClass="btn btn-success" Text="Kaydet"  OnClick="btnkaydet_Ruhsat_Veren_Kazi_Birimi_Click"   />
                </div>
            </div>
</asp:Panel>

<asp:Panel ID="Panel_Ruhsat_Veren"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">

           <div class="row">
            <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">İl:</label>
                                        <asp:DropDownList ID="com_il_sec_ruhsat_veren"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"         > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="com_il_sec_ruhsat_veren"  ValidationGroup="Ruhsat_Veren"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
                                   <div class="form-group">
                                       <label class="text-primary" for="usr">Ruhsat Veren İdarenin Adı:</label>
 
                                       <asp:TextBox ID="txt_ruhsat_veren" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txt_ruhsat_veren" ValidationGroup="Ruhsat_Veren" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>

               </div>

            </div>
           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnkaydet_Ruhsat_Veren" runat="server" ValidationGroup="Ruhsat_Veren" CssClass="btn btn-success" Text="Kaydet"  OnClick="btnkaydet_Ruhsat_Veren_Click"      />
                    </div>
           </div>
</asp:Panel>

<asp:Panel ID="Panel_Donem"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
           <div class="row">
            <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">İl:</label>
                                        <asp:DropDownList ID="com_il_sec_Donem"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"    OnSelectedIndexChanged="com_il_sec_Donem_SelectedIndexChanged"       > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="com_il_sec_Donem"  ValidationGroup="Donem"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Ruhsat Veren İdare:</label>
                                        <asp:DropDownList ID="com_ruhsat_veren_sec_donem"  DataTextField="RUHSAT_VEREN" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="com_ruhsat_veren_sec_donem"  ValidationGroup="Donem"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>

                                   <div class="form-group">
                                       <label class="text-primary" for="usr">Dönem:</label>
 
                                       <asp:TextBox ID="txt_donem"  onkeypress="return onlyDotsAndNumbers(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_donem" ValidationGroup="Donem" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>

            </div>
           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                     <asp:Button ID="btnkaydet_Donem" runat="server" ValidationGroup="Donem" CssClass="btn btn-success" Text="Kaydet" OnClick="btnkaydet_Donem_Click"     />
                </div>
           </div>
</asp:Panel>

<asp:Panel ID="Panel_Kazi_Turu"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">

           <div class="row">
               <div class="col-md-6">
                        <div class="form-group">
                            <label class="text-primary" for="usr">İl:</label>
                            <asp:DropDownList ID="com_il_sec_Kazi_Turu"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"    OnSelectedIndexChanged="com_il_sec_Kazi_Turu_SelectedIndexChanged"        > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="com_il_sec_Kazi_Turu"  ValidationGroup="Kazi_Turu"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                        </div>
               </div>
               <div class="col-md-6">
                        <div class="form-group">
                            <label class="text-primary" for="usr">Dönem Seç:</label>
                            <asp:DropDownList ID="com_donem_sec_Kazi_Turu"  DataTextField="DONEM" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"         > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="com_donem_sec_Kazi_Turu"  ValidationGroup="Kazi_Turu"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                        </div>
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                        <div class="form-group">
                            <label class="text-primary" for="usr">Ruhsat Veren İdare Seç :</label>
                            <asp:DropDownList ID="com_ruhsat_veren_sec_Kazi_Turu"  DataTextField="RUHSAT_VEREN" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="com_ruhsat_veren_sec_Kazi_Turu_SelectedIndexChanged"       > </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="com_ruhsat_veren_sec_Kazi_Turu"  ValidationGroup="Kazi_Turu"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                        </div>
               </div>
               <div class="col-md-6">
                        <div class="form-group">
                            <label class="text-primary" for="usr">Kazı Türü:</label>
                            <asp:TextBox ID="txt_kazi_turu_Kazi_Turu" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_kazi_turu_Kazi_Turu" ValidationGroup="Kazi_Turu" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                        </div>
               </div>
           </div>

           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnkaydet_Kazi_Turu" runat="server" ValidationGroup="Kazi_Turu" CssClass="btn btn-success" Text="Kaydet" OnClick="btnkaydet_Kazi_Turu_Click"     />
                </div>
            </div>
</asp:Panel>

<asp:Panel ID="Panel_Kazi_Tipi"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">İl :</label>
                                        <asp:DropDownList ID="com_il_sec_Kazi_Tipi"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="com_il_sec_Kazi_Tipi_SelectedIndexChanged"            > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="com_il_sec_Kazi_Tipi"  ValidationGroup="Kazi_Tipi"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                   <div class="form-group">
                                       <label class="text-primary" for="usr">Kazı Tipi:</label>
                                       <asp:TextBox ID="txt_kazi_tipi_Kazi_Tipi" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txt_kazi_tipi_Kazi_Tipi" ValidationGroup="Kazi_Tipi" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Ruhsat Veren İdare Seç :</label>
                                        <asp:DropDownList ID="com_ruhsat_veren_sec_Kazi_Tipi"  DataTextField="RUHSAT_VEREN" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="com_ruhsat_veren_sec_Kazi_Tipi_SelectedIndexChanged"        > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="com_ruhsat_veren_sec_Kazi_Tipi"  ValidationGroup="Kazi_Tipi"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                  <div class="form-group">
                                       <label class="text-primary" for="usr">Birim Ücreti:</label>
                                       <asp:TextBox ID="txt_kazi_tarife_Kazi_Tipi" onkeypress="return onlyDotsAndNumbers(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txt_kazi_tarife_Kazi_Tipi" ValidationGroup="Kazi_Tipi" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Tarife Dönemi Seç:</label>
                                        <asp:DropDownList ID="com_donem_sec_Kazi_Tipi"  DataTextField="DONEM" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="com_donem_sec_Kazi_Tipi_SelectedIndexChanged"             > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="com_donem_sec_Kazi_Tipi"  ValidationGroup="Kazi_Tipi"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Birim Seç:</label>
                                        <asp:DropDownList ID="com_birim_sec_Kazi_Tipi"  DataTextField="BIRIM" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"       > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="com_birim_sec_Kazi_Tipi"  ValidationGroup="Kazi_Tipi"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Kazı Türü Seç:</label>
                                        <asp:DropDownList ID="com_kazi_turu_Kazi_Tipi"  DataTextField="KAZI_TURU" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"         > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="com_kazi_turu_Kazi_Tipi"  ValidationGroup="Kazi_Tipi"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                        <label class="text-primary" for="usr">KDV:</label>
                                        <label class="switch">
                                            <asp:CheckBox ID="chc_kdv_Kazi_Tipi" runat="server"  Checked="true" AutoPostBack="True"  OnCheckedChanged="chc_kdv_Kazi_Tipi_CheckedChanged"                  />
                                            <span class="slider round" style="scale: 0.7;"></span> 
                                        </label>       
                                        <asp:Label ID="lbl_kdv_Kazi_Tipi" style="font-size:14px"  runat="server" ></asp:Label>
               </div>
           </div>

           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnkaydet_Kazi_Tipi" runat="server" ValidationGroup="Kazi_Tipi" CssClass="btn btn-success" Text="Kaydet" OnClick="btnkaydet_Kazi_Tipi_Click"     />
                </div>
            </div>
</asp:Panel>

<asp:Panel ID="Panel_Ruhsat_Tarife"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">İl :</label>
                                        <asp:DropDownList ID="com_il_sec_Ruhsat_Tarife"  DataTextField="IL" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="com_il_sec_Ruhsat_Tarife_SelectedIndexChanged"              > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="com_il_sec_Ruhsat_Tarife"  ValidationGroup="Ruhsat_Tarife"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                   <div class="form-group">
                                       <label class="text-primary" for="usr">Keşif ve Kontrol Bedeli Tutarı:</label>
                                       <asp:TextBox ID="txt_kesif_kontrol_bedeli_Ruhsat_Tarife" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txt_kesif_kontrol_bedeli_Ruhsat_Tarife" ValidationGroup="Ruhsat_Tarife" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Ruhsat Veren İdare Seç :</label>
                                        <asp:DropDownList ID="com_ruhsat_veren_sec_Ruhsat_Tarife"  DataTextField="RUHSAT_VEREN" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="com_ruhsat_veren_sec_Ruhsat_Tarife_SelectedIndexChanged"         > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="com_ruhsat_veren_sec_Ruhsat_Tarife"  ValidationGroup="Ruhsat_Tarife"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                  <div class="form-group">
                                       <label class="text-primary" for="usr">Ruhsat Bedeli:</label>
                                       <asp:TextBox ID="txt_ruhsat_bedeli_Ruhsat_Tarife" onkeypress="return onlyDotsAndNumbers(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txt_ruhsat_bedeli_Ruhsat_Tarife" ValidationGroup="Ruhsat_Tarife" runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Boş geçilemez." />
                                   </div>
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Dönem Seç:</label>
                                        <asp:DropDownList ID="com_donem_sec_Ruhsat_Tarife"  DataTextField="DONEM" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="com_donem_sec_Ruhsat_Tarife_SelectedIndexChanged"                > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="com_donem_sec_Ruhsat_Tarife"  ValidationGroup="Ruhsat_Tarife"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Kazı İzin Harcı:</label>
                                        <asp:TextBox ID="txt_kazi_izin_harci_Ruhsat_Tarife" onkeypress="return onlyDotsAndNumbers(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ControlToValidate="txt_kazi_izin_harci_Ruhsat_Tarife"  ValidationGroup="Ruhsat_Tarife"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Kazı Türü Seç:</label>
                                        <asp:DropDownList ID="com_kazi_turu_Ruhsat_Tarife"  DataTextField="KAZI_TURU" DataValueField="ID"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server" AutoPostBack="True"         > </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ControlToValidate="com_kazi_turu_Ruhsat_Tarife"  ValidationGroup="Ruhsat_Tarife"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>
               </div>
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Teminata Dahil Olma Alt Limiti:</label>
                                        <asp:TextBox ID="txt_teminat_limiti_Ruhsat_Tarife" onkeypress="return onlyDotsAndNumbers(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ControlToValidate="txt_teminat_limiti_Ruhsat_Tarife"  ValidationGroup="Ruhsat_Tarife"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>                               
               </div>
           </div>
           <div class="row">
               <div class="col-md-6">
                                        <label class="text-primary" for="usr">KDV:</label>
                                        <label class="switch">
                                            <asp:CheckBox ID="chc_kdv_Ruhsat_Tarife" runat="server"  Checked="true" AutoPostBack="True"   OnCheckedChanged="chc_kdv_Ruhsat_Tarife_CheckedChanged"                        />
                                            <span class="slider round" style="scale: 0.7;"></span> 
                                        </label>       
                                        <asp:Label ID="lbl_kdv_Ruhsat_Tarife" style="font-size:14px"  runat="server" ></asp:Label>
               </div>
               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="text-primary" for="usr">Teminata Dahil Olma Yüzdesi:</label>
                                        <asp:TextBox ID="txt_teminat_yuzde_Ruhsat_Tarife" onkeypress="return onlyDotsAndNumbers(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ControlToValidate="txt_teminat_yuzde_Ruhsat_Tarife"  ValidationGroup="Ruhsat_Tarife"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="İl seçiniz." ForeColor="red" />
                                    </div>         
               </div>
           </div>


           <div class="row">
                <div class="col-md-9"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnkaydet_Ruhsat_Tarife" runat="server" ValidationGroup="Ruhsat_Tarife" CssClass="btn btn-success" Text="Kaydet"  OnClick="btnkaydet_Ruhsat_Tarife_Click"        />
                </div>
            </div>
</asp:Panel>







                                    <asp:Label ID="lblidcihaz" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_il_sec" runat="server" Text="" Visible="false"></asp:Label>

           </div>
       

                      <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>

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
                  <div class="messagealert mt-3 mb-3" id="alert_container_sil">
                  </div>
              </div>

              <!-- Modal footer -->
              <div class="modal-footer">
                  <button type="button"   class="btn btn-secondary" data-dismiss="modal">Kapat</button>
                  <asp:Button ID="btnsil" CssClass="btn btn-danger"  runat="server" Text="Sil"   />
                  <asp:Label ID="lblidsil" Visible="false" runat="server" Text="Label"></asp:Label>
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
