
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

namespace SunExposureApp
{
	[Activity (Label = "SaveProfileActivity",
        Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
    public class SaveProfileActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.SaveProfile);

			// Create your application here
		}
	}
}

