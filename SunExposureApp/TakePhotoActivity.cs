
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
	[Activity (Label = "TakePhotoActivity")]			
	public class TakePhotoActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			StartActivityForResult (intent, 100);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == 100) {
				if (resultCode == Result.Canceled) {
					StartActivity (typeof(MainActivity));
				} else {
					StartActivity (typeof(MainActivity));
				}
			}
		}
	}
}

