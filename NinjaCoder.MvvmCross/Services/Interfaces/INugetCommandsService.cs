// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the INugetCommandsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the INugetCommandsService type.
    /// </summary>
    public interface INugetCommandsService
    {
        /// <summary>
        /// Gets the MVVM cross core commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossCoreCommands();

        /// <summary>
        /// Gets the core tests commands.
        /// </summary>
        IEnumerable<string> GetCoreTestsCommands();

        /// <summary>
        /// Gets the MVVM cross droid commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossDroidCommands();

        /// <summary>
        /// Gets the MVVM cross ios commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossiOSCommands();

        /// <summary>
        /// Gets the MVVM cross windows phone commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossWindowsPhoneCommands();

        /// <summary>
        /// Gets the MVVM cross windows store commands.
        /// </summary>

        IEnumerable<string> GetMvvmCrossWindowsStoreCommands();

        /// <summary>
        /// Gets the MVVM cross WPF commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossWpfCommands();

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        IEnumerable<string> GetXamarinFormsCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin form droid commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossXamarinFormDroidCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin formsi os commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossXamarinFormsiOSCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin forms windows phone commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossXamarinFormsWindowsPhoneCommands();
    }
}
