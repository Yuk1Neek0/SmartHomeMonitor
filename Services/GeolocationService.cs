using Microsoft.Maui.Devices.Sensors;

namespace SmartHomeMonitor.Services
{
    public class GeolocationService
    {
        public async Task<(double? Latitude, double? Longitude, string Message)> GetCurrentLocationAsync()
        {
            try
            {
                // Request location permission
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status != PermissionStatus.Granted)
                {
                    return (null, null, "Location permission denied");
                }

                // Get location with medium accuracy
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    return (location.Latitude, location.Longitude, "Location retrieved successfully");
                }

                return (null, null, "Unable to get location");
            }
            catch (FeatureNotSupportedException)
            {
                return (null, null, "Geolocation not supported on this device");
            }
            catch (PermissionException)
            {
                return (null, null, "Location permission denied");
            }
            catch (Exception ex)
            {
                return (null, null, $"Error: {ex.Message}");
            }
        }
    }
}
