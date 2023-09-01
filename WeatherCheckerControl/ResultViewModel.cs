using System;
using Vishnu.Interchange;
using System.Collections.ObjectModel;
using Vishnu.ViewModel;
using System.Collections.Generic;
using System.Globalization;
using WeatherChecker;

namespace Vishnu_UserModules
{
    /// <summary>
    /// ViewModel für das User-spezifische Result.
    /// Löst das ReturnObject eines Checkers in Properties auf.
    /// </summary>
    /// <remarks>
    /// Autor: Erik Nagel
    ///
    /// 21.10.2022 Erik Nagel: erstellt.
    /// </remarks>
    public class ResultViewModel : DynamicUserControlViewModelBase
    {
        /// <summary>
        /// Die Anzahl der Datenpunkte.
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this.GetResultProperty<int>(typeof(WeatherChecker_ReturnObject), "RecordCount");
            }
        }

        /// <summary>
        /// Der Initialisierungszeitpunkt.
        /// </summary>
        public string? Init
        {
            get
            {
                return this.GetResultProperty<string>(typeof(WeatherChecker_ReturnObject), "Init");
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

        /// <summary>
        /// Konstruktor - übernimmt den DataContext des zugehörigen Vishnu-Knotens.
        /// </summary>
        /// <param name="parentViewModel">DataContext des zugehörigen Vishnu-Knotens.</param>
        public ResultViewModel(IVishnuViewModel parentViewModel)
        {
            this.ParentViewModel = parentViewModel;
            if (parentViewModel != null) // wg. ReferenceNullException im DesignMode
            {
                this.ParentViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(parentViewModel_PropertyChanged);
            }
            this.SubResults = new ObservableCollection<SubResultViewModel>();
        }

        /// <summary>
        /// Wird ausgeführt, wenn sich die Result-Property des ViewModels
        /// des zugehörigen Vishnu-Knotens geändert hat.
        /// </summary>
        public void HandleResultPropertyChanged()
        {
            //DispatcherOperation op = this.Dispatcher.BeginInvoke(new Action(()
            this.Dispatcher.Invoke(new Action(()
                =>
                {
                    this.SubResults.Clear();
                    List<WeatherChecker_ReturnObject.ForecastDataPoint>? dataseries
                        = this.GetResultProperty<List<WeatherChecker_ReturnObject.ForecastDataPoint>>(typeof(WeatherChecker_ReturnObject), "Dataseries");
                    /*
                        WeatherChecker_ReturnObject returnObject = this.GetResultProperty<WeatherChecker_ReturnObject>(typeof(WeatherChecker_ReturnObject), null);
                        System.InvalidCastException
                            HResult=0x80004002
                            Nachricht = [A]Vishnu_UserModules.WeatherChecker_ReturnObject kann nicht in [B]Vishnu_UserModules.WeatherChecker_ReturnObject umgewandelt werden.
                            Der Typ "A" stammt von der "WeatherChecker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" im Kontext "LoadNeither" in einem Bytearray..
                            Der Typ "B" stammt von der "WeatherChecker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" im Kontext "LoadNeither" in einem Bytearray..
                            Quelle = Vishnu.ViewModel
                            Stapelüberwachung:
                            bei Vishnu.ViewModel.DynamicUserControlViewModelBase.GetResultProperty[T](Type requiredReturnObjectType, String propertyName) in C:\Users\micro\Documents\private4\WPF\Vishnu_Root\VishnuHome\Vishnu\ViewModel\DynamicUserControlViewModelBase.cs: Zeile49
                    */

                    if (dataseries != null)
                    {
                        GeoLocation_ReturnObject? location
                            = this.GetResultProperty<GeoLocation_ReturnObject>(typeof(WeatherChecker_ReturnObject), "Location");
                        if (location != null)
                        {
                            this.Headline = location.city + ", " + location.regionName; // + ", " + location.country;
                        }
                        else
                        {
                            this.Headline = "Wetter Düsseldorf, Nordrhein-Westfalen";
                        }
                        this.RaisePropertyChanged("Headline");

                        this.RaisePropertyChanged("SubResults");
                        DateTime initDateTime = DateTime.Now;
                        int initInt = 0;
                        if (this.Init != null && this.Init.Length == 10)
                        {
                            DateTime.TryParseExact(this.Init.Substring(0, 8),
                                              "yyyyMMdd",
                                              CultureInfo.InvariantCulture,
                                              DateTimeStyles.None,
                                              out initDateTime);
                            int.TryParse(this.Init.Substring(8), out initInt);
                        }
                        for (int i = 0; i < dataseries.Count; i++)
                        {
                            SubResultViewModel subResultViewModel = new SubResultViewModel(initDateTime, initInt);
                            subResultViewModel.Result = dataseries[i];
                            this.SubResults.Add(subResultViewModel);
                        }
                    }
                }
            ));
            this.RaisePropertyChanged("RecordCount");
        }

        private void parentViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                this.HandleResultPropertyChanged();
            }
        }

    }
}
