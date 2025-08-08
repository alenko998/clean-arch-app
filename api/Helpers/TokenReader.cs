using System.Security.Claims;

namespace api.Helpers
{
    public static class TokenReader
    {
        public static int GetWriterIdFromClaims(ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == "WriterId");
            if (claim == null)
                throw new UnauthorizedAccessException("WriterId not found in token.");

            return int.Parse(claim.Value);
        }
    }
}
