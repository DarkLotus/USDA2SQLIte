using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace usda2sqlite
{
    public class nutrient_data : IData
    {
        [Order]
        public int ndb_id { get; set; }
        [Order]
        public int nutr_no { get; set; }
        [Order]
        public float nutr_val { get; set; }
        [Order]
        public int num_data_pts { get; set; }
        [Order]
        public float std_error { get; set; }
        [Order]
        public string source_code { get; set; }
        [Order]
        public string deriv_code { get; set; }
        [Order]        
        public int ref_ndb_no { get; set; }
        [Order] 
        public string add_nutr_mark { get; set; }
        [Order]        
        public int num_studies { get; set; }
        [Order]
        public float min { get; set; }
        [Order]
        public float max { get; set; }
        [Order]
        public int df { get; set; }
        [Order]
        public float low_eb { get; set; }
        [Order]
        public float up_eb { get; set; }
        [Order]
        public string stat_cmt { get; set; }
        [Order]
        public string add_mod_date { get; set; }
        [Order]
        public int cc { get; set; }



        public void build(string line)
        {
            line = line.Replace("~", "");
            line = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(line.ToLower());
            string[] splits = line.Split(new string[] { "^" }, StringSplitOptions.None);
            var properties = from property in typeof(nutrient_data).GetProperties()
                             let orderAttribute = property.GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() as OrderAttribute
                             orderby orderAttribute.Order
                             select property;
            int cnt = 0;
            foreach (var myprop in properties)
            {
                if (myprop.PropertyType == typeof(float))
                {
                    if(splits[cnt].Length != 0)
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
                }
                if (myprop.PropertyType == typeof(char))
                {
                    if (splits[cnt].Length != 0)
                        myprop.SetValue(this, splits[cnt]);
                }
                cnt++;
            }
        }
    }
}
