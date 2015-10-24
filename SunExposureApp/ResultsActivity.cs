
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
using Android.Provider;

namespace SunExposureApp
{
	[Activity (Label = "ResultsActivity")]			
	public class ResultsActivity : Activity, Android.Views.View.IOnClickListener
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Results);

			// Create your application here
			var saveProfile = FindViewById<Button>(Resource.Id.saveProfile);
			var setReminder = FindViewById<Button> (Resource.Id.setReminder);

			saveProfile.SetOnClickListener (this);
			setReminder.SetOnClickListener (this);
		}

		public void OnClick (View v)
		{
			switch (v.Id) {
			case Resource.Id.saveProfile:
				// StartActivity (typeof(SaveProfileActivity));
				break;
			case Resource.Id.setReminder:
				// StartActivity (typeof(SetReminderActivity));
				break;
			}
		}
	}
}

