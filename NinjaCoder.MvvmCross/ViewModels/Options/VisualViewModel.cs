// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the VisualViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    using MahApps.Metro;

    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Entities;
    using Scorchio.Infrastructure.EventArguments;
    using Scorchio.Infrastructure.Factories;
    using Scorchio.Infrastructure.Wpf.ViewModels;

    using BaseViewModel = NinjaCoder.MvvmCross.ViewModels.NinjaBaseViewModel;

    /// <summary>
    ///  Defines the VisualViewModel type.
    /// </summary>
    public class VisualViewModel : BaseViewModel
    {
        /// <summary>
        /// The selected langauge.
        /// </summary>
        private string selectedLangauge;

        /// <summary>
        /// The theme factory.
        /// </summary>
        private readonly IThemeFactory themeFactory;

        /// <summary>
        /// The language factory.
        /// </summary>
        private readonly ILanguageFactory languageFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="themeFactory">The theme factory.</param>
        /// <param name="languageFactory">The language factory.</param>
        public VisualViewModel(
            ISettingsService settingsService,
            IThemeFactory themeFactory,
            ILanguageFactory languageFactory)
            : base(settingsService)
        {
            this.themeFactory = themeFactory;
            this.languageFactory = languageFactory;
            this.Init();
        }
        
        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Occurs when the theme changed.
        /// </summary>
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        /// <summary>
        /// Gets the theme view model.
        /// </summary>
        public ColorsViewModel ThemeViewModel { get; private set; }

        /// <summary>
        /// Gets the color view model.
        /// </summary>
        public ColorsViewModel ColorViewModel { get; private set; }

        /// <summary>
        /// Sets the colors.
        /// </summary>
        public IEnumerable<AccentColor> Colors
        {
            set
            {
                this.ColorViewModel.Colors = value;
                this.ColorViewModel.SelectedColor = value.FirstOrDefault(x => x.Name == this.SettingsService.ThemeColor);
            }
        }

        /// <summary>
        /// Gets the langauges.
        /// </summary>
        public IEnumerable<string> Langauges
        {
            get { return this.languageFactory.Languages; }
        }

        /// <summary>
        /// Gets or sets the selected langauge.
        /// </summary>
        public string SelectedLangauge
        {
            get { return this.selectedLangauge; }
            set { this.SetProperty(ref this.selectedLangauge, value); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.LanguageOverride = this.selectedLangauge;

            //// remove the references.
            WeakEventManager<ColorsViewModel, ColorChangedEventArgs>
                    .RemoveHandler(this.ThemeViewModel, "ColorChanged", this.ColorChangedHandler);

            WeakEventManager<ColorsViewModel, ColorChangedEventArgs>
                    .RemoveHandler(this.ColorViewModel, "ColorChanged", this.ColorChangedHandler);
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            string theme = this.SettingsService.Theme;

            this.ThemeViewModel = new ColorsViewModel
                                  {
                                      Colors = this.themeFactory.Themes
                                  };

            this.ThemeViewModel.SelectedColor = this.ThemeViewModel.Colors.FirstOrDefault(x => x.Name == theme);

            //// use weak references.
            WeakEventManager<ColorsViewModel, ColorChangedEventArgs>
                .AddHandler(this.ThemeViewModel, "ColorChanged", this.ColorChangedHandler);
            
            this.ColorViewModel = new ColorsViewModel();

            //// use weak references.
            WeakEventManager<ColorsViewModel, ColorChangedEventArgs>
                .AddHandler(this.ColorViewModel, "ColorChanged", this.ColorChangedHandler);

            this.selectedLangauge = this.SettingsService.LanguageOverride;
        }

        /// <summary>
        /// Colors the changed handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="colorChangedEventArgs">The <see cref="ColorChangedEventArgs"/> instance containing the event data.</param>
        internal void ColorChangedHandler(
            object sender, 
            ColorChangedEventArgs colorChangedEventArgs)
        {
            this.UpdateTheme();
        }

        /// <summary>
        /// Updates the theme.
        /// </summary>
        internal void UpdateTheme()
        {
            if (this.ThemeViewModel.SelectedColor != null &&
                this.ColorViewModel.SelectedColor != null)
            {
                Accent accent = ThemeManager.DefaultAccents.First(x => x.Name == this.ColorViewModel.SelectedColor.Name);

                if (accent != null)
                {
                    this.SettingsService.Theme = this.ThemeViewModel.SelectedColor.Name;
                    this.SettingsService.ThemeColor = this.ColorViewModel.SelectedColor.Name;

                    Theme theme = Theme.Light;

                    if (this.ThemeViewModel.SelectedColor.Name == "Dark")
                    {
                        theme = Theme.Dark;
                    }

                    if (this.ThemeChanged != null)
                    {
                        this.ThemeChanged(this, new ThemeChangedEventArgs { Theme = theme, Accent = accent });
                    }
                }
            }
        }
    }
}
