using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ch.swisstxt.mh3.externaltask.extension
{
    public static class DictionaryExtension
    {

        public static TReturn GetAs<TReturn>(this Dictionary<string, object> dict, string key) where TReturn : new()
        {
            TReturn value = new TReturn { };

            foreach (var propertyInfo in typeof(TReturn).GetProperties())
            {
                propertyInfo.SetValue(value, (dict[key] as Dictionary<string, object>)[propertyInfo.Name]);
            }

            return value;
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                object value = property.GetValue(source);
                if (value is T)
                    dictionary.Add(property.Name, (T)value);

            }
            return dictionary;
        }

    }
}
