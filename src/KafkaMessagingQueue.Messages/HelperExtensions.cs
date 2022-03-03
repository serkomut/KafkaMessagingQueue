using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace KafkaMessagingQueue.Messages
{
    public static class HelperExtensions
    {
        public static bool IsPhoneNumber(this string value)
        {
            const string desen = @"^(05(\d{9}))$";
            var match = Regex.Match(value, desen, RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static string UserId(this IPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            var result = identity?.FindFirst("sub")?.Value ?? identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return result ?? "SYSTEM";
        }
    }
}
