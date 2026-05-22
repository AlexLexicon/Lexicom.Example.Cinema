//using Lexicom.Example.Cinema.Client.Application.Models;

//namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Services;

//public interface INavigationDomainService
//{
//    Task<int> GetPagesCountAsync(Domain domain);
//}
//public class NavigationDomainService : INavigationDomainService
//{
//    public NavigationDomainService()
//    {
//        DomainToPagesCount = [];
//    }

//    public Dictionary<Domain, int> DomainToPagesCount { get; }


//    public Task<int> GetPagesCountAsync(Domain domain)
//    {
//        if (!DomainToPagesCount.TryGetValue(domain, out int count))
//        {
//            count = 0;
//        }

//        return Task.FromResult(count);
//    }
//}
