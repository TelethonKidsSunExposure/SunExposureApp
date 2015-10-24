using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SunExposureApp
{
	[Activity (Label = "SunExposureApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

//			// Get our button from the layout resource,
//			// and attach an event to it
//			Button button = FindViewById<Button> (Resource.Id.myButton);
//			
//			button.Click += delegate {
//				button.Text = string.Format ("{0} clicks!", count++);
//			};
			((ViewGroup) FindViewById<TextView>(Resource.Id.InstructionStep1).Parent).SetOnTouchListener(new OnSwipeListener(this));
		}

		private class OnSwipeListener : OnSwipeTouchListener 
		{
			private readonly MainActivity activity;
			
			public OnSwipeListener(MainActivity activity)
			{
				this.activity = activity;
			}

			public override bool OnSwipeLeft() {
				activity.StartActivity (typeof(TakePhotoActivity));
				return true;
			}
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
