using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenDiary.Models
{
    public class BaseResultModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public BaseResultModel()
        { }
        public BaseResultModel(int _code ,string _message)
        {
            code = _code;
            message = _message;
        }

        public JObject GetObject()
        {
            return (JObject)data;
        }
        public JArray GetArray()
        {
            return (JArray)data;
        }

        public T GetTData<T>()
        {
            if (data is JObject)
            {
                return ((JObject)data).ToObject<T>();
            }
            else
            {
                return default(T);
            }
        }
        public List<T> GetTArray<T>()
        {
            if (data is JArray)
            {
                return ((JArray)data).ToObject<List<T>>();
            }
            else
            {
                return default(List<T>);
            }
        }

        public bool Successfully()
        {
            return code == 0;
        }
    }
}
