namespace Core.Options
{
    public class JWTOption
    {

            public string Issuer { get; set; }
            public string Aduience { get; set; }
            public int Lifetime { get; set; }
            public string SigningKey { get; set; }


    }
}
