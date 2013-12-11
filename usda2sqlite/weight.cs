using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace usda2sqlite
{
    public class weight : IData
    {
        [Order]
        public int ndb_no { get; set; }
        [Order]
        public int seq { get; set; }
        [Order]
        public float amount { get; set; }
        [Order]
        public string desc { get; set; }
        [Order]
        public float gram_weight { get; set; }
        [Order]
        public int num_data_pts { get; set; }
        [Order]
        public float std_dev { get; set; }

        public void build(string line)
        {
            line = line.Replace("~", "");
            line = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(line.ToLower());
            string[] splits = line.Split(new string[] { "^" }, StringSplitOptions.None);
            var properties = from property in typeof(weight).GetProperties()
                             let orderAttribute = property.GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() as OrderAttribute
                             orderby orderAttribute.Order
                             select property;
            int cnt = 0;
            foreach (var myprop in properties)
            {
                //var value = Convert.ChangeType(splits[cnt], myprop.PropertyType);
                //Console.WriteLine(value.GetType() + "  " + myprop.PropertyType);
                //myprop.SetValue(this, value);
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
