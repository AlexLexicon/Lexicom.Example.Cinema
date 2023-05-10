using Microsoft.AspNetCore.Components;
using System.Windows.Input;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Extensions;
public static class CommandExtensions
{
    public static Action Binda(this ICommand command)
    {
        return () =>
        {
            command.Execute(null);
        };
    }

    public static EventCallback<ChangeEventArgs> Bindx(this ICommand command)
    {
        return new EventCallback<ChangeEventArgs>(new Test(command), () => { });
    }

    private class Test : IHandleEvent
    {
        private readonly ICommand _command;

        public Test(ICommand command)
        {
            _command = command;
        }

        public Task HandleEventAsync(EventCallbackWorkItem item, object? arg)
        {
            object? parameter = null;
            if (arg is ChangeEventArgs changeEventArgs)
            {
                parameter = changeEventArgs.Value;
            }

            _command.Execute(parameter);

            return Task.CompletedTask;
        }
    }
}