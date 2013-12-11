using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace usda2sqlite
{
    public class nutrient_def : IData
    {
        [PrimaryKey]
        [Order]
        public int nutr_no { get; set; }
        [Order]
        public string units { get; set; }
        [Order]
        public string tag_name { get; set; }
        [Order]
        public string nutr_desc { get; set; }
        [Order]
        public int num_dec { get; set; }
        [Order]
        public int sr_order { get; set; }


        public void build(string line)
        {
            line = line.Replace("~", "");
            line = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(line.ToLower());
            string[] splits = line.Split(new string[] { "^" }, StringSplitOptions.None);
            var properties = from property in typeof(nutrient_def).GetProperties()
                             let orderAttribute = property.GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() as OrderAttribute
                             orderby orderAttribute.Order
                             select property;
            int cnt = 0;
            foreach (var myprop in properties)
            {
                if (myprop.PropertyType == typeof(float))
                {
                    if (splits[cnt].Length != 0)
                        myprop.SetValue(this, float.Parse(splits[cnt]));
                }
                if (myprop.PropertyType == typeof(Int32))
                {
                    if (splits[cnt].Length != 0)
                        myprop.SetValue(this, int.Parse(splits[cnt]));
                }
                if (myprop.PropertyType == typeof(string))
                {
                    if (splits[cnt].Length != 0)
                        myprop.SetValue(this, splits[cnt]);
                    else
                        myprop.SetValue(this, "");
                }
                cnt++;
            }
        }
    
    
    }
}
