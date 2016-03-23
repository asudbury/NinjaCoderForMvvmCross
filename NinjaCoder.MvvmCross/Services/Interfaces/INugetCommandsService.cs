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
        /// Gets the test commands.
        /// </summary>
        IEnumerable<string> GetTestCommands();

        /// <summary>
        /// Getis the os test commands.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetiOSTestCommands();

        /// <summary>
        /// Gets the android test commands.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAndroidTestCommands();

        /// <summary>
        /// Gets the MVVM cross core commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossCoreCommands();

        /// <summary>
        /// Gets the MVVM cross tests commands.
        /// </summary>
        IEnumerable<string> GetMvvmCrossTestsCommands();

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
        /// Gets the no frameworks core commands.
        /// </summary>
        IEnumerable<string> GetNoFrameworksCoreCommands();

        /// <summary>
        /// Gets the no frameworks commands.
        /// </summary>
        IEnumerable<string> GetNoFrameworksCommands();

        /// <summary>
        /// Gets the no frameworks iOS commands.
        /// </summary>
        IEnumerable<string> GetNoFrameworksiOSCommands();

        /// <summary>
        /// Gets the xamarin forms core commands.
        /// </summary>
        IEnumerable<string> GetXamarinFormsCoreCommands();

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        IEnumerable<string> GetXamarinFormsCommands();

        /// <summary>
        /// Gets the xamarin forms ios commands.
        /// </summary>
        IEnumerable<string> GetXamarinFormsiOSCommands();

        /// <summary>
        /// Gets the xamarin forms android commands.
        /// </summary>
        IEnumerable<string> GetXamarinFormsAndroidCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin forms core commands.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetMvvmCrossXamarinFormsCoreCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin forms commands.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetMvvmCrossXamarinFormsCommands();

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

        /// <summary>
        /// Gets the xamarin android commands.
        /// </summary>
        IEnumerable<string> GetXamarinAndroidCommands();

        /// <summary>
        /// Gets the MVVM cross ios story board command.
        /// </summary>
        string GetMvvmCrossIosStoryBoardCommand();
    }
}
