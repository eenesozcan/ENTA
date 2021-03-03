<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HaritaAnaliz.aspx.cs" Inherits="bim.Haritalar.HaritaAnaliz" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Scripts/jquery-2.0.3.js"></script>
    <script src="../Scripts/jquery-2.0.3.min.js"></script>


	<meta http-equiv="content-type" content="text/html; charset=UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">

	<!-- Leaflet (JS/CSS) -->
	<link rel="stylesheet" href="https://unpkg.com/leaflet@1.3.4/dist/leaflet.css">
	<script src="https://unpkg.com/leaflet@1.3.4/dist/leaflet.js"></script>

	<!-- JSZIP -->
	<script src="https://unpkg.com/jszip@3.1.5/dist/jszip.min.js"></script>

	<!-- @tmcw/togeojson -->
	<script src="https://unpkg.com/@tmcw/togeojson@3.0.1/dist/togeojsons.min.js"></script>

	<!-- geojson-vt -->
	<script src="https://unpkg.com/geojson-vt@3.0.0/geojson-vt.js"></script>

	<!-- Leaflet-KMZ -->
	<script src="https://unpkg.com/leaflet-kmz@0.2.0/dist/leaflet-kmz.js"></script>
	<script src="layer/tile/Yandex.js"></script>
	<script src="http://api-maps.yandex.ru/2.0/?load=package.map&lang=tr-TR" type="text/javascript"></script>










	<style>
		
		.map {
			
			width: 98%;
			height: 80%;
            position: absolute;
            margin-bottom:20px;
		}
	</style>

    


    
  <div class="row align-middle text-left searchBg p-20 mb-2  pt-3 border " >
    
          <div class="col-md-4 col-xl-2">
          <div class="form-group">
              <label for="">İlçe</label>
              <asp:DropDownList ID="comilce" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
                  <asp:ListItem Value="0">Hepsi</asp:ListItem>
                  <asp:ListItem >Altıeylül</asp:ListItem>
                  <asp:ListItem>Karesi</asp:ListItem>
                  <asp:ListItem>Bandırma</asp:ListItem>
                  </asp:DropDownList>
             </div>

       </div>
          <div class="col-md-4 col-xl-2">
              <div class="form-group">
                <label for="">Proje Tipi</label>
                  <asp:DropDownList ID="comproje" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
                            <asp:ListItem Value="0">Hepsi</asp:ListItem>
                            <asp:ListItem>Yeni Şebeke</asp:ListItem>
                            <asp:ListItem>Revizyon</asp:ListItem>
                            <asp:ListItem>Güzergah Yapımı</asp:ListItem>
                            <asp:ListItem>Deplase</asp:ListItem>
                            <asp:ListItem>Hasar</asp:ListItem>
                            <asp:ListItem>Kapasite ve Performans İyileştirme</asp:ListItem>

                  </asp:DropDownList>
               </div>
          </div>
          <div class="col-md-4 col-xl-2">
              
                  <asp:Label ID="lblmesaj" CssClass="label2" runat="server" Text=""></asp:Label>
              
          </div>
          <div class="col-md-4 col-xl-2">

          </div>
          <div class="col-md-3 col-xl-1">

          </div>
          <div class="col-md-5 col-xl-3">
                <label for=""></label>
              <div class="input-group">
                  <span class="input-group-btn ml-20">
                  <asp:Button ID="btnara" runat="server"  CssClass="btn  btn-primary " Text="Haritada Göster" OnClientClick="return  Filtre();"  />
                      </span>
                  <span class="input-group-btn ml-20">
                    
                      </span>
               </div>
          </div>
        
               

  </div>


    <div id="kmz_map">
            <div id="map" class="map"></div>
    </div>
    

	<script>

        //var map_bim = [];
        var map_filtre = "";
        var map_data = [];

        function harita_goster() {
            var map = L.map('map');
	        map.setView(new L.LatLng(39.64, 27.64), 8);
	        var yndx = new L.Yandex("satellite");
	        var OpenTopoMap = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
	        maxZoom: 17,
	        attribution: 'Map data: &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>, <a href="http://viewfinderpanoramas.org">SRTM</a> | Map style: &copy; <a href="https://opentopomap.org">OpenTopoMap</a> (<a href="https://creativecommons.org/licenses/by-sa/3.0/">CC-BY-SA</a>)',
	        opacity: 0.90
            });

	        OpenTopoMap.addTo(map);
            map.locate({ setView: true, maxZoom: 20 });

		    // Instantiate KMZ parser (async)
		    var kmzParser = new L.KMZParser({
			    onKMZLoaded: function(layer, name) {
				    control.addOverlay(layer, name);
				    layer.addTo(map);
			    }
		    });
		    // Add remote KMZ files as layers (NB if they are 3rd-party servers, they MUST have CORS enabled)
		    //kmzParser.load('tt.kmz');
		    //kmzParser.load('tt2.kmz');
		    //kmzParser.load('http://bim.hasansavun.com.tr/Download/indir?id=28bc2e87-7aa2-4952-9a82-4cf551459ba7');
            //kmzParser.load('http://bim.hasansavun.com.tr/Download/indir?id=d87de649-624c-4f43-8223-f6e87ff54500');

            for (var i = 0; i < map_data.length; i++) {

                kmzParser.load(map_data[i]);
                //alert(map_data[i]);
                
            }

            var control = L.control.layers({ "Harita": OpenTopoMap, "Uydu Yandex": yndx }).addTo(map);
        }

        function GetDataMap() {
            //alert("getdatamap > map_filtre :"+map_filtre);
            var sonuc = [];
            var sonucstr = "";
            $.ajax({
                type: "POST",
                url: "HaritaData/istek.ashx?istek=kmz&filtre="+map_filtre,
                //contentType: "application/json; charset=utf-8",
               //data: Text,
                dataType: "text",
                crossDomain:true,
                error: function (jqXHR, sStatus, sErrorThrown) {
                    alert( 'data:  ' + sErrorThrown);
                    alert( 'Get Data Error:  ' + sStatus+" - "+jqXHR.status);
                },
                success: function (data) {
                    sonucstr = data;
                    var str1 = "";
                    //alert("sonucc str : " + sonucstr);
                    for (var i = 0; i < sonucstr.length; i++) {

                        if (sonucstr[i] != ",") {
                            str1 = str1 + sonucstr[i];
                        }
                        else {
                            str1 = "http://bim.hasansavun.com.tr/Download/indir?id=" + str1;
                            //alert("map for "+i+" : "+str1);
                            sonuc.push(str1);
                            //kmzParser.load(str1);
                            str1 = "";
                        }
                    }

                //alert("ajax sucsess data: "+data);
                    //alert("ajax sucsess sonuc: " + sonuc);
                    document.getElementById("MainContent_lblmesaj").innerHTML = sonuc.length+" adet kayıt bulunmuştur.";
                    map_data = sonuc;
                    harita_goster();
                }
            });
            //alert(sonuc.length);
            //document.getElementById("MainContent_lblmesaj").innerHTML = sonuc;
            
        }

        function Filtre() {
            //var dropdownListId = document.getElementById("comilce"); 
            var e = document.getElementById("MainContent_comilce"); 
            var ilce = e.options[e.selectedIndex].value;

            var e2 = document.getElementById("MainContent_comproje"); 
            var proje = e2.options[e2.selectedIndex].value;

            var filter = "";

            if (ilce != "0")
                filter = " AND ILCE='" + ilce+"' ";

            if (proje != "0")
                filter += " AND PROJE_TIPI='" + proje+"' ";

            map_filtre = filter.replace(/ /g, '*');

            //alert(filter);

            
            document.getElementById('kmz_map').innerHTML = "<div id='map' class='map'></div>";

            GetDataMap();

            return false;
        }

        

        
	</script>


    </asp:Content>
