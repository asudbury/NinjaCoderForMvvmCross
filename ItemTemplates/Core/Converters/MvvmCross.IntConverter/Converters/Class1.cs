using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using System.ComponentModel;

namespace $rootnamespace$
{
    /// <summary>
    /// The $safeitemrootname$ class.
    /// </summary>
    public class $safeitemrootname$ : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            //  Get and fire the event.
            var theEvent = PropertyChanged;
            if (theEvent != null)
                theEvent(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
