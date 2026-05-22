using Lexicom.Example.Cinema.Client.Application.Models;

namespace Lexicom.Example.Cinema.Client.Application.Services;

public interface IPageService
{
    Task OpenPage(Domain domain, Guid pageId);
}
public class PageService : IPageService
{

}
