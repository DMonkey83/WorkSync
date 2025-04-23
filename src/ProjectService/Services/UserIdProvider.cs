using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectService.Services
{
    public interface IUserIdProvider
    {
        bool TryGetUserId(ClaimsPrincipal user, out Guid userId);
    }

    public class UserIdProvider : IUserIdProvider
    {
        private readonly ILogger<UserIdProvider> _logger;
        public UserIdProvider(ILogger<UserIdProvider> logger)
        {
            _logger = logger;
        }
        public bool TryGetUserId(ClaimsPrincipal user, out Guid userId)
        {
            userId = Guid.Empty;
            if (user == null)
            {
                _logger.LogWarning("User is null");
                return false;
            }

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out userId))
            {
                _logger.LogWarning("Invalid or missing nameidentifier claim. Found claims: {Claims}", string.Join(", ", user.Claims.Select(c => $"{c.Type}: {c.Value}")));
                return false;
            }
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("User ID is empty");
                return false;
            }

            return true;
        }
        
    }
}