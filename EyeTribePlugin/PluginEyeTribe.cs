using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTribe.ClientSdk;
using EyeTribe.ClientSdk.Data;
using InterfazRastreoOcular;

namespace EyeTribePlugin
{
    public class PluginEyeTribe : IEyeTracking, IGazeListener
    {
        private Dictionary<string, string> _Data = new Dictionary<string, string>();
        public Dictionary<string, string> Data
        {
            get { return _Data; }
            set
            {
                _Data = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public bool OpenConnection()
        {
            try
            {
                GazeManager.Instance.Activate(GazeManagerCore.ApiVersion.VERSION_1_0, GazeManagerCore.ClientMode.Push);

                // Register this class for events
                GazeManager.Instance.AddGazeListener(this);

                //Thread.Sleep(5000); // simulate app lifespan (e.g. OnClose/Exit event)

                // Disconnect client
                //GazeManager.Instance.Deactivate();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


        public void OnGazeUpdate(GazeData gazeData)
        {
            int gX = Convert.ToInt32(gazeData.SmoothedCoordinates.X);
            int gY = Convert.ToInt32(gazeData.SmoothedCoordinates.Y);
            Dictionary<string, string> auxDict = new Dictionary<string, string>
            {
                { "X_Coordinate", gX.ToString()},
                { "Y_Coordinate", gY.ToString()},
                { "Timestamp", gazeData.TimeStamp.ToString()}
            };
            Data = auxDict;
            // Move point, do hit-testing, log coordinates etc.
        }
    }
}
