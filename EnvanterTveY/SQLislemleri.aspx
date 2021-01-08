<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SQLislemleri.aspx.cs" Inherits="EnvanterTveY.SQLislemleri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="Scripts/KabloHE-js.js"></script>
    
  

     <div class="container-fluid" style="margin-top:30px">

          
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

      <div class="row  " >
    
      <div class="col-md-3 col-xl-3 col-sm-9 text-left   ">
         <div class="row align-middle searchBg border m-0 p-2 mb-1  " >
          <div class="form-group">
                <label for="">Tablo Seç</label>
            
                <asp:DropDownList ID="comtable" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"  
                    DataSourceID="SqlDataSource1" DataTextField="NAME" DataValueField="NAME" >
                    </asp:DropDownList>
                <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary mt-1 " Text="Tabloyu Yükle" OnClick="btnara_Click"   />
         
             </div>
        </div>
       </div>
          <div class="col-md-4 col-xl-4  ">
             <div class="row border m-0 p-2 mb-1  " >

                  <div class="form-group Width=847px" >
                      <label for="">SQL</label>
                         <asp:TextBox ID="TextBox1" runat="server" Width="400px"   CssClass="form-control wd-400" 
                    TextMode="MultiLine" Height="75px" ></asp:TextBox>

                      <br />
                        <asp:Button ID="Button11" runat="server" Text="Tabloya Yükle" CssClass="btn  btn-primary" 
                     OnClick="Button11_Click"  />
                        <asp:Button ID="Button3" runat="server" Text="Çalıştır" CssClass="btn  btn-primary" 
                     OnClick="Button3_Click"/>

             </div>

              </div>
              
          </div>

           <div class="col-md-3 col-xl-3   ">
             <div class="row align-middle searchBg border m-0 p-2 mb-1  " >

                 <div class="form-group">
                     <asp:FileUpload ID="FileUpload1" runat="server"  CssClass="btn btn-info" Width="300px" Enabled="true"/>
                     <asp:Button ID="Button13" runat="server" Text="Excel Yükle"  CssClass="btn  btn-info" OnClick="Button13_Click" style="margin: 2px;"   />
                     <br />
                     <asp:Button ID="Button14" runat="server" Text="Komut Çalıştır" CssClass="btn  btn-danger" OnClick="Button14_Click" style="margin: 2px;"  />
                       <br />
                     özel komut insert into dosyalar (bordro_id,dosya_id,dosya_adi) values 
        (&#39;@deger1&#39;,&#39;12&#39;,&#39;imzalı_bordro&#39;)
        <br />
                </div>
              </div>
              
          </div>


        
               

  </div>

     <div class="row align-middle  " >
         <div class="col-md-3 col-xl-3 col-sm-9 text-left   ">
             <div class="row align-middle searchBg border m-0 p-2 mb-1  " >

              </div>
          </div>

         <div class="col-md-3 col-xl-3 col-sm-9 text-left   ">
             <div class="row align-middle searchBg border m-0 p-2 mb-1  " >

              </div>
          </div>

         <div class="col-md-3 col-xl-3 col-sm-9 text-left   ">
             <div class="row align-middle searchBg border m-0 p-2 mb-1  " >

              </div>
          </div>

         <div class="col-md-3 col-xl-3 col-sm-9 text-left   ">
             <div class="row align-middle searchBg border m-0 p-2 mb-1  " >

              </div>
          </div>
            

     </div>

   
      <div class="row">
          <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
          <asp:Label ID="lbldosyadi" runat="server" CssClass="label1" Visible="False"></asp:Label>

        </div>
      <div class="row">
    <div class="table-responsive">

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" CssClass="table table-striped table-bordered table-condensed table-hover"  ShowHeaderWhenEmpty="True" EmptyDataText="Kayıt bulunamadı" >
            <Columns>

               
            </Columns>
        </asp:GridView>
  
        
        </div>
    
          
  </div>


          <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=ENESOZCANN\SQLEXPRESS;Initial Catalog=KabloHE;Persist Security Info=True;User ID=enes;Password=esk.2626" ProviderName="System.Data.SqlClient" 
            ></asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="Data Source=ENESOZCANN\SQLEXPRESS;Initial Catalog=ENTA;Persist Security Info=True;User ID=enes;Password=esk.2626" 
             
            ></asp:SqlDataSource>
            <br />
            <br />
            <br />
               <br />
            <br />
            <br />

                   <div class="col-12 text-left   ">

              
          </div>
        
</ContentTemplate>
        <Triggers>
           
            <asp:PostBackTrigger ControlID="Button13" />
           
        </Triggers>
</asp:UpdatePanel>

</div>
                                                           <div id="YeniMesajDiv">Some text some message..</div>
</asp:Content>
