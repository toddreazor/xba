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
	public class ProximityIntentReceiver : BroadcastReceiver 
	{
		//private static  int NOTIFICATION_ID = 1000;
		//TextView latText;
		Activity activity;
		String location;

		public override void OnReceive(Context context, Intent intent) {
			String key = LocationManager.KeyProximityEntering;
			Boolean entering = intent.GetBooleanExtra (key, false);

			//latText = activity.FindViewById<TextView> (Resource.Id.textNotify);

			if (entering) {
				notifyUser ("Region Entered", location);
				//System.Console.WriteLine (">>>>>>>>>>>>>>>>REGION ENTERED!!!!!!!!<<<<<<<<<<<" + location);
			} else {
				//Do not notify the user.
			}

			
		}

		public ProximityIntentReceiver (Activity a,String l)
		{
			activity = a;
			location = l;
			System.Console.WriteLine("Location Added " +  l);
		}

	
		//Notifications:
		private void notifyUser(String title, String message) {
			Notification.Builder builder = new Notification.Builder (activity)
				.SetContentTitle (title)
			

				.SetSubText (message)
				.SetContentText ("You will not be notified of leaving a region.");
			
			builder.SetSmallIcon (Resource.Drawable.green_button);
			builder.SetSound (Android.Media.RingtoneManager.GetDefaultUri (Android.Media.RingtoneType.Notification));
			Notification notification = builder.Build();

			NotificationManager notificationManager = activity.GetSystemService (Context.NotificationService) as NotificationManager;
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);
		}


	}
}

