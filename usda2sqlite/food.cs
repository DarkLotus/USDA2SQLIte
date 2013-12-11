using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace usda2sqlite
{
    public class food : IData
    {
        [PrimaryKey]
        public int id { get; set; }
        public int food_group_id { get; set; }
        [MaxLengthAttribute(200)]
        public string long_desc { get; set; }
        public string short_desc { get; set; }
        public string common_names { get; set; }
        public string manufac_name { get; set; }
        public string survey { get; set; }
        public string ref_desc { get; set; }
        public int refuse { get; set; }
        public string sci_name { get; set; }
        public float nitrogen_factor { get; set; }
        public float protein_factor { get; set; }
        public float fat_factor { get; set; }
        public float calorie_factor { get; set; }
        public food()
        {
          

        }

        public void build(string food)
        {
            food = food.Replace("~", "");
            food = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(food.ToLower());
            string[] splits = food.Split(new string[] { "^" }, StringSplitOptions.None);

            id = Int32.Parse(splits[0]);
            food_group_id = Int32.Parse(splits[1]);
            long_desc = splits[2].Replace("~", "");
            short_desc = splits[3].Replace("~", "");
            common_names = splits[4].Replace("~", "");
            manufac_name = splits[5].Replace("~", "");
            survey = splits[6].Replace("~", "");
            ref_desc = splits[7].Replace("~", "");
            if (splits[8].Length != 0)
                refuse = Int32.Parse(splits[8]);
            sci_name = splits[9].Replace("~", "");
            if (splits[10].Length != 0)
                nitrogen_factor = float.Parse(splits[10]);
            if (splits[11].Length != 0)
                protein_factor = float.Parse(splits[11]);
            if (splits[12].Length != 0)
                fat_factor = float.Parse(splits[12]);
            if (splits[13].Length != 0)
                calorie_factor = float.Parse(splits[13]);

        }
    }
}
