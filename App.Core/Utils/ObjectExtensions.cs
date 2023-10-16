﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Core.Utils
{
    public static class ObjectExtensions
    {
        private static object ConvertIfNonAvroType(object obj)
        {
            return obj is DateTime ? ((DateTime)obj).ToString("o") : obj;
        }
        public static JObject GetJsonObjetc(this Dictionary<string, object> document)
        {
            return JObject.Parse(JsonConvert.SerializeObject(document));
        }
        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }
        public static JObject GetJsonObjetc(this object document)
        {
            return JObject.Parse(JsonConvert.SerializeObject(document));
        }
        public static T ToObject<T>(this object obj)
        {
            Type TypeOfClass = typeof(T);
            return (T)Convert.ChangeType(obj, TypeOfClass);
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static string ProcessStringCharacter(this string str)
        {

            return (RemoveDiacritics(str.Replace(",", " ").Replace("-", " ").Replace("_", " ").Replace(";", " ").Replace("&", " ")));
        }
        public static T ToConvertObjects<T>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T ToConvertObjects<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

}
