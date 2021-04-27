using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Async;

namespace ModuloLog
{
    /// <summary>
    /// Class for data logging through async Serilog
    /// </summary>
    public class StandardLogging : InterfazLogging.ILogging<Logger>
    {
        private Logger _Target;

        public Logger Target
        {
            get { return _Target; }
            set { _Target = value; }
        }
      
        public bool CloseLogTarget()
        {
            try
            {
                Target.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool CreateLogTarget(string directory, string name, int customBuffer = 1000)
        {
            string fileRouteName = ((name == "") ? directory : directory + "\\" + name);

            try
            {
                Target = new LoggerConfiguration()
                    .WriteTo.Async(a => a.File(fileRouteName, outputTemplate: "{Message}{NewLine}{Exception}", retainedFileCountLimit: null), bufferSize: customBuffer, blockWhenFull: true)
                    .CreateLogger();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool WriteToLog(string contents)
        {
            try
            {
                Target.Information(contents);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
