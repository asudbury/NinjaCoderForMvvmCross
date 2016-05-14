// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LoadingIndicators type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.UserControls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// A control featuring a range of loading indicating animations.
    /// </summary>
    [TemplatePart(Name = "Border", Type = typeof(Border))]
    public class LoadingIndicator : Control
    {
        /// <summary>
        /// The part border.
        /// </summary>
        protected Border PartBorder;

        /// <summary>
        /// The speed ratio property
        /// </summary>
        public static readonly DependencyProperty SpeedRatioProperty = DependencyProperty.Register(
            "SpeedRatio",
            typeof(double),
            typeof(LoadingIndicator),
            new PropertyMetadata(
                1d,
                (o, e) =>
                {
                    LoadingIndicator li = (LoadingIndicator)o;

                    if (li.PartBorder == null || li.IsActive == false)
                    {
                        return;
                    }

                    foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.PartBorder))
                    {
                        if (group.Name == "ActiveStates")
                        {
                            foreach (VisualState state in group.States)
                            {
                                if (state.Name == "Active")
                                {
                                    state.Storyboard.SetSpeedRatio(li.PartBorder, (double)e.NewValue);
                                }
                            }
                        }
                    }
                }));

        /// <summary>
        /// The is active property
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive",
            typeof(bool),
            typeof(LoadingIndicator),
            new PropertyMetadata(
                true,
                (o, e) =>
                {
                    LoadingIndicator li = (LoadingIndicator)o;

                    if (li.PartBorder == null)
                    {
                        return;
                    }

                    if ((bool)e.NewValue == false)
                    {
                        VisualStateManager.GoToElementState(li.PartBorder, "Inactive", false);
                        li.PartBorder.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        VisualStateManager.GoToElementState(li.PartBorder, "Active", false);
                        li.PartBorder.Visibility = Visibility.Visible;

                        foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.PartBorder))
                        {
                            if (group.Name == "ActiveStates")
                            {
                                foreach (VisualState state in group.States)
                                {
                                    if (state.Name == "Active")
                                    {
                                        state.Storyboard.SetSpeedRatio(li.PartBorder, li.SpeedRatio);
                                    }
                                }
                            }
                        }
                    }
                }));

        /// <summary>
        /// Gets or sets the speed ratio.
        /// </summary>
        public double SpeedRatio
        {
            get { return (double)this.GetValue(SpeedRatioProperty); }
            set { this.SetValue(SpeedRatioProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive
        {
            get { return (bool)this.GetValue(IsActiveProperty); }
            set { this.SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code
        /// or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PartBorder = (Border)this.GetTemplateChild("PartBorder");

            if (this.PartBorder != null)
            {
                VisualStateManager.GoToElementState(this.PartBorder, this.IsActive ? "Active" : "Inactive", false);
                foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(this.PartBorder))
                {
                    if (group.Name == "ActiveStates")
                    {
                        foreach (VisualState state in group.States)
                        {
                            if (state.Name == "Active")
                            {
                                state.Storyboard.SetSpeedRatio(this.PartBorder, this.SpeedRatio);
                            }
                        }
                    }
                }

                this.PartBorder.Visibility = this.IsActive ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
