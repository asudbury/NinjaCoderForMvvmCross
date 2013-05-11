// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.UI
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the Program type.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
