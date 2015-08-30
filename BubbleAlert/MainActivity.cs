using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using System.Collections.Generic;

namespace BubbleAlert
{
	[Activity (Label = "BubbleAlert", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILocationListener
	{
		
		//Location stuffs:
		//*All lengths are in meters and time in milliseconds*

		private static long MIN_DISTANCE_UPDATE = 1;
		private static long MIN_TIME_UPDATE = 1000; 
		private static long BUBBLE_ALERT_EXPIRE = -1; //Never expire;
		private static String LATITUDE_KEY = "LATITUDE_KEY";
		private static String LONGITUDE_KEY = "LONGITUDE_KEY";

		private static String PROX_ALERT_INTENT = "Bubble_Alert";

	
		LocationManager locationManager;
		String locationProvider;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);



			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			SetContentView(Resource.Layout.LocationsLayout);

		
			//Create tabs for the interface:
			ActionBar.Tab tab1 = ActionBar.NewTab();
			tab1.SetText (Resources.GetString (Resource.String.tab_1));
			tab1.TabSelected += (sender, args) => {
				SetContentView(Resource.Layout.LocationsLayout);
				//TextView textLocation =  FindViewById<TextView> (Resource.Id.textAddress);
				//textLocation.Text = "You are NOWHERE!? \n YOU DON'T EXIST!! \n HEATHEN!!! \n WITCH I TELL YOU, WITCH!";

				System.Console.WriteLine ("Tab 1 is ticklish!");
			};

			ActionBar.AddTab (tab1);

			//ActionBar.Tab tab2 = ActionBar.NewTab();
			//tab2.SetText (Resources.GetString (Resource.String.tab_2));
			//tab2.TabSelected += (sender, args) => {
				
			//};

			//ActionBar.AddTab (tab2);

			ActionBar.Tab tab3 = ActionBar.NewTab();
			tab3.SetText (Resources.GetString (Resource.String.tab_3));
			tab3.TabSelected += (sender, args) => {
				//Set the view to the about layout:
				SetContentView(Resource.Layout.AboutLayout);
			};

			ActionBar.AddTab (tab3);

			//Begin button:
			Button beginButton = FindViewById<Button>(Resource.Id.buttonBegin);
			beginButton.Click += delegate(object sender, EventArgs e) {
				

				TextView location1 = FindViewById<TextView>(Resource.Id.textLocation1);
				TextView location2 = FindViewById<TextView>(Resource.Id.textLocation2);
				TextView location3 = FindViewById<TextView>(Resource.Id.textLocation3);
				TextView location4 = FindViewById<TextView>(Resource.Id.textLocation4);

				//System.Console.Out.WriteLine("<<<<<<<>>>>>>" + location1.Text);
				//System.Console.Out.WriteLine("<<<<<<<>>>>>>" + location2.Text);
				//System.Console.Out.WriteLine("<<<<<<<>>>>>>" + location3.Text);
				//System.Console.Out.WriteLine("<<<<<<<>>>>>>" + location4.Text);

				geocodeAddress(location1.Text);


				geocodeAddress(location2.Text);


				geocodeAddress(location3.Text);


				geocodeAddress(location4.Text);

			};

		
			//Start location:
			InitLocationManager();





		}

		//Start the location manager:
		public void InitLocationManager() {
			locationManager = (LocationManager)GetSystemService (LocationService);
			Criteria criteriaForLocationService = new Criteria {
				Accuracy = Accuracy.Fine
			};

			locationProvider = locationManager.GetBestProvider (criteriaForLocationService, true);
			locationManager.GetBestProvider (criteriaForLocationService, true);

		}

		//Location stuffs:
		public void OnLocationChanged(Location location) 
		{
			
			if (location != null) {
				//There is a location. Do something with it.
				//reverseGeocode(location.Latitude,location.Longitude);

			}
		}

		protected override void OnResume() {
			base.OnResume();
			locationManager.RequestLocationUpdates(locationProvider,0,0,this);
		}

		public void OnStatusChanged(String Provider, Availability status, Bundle extras)
		{

		}

		public void OnProviderEnabled(string provider) {

		}

		public void OnProviderDisabled(String provider) {
			//TextView textLocation =  FindViewById<TextView> (Resource.Id.textAddress);
			//textLocation.Text = "You are NOWHERE!? \n YOU DON'T EXIST!! \n HEATHEN!!! \n WITCH I TELL YOU, WITCH!";
		}

		private async void reverseGeocode(double lat, double longi) {
			String results = "";

			var geo = new Geocoder (this);
			var addresses = await geo.GetFromLocationAsync (lat, longi, 1);



			if (addresses != null) {
				int c = 0;

				foreach (var temp in addresses) {
					c++;
					results = results + temp;
				}
				//TextView textLocation = FindViewById<TextView> (Resource.Id.textAddress);
				//textLocation.Text = textLocation.Text = results;
			}

		}

			private async void geocodeAddress(String a) {
			

				var g = new Geocoder (this);
				var addresses = await g.GetFromLocationNameAsync (a, 1);


				double lat = 0;
				double longi = 0;


				
					foreach (var temp in addresses) {
						lat = temp.Latitude;
						longi = temp.Longitude;
					}
					
			addProximityAlert (lat, longi, a);

		}

		//Add proximity alerts:

		 public void addProximityAlert( double latitude, double longitude, String l) {
			Intent intent = new Intent (l);
			PendingIntent proximityIntent = PendingIntent.GetBroadcast (this, 0, intent, 0);
			locationManager.AddProximityAlert (latitude, longitude, 403, -1, proximityIntent);
			IntentFilter filter = new IntentFilter (l);
			RegisterReceiver (new ProximityIntentReceiver (this,l), filter);
		}

		private void notifyUser(String title, String message) {
			Notification.Builder builder = new Notification.Builder (this)
				.SetContentTitle (title)
				.SetContentText (message);
			builder.SetSmallIcon (Resource.Drawable.green_button);
			builder.SetSound (Android.Media.RingtoneManager.GetDefaultUri (Android.Media.RingtoneType.Notification));
			Notification notification = builder.Build();

			NotificationManager notificationManager = GetSystemService (Context.NotificationService) as NotificationManager;
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);
		}
	}
}



