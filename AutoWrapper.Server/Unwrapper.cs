using AutoWrapper.Server.Helpers;
using NS = Newtonsoft.Json;
using DN = System.Text.Json;

namespace AutoWrapper.Server
{
    public class WrapTo<T>
    {
        public T Result { get; set; }
    }
    public class Unwrapper
    {
        public static T Unwrap<T>(string jsonString, string propertyToUnwrap = "", bool ignoreCase = true)
        {
            WrapTo<T> data = null;
            if (string.IsNullOrEmpty(propertyToUnwrap))
            {
                data = DN.JsonSerializer.Deserialize<WrapTo<T>>(jsonString, JsonSettings.DotNetJsonSettings(ignoreCase));
                return data.Result;
            }
            else
            {
                data = NS.JsonConvert.DeserializeObject<WrapTo<T>>(jsonString, JsonSettings.NewtonsoftJsonSettings<WrapTo<T>>(propertyToUnwrap));
                return data.Result;
            }
        }

    }
}
