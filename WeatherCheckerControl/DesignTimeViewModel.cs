using NetEti.MVVMini;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vishnu.Interchange;
using Vishnu_UserModules;
using WeatherChecker;

namespace WeatherCheckerControl
{
    public class DesignTimeViewModel : ObservableObject
    {
        /// <summary>
        /// Die Anzahl der Datenpunkte.
        /// </summary>
        public int RecordCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Der Initialisierungszeitpunkt.
        /// </summary>
        public string? Creation
        {
            get
            {
                return DateTime.Now.ToString("HH:mm:ss");
            }
        }

        /// <summary>
        /// Die Überschrift
        /// </summary>
        public string? Headline { get; private set; }

        /// <summary>
        /// Die Einzelnen Ergebnisse.
        /// </summary>
        public ObservableCollection<SubResultViewModel> SubResults { get; private set; }

        public DesignTimeViewModel(IVishnuViewModel parentViewModel)
        {
            Console.WriteLine("*** im DesignTimeViewModel ***");
            this.SubResults = new ObservableCollection<SubResultViewModel>()
            { new SubResultViewModel(DateTime.Now, 0) };
        }
    }
}
