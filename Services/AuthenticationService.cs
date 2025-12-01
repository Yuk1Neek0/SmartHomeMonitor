using System.Security.Cryptography;
using System.Text;

namespace SmartHomeMonitor.Services
{
    public class AuthenticationService
    {
        private const string PIN_HASH_KEY = "user_pin_hash";
        private const string IS_AUTHENTICATED_KEY = "is_authenticated";
        private const string LAST_AUTH_TIME_KEY = "last_auth_time";
        private const int SESSION_TIMEOUT_MINUTES = 15;

        /// <summary>
        /// Check if a PIN has been set up
        /// </summary>
        public async Task<bool> IsPinSetupAsync()
        {
            try
            {
                var hash = await SecureStorage.Default.GetAsync(PIN_HASH_KEY);
                return !string.IsNullOrEmpty(hash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Set up a new PIN (hashed with SHA256)
        /// </summary>
        public async Task<bool> SetupPinAsync(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin) || pin.Length != 4)
            {
                return false;
            }

            try
            {
                var hashedPin = HashPin(pin);
                await SecureStorage.Default.SetAsync(PIN_HASH_KEY, hashedPin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate PIN and create authenticated session
        /// </summary>
        public async Task<bool> ValidatePinAsync(string pin)
        {
            try
            {
                var storedHash = await SecureStorage.Default.GetAsync(PIN_HASH_KEY);
                if (string.IsNullOrEmpty(storedHash))
                {
                    return false;
                }

                var inputHash = HashPin(pin);
                if (storedHash == inputHash)
                {
                    // Create session
                    await SecureStorage.Default.SetAsync(IS_AUTHENTICATED_KEY, "true");
                    await SecureStorage.Default.SetAsync(LAST_AUTH_TIME_KEY, DateTime.UtcNow.ToString("o"));
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if user is authenticated and session is valid
        /// </summary>
        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var isAuth = await SecureStorage.Default.GetAsync(IS_AUTHENTICATED_KEY);
                if (isAuth != "true")
                {
                    return false;
                }

                var lastAuthStr = await SecureStorage.Default.GetAsync(LAST_AUTH_TIME_KEY);
                if (string.IsNullOrEmpty(lastAuthStr))
                {
                    return false;
                }

                var lastAuthTime = DateTime.Parse(lastAuthStr);
                var elapsed = DateTime.UtcNow - lastAuthTime;

                if (elapsed.TotalMinutes > SESSION_TIMEOUT_MINUTES)
                {
                    await LogoutAsync();
                    return false;
                }

                // Refresh session time
                await SecureStorage.Default.SetAsync(LAST_AUTH_TIME_KEY, DateTime.UtcNow.ToString("o"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Logout and clear session
        /// </summary>
        public async Task LogoutAsync()
        {
            try
            {
                SecureStorage.Default.Remove(IS_AUTHENTICATED_KEY);
                SecureStorage.Default.Remove(LAST_AUTH_TIME_KEY);
                await Task.CompletedTask;
            }
            catch
            {
                // Ignore errors
            }
        }

        /// <summary>
        /// Clear all authentication data (for testing/reset)
        /// </summary>
        public void ClearAll()
        {
            try
            {
                SecureStorage.Default.RemoveAll();
            }
            catch
            {
                // Ignore errors
            }
        }

        /// <summary>
        /// Hash PIN with SHA256 and salt
        /// </summary>
        private string HashPin(string pin)
        {
            const string SALT = "SmartHome_Salt_2025";
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pin + SALT));
            return Convert.ToBase64String(bytes);
        }
    }
}
