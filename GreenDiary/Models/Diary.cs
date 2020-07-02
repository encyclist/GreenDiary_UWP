using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenDiary.Models
{
    public class Diary
    {
        public int id { get; set; }
        public int uid { get; set; }
        public String imgs { get; set; }
        private List<string> imgs2;
        public String content { get; set; }
        public String time { get; set; }
        public int weather { get; set; }
        public int mood { get; set; }
        public String address { get; set; }
        public String latitude { get; set; }
        public String longitude { get; set; }
        public String props { get; set; }
        /*额外的字段*/
        public int praise { get; set; }

        public string RandomImage
        {
            get
            {
                if (imgs2 == null) 
                {
                    imgs2 = GetImages();
                }
                if (imgs2.Count > 0)
                {
                    return imgs2[new Random().Next(imgs2.Count)];
                }
                else
                {
                    return null;
                }
            }
        }
        public List<string> GetImages()
        {
            var images = JsonConvert.DeserializeObject<List<string>>(imgs);
            List<string> result = new List<string>(images.Count);
            images.ForEach(delegate (string item)
            {
                result.Add(App.URL + item);
            });

            return result;
        }
    }
}
