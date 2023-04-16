namespace Lexicom.Example.Cinema.Server.Persons.Api.Contracts;
public class SearchGetResponseBodyPerson
{
    public required List<SearchGetResponseBodyPersonCredits> Credits { get; set; }
}
