using System.Windows;
using Practice_CORE.Infrastructure.Commands.Base;

namespace Practice_CORE.Infrastructure.Commands;

public class CloseApplicationCommand: Command
{
    public override bool CanExecute(object? parameter) => true;
    public override void Execute(object? parameter) => Application.Current.Shutdown();
}