using NetEti.MVVMini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker;

namespace WeatherCheckerControl
{
    public class DesignTimeSubResultViewModel: ObservableObject
    {
        /// <summary>
        /// Der darzustellende Wettertyp als String.
        /// </summary>
        public string? Weather
        {
            get
            {
                return "tsrainday";
            }
        }

        /// <summary>
        /// Das aktuelle Datum als String.
        /// </summary>
        public string Date
        {
            get
            {
                return "2024-09-15T17:23:45";
            }
        }

        /// <summary>
        /// Die darzustellende Uhrzeit als String.
        /// </summary>
        public string Time
        {
            get
            {
                return "17:23 Uhr";
            }
        }

        /// <summary>
        /// Die darzustellende Temperatur als String.
        /// </summary>
        public string Temperature
        {
            get
            {
                return "18.5 °C";
            }
        }

        /// <summary>
        /// Die darzustellende Luftfeuchtigkeit als String.
        /// </summary>
        public string Humidity
        {
            get
            {
                return "44%";
            }
        }

        /// <summary>
        /// Ein Datensatz als string.
        /// </summary>
        public string? ResultRecordString
        {
            get
            {
                return "Result-Record";
            }
        }

        /// <summary>
        /// Überschriebene ToString()-Methode.
        /// Liefert die Properties als String aufbereitet. 
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            return this.ResultRecordString;
        }

        /// <summary>
        /// Konstruktor - übernimmt den Init-Zeitpunkt vom ResultViewModel.
        /// </summary>
        /// <param name="initDateTime">Datum der Initialisierung.</param>
        /// <param name="init">Stunden der Initialisierung.</param>
        public DesignTimeSubResultViewModel(DateTime initDateTime, int init)
        {
            this._initDateTime = initDateTime;
            this._init = init == 18 ? 0 : init + 6;
        }

        internal WeatherChecker_ReturnObject.ForecastDataPoint? Result
        {
            get
            {
                return this._result;
            }
            set
            {
                if (this._result != value)
                {
                    this._result = value;
                }
                this.RaisePropertyChanged("Time");
                this.RaisePropertyChanged("Weather");
                this.RaisePropertyChanged("Humidity");
                this.RaisePropertyChanged("Temperature");
            }
        }
        private DateTime _initDateTime;
        private int _init;
        private WeatherChecker_ReturnObject.ForecastDataPoint? _result;

    }
}
