namespace Lexicom.Example.Cinema.Server.Persons.Api.Contracts;
public class SearchGetResponseBody
{
    public required int TotalPersons { get; set; }
    public required List<SearchGetResponseBodyPerson> Persons { get; set; }
}
