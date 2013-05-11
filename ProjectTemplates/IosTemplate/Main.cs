// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Main type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IosTemplate
{
    using MonoTouch.UIKit;

    /// <summary>
    ///    Defines the Main type.
    /// </summary>
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}