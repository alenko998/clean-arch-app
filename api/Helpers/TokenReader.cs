using System.Security.Claims;

namespace api.Helpers
{
    public static class TokenReader
    {
        public static int GetWriterIdFromClaims(ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == "WriterId");

            if (claim == null || !int.TryParse(claim.Value, out int writerId))
                return -1; // vraćamo -1 ako WriterId ne postoji ili nije broj

            return writerId;
        }
    }
}
