using AutoWrapper.Server.Wrapper;
using Newtonsoft.Json;
using System.Text.Json;


namespace AutoWrapper.Server.Helpers
{
    public class JsonSettings
    {
        private const string DefaultResultProperty = "Result";
        public static JsonSerializerOptions DotNetJsonSettings(bool ignoreCase = true)
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = ignoreCase
            };
        }

        public static JsonSerializerSettings NewtonsoftJsonSettings(string newJsonProperty)
        {
            var jsonResolver = new CustomResultAttributeResolver();
            jsonResolver.RenameProperty(typeof(AutoWrapperResponse), DefaultResultProperty, newJsonProperty);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = jsonResolver
            };

            return settings;
        }
    }
}
