using Lexicom.Example.Cinema.Client.Core.Models;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
public record class OpenPageNotification(Domains Domain, Guid Id) : INotification;