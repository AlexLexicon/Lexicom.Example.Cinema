namespace Lexicom.Example.Cinema.Server.Shared.Authentication;
public static class Policies
{
    public static class Permissions
    {
        public static IReadOnlyList<string> All { get; } = new List<string>
        {
            Authority.Password.PATCH,
            Authority.User.GET,
            Authority.User.PATCH,
            Authority.Email.EMAIL_CHANGE_POST,
            Authority.Email.EMAIL_CHANGE_CONFIRM_POST,
            Movies.Movie.POST,
            Movies.Movie.PATCH,
        };

        public static class Authority
        {
            public static class Password
            {
                public const string PATCH = "user:password:patch";
            }
            public static class User
            {
                public const string GET = "user:get";
                public const string PATCH = "user:patch";
            }
            public static class Email
            {
                public const string EMAIL_CHANGE_POST = "user:email:change:post";
                public const string EMAIL_CHANGE_CONFIRM_POST = "user:email:change:confirm:post";
            }
        }
        public static class Persons
        {

        }
        public static class Movies
        {
            public static class Movie
            {
                public const string POST = "movie:post";
                public const string PATCH = "movie:patch";
            }
        }
    }
}
