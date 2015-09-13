using System;
using System.Windows.Input;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Pivot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            VisualStateManager.GoToState(this, "Normal", false);

            //Touch.FrameReported += new TouchFrameEventHandler(Touch_FrameReported);

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -100)
            {
                MoveViewWindow(-420);
            }
            else
            {
                MoveViewWindow(0);
            }
        }
        private void OpenClose_Right(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -520)
            {
                MoveViewWindow(-840);
            }
            else
            {
                MoveViewWindow(-420);

            }
        }

        void MoveViewWindow(double left)
        {
            _viewMoved = true;
            if (left == -420)
            {

            }
            else
            { }
            ((Storyboard)canvas.Resources["moveAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)canvas.Resources["moveAnimation"]).Children[0]).To = left;
            ((Storyboard)canvas.Resources["moveAnimation"]).Begin();
        }

        /*
        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
                Canvas.SetLeft(LayoutRoot, Math.Min(Math.Max(-840, Canvas.GetLeft(LayoutRoot) + e.DeltaManipulation.Translation.X), 0));
        }
        */

        double initialPosition;
        bool _viewMoved = false;
        private void canvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _viewMoved = false;
            initialPosition = Canvas.GetLeft(LayoutRoot);
        }

        private void canvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (_viewMoved)
                return;
            if (Math.Abs(initialPosition - left) < 100)
            {
                //bouncing back
                MoveViewWindow(initialPosition);
                return;
            }
            //change of state
            if (initialPosition - left > 0)
            {
                //slide to the left
                if (initialPosition > -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(-840);
            }
            else
            {
                //slide to the right
                if (initialPosition < -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(0);
            }

        }

        /* double preFlickX;
        double preFlickY;
        bool firstTime = false;
        int id = -1;
        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            TouchPointCollection tpc = e.GetTouchPoints(canvas);
            if (id == -1)
            {
                if ((tpc.Count == 1) && (tpc[0].Action == TouchAction.Down))
                {
                    id = tpc[0].TouchDevice.Id;
                    X = tpc[0].Position.X;
                    preFlickX = Canvas.GetLeft(LayoutRoot);
                    firstTime = true;
                    ((Storyboard)canvas.Resources["moveAnimation"]).Stop();
                }
            }
            else
            {
                foreach (var tpoint in tpc)
                    if (tpoint.TouchDevice.Id == id)
                    {
                        var delta = tpoint.Position.X - X;
                        System.Diagnostics.Debug.WriteLine(delta + " " + X + " " + tpoint.Position.X);
                        if ((Math.Abs(delta) > 10) && ((((Storyboard)canvas.Resources["moveAnimation"]).GetCurrentState() == ClockState.Filling) || (((Storyboard)canvas.Resources["moveAnimation"]).GetCurrentState() == ClockState.Stopped)))
                        {
                            if ((Math.Abs(delta) > 50) && (firstTime))
                                id = -1;
                            else
                            {
                                firstTime = false;
                                X = tpoint.Position.X;
                                ((DoubleAnimation)((Storyboard)canvas.Resources["moveAnimation"]).Children[0]).To = Math.Min(Math.Max(-840, Canvas.GetLeft(LayoutRoot) + delta), 0);
                                ((Storyboard)canvas.Resources["moveAnimation"]).Begin();
                            }

                        }

                        if ((tpoint.Action == TouchAction.Up) || (id == -1))
                        {

                            var left = Canvas.GetLeft(LayoutRoot);
                            id = -1;
                            ((Storyboard)canvas.Resources["moveAnimation"]).SkipToFill();
                            if ((left - preFlickX > 100) || (delta > 50))
                            {
                                //right flick
                                if (((VisualStateGroup)VisualStateManager.GetVisualStateGroups(canvas as FrameworkElement)[0]).CurrentState.Name == "RightMenuOpened")
                                {
                                    ApplicationBar.IsVisible = true;
                                    VisualStateManager.GoToState(this, "Normal", true);
                                }
                                else
                                {
                                    ApplicationBar.IsVisible = false;
                                    VisualStateManager.GoToState(this, "LeftMenuOpened", true);
                                }
                                return;
                            }
                            if ((left - preFlickX < -100) || (delta < -50))
                            {
                                //right flick
                                if (((VisualStateGroup)VisualStateManager.GetVisualStateGroups(canvas as FrameworkElement)[0]).CurrentState.Name == "LeftMenuOpened")
                                {
                                    ApplicationBar.IsVisible = true;
                                    VisualStateManager.GoToState(this, "Normal", true);
                                }
                                else
                                {
                                    ApplicationBar.IsVisible = false;
                                    VisualStateManager.GoToState(this, "RightMenuOpened", true);
                                }
                                return;
                            }
                            Canvas.SetLeft(LayoutRoot, preFlickX);
                        }
                    }
            }

        }
        double X;*/



        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
