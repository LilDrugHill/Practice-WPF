using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Practice_CORE.Infrastructure.Commands;
using Practice_CORE.Models.Decanat;
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

    public ObservableCollection<Group> Groups { get; }

    public PlotModel PlotModel { get; private set; }


    public MainWindowViewModel()
    {
        CloseApplicationCommand = new ReplyCommand(CanCloseApplicationCommandExecute, CloseApplicationCommandExecute);
        // CloseApplicationCommand = new ReplyCommand((o => true), (o) => Application.Current.Shutdown());

        var ls = new LineSeries();
        for (var x = 0d; x <= 360; x += 0.1d)
        {
            const double toRad =  Math.PI / 180;
            var y = Math.Sin(toRad * x);

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

        var studentIndex = 1;
        var students = Enumerable.Range(1, 20).Select(i => new Student
        {
            Name = $"Student{studentIndex}",
            Surname = $"Surname{studentIndex}",
            Patronymic = $"Patronymic{studentIndex}",

        });

        var groups = Enumerable.Range(1, 20).Select(i => new Group
        {
            Name = $"Group {i}",
            Students = new ObservableCollection<Student>()
        })

        Groups = new ObservableCollection<Group>();



    }
}