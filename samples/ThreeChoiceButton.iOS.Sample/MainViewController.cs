using System;

#if __UNIFIED__
using UIKit;
using Foundation;
using CoreGraphics;
using CoreAnimation;
#else
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
#endif

using ThreeChoice;

namespace App
{
	public partial class MainViewController : UIViewController
	{
		static bool IsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public MainViewController ()
			: base (IsPhone ? "MainViewController_iPhone" : "MainViewController_iPad", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var bgImg = UIImage.FromFile ("Images/bg.png");
			bgImg = bgImg.StretchableImage ((int)bgImg.Size.Width / 2 - 1, (int)bgImg.Size.Height / 2 - 1);

			var bgView = new UIImageView (View.Bounds);
			bgView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			bgView.Image = bgImg;
			View.AddSubview (bgView);

			//
			// Simple threeChoiceButton
			//
			var threeChoiceButton = new ThreeChoiceButton (new CGPoint (10, 10));
			threeChoiceButton.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin;
			threeChoiceButton.StateChanged += HandleStateChanged;

			View.AddSubview (threeChoiceButton);

			//
			// Customized threeChoiceButton
			//
			threeChoiceButton = new ThreeChoiceButton (new CGPoint (View.Bounds.Width - 110, 60), 100);
			threeChoiceButton.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;

			threeChoiceButton.LeftText = "Maybe";
			threeChoiceButton.MiddleText = "Now";
			threeChoiceButton.RightText = "Never";

			threeChoiceButton.ExpandDuration = 0.2f;
			threeChoiceButton.CollapseDuration = 0.5f;
			threeChoiceButton.CollapseDelay = 1;

			threeChoiceButton.ExpandDirection = ThreeChoiceButtonExpandDirection.Left;

			threeChoiceButton.StateChanged += HandleStateChanged;

			View.AddSubview (threeChoiceButton);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void HandleStateChanged (object sender, EventArgs e)
		{
			var threeChoiceButton = sender as ThreeChoiceButton;
			if (threeChoiceButton != null) {
				Console.WriteLine ("State: {0}", threeChoiceButton.State);
			}
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (IsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}
	}
}

