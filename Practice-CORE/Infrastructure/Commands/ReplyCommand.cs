using Practice_CORE.Infrastructure.Commands.Base;

namespace Practice_CORE.Infrastructure.Commands;

public class ReplyCommand : Command
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool> _canExecute;

    public ReplyCommand(Func<object?, bool> canExecute, Action<object?> execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
    public override void Execute(object? parameter) => _execute(parameter);
}