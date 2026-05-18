using Lexicom.Example.Cinema.Client.Application.Models;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;

public record class OpenPageMessage(Domains Domain, Guid PageId);