using Android.App;
using Android.Widget;
using Android.OS;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Mapboxsdk;
using Com.Mapbox.Mapboxsdk.Annotations;
using Com.Mapbox.Mapboxsdk.Geometry;
using MapboxAccountManager = Com.Mapbox.Mapboxsdk.Mapbox;
using Com.Mapbox.Mapboxsdk.Camera;
using AndroidX.AppCompat.App;
using Com.Mapbox.Mapboxsdk.Plugins.Annotation;
using Java.Lang;
using Xamarin.Essentials;
using System;

namespace XNMapboxDemo.Droid
{
    [Activity(Label = "XNMapboxDemo", MainLauncher = true, Icon = "@mipmap/icon" , Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback , Style.IOnStyleLoaded , IOnSymbolClickListener
    {
        MapView mapView;
        MapboxMap mapbox;
        Location currentLocation;
        Button btnLoc;
        TextView txtLoc;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            //Mapbox.GetInstance(this, Resources.GetString(Resource.String.mapbox_token));
            MapboxAccountManager.GetInstance(this, Resources.GetString(Resource.String.mapbox_token));
            SetContentView(Resource.Layout.Main);



            btnLoc = FindViewById<Button>(Resource.Id.btngetLoc);
            txtLoc = FindViewById<TextView>(Resource.Id.txtLatLong);
            btnLoc.Click += BtnLoc_Click;
            mapView = FindViewById<MapView>(Resource.Id.mapView);
            
            mapView.Touch += MapView_Touch;
            mapView.OnCreate(savedInstanceState);
            mapView.GetMapAsync(this);

        }

        private async void BtnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                currentLocation = await Geolocation.GetLastKnownLocationAsync();

                if (currentLocation != null)
                {
                    txtLoc.Text = $"Latitude={currentLocation.Latitude} , Longitude={currentLocation.Longitude} ";
                    Console.WriteLine($"Latitude: {currentLocation.Latitude}, Longitude: {currentLocation.Longitude}, Altitude: {currentLocation.Altitude}");

                    //mapView.GetMapAsync(this);


                    Com.Mapbox.Geojson.Point point = Com.Mapbox.Geojson.Point.FromLngLat(currentLocation.Longitude, currentLocation.Latitude);
                    mapbox.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(point.Latitude(), point.Longitude()), 15));

                    var options = new SymbolOptions();
                    options.WithIconImage("airport-15");
                    options.WithGeometry(point);
                    options.WithIconSize(new Float(2f))
                        .WithDraggable(true);

                    var symbol = symbolManager.Create(options);
                }
            }
            catch
            {

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void MapView_Touch(object sender, Android.Views.View.TouchEventArgs e)
        {
            var toast = Toast.MakeText(this, "Map view touched", ToastLength.Short);
            toast.Show();
        }

        protected override void OnStart()
        {
            base.OnStart();
            mapView.OnStart();
        }
        protected override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }
        protected override void OnPause()
        {
            mapView.OnPause();
            base.OnPause();
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView.OnSaveInstanceState(outState);
        }
        protected override void OnStop()
        {
            base.OnStop();
            mapView.OnStop();
        }
        protected override void OnDestroy()
        {
            mapView.OnDestroy();
            base.OnDestroy();
        }
        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }

        public void OnMapReady(MapboxMap p0)
        {
            mapbox = p0;
            //System.Console.Write(p0);
            //mapbox.SetMinZoomPreference(11);

            //mapbox.SetStyle(Style.MAPBOX_STREETS ,this);




            mapbox.SetStyle(new Style.Builder().FromUri("mapbox://styles/abhishekswaraj23/ckcahuo1c44tz1ipjl72myvgz"),this);// mapbox://styles/abhishekswaraj23/ckca5b9ol1xyl1inypmizjc8g"),this);//"mapbox://styles/abhishekswaraj23/ckca5b9ol1xyl1inypmizjc8g"));

            //MarkerOptions marker = new MarkerOptions();
            //marker.SetPosition(new LatLng(21.0276, 105.8355));
            //marker.SetTitle("Xin Chao");
            //marker.SetSnippet("Well come to ViewNam");

            //mapbox.AddMarker(marker);

            mapbox.MarkerClick += Mapbox_MarkerClick;
            mapbox.CameraMove += Mapbox_CameraMove;
            mapbox.InfoWindowClick += Mapbox_InfoWindowClick;
        }

        private void Mapbox_InfoWindowClick(object sender, MapboxMap.InfoWindowClickEventArgs e)
        {
            var toast = Toast.MakeText(this, "Map view Mapbox_InfoWindowClick", ToastLength.Short);
            toast.Show();
        }

        private void Mapbox_CameraMove(object sender, System.EventArgs e)
        {
            var toast = Toast.MakeText(this, "Map view Mapbox_CameraMove", ToastLength.Short);
            toast.Show();
        }

        private void Mapbox_MarkerClick(object sender, MapboxMap.MarkerClickEventArgs e)
        {
            var toast = Toast.MakeText(this, "Map view Mapbox_MarkerClick", ToastLength.Short);
            toast.Show();
        }


        SymbolManager symbolManager;
        public void OnStyleLoaded(Style p0)
        {
            symbolManager = new SymbolManager(mapView, mapbox, p0);
            Com.Mapbox.Geojson.Point point = Com.Mapbox.Geojson.Point.FromLngLat(105.505, 21.033);
            mapbox.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(point.Latitude(), point.Longitude()), 8));

            var options = new SymbolOptions();
            options.WithIconImage("fire-station-11");
            options.WithGeometry(point);
            options.WithIconSize(new Float(2f))
                .WithDraggable(true);

            var symbol = symbolManager.Create(options);

            Com.Mapbox.Geojson.Point point2 = Com.Mapbox.Geojson.Point.FromLngLat(25.8672299, 85.1720807);
            var options2 = new SymbolOptions()
                .WithIconImage("fire-station-11")
                .WithGeometry(point2)
                .WithIconSize(new Float(2f))
                .WithDraggable(true);

            var symbol2 = symbolManager.Create(options2);

            symbolManager.AddClickListener(this);
        }

        public void OnAnnotationClick(Symbol symbol)
        {
            //throw new System.NotImplementedException();
        }
    }
}

