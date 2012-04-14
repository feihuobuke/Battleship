using System.Collections.Generic;
using Newtonsoft.Json;

namespace Feihuobuke.Battleship.Library.Util
{
    public static class JsonExtension
    {
        public static T Single<T>(this string s)
        {
            if (s == null) return default(T);
            return (T)JsonConvert.DeserializeObject(s, typeof(T));
        }

        public static List<T> ToList<T>(this string s)
        {
            if (s == null) return new List<T>();
            return (List<T>)JsonConvert.DeserializeObject(s, typeof(List<T>));
        }

        public static string ToJson(this object o)
        {
            if (o == null) return null;
            return JsonConvert.SerializeObject(o);
        }
    }
}