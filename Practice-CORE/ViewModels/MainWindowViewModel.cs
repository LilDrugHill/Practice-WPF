using System.Collections.Immutable;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Practice_CORE.Infrastructure.Commands;
using Practice_CORE.ViewModels.Base;

namespace Practice_CORE.ViewModels;

internal class MainWindowViewModel : ViewModel
{
    # region Title
    private string _title = "Statistics Analyzer";
    public string Title
    {
        get => _title;
        set => Set( ref _title, value);
    }
    #endregion
    #region Status : string
    private string _status = "Ready";
    public string Status
    {
        get => _status;
        set => Set( ref _status, value);
    }
    #endregion
    #region Exit Command

    public ICommand? CloseApplicationCommand { get; }
    private bool CanCloseApplicationCommandExecute(object? p) => true;
    private void CloseApplicationCommandExecute(object? p) => Application.Current.Shutdown();

    #endregion

    public PlotModel PlotModel { get; private set; }


    public MainWindowViewModel()
    {
        CloseApplicationCommand = new ReplyCommand(CanCloseApplicationCommandExecute, CloseApplicationCommandExecute);
        // CloseApplicationCommand = new ReplyCommand((o => true), (o) => Application.Current.Shutdown());

        var ls = new LineSeries();
        for (var x = 0d; x <= 360; x += 0.1d)
        {
            const double to_rad =  Math.PI / 180;
            var y = Math.Sin(to_rad * x);

            ls.Points.Add(new DataPoint(x, y));
        }

        var yAxis = new LinearAxis { Position = AxisPosition.Left };
        var xAxis = new LinearAxis { Position = AxisPosition.Bottom };
        PlotModel = new PlotModel
        {
            Title = "Test Data Points",
            Axes = { xAxis, yAxis },
            Series = { ls }
        };

    }
}