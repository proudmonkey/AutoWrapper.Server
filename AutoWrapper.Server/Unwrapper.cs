using AutoWrapper.Server.Helpers;
using DN = System.Text.Json;
using NS = Newtonsoft.Json;

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

        /// <summary>
        /// Unwrap Api Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json string to deserialize</param>
        /// <param name="propertyToUnwrap"></param>
        /// <param name="settings">Newtonsoft SerializerSettings</param>
        /// <returns>return the deserialize value</returns>
        public static T Unwrap<T>(string jsonString, string propertyToUnwrap = "", NS.JsonSerializerSettings settings = null)
        {
            WrapTo<T> data;
            if (string.IsNullOrEmpty(propertyToUnwrap))
            {
                data = NS.JsonConvert.DeserializeObject<WrapTo<T>>(jsonString, settings);
                return data.Result;
            }
            else
            {
                data = NS.JsonConvert.DeserializeObject<WrapTo<T>>(jsonString, JsonSettings.NewtonsoftJsonSettings<WrapTo<T>>(propertyToUnwrap, settings));
                return data.Result;
            }
        }
    }
}