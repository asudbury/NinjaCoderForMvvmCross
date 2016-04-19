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
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetTestCommands();

        /// <summary>
        /// Getis the os test commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        // ReSharper disable once InconsistentNaming
        IEnumerable<string> GetiOSTestCommands();

        /// <summary>
        /// Gets the android test commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetAndroidTestCommands();

        /// <summary>
        /// Gets the MVVM cross core commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossCoreCommands();

        /// <summary>
        /// Gets the MVVM cross tests commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossTestsCommands();

        /// <summary>
        /// Gets the MVVM cross droid commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossDroidCommands();

        /// <summary>
        /// Gets the MVVM cross ios commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        // ReSharper disable once InconsistentNaming
        IEnumerable<string> GetMvvmCrossiOSCommands();

        /// <summary>
        /// Gets the MVVM cross windows phone commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossWindowsPhoneCommands();

        /// <summary>
        /// Gets the MVVM cross windows store commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossWindowsStoreCommands();

        /// <summary>
        /// Gets the MVVM cross WPF commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossWpfCommands();

        /// <summary>
        /// Gets the no frameworks core commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetNoFrameworksCoreCommands();

        /// <summary>
        /// Gets the no frameworks commands.
        /// </summary>
        IEnumerable<string> GetNoFrameworksCommands();

        /// <summary>
        /// Gets the no frameworks iOS commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        // ReSharper disable once InconsistentNaming
        IEnumerable<string> GetNoFrameworksiOSCommands();

        /// <summary>
        /// Gets the xamarin forms core commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetXamarinFormsCoreCommands();

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        /// <param name="addScorchioPage">if set to <c>true</c> [add scorchio page].</param>
        /// <returns>
        /// A List of Nuget commands.
        /// </returns>
        IEnumerable<string> GetXamarinFormsCommands(bool addScorchioPage = true);

        /// <summary>
        /// Gets the xamarin forms ios commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        // ReSharper disable once InconsistentNaming
        IEnumerable<string> GetXamarinFormsiOSCommands();

        /// <summary>
        /// Gets the xamarin forms android commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetXamarinFormsAndroidCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin forms core commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossXamarinFormsCoreCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin forms commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossXamarinFormsCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin form droid commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossXamarinFormDroidCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin formsi os commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        // ReSharper disable once InconsistentNaming
        IEnumerable<string> GetMvvmCrossXamarinFormsiOSCommands();

        /// <summary>
        /// Gets the MVVM cross xamarin forms windows phone commands.
        /// </summary>
        /// <returns>A List of Nuget commands.</returns>
        IEnumerable<string> GetMvvmCrossXamarinFormsWindowsPhoneCommands();

        /// <summary>
        /// Gets the MVVM cross ios story board command.
        /// </summary>
        /// <returns>A Nuget command.</returns>
        string GetMvvmCrossIosStoryBoardCommand();
    }
}
