using Lexicom.Example.Cinema.Client.Application.Models;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
public record class SearchInitiateNotification(Domains Domain) : INotification;