<%@ Page Title="Ana Sayfa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnvanterTveY._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
    <center>     <h1>  <asp:Label ID="lblprogramadi" runat="server" Text="Label"></asp:Label> </h1></center>
    </div>

 <div class="row">
  <div class="col-md-4">
    <div class="thumbnail">
      <a href="/images/s1.jpg">
        <img src="/images/s1.jpg" alt="Lights" style="width:100%">
        <div class="caption">

        </div>
      </a>
    </div>
  </div>
  <div class="col-md-4">
    <div class="thumbnail">
      <a href="/images/s2.jpg">
        <img src="/images/s2.jpg" alt="Nature" style="width:100%">
        <div class="caption">

        </div>
      </a>
    </div>
  </div>
  <div class="col-md-4">
    <div class="thumbnail">
      <a href="/images/s3.jpg">
        <img src="/images/s3.jpg" alt="Fjords" style="width:100%">
        <div class="caption">

        </div>
      </a>
    </div>
  </div>
</div>

                <div class="row">
                <div class="col-md-12">
      
                </div>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/s1.jpg" Width="100%" Height="100%" CssClass="img-evler" />
            </div>

</asp:Content>
