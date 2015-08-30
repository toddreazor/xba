
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;

namespace BubbleAlert
{
	[Activity (Label = "MainInterfaceActivity")]			
	public class MainInterfaceActivity : Activity //, ILocationListener
	{
		//Location stuffs:
		//*All lengths are in meters and time in milliseconds*

		private static long MIN_DISTANCE_UPDATE = 1;
		private static long MIN_TIME_UPDATE = 1000; 
		private static long BUBBLE_ALERT_EXPIRE = -1; //Never expire;
		private static String LATITUDE_KEY = "LATITUDE_KEY";
		private static String LONGITUDE_KEY = "LONGITUDE_KEY";

		private static String PROX_ALERT_INTENT = "Bubble_Alert";

		TextView textLocation;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			SetContentView (Resource.Layout.MainInterface);

			//Create tabs for the interface:
			ActionBar.Tab tab1 = ActionBar.NewTab();
			tab1.SetText (Resources.GetString (Resource.String.tab_1));
			tab1.TabSelected += (sender, args) => {
				//textLocation = FindViewById<TextView> (Resource.Id.textAddress);
				//textLocation.Text = "You are NOWHERE!!!!! YOU DON'T EXIST!";
			};

			ActionBar.AddTab (tab1);

			ActionBar.Tab tab2 = ActionBar.NewTab();
			tab2.SetText (Resources.GetString (Resource.String.tab_2));
			tab2.TabSelected += (sender, args) => {

			};

			ActionBar.AddTab (tab2);

		
		

		}
	}
}


