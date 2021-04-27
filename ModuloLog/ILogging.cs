using System;

namespace InterfazLogging
{
    /// <summary>
    /// Interface for managing data logging
    /// </summary>
    /// <typeparam name="T">Target of data to be written (StreamWriter, File, Logger, etc)</typeparam>
    public interface ILogging<T>
    {
        /// <summary>
        /// Object where data will be written.
        /// </summary>
        T Target { set; get; }

        /// <summary>
        /// Method for creating a reference to a file (which could be a StreamWriter, File, Logger, etc)
        /// </summary>
        /// <param name="name">Name of the file to be created</param>
        /// <param name="directory">Directory where the file is created</param>
        /// <param name="customBuffer">Size of the buffer to be used, default value is equal to 1000 items on buffer</param>
        /// <returns>True if target is created correcly, False if an exception occurs</returns>
        bool CreateLogTarget(string name, string directory, int customBuffer = 1000);

        /// <summary>
        /// Method to write on a file previously created
        /// </summary>
        /// <param name="file">Reference to the file to be written upon</param>
        /// <param name="contents">Contents to be written on the file</param>
        /// <returns>True if written correctly, False if an exception occurs</returns>
        bool WriteToLog(string contents);

        /// <summary>
        /// Method to close a file and free resources associated with it
        /// </summary>
        /// <returns>True if closed correctly, False if an exception occurs</returns>
        bool CloseLogTarget();

    }
}
