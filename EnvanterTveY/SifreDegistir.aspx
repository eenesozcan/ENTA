<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SifreDegistir.aspx.cs" Inherits="EnvanterTveY.SifreDegistir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Content/Site.css" rel="stylesheet" />
        <script src="Scripts/KabloHE-js.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    
    <div class="container-fluid" style="margin-top:20px">
            <div class="row text-center" >  <span class="baslik">Şifre Değiştirme</span>  </div>
        <div class="row"> 
                            <div class="col-md-4 col-xs-12">
                            </div>

                <!--Grid column-->
        <div class="col-lg-4 col-md-6 mb-4">

          <div class="card">
            <div class="card-header">
                                  <span>Şifre Değiştirme</span>

            </div>
            <div class="card-body">

                            <div class="panel-body ">
                                <div class="form-group mb-5i">
                                     <tr> 
                                        <td>    <label for="">Mevcut Şifre</label></td>
                                        <td>    <asp:TextBox ID="txteskisifree" placeholder="Mevcut Şifre" type="password" ValidationGroup="sifreyenile"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                                     </tr>
                                     <tr> 
                                        <td>    <label for="">Yeni Şifre</label></td>
                                        <td>    <asp:TextBox ID="txtyenisifre1" placeholder="Yeni Şifre" type="password" ValidationGroup="sifreyenile"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                                     </tr>
                                     <tr> 
                                        <td>    <label for="">Yeni Şifre Tekrar</label></td>
                                        <td>    <asp:TextBox ID="txtyenisifre2" placeholder="Yeni Şifre Tekrar" type="password" ValidationGroup="sifreyenile"  CssClass="form-control" runat="server" style="margin: 2px;"></asp:TextBox></td>
                                     </tr>
                                     <br />
                                     <br />
                                        <asp:Button ID="btnsifreyenile" runat="server" ValidationGroup="sifreyenile" CssClass="btn btn-success" Text="Değiştir" OnClick="btnsifreyenile_Click" />
                                             
                                            <tr style="  width:100%;">
                                                <td class="style1" colspan="2">
                                                     <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&.,])[A-Za-z\d$@$!%*?&.,]{8,16}" 
                                                        ErrorMessage="Şifreniz en az 8 karakter, bir rakam, bir büyük harf, bir küçük harf ve 1 özel karakter ($@$!%*?&.,) içermelidir." 
                                                        ControlToValidate="txtyenisifre1"  CssClass="text text-danger" 
                                                        ValidationGroup="sifreyenile"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                      <asp:Label ID="lbldurum" runat="server" CssClass="label_durum_hata"></asp:Label>
                                </div>
                            </div>
            </div>
          </div>

        </div>
        <!--Grid column-->
                            <div class="col-md-3 col-xs-12">
                            </div>
         </div>
    </div>
                        </ContentTemplate>
        </asp:UpdatePanel>

      <div id="YeniMesajDiv">Some text some message..</div>
    <div class="loading" align="center">
<br />
Yükleniyor. Lütfen bekleyiniz.<br />
<br />
<div class="ball"></div>
<div class="ball1"></div>
</div>  

</asp:Content>
