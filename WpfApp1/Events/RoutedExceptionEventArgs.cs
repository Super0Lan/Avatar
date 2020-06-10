using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Events
{
    public class RoutedExceptionEventArgs : RoutedEventArgs
    {
        public RoutedExceptionEventArgs(
            RoutedEvent routedEvent,
            object sender,
            Exception errorException
            ) : base(routedEvent, sender)
        {
            if (errorException == null)
            {
                throw new ArgumentNullException("errorException");
            }

            ErrorException = errorException;
        }

        /// <summary>
        /// The exception that describes the media failure.
        /// </summary>
        public Exception ErrorException { get; private set; }
    }
}
