<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuYonetimi.aspx.cs" Inherits="EnvanterTveY.MenuYonetimi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
     <script type="text/javascript">

        function ShowMessage(message, messagetype,yer) {
           
            var cssclass;
            var yer;
            switch (messagetype) {
                case 'Tamam':
                    cssclass = 'alert-success'
                    break;
                case 'Hata':
                    cssclass = 'alert-danger'
                    break;
                case 'Sil':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
           
            $('#alert_container_'+yer).append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 0px 0px 2px #999;" class="alert  ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong></strong> <span>' + message + '</span></div>');
        }

       
    </script>
    
    <div class="container-fluid" style="margin-left:30px">

    <div class="row">
        <span class="baslik">Kullanıcı Yönetimi</span>
    </div>
    <div class="row">

        <div class="col-md-4 col-xs-12  ">
            <asp:TreeView ID="TreeView1" runat="server" 
            onselectednodechanged="TreeView1_SelectedNodeChanged" ShowLines="True" 
            Font-Size="10pt">
        </asp:TreeView>

            </div>
          <div class="col-md-8 col-xs-12 ">

                 <div class="messagealert mt-3 mb-5" id="alert_container_menu">
                   </div>

              <div class="row">
                  <div class="col-md-12" >
                      <div class="form-group">
                           <label for="usr">İşlem:</label>
                           <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                       </div>
                  </div>
                   <div class="col-md-12" >
                      <div class="form-group">
                           <label for="usr">Menü İsmi:</label>
                              <asp:TextBox ID="txtmenuismi" CssClass="form-control" runat="server"></asp:TextBox>
                       </div>
                  </div>
                  <div class="col-md-12" >
                      <div class="form-group">
                           <label for="usr">Link:</label>
                              <asp:TextBox ID="txtlink" CssClass="form-control" runat="server"></asp:TextBox>
                       </div>
                  </div>
                  <div class="col-md-12" >
                      <div class="form-group">
                           <label for="usr">Ana Menü ID:</label>
                                <asp:DropDownList ID="comanamenu" runat="server"  CssClass="form-control mb-2 mr-sm-2 mb-sm-0" 
                                DataSourceID="SqlDataSource1" DataTextField="MENU_ISMI" DataValueField="ID" 
                                Width="150px">
                                </asp:DropDownList>
                       </div>
                  </div>  
                  <div class="col-md-12" >
                      <div class="form-group">
                           <label for="usr">Sıra:</label>
                              <asp:TextBox ID="txtsira" CssClass="form-control" runat="server"></asp:TextBox>
                       </div>
                  </div>    
                    <div class="col-md-12" >
                      <div class="form-group">
                           <label for="usr">SVG:</label>
                              <asp:TextBox ID="txtsvg" CssClass="form-control" Height="100" Width="300" runat="server"></asp:TextBox>
                       </div>
                  </div>    
                  
                  <div class="col-md-12" >
                      <div class="form-group">
                           <label for="usr">Bakıma Al:</label>
                               <asp:CheckBox ID="chcbakim" runat="server" CssClass="label" Text="Bakımda" />
                       </div>
                  </div>
                  <div class="col-md-12" >
                      <div class="">
                             
                                 <asp:Button ID="btnekle" runat="server"  CssClass="btn  btn-success " Text="Kaydet" OnClick="btnekle_Click"   />
                                <asp:Button ID="btniptal" runat="server" Text="İptal"  CssClass="btn btn-warning" OnClick="btniptal_Click"  />
                                <asp:Button ID="btnsil" runat="server" Text="Sil"  CssClass="btn btn-danger" OnClick="btnsil_Click"  />
                       </div>
                  </div>
               

              </div>

            </div>

        </div>

        </div>
    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:KAbloHE %>" 
                        ProviderName="<%$ ConnectionStrings:KabloHE.ProviderName %>" 
                       >
                    </asp:SqlDataSource>

</asp:Content>
