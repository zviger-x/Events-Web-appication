namespace EventsManagement.WebAPI.Server
{
    internal static class JwtSettings
    {
        public static string SecretKey { get; private set; }

        public static string Issuer { get; private set; }

        public static string Audience { get; private set; }

        private static bool _isFilled;

        public static void Set(string secretKey, string issuer, string audience)
        {
            if (_isFilled)
                return;

            SecretKey = secretKey;
            Issuer = issuer;
            Audience = audience;
            _isFilled = true;
        }
    }
}
