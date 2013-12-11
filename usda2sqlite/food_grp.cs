using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace usda2sqlite
{
    public class food_grp : IData
    {
        [PrimaryKey]
        public int FdGrp_Cd { get; set; }
        public string FdGrp_Desc { get; set; }

        public void build(string line)
        {
            line = line.Replace("~", "");
            line = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(line.ToLower());
            string[] splits = line.Split(new string[] { "^" }, StringSplitOptions.None);
            FdGrp_Cd = Int32.Parse(splits[0]);
            FdGrp_Desc = splits[1];
        }
    }
}
