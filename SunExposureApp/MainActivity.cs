using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;

namespace SunExposureApp
{
	[Activity (Label = "SunExposureApp", MainLauncher = true, Icon = "@drawable/icon",
        Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
	public class MainActivity : Activity, View.IOnClickListener
    {
		private ViewFlipper _viewFlipper;
	    private Button _button;

	    protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//var viewGroup = ((ViewGroup)FindViewById<TextView> (Resource.Id.InstructionStep1).Parent);
			//viewGroup.SetOnTouchListener(new OnSwipeListener(this));
			_viewFlipper = FindViewById<ViewFlipper>(Resource.Id.ViewFlipper);

            // Button
			_button = FindViewById<Button>(Resource.Id.button1);
            _button.SetOnClickListener(this);

			var onSwipeListener = new OnSwipeListener (_viewFlipper, this);
			FindViewById(Resource.Id.step1View).SetOnTouchListener (onSwipeListener);
			FindViewById(Resource.Id.step2View).SetOnTouchListener (onSwipeListener);
			FindViewById(Resource.Id.step3View).SetOnTouchListener (onSwipeListener);
			FindViewById(Resource.Id.step4View).SetOnTouchListener (onSwipeListener);
		}

		private class OnSwipeListener : OnSwipeTouchListener 
		{
			private readonly ViewFlipper _viewFlipper;
			private readonly MainActivity _activity;
			
			public OnSwipeListener(ViewFlipper viewFlipper, MainActivity activity)
			{
				_viewFlipper = viewFlipper;
				_activity = activity;
			}

			public override bool OnSwipeLeft() {
				_viewFlipper.SetInAnimation (_activity, Resource.Animation.slideInFromRight);
				_viewFlipper.SetOutAnimation(_activity, Resource.Animation.slideOutToLeft);
				_viewFlipper.ShowNext ();
//				Intent intent = new Intent(MediaStore.ActionImageCapture);
//				activity.StartActivityForResult (intent, 100);
				return true;
			}

			public override bool OnSwipeRight() {
				_viewFlipper.SetInAnimation (_activity, Resource.Animation.slideInFromLeft);
				_viewFlipper.SetOutAnimation(_activity, Resource.Animation.slideOutToRight);
				_viewFlipper.ShowPrevious ();
				return true;
			}
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == 100 && resultCode != Result.Canceled) {
				StartActivity (typeof(ResultsActivity));
			}
		}

	    public void OnClick(View v)
	    {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            this.StartActivityForResult (intent, 100);
        }
    }
}

public class OnSwipeTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener {

	private readonly GestureDetector gestureDetector;

	public OnSwipeTouchListener()
	{
		gestureDetector = new GestureDetector(new GestureListener(this));
	}

	public bool OnTouch(View v, MotionEvent @event) {
		gestureDetector.OnTouchEvent(@event);
		return true;
	}

	private sealed class GestureListener : Android.Views.GestureDetector.SimpleOnGestureListener {

		private const int SWIPE_THRESHOLD = 100;
		private const int SWIPE_VELOCITY_THRESHOLD = 100;

		private readonly OnSwipeTouchListener listener;

		public GestureListener(OnSwipeTouchListener listener)
		{
			this.listener = listener;
		}

		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) {
			bool result = false;
			try {
				float diffY = e2.GetY() - e1.GetY();
				float diffX = e2.GetX() - e1.GetX();
				if (Math.Abs(diffX) > Math.Abs(diffY)) {
					if (Math.Abs(diffX) > SWIPE_THRESHOLD && Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD) {
						if (diffX > 0) {
							result = listener.OnSwipeRight();
						} else {
							result = listener.OnSwipeLeft();
						}
					}
				} else {
					if (Math.Abs(diffY) > SWIPE_THRESHOLD && Math.Abs(velocityY) > SWIPE_VELOCITY_THRESHOLD) {
						if (diffY > 0) {
							result = listener.OnSwipeBottom();
						} else {
							result = listener.OnSwipeTop();
						}
					}
				}
			} catch (Exception exception) {
				// exception.PrintStackTrace();
			}
			return result;
		}
	}

	public virtual bool OnSwipeRight() {
		return false;
	}

	public virtual bool OnSwipeLeft() {
		return false;
	}

	public virtual bool OnSwipeTop() {
		return false;
	}

	public virtual bool OnSwipeBottom() {
		return false;
	}
}
