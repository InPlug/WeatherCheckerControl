using NetEti.MVVMini;
using System;
using System.Windows;
using System.Windows.Input;
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
        /// Die Bezeichnung für die darzustellende Temperatur als String.
        /// </summary>
        public string TemperatureName
        {
            get
            {
                return "Temperatur";
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
        /// Die Bezeichnung für die darzustellende Luftfeuchtigkeit als String.
        /// </summary>
        public string HumidityName
        {
            get
            {
                return "Luftfeuchtigkeit";
            }
        }

        /// <summary>
        /// Die gefühlte Temperatur als String.
        /// </summary>
        public string ApparentTemperature
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.ApparentTemperature);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die gefühlte Temperatur als String.
        /// </summary>
        public string ApparentTemperatureName
        {
            get
            {
                return "gefühlte Temperatur";
            }
        }

        /// <summary>
        /// Die Niederschlagswahrscheinlichkeit als String.
        /// </summary>
        public string PrecipitationProbability
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.PrecipitationProbability);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Niederschlagswahrscheinlichkeit als String.
        /// </summary>
        public string PrecipitationProbabilityName
        {
            get
            {
                return "Niederschlagswahrscheinlichkeit";
            }
        }

        /// <summary>
        /// Die Regenmenge als String.
        /// </summary>
        public string Rain
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Rain);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Regenmenge als String.
        /// </summary>
        public string RainName
        {
            get
            {
                return "Regenmenge";
            }
        }

        /// <summary>
        /// Die Regenschauermenge als String.
        /// </summary>
        public string Showers
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Showers);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Regenschauermenge als String.
        /// </summary>
        public string ShowersName
        {
            get
            {
                return "Regenschauermenge";
            }
        }

        /// <summary>
        /// Die Schneefallmenge als String.
        /// </summary>
        public string Snowfall
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Snowfall);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Schneefallmenge als String.
        /// </summary>
        public string SnowfallName
        {
            get
            {
                return "Schneefallmenge";
            }
        }

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
        /// Die Bezeichnung für den darzustellenden Wettertyp als String.
        /// </summary>
        public string? WeatherName
        {
            get
            {
                return "Wetter-Kurzbezeichnung";
            }
        }

        /// <summary>
        /// Die Luftdruck an der Oberfläche als String.
        /// </summary>
        public string SurfacePressure
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.SurfacePressure);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für den Luftdruck an der Oberfläche als String.
        /// </summary>
        public string SurfacePressureName
        {
            get
            {
                return "Luftdruck";
            }
        }

        /// <summary>
        /// Die Bewölkung als String.
        /// </summary>
        public string Cloudcover
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Cloudcover);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Bewölkung als String.
        /// </summary>
        public string CloudcoverName
        {
            get
            {
                return "Bewölkung";
            }
        }

        /// <summary>
        /// Die Windgeschwindigkeit als String.
        /// </summary>
        public string WindSpeed
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Wind10m?.Speed);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Windgeschwindigkeit als String.
        /// </summary>
        public string WindSpeedName
        {
            get
            {
                return "Windgeschwindigkeit";
            }
        }
        /// <summary>
        /// Die Windgeschwindigkeit und -Richtung als String.
        /// </summary>
        public string Wind
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0} aus {1}", this.Result.Wind10m?.Speed, this.Result.Wind10m?.Direction);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Windgeschwindigkeit als String.
        /// </summary>
        public string WindName
        {
            get
            {
                return "Wind";
            }
        }

        /// <summary>
        /// Die Windrichtung als String.
        /// </summary>
        public string WindDirection
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Wind10m?.Direction);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Windrichtung als String.
        /// </summary>
        public string WindDirectionName
        {
            get
            {
                return "Windrichtung";
            }
        }

        /// <summary>
        /// Die Geschwindigkeit von Windböen als String.
        /// </summary>
        public string WindGustsSpeed
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Wind10m?.GustsSpeed);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Geschwindigkeit von Windböen als String.
        /// </summary>
        public string WindGustsSpeedName
        {
            get
            {
                return "Windböen";
            }
        }

        /// <summary>
        /// Die Zeit des Sonnenaufgangs als String.
        /// </summary>
        public string Sunrise
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Sunrise).Replace("T"," ");
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Zeit des Sonnenaufgangs als String.
        /// </summary>
        public string SunriseName
        {
            get
            {
                return "Sonnenaufgang";
            }
        }

        /// <summary>
        /// Die Zeit des Sonnenuntergangs als String.
        /// </summary>
        public string Sunset
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.Sunset).Replace("T", " ");
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die Zeit des Sonnenuntergangs als String.
        /// </summary>
        public string SunsetName
        {
            get
            {
                return "Sonnenuntergang";
            }
        }

        /// <summary>
        /// Die Sonnenscheindauer als String.
        /// </summary>
        public string SunshineDuration
        {
            get
            {
                if (this.Result != null)
                {
                    return this.CalculateSunshineDuration(this.Result.SunshineDuration);
                }
                return "";
            }
        }

        private string CalculateSunshineDuration(string? sunshineDuration)
        {
            string d = sunshineDuration ?? "0";
            int seconds = (int)Math.Round(decimal.Parse(d.Replace(" s", "").Replace(",", ".")));
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}h:{1:D2}m",
                            t.Hours,
                            t.Minutes);
        }

        /// <summary>
        /// Die Bezeichnung für die Sonnenscheindauer als String.
        /// </summary>
        public string SunshineDurationName
        {
            get
            {
                return "Sonnenscheindauer";
            }
        }

        /// <summary>
        /// Die tägliche Regenmenge als String.
        /// </summary>
        public string RainSum
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.RainSum);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die tägliche Regenmenge als String.
        /// </summary>
        public string RainSumName
        {
            get
            {
                return "tägliche Regenmenge";
            }
        }

        /// <summary>
        /// Die tägliche Regenschauermenge als String.
        /// </summary>
        public string ShowersSum
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.ShowersSum);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die tägliche Regenschauermenge als String.
        /// </summary>
        public string ShowersSumName
        {
            get
            {
                return "tägliche Regenschauermenge";
            }
        }

        /// <summary>
        /// Die tägliche Schneefallmenge als String.
        /// </summary>
        public string SnowfallSum
        {
            get
            {
                if (this.Result != null)
                {
                    return String.Format("{0}", this.Result.SnowfallSum);
                }
                return "";
            }
        }

        /// <summary>
        /// Die Bezeichnung für die tägliche Schneefallmenge als String.
        /// </summary>
        public string SnowfallSumName
        {
            get
            {
                return "tägliche Schneefallmenge";
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
        /// Gibt an, ob ein Popup offen ist.
        /// </summary>
        public bool IsPopupOpen { get; set; }

        /// <summary>
        /// Kommando zum Schließen des Popups
        /// </summary>
        public ICommand ClosePopupCommand { get; }

        private void ClosePopup(object? parameter)
        {
            IsPopupOpen = false;
            RaisePropertyChanged(nameof(IsPopupOpen));
        }

        /// <summary>
        /// Kommando zum Schließen des Popups
        /// </summary>
        public ICommand OpenPopupCommand { get; }

        private void OpenPopup(object? parameter)
        {
            IsPopupOpen = true;
            RaisePropertyChanged(nameof(IsPopupOpen));
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
            // Initialisierung des Schließkommandos
            OpenPopupCommand = new RelayCommand(OpenPopup);
            ClosePopupCommand = new RelayCommand(ClosePopup);
            this.IsPopupOpen = false;
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

