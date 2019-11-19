using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AutoWrapper.Server.Helpers
{
    public class JsonSettings
    {
        public static JsonSerializerOptions JsonDeserializerSettings(bool ignoreCase = true)
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = ignoreCase
            };
        }
    }
}
