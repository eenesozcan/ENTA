<%@ Page Title="Ana Sayfa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnvanterTveY._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!--
    <div class="jumbotron">
    <center>     <h1>  <asp:Label ID="lblprogramadi" runat="server" Text="Label"></asp:Label> </h1></center>
    </div>
    -->
    <br />
    <br />
    <br />
    <style>
.img_default { 
  background-image: url('/images/arka1.png') ; 
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-position: top; 

 
  -webkit-background-size: cover;
  -moz-background-size: cover;
  -o-background-size: cover;
  background-size: auto;
  min-height: 100%;
  min-width: 1024px;
	
  /* Set up proportionate scaling */
  width: 100%;
  height: auto;
	padding:200px;
  /* Set up positioning */
  position: relative;
  top: 0;
  left: 0;

}


@media screen and (max-width: 1024px) { /* Specific to this particular image */
  img_default {
    left: 50%;
    margin-left: -512px;   /* 50% */
    bottom:0px;
  }
}

</style>

 
    <div class="img_default">
     
    </div>


<!--
  <div class="col-md-4">
    <div class="thumbnail">
      <a href="/images/bb.jpg">
        <img src="/images/bb.jpg" alt="Nature" style="width:100%">
        <div class="caption">

        </div>
      </a>
    </div>
  </div>
  <div class="col-md-4">
    <div class="thumbnail">
      <a href="/images/cc.jpg">
        <img src="/images/cc.jpg" alt="Fjords" style="width:100%">
        <div class="caption">

        </div>
      </a>
    </div>
  </div>
</div>

                <div class="row">
                <div class="col-md-12">
      
                </div>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/dd.jpg" Width="100%" Height="100%" CssClass="img-evler" />
            </div>
    -->
</asp:Content>
