using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace InterfazRastreoOcular
{
    /// <summary>
    /// Interface for managing the creation of plugins for different eye tracking devices
    /// </summary>
    public interface IEyeTracking : INotifyPropertyChanged
    {
        /// <summary>
        /// Property that stores the values received from an eyetracker
        /// </summary>
        Dictionary<string, string> Data { set; get; }


        /// <summary>
        /// Establishes connection with eyetracking device
        /// </summary>
        /// <returns>True if connection is established, False otherwise</returns>
        bool OpenConnection();

        //bool closeConnection();

        /// <summary>
        /// Event to be raised when the value of the property Data changes
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Event arguments</param>
        void OnPropertyChanged(object sender, PropertyChangedEventArgs e);

        // Based on the web module interfaces
        //string pluginName();
        //string pluginVersion();
    }
}
