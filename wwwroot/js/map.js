require(["esri/config", "esri/Map", "esri/views/MapView", "esri/layers/FeatureLayer", "esri/layers/WMSLayer"],
    function (esriConfig, Map, MapView, FeatureLayer, WMSLayer) {
        esriConfig.apiKey = "";
        const wmsUrl = "http://127.0.0.1/qgisserver/cgi_bin/qgis_mapserv.fcgi?MAP=/home/yassine/Desktop/casa.qgs&SERVICE=WMS&REQUEST=GetCapabilities";
        const wmsLayer = new WMSLayer({
            url: wmsUrl,
            sublayers: [
                { name: "Communes" },
                { name: "voirie casa" },
                { name: "CasaPop" },
                { name: "Site" }
            ]
        });
        const map = new Map({
            basemap: "streets-vector",
            layers: [wmsLayer]
        });
        const view = new MapView({
            map: map,
            center: [-7.62, 33.59], // Longitude, latitude
            zoom: 9, // Zoom level
            container: "mapDiv"
        });
    });