using NetEti.MVVMini;
using System;
using System.Windows;
using System.Windows.Input;
using Vishnu.Interchange;
using Vishnu.ViewModel;
using WeatherChecker;

namespace Vishnu_UserModules
{
    /// <summary>
    /// Demo-SingleNodeViewModel.
    /// Dient zum Test von UserNodeControls außerhalb von Vishnu.
    /// Weiter unten werden ResultProperties zum Test befüllt.
    /// </summary>
    /// <remarks>
    /// 21.10.2022 Erik Nagel: erstellt.
    /// </remarks>
    public class SingleNodeViewModel : VishnuViewModelBase
    {
        /// <summary>
        /// Der Name des zugehörigen LogicalTaskTree-Knotens
        /// für die UI verfügbar gemacht.
        /// </summary>
        public string? Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Das logische Ergebnis des zugehörigen LogicalTaskTree-Knotens
        /// für die UI verfügbar gemacht.
        /// </summary>
        public bool? Logical
        {
            get
            {
                return this._logical;
            }
            set
            {
                if (this._logical != value)
                {
                    this._logical = value;
                    this.RaisePropertyChanged("Logical");
                }
            }
        }

        /// <summary>
        /// Merkfeld für den letzten Zustand von Logical, der nicht null war;
        /// Wird benötigt, damit Worker nur dann gestartet werden, und die 
        /// Anzeige wechselt, wenn sich der Zustand von Logical signifikant
        /// geändert hat und nicht jedes mal, wenn der Checker arbeitet (Logical = null).
        /// </summary>
        public bool? LastNotNullLogical
        {
            get
            {
                return this._lastNotNullLogical;
            }
            set
            {
                if (this._lastNotNullLogical != value)
                {
                    this._lastNotNullLogical = value;
                    this.RaisePropertyChanged("LastNotNullLogical");
                }
            }
        }

        /// <summary>
        /// Reicht einen u.U. aus mehreren technischen Quellen kombinierten
        /// Zustand als Aufzählungstyp an die GUI (und den IValueConverter) weiter.
        /// Default: None
        /// </summary>
        public VisualNodeState VisualState
        {
            get
            {
                return this._visualState;
            }
            set
            {
                if (this._visualState != value)
                {
                    this._visualState = value;
                    this.RaisePropertyChanged("VisualState");
                }
            }
        }

        /// <summary>
        /// Kombinierter NodeWorkerState für alle zugeordneten NodeWorker.
        /// </summary>
        public VisualNodeWorkerState WorkersState
        {
            get
            {
                return this._workersState;
            }
            set
            {
                if (this._workersState != value)
                {
                    this._workersState = value;
                    this.RaisePropertyChanged("WorkersState");
                }
            }
        }

        /// <summary>
        /// Ein Text für die Anzahl der beendeten Endknoten dieses Teilbaums
        /// zur Anzeige im ProgressBar (i.d.R. nnn%) für die UI verfügbar gemacht.
        /// </summary>
        public string? ProgressText
        {
            get
            {
                return this._progressText;
            }
            set
            {
                if (this._progressText != value)
                {
                    this._progressText = value;
                    this.RaisePropertyChanged("ProgressText");
                }
            }
        }

        /// <summary>
        /// Die Anzahl der beendeten Endknoten dieses Teilbaums
        /// für die UI verfügbar gemacht.
        /// </summary>
        public int SingleNodesFinished
        {
            get
            {
                return this._singleNodesFinished;
            }
            set
            {
                if (this._singleNodesFinished != value)
                {
                    this._singleNodesFinished = value;
                    this.RaisePropertyChanged("SingleNodesFinished");
                    this.RaisePropertyChanged("ProgressText");
                }
            }
        }

        /// <summary>
        /// Das ReturnObject der zugeordneten LogicalNode.
        /// </summary>
        public override Result? Result
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
                    this.RaisePropertyChanged("Result");
                }
            }
        }

        /// <summary>
        /// Zeitpunkt des letzten Starts des Knoten als String.
        /// </summary>
        public string LastRunInfo
        {
            get
            {
                return this._lastRun.ToString();
            }
            set
            {
                this.RaisePropertyChanged("LastRunInfo");
            }
        }

        /// <summary>
        /// Zeitpunkt des nächsten Starts des Knoten als String.
        /// </summary>
        public string NextRunInfo
        {
            get
            {
                return "- Demo, kein nächster Lauf -";
            }
            set
            {
                this.RaisePropertyChanged("NextRunInfo");
            }
        }

        /// <summary>
        /// Kombinierte Ausgabe von NextRunInfo (wann ist der nächste Durchlauf)
        /// und Result (in voller Länge).
        /// </summary>
        public string NextRunInfoAndResult
        {
            get
            {
                // return String.Format("nächster Lauf: {0}\nletztes Ergebnis: {1}", "- Demo, kein nächster Lauf -", (this.Result == null ? "" : this.Result.ToString()));
                return "- Demo, kein nächster Lauf -";
            }
        }

        /// <summary>
        /// True zeigt an, dass es sich um einen Knoten innerhalb
        /// eines geladenen Snapshots handelt.
        /// </summary>
        public bool IsSnapshotDummy
        {
            get
            {
                return this._isSnapshotDummy;
            }
            set
            {
                if (this._isSnapshotDummy != value)
                {
                    this._isSnapshotDummy = value;
                    this.RaisePropertyChanged("IsSnapshotDummy");
                }
            }
        }

        /// <summary>
        /// Veröffentlicht einen ButtonText entsprechend this._myLogicalNode.CanTreeStart.
        /// </summary>
        public string ButtonRunText
        {
            get
            {
                return "Run";
            }
            set
            {
                this.RaisePropertyChanged("ButtonRunText");
            }
        }

        /// <summary>
        /// Veröffentlicht einen ButtonText entsprechend this._myLogicalNode.CanTreeStart.
        /// </summary>
        public string ButtonBreakText
        {
            get
            {
                return "Break";
            }
            set
            {
                this.RaisePropertyChanged("ButtonBreakText");
            }
        }

        /// <summary>
        /// Command für den Run-Button im LogicalTaskTreeControl.
        /// </summary>
        public ICommand RunLogicalTaskTree { get { return this._btnRunTaskTreeRelayCommand; } }

        /// <summary>
        /// Command für den Break-Button im LogicalTaskTreeControl.
        /// </summary>
        public ICommand BreakLogicalTaskTree { get { return this._btnBreakTaskTreeRelayCommand; } }

        /// <summary>
        /// Standard Konstruktor - setzt alle Demo-Properties.
        /// </summary>
        public SingleNodeViewModel()
        {
            this.Name = "Demo-View";
            this.Logical = false;
            this.LastNotNullLogical = false;
            this.VisualState = VisualNodeState.Done;
            this.WorkersState = VisualNodeWorkerState.Valid;
            this.SingleNodesFinished = 100;
            this.ProgressText = "100 %";
            this.IsSnapshotDummy = false;
            this._lastRun = DateTime.Now;
            this._btnRunTaskTreeRelayCommand = new RelayCommand(runTaskTreeExecute, canRunTaskTreeExecute);
            this._btnBreakTaskTreeRelayCommand = new RelayCommand(breakTaskTreeExecute, canBreakTaskTreeExecute);
            this._btnCopyToolTipInfoToClipboardCommand = new RelayCommand(this.CmdBtnCopy_Executed, this.CmdBtnCopy_CanExecute);

            WeatherChecker_ReturnObject demoReturnObject = new WeatherChecker_ReturnObject();
            TreeEvent treeEvent = new TreeEvent("True", "Predecessor", "Sender", "SenderName", "./SenderPath",
                                                true, NodeLogicalState.Done,
              new ResultDictionary() { { "Predecessor",
                                   new Result("Main", true, NodeState.Finished, NodeLogicalState.Done, demoReturnObject) }  },
              new ResultDictionary() { { "Predecessor",
                                   new Result("Main", true, NodeState.Finished, NodeLogicalState.Done, demoReturnObject) } });

            // Der nachfolgende Teil reicht für den Aufruf eines einfachen Checkers:
            WeatherChecker.WeatherChecker demoChecker = new();
            bool? logicalResult = demoChecker.Run(@"UserParameter", new TreeParameters("MainTree", null), treeEvent);
            this._returnObject = ((WeatherChecker_ReturnObject?)demoChecker.ReturnObject);
            /*
            // Dieser auskommentierte Teil simuliert ein Ergebnis des WeatherCheckers; Kann benutzt werden,
            // falls die API mal nicht erreichbar sein sollte.
            this._returnObject = new WeatherChecker_ReturnObject()
            {
                Dataseries = new List<WeatherChecker_ReturnObject.ForecastDataPoint>()
                {
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 3
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 0, Temp2m = 12, Rh2m = "81%",
                    Wind10m = new Wind() {Direction = "S", Speed = 2}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 6
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 0, Temp2m = 11, Rh2m = "90%",
                    Wind10m = new Wind() {Direction = "S", Speed = 2}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 9
                    , Cloudcover = 7, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 0, Temp2m = 15, Rh2m = "61%",
                    Wind10m = new Wind() {Direction = "SW", Speed = 3}, Weather = "mcloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 12
                    , Cloudcover = 6, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 0, Temp2m = 18, Rh2m = "50%",
                    Wind10m = new Wind() {Direction = "SW", Speed = 3}, Weather = "mcloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 15
                    , Cloudcover = 8, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 0, Temp2m = 18, Rh2m = "55%",
                    Wind10m = new Wind() {Direction = "SW", Speed = 2}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 18
                    , Cloudcover = 8, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 1, Temp2m = 16, Rh2m = "75%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 2}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 21
                    , Cloudcover = 8, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 1, Temp2m = 16, Rh2m = "85%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 2}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 24
                    , Cloudcover = 8, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 1, Temp2m = 15, Rh2m = "85%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 27
                    , Cloudcover = 5, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 2, Temp2m = 15, Rh2m = "95%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "ishowernight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 30
                    , Cloudcover = 6, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 2, Temp2m = 15, Rh2m = "94%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "oshowerday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 33
                    , Cloudcover = 8, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 2, Temp2m = 18, Rh2m = "82%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 36
                    , Cloudcover = 8, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 2, Temp2m = 22, Rh2m = "56%",
                    Wind10m = new Wind() {Direction = "W", Speed = 2}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 39
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 2, Temp2m = 16, Rh2m = "78%",
                    Wind10m = new Wind() {Direction = "NW", Speed = 2}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 42
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 2, Temp2m = 14, Rh2m = "93%",
                    Wind10m = new Wind() {Direction = "W", Speed = 2}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 45
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 14, Rh2m = "97%",
                    Wind10m = new Wind() {Direction = "W", Speed = 2}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 48
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 13, Rh2m = "98%",
                    Wind10m = new Wind() {Direction = "W", Speed = 2}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 51
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 13, Rh2m = "95%",
                    Wind10m = new Wind() {Direction = "NW", Speed = 2}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 54
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 12, Rh2m = "97%",
                    Wind10m = new Wind() {Direction = "NW", Speed = 2}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 57
                    , Cloudcover = 8, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 3, Temp2m = 14, Rh2m = "86%",
                    Wind10m = new Wind() {Direction = "NW", Speed = 2}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 60
                    , Cloudcover = 8, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 3, Temp2m = 16, Rh2m = "70%",
                    Wind10m = new Wind() {Direction = "N", Speed = 2}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 63
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 3, Temp2m = 16, Rh2m = "58%",
                    Wind10m = new Wind() {Direction = "N", Speed = 2}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 66
                    , Cloudcover = 8, Lifted_Index = 10, Prec_Type = "none", Prec_Amount = 3, Temp2m = 12, Rh2m = "71%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 2}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 69
                    , Cloudcover = 6, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 11, Rh2m = "74%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 2}, Weather = "mcloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 72
                    , Cloudcover = 6, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 9, Rh2m = "80%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 2}, Weather = "mcloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 75
                    , Cloudcover = 6, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 8, Rh2m = "73%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 2}, Weather = "mcloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 78
                    , Cloudcover = 7, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 8, Rh2m = "74%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 2}, Weather = "mcloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 81
                    , Cloudcover = 8, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 11, Rh2m = "55%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 3}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 84
                    , Cloudcover = 6, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 15, Rh2m = "35%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 3}, Weather = "mcloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 87
                    , Cloudcover = 5, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 15, Rh2m = "37%",
                    Wind10m = new Wind() {Direction = "NE", Speed = 3}, Weather = "pcloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 90
                    , Cloudcover = 6, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 12, Rh2m = "64%",
                    Wind10m = new Wind() {Direction = "E", Speed = 3}, Weather = "mcloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 93
                    , Cloudcover = 9, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 10, Rh2m = "70%",
                    Wind10m = new Wind() {Direction = "E", Speed = 2}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 96
                    , Cloudcover = 9, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 10, Rh2m = "74%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 2}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 99
                    , Cloudcover = 7, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 9, Rh2m = "64%",
                    Wind10m = new Wind() {Direction = "E", Speed = 3}, Weather = "mcloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 102
                    , Cloudcover = 8, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 9, Rh2m = "60%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 105
                    , Cloudcover = 9, Lifted_Index = 15, Prec_Type = "none", Prec_Amount = 3, Temp2m = 13, Rh2m = "58%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 108
                    , Cloudcover = 9, Lifted_Index = 10, Prec_Type = "none", Prec_Amount = 3, Temp2m = 16, Rh2m = "54%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 111
                    , Cloudcover = 9, Lifted_Index = 10, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 16, Rh2m = "56%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "lightrainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 114
                    , Cloudcover = 9, Lifted_Index = 10, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 14, Rh2m = "78%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 117
                    , Cloudcover = 9, Lifted_Index = 10, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 13, Rh2m = "80%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 2}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 120
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 13, Rh2m = "85%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 123
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 12, Rh2m = "92%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 126
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "rain", Prec_Amount = 3, Temp2m = 12, Rh2m = "96%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "lightrainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 129
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 12, Rh2m = "92%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 132
                    , Cloudcover = 7, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 17, Rh2m = "90%",
                    Wind10m = new Wind() {Direction = "SE", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 135
                    , Cloudcover = 7, Lifted_Index = -1, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 17, Rh2m = "75%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 138
                    , Cloudcover = 8, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 15, Rh2m = "90%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 141
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 14, Rh2m = "93%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 144
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 14, Rh2m = "80%",
                    Wind10m = new Wind() {Direction = "S", Speed = 2}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 147
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 13, Rh2m = "87%",
                    Wind10m = new Wind() {Direction = "SW", Speed = 2}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 150
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 13, Rh2m = "94%",
                    Wind10m = new Wind() {Direction = "SW", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 153
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 14, Rh2m = "80%",
                    Wind10m = new Wind() {Direction = "SW", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 156
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 16, Rh2m = "60%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 159
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 4, Temp2m = 16, Rh2m = "60%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "cloudyday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 162
                    , Cloudcover = 9, Lifted_Index = 6, Prec_Type = "none", Prec_Amount = 4, Temp2m = 15, Rh2m = "74%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "cloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 165
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 14, Rh2m = "75%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 168
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 15, Rh2m = "68%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 171
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 15, Rh2m = "72%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 174
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 14, Rh2m = "74%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 177
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 15, Rh2m = "68%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 180
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 15, Rh2m = "70%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 183
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 16, Rh2m = "72%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainday"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 186
                    , Cloudcover = 9, Lifted_Index = 2, Prec_Type = "rain", Prec_Amount = 4, Temp2m = 14, Rh2m = "72%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "rainnight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 189
                    , Cloudcover = 7, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 4, Temp2m = 13, Rh2m = "73%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "mcloudynight"},
                    new WeatherChecker_ReturnObject.ForecastDataPoint() { Timepoint = 192
                    , Cloudcover = 8, Lifted_Index = 2, Prec_Type = "none", Prec_Amount = 4, Temp2m = 13, Rh2m = "65%",
                    Wind10m = new Wind() {Direction = "S", Speed = 3}, Weather = "cloudynight"}
                }
            };
            */

            this.Result = new Result("TestResult", true, NodeState.Finished, NodeLogicalState.Done, this._returnObject);
        }

        public void RaiseAllProperties()
        {
            this.RaisePropertyChanged("Name");
            this.RaisePropertyChanged("Logical");
            this.RaisePropertyChanged("LastNotNullLogical");
            this.RaisePropertyChanged("VisualState");
            this.RaisePropertyChanged("WorkersState");
            this.RaisePropertyChanged("SingleNodesFinished");
            this.RaisePropertyChanged("ProgressText");
            this.RaisePropertyChanged("IsSnapshotDummy");
            this.RaisePropertyChanged("Result");
            this.RaisePropertyChanged("LastRunInfo");
            this.RaisePropertyChanged("NextRunInfo");
            this.RaisePropertyChanged("NextRunInfoAndResult");
            this.RaisePropertyChanged("ButtonRunText");
            this.RaisePropertyChanged("ButtonBreakText");
        }

        private Vishnu.Interchange.Result? _result;
        private WeatherChecker_ReturnObject? _returnObject;
        private bool? _logical;
        private bool? _lastNotNullLogical;
        private VisualNodeState _visualState;
        private string? _progressText;
        private int _singleNodesFinished;
        private string? _name;
        private VisualNodeWorkerState _workersState;
        private RelayCommand _btnRunTaskTreeRelayCommand;
        private RelayCommand _btnBreakTaskTreeRelayCommand;
        private RelayCommand _btnCopyToolTipInfoToClipboardCommand;
        private DateTime _lastRun;
        private bool _isSnapshotDummy;

        private void runTaskTreeExecute(object? parameter)
        {
            this._lastRun = DateTime.Now;
            this.Logical = true;
            this.LastNotNullLogical = true;
            this.VisualState = VisualNodeState.Done;
            this.WorkersState = VisualNodeWorkerState.Valid;
            this.SingleNodesFinished = 100;
            this.ProgressText = "100 %";
            this.RaiseAllProperties();
        }

        private bool canRunTaskTreeExecute()
        {
            return true;
        }

        private void breakTaskTreeExecute(object? parameter)
        {
            this.Logical = null;
            this.LastNotNullLogical = null;
            this.VisualState = VisualNodeState.Aborted;
            this.WorkersState = VisualNodeWorkerState.Valid;
            this.SingleNodesFinished = 0;
            this.ProgressText = "0 %";
            this.RaiseAllProperties();
        }

        private bool canBreakTaskTreeExecute()
        {
            return true;
        }

        private void CmdBtnCopy_Executed(object? parameter)
        {
            Clipboard.SetText(this.NextRunInfoAndResult);
        }

        private bool CmdBtnCopy_CanExecute()
        {
            return true;
        }

    }
}
