<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaporPort.aspx.cs" Inherits="EnvanterTveY.RaporPort" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="Scripts/KabloHE-js.js"></script>



 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

    <div class="container-fluid" style="margin-left:10px; margin-top: 20px">

        <div class="row">
            <span class="baslik">Raporlar</span>
        </div>
        <br />
        <div class="row">
                    <asp:CheckBox class="text rounded p-4 label-rapor-secim bg-success" style="margin-right:5px;" ID="chc_tarih"   runat="server" Text="Tarih Bazlı Arama" AutoPostBack="true"  OnCheckedChanged="chc_tarih_CheckedChanged"  Visible="true" />
                    <asp:CheckBox class="text rounded p-4 label-rapor-secim bg-success" style="margin-right:5px;" ID="chc_genel"   runat="server" Text="Genel Rapor Arama" AutoPostBack="true"  OnCheckedChanged="chc_genel_CheckedChanged"  Visible="true" />
                    <asp:CheckBox class="text rounded p-4 label-rapor-secim bg-success" style="margin-right:5px;" ID="chc_cmts"    runat="server" Text="CMTS Rapor Arama" AutoPostBack="true"   OnCheckedChanged="chc_cmts_CheckedChanged"   />
                    <asp:CheckBox class="text rounded p-4 label-rapor-secim bg-success" style="margin-right:5px;" ID="chc_port"    runat="server" Text="Port Rapor Arama" AutoPostBack="true"   OnCheckedChanged="chc_port_CheckedChanged"   />
                    <asp:CheckBox class="text rounded p-4 label-rapor-secim bg-success" style="margin-right:5px;" ID="chc_mac"     runat="server" Text="Mac Adresi Arama" AutoPostBack="true"   OnCheckedChanged="chc_mac_CheckedChanged"    />
                    <asp:CheckBox class="text rounded p-4 label-rapor-secim bg-success" style="margin-right:5px;" ID="chc_devreno" runat="server" Text="Devre No Arama" AutoPostBack="true"     OnCheckedChanged="chc_devreno_CheckedChanged"/>
                    <asp:CheckBox class="text rounded p-4 label-rapor-secim bg-success" style="margin-right:5px;" ID="chc_deger"   runat="server" Text="Değer Arama" AutoPostBack="true"        OnCheckedChanged="chc_deger_CheckedChanged"  />

        </div>
        <div class="row">
             <div class="col-md-12 col-sm-12">
                <asp:Panel ID="panel_tarih"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-md-2 col-sm-12">
                             <label for="">Yıl Seç</label>
                            <asp:DropDownList ID="comrapor_yil_tarih"   AutoPostBack="true" CssClass="form-control form-control-sm" runat="server"  OnSelectedIndexChanged="comrapor_yil_tarih_SelectedIndexChanged"        > </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                             <label for="">Ay Seç</label>
                            <asp:DropDownList ID="comrapor_ay_tarih"    AutoPostBack="true" CssClass="form-control form-control-sm" runat="server"  OnSelectedIndexChanged="comrapor_ay_tarih_SelectedIndexChanged"       > </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                             <label for="">Gün Seç</label>
                            <asp:DropDownList ID="comrapor_gun_tarih"   AutoPostBack="true" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="comrapor_gun_tarih_SelectedIndexChanged"      > </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                             <label for="">Rapor Seç</label>
                            <asp:DropDownList ID="comrapor_raporsec_tarih"  AutoPostBack="true" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="comrapor_raporsec_tarih_SelectedIndexChanged"         > </asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="row">
                <div class="col-md-12 col-sm-12">

                    <asp:Panel ID="panel_port"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-2 col-sm-12">
                          <label for="">US Port</label>
                         <asp:DropDownList ID="comusport_port"    AutoPostBack="true" CssClass="form-control form-control-sm" runat="server"    >   </asp:DropDownList>
                                </div>
                            <div class="col-md-2 col-sm-12">
                         <label for="">US Port</label>
                            <asp:TextBox ID="txt_usport_port" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12">
                         <label for="">DS Port</label>
                            <asp:TextBox ID="txt_dsport_port" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panel_devreno"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                         <label for="">Devre No</label>
                            <asp:TextBox ID="txt_devreno" CssClass="form-control" runat="server"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel ID="panel_deger"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-2 col-sm-12">
                             <label for="">US Pwr</label>
                                <asp:TextBox ID="txt_uspwr" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-md-2 col-sm-12">
                             <label for="">US SNR</label>
                                <asp:TextBox ID="txt_ussnr" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-md-2 col-sm-12">
                             <label for="">DS Pwr</label>
                                <asp:TextBox ID="txt_dspwr" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>

                            <div class="col-md-2 col-sm-12">
                             <label for="">DS SNR</label>
                                <asp:TextBox ID="txt_dssnr" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panel_mac"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                         <label for="">Mac Adres*</label>
                            <asp:TextBox ID="txt_mac" CssClass="form-control" runat="server"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel ID="panel_2"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">

                    </asp:Panel>
                    <asp:Panel ID="panel_cmts"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
                         <label for="">CMTS*</label>
                         <asp:DropDownList ID="comrapor_cmts"    AutoPostBack="true" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="comrapor_cmts_SelectedIndexChanged"    >   </asp:DropDownList>
                    </asp:Panel>
                    <asp:Panel ID="panel_raporlama"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">

                         <label for="">Raporlama Seç</label>
                         <asp:DropDownList ID="comraporlama_sec"    AutoPostBack="true" CssClass="form-control form-control-sm" runat="server" Width="600px" OnSelectedIndexChanged="comraporlama_sec_SelectedIndexChanged"       >   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="comraporlama_sec"  ValidationGroup="raporlama"  runat="server" Display="Dynamic"  CssClass="field-validation-error lb-md" Text="Raporlama türünü seçiniz." ForeColor="red"  />

                    </asp:Panel>
                </div>

        </div>


<asp:Panel ID="panel2"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
        <div class="row">





        </div>
</asp:Panel>


<asp:Panel ID="panel3"  CssClass="panel panel-primary" DefaultButton="" runat="server" Visible="false">
        <div class="row">





        </div>
</asp:Panel>


        <div class="row">
              <div class="col-md-2 col-sm-12 text-center">
                <br />
                  <asp:Button ID="test" runat="server"  CssClass="btn  btn-primary btn-sm" Width="130px"  Text="test" OnClick="test_Click"    />
                  <asp:Button ID="btnara" runat="server" ValidationGroup="raporlama"  CssClass="btn  btn-primary btn-sm" Width="130px"  Text="Göster" OnClick="btnara_Click"  />
                    <asp:Label ID="lblaciklama"        Text="ghytthg" runat="server" />

              </div>
        </div>
                     <asp:Label ID="lblsqlsilineceklbl" runat="server" Visible="false" Text=""></asp:Label>



        <br />
        <br />
   <div class="row">
        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" >
                <Columns>
                </Columns>
            </asp:GridView>
        </div>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
   </div>

        </div>
</ContentTemplate>
</asp:UpdatePanel>

      <div id="YeniMesajDiv">Some text some message..</div>

</asp:Content>
