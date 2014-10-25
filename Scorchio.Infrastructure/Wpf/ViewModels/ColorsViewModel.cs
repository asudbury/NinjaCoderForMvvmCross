// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ColorsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Entities;

    using EventArguments;

    /// <summary>
    /// Defines the ColorsViewModel type.
    /// </summary>
    public class ColorsViewModel : BaseViewModel
    {
        /// <summary>
        /// Occurs when [color changed].
        /// </summary>
        public event EventHandler<ColorChangedEventArgs> ColorChanged;
        
        /// <summary>
        /// The themes
        /// </summary>
        private IEnumerable<AccentColor> colors;

        /// <summary>
        /// The selected color.
        /// </summary>
        private AccentColor selectedColor;

        /// <summary>
        /// Gets or sets the themes.
        /// </summary>
        public IEnumerable<AccentColor> Colors
        {
            get { return this.colors; }
            set { this.SetProperty(ref this.colors, value); }
        }

        /// <summary>
        /// Gets or sets the color of the selected.
        /// </summary>
        public AccentColor SelectedColor
        {
            get
            {
                return this.selectedColor;
            }

            set
            {
                this.SetProperty(ref this.selectedColor, value);
                this.UpdateColor();
            }
        }

        /// <summary>
        /// Updates the color.
        /// </summary>
        internal void UpdateColor()
        {
            if (this.SelectedColor != null)
            {
                if (this.ColorChanged != null)
                {
                    this.ColorChanged(this, new ColorChangedEventArgs { Color = this.SelectedColor });
                }
            }
        }
    }
}
