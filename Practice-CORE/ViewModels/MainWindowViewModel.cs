using System.Windows;
using System.Windows.Input;
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
    public MainWindowViewModel()
    {
        CloseApplicationCommand = new ReplyCommand(CanCloseApplicationCommandExecute, CloseApplicationCommandExecute);
        // CloseApplicationCommand = new ReplyCommand((o => true), (o) => Application.Current.Shutdown());
    }
}