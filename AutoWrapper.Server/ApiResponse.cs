using System.Text.Json;

namespace AutoWrapper.Server
{
    public class ApiResponse
    {
        public static T Unwrap<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
