using Lexicom.Example.Cinema.Client.Core.Models;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
public record class OpenPagesCountChangedNotification(Domains Domain, int Count) : INotification;
