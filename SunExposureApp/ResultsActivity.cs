﻿
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
using Android.Graphics;

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
			var buttons = new List<Button> {FindViewById<Button>(Resource.Id.saveProfile), FindViewById<Button> (Resource.Id.setReminder)};

			var lightingColorFilter = new LightingColorFilter (0xFFFFFFFF, 0xFF1A1D30);
			buttons.ForEach (x => x.Background.SetColorFilter (lightingColorFilter));
			buttons.ForEach (x => x.SetOnClickListener (this));
		}

		public void OnClick (View v)
		{
			switch (v.Id) {
			case Resource.Id.saveProfile:
				StartActivity (typeof(SaveProfileActivity));
				break;
			case Resource.Id.setReminder:
				StartActivity (typeof(SetReminderActivity));
				break;
			}
		}
	}
}

