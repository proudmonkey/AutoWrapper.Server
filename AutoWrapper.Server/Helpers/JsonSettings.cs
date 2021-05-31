﻿using AutoWrapper.Server.Wrapper;
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

        public static JsonSerializerSettings NewtonsoftJsonSettings<T>(string newJsonProperty)
        {
            var jsonResolver = new CustomResultAttributeResolver();
            jsonResolver.RenameProperty(typeof(T), DefaultResultProperty, newJsonProperty);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = jsonResolver
            };

            return settings;
        }

        public static JsonSerializerSettings NewtonsoftJsonSettings<T>(string newJsonProperty, JsonSerializerSettings jsonSettings = null)
        {
            var jsonResolver = new CustomResultAttributeResolver();
            jsonResolver.RenameProperty(typeof(T), DefaultResultProperty, newJsonProperty);

            if (jsonSettings != null)
            {
                jsonSettings.ContractResolver = jsonResolver;
                return jsonSettings;
            }
            else
            {
                return new JsonSerializerSettings
                {
                    ContractResolver = jsonResolver
                };
            }
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