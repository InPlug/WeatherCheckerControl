using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vishnu.Interchange;
using Vishnu.ViewModel;

namespace Vishnu_UserModules
{
    /// <summary>
    /// Interaktionslogik für WeatherCheckerControl.xaml.
    /// 
    /// The weather-forecasts and the used weather-icons come from http://www.7timer.info.
    /// Many thanks to Chenzhou Cui and his friends, who run this wonderful free weather-forecast site.
    /// Wiki on https://github.com/Yeqzids/7timer-issues/wiki/Wiki.
    /// API on http://www.7timer.info/bin/api.pl?lon=6.7821&amp;lat=51.2375&amp;product=civil&amp;output=json.
    /// 
    /// </summary>
    public partial class WeatherCheckerControl : DynamicUserControlBase
    {
        /// <summary>
        /// Konstruktor - hängt einen EventHandler in ContentRendered.
        /// </summary>
        public WeatherCheckerControl()
        {
            InitializeComponent();
            DynamicUserControl_ContentRendered += new RoutedEventHandler(content_Rendered);
        }

        /// <summary>
        /// Konkrete Überschreibung von GetUserResultViewModel, returnt ein spezifisches ResultViewModel.
        /// </summary>
        /// <param name="vishnuViewModel">Ein spezifisches ResultViewModel.</param>
        /// <returns></returns>
        protected override DynamicUserControlViewModelBase GetUserResultViewModel(IVishnuViewModel vishnuViewModel)
        {
            return new ResultViewModel((IVishnuViewModel)this.DataContext);
        }

        /// <summary>
        /// Hier wird aufgeräumt: ruft für alle User-Elemente, die Disposable sind, Dispose() auf.
        /// </summary>
        /// <param name="disposing">Bei true wurde diese Methode von der öffentlichen Dispose-Methode
        /// aufgerufen; bei false vom internen Destruktor.</param>
        protected override void Dispose(bool disposing)
        {
            DynamicUserControl_ContentRendered -= new RoutedEventHandler(content_Rendered);
            base.Dispose(disposing);
        }

        private void content_Rendered(object sender, RoutedEventArgs e)
        {
            (this.UserResultViewModel as ResultViewModel).HandleResultPropertyChanged();
        }

        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            ScrollViewer scrollviewer = UIHelper.FindFirstVisualChildOfType<ScrollViewer>(listBox);
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;

        }

    }
}
