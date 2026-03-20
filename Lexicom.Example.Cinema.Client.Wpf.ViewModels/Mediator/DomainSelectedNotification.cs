using Lexicom.Example.Cinema.Client.Application.Models;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
public record class DomainSelectedNotification(Domains SelectedDomain) : INotification;