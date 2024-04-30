using System;
using Vishnu.ViewModel;
using WeatherChecker;

namespace Vishnu_UserModules
{
    /// <summary>
    /// ViewModel für  ein Teilergebnis des User-spezifische Results.
    /// </summary>
    /// <remarks>
    /// Autor: Erik Nagel
    ///
    /// The weather-forecasts and the used weather-icons come from http://www.7timer.info.
    /// Many thanks to Chenzhou Cui and his friends, who run this wonderful free weather-forecast site.
    /// Wiki on https://github.com/Yeqzids/7timer-issues/wiki/Wiki.
    /// API on http://www.7timer.info/bin/api.pl?lon=6.7821&amp;lat=51.2375&amp;product=civil&amp;output=json.
    /// 
    /// 21.10.2022 Erik Nagel: erstellt.
    /// </remarks>
    public class SubResultViewModel : DynamicUserControlViewModelBase
    {
        /// <summary>
        /// Der darzustellende Wettertyp als String.
        /// </summary>
        public string? Weather
        {
            get
            {
                if (this.Result != null)
                {
                    return this.Result.Weather;
                }
                return "";
            }
        }

        /// <summary>
        /// Das aktuelle Datum als String.
        /// </summary>
        public string Date
        {
            get
            {
                if (this.Result != null)
                {
                    return this.CalculateDDMM(this.Result.Timepoint?? "0000-00-00T00:00:00");
                }
                return "";
            }
        }

        /// <summary>
        /// Die darzustellende Uhrzeit als String.
        /// </summary>
        public string Time
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0} Uhr", this.Result?.Timepoint?.Substring(11, 5) ?? "00:00");
                }
                return "";
            }
        }

        /// <summary>
        /// Die darzustellende Temperatur als String.
        /// </summary>
        public string Temperature
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Temperature);
                }
                return "";
            }
        }

        /// <summary>
        /// Die darzustellende Luftfeuchtigkeit als String.
        /// </summary>
        public string Humidity
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Humidity);
                }
                return "";
            }
        }

        /// <summary>
        /// Ein Datensatz als string.
        /// </summary>
        public string? ResultRecordString
        {
            get
            {
                return this.Result?.ToString();
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
        public SubResultViewModel(DateTime initDateTime, int init)
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

        private string CalculateDDMM(string timepoint)
        {
            return DateTime.Parse(timepoint).ToString("dd.MM");
        }

    }

}

