using ACT.Core.Extensions;
using System;

namespace ACT.Applications.WebContentManager.Structs
{
    public class Database_Topic : CoreClass
    {
        public int ID;
        public int Parent_ID;
        public string Topic_Title;
        public string Topic_Desc;
        public int Version;
        public string DateModified;

        public Database_Topic(string TopicTitle)
        {
            var DT = Globals.DB.GetTableData("Select * from Topics Where Topic_Title = '" + TopicTitle.Replace("'", "''") + "' Order By Version Desc LIMIT 1");

            if (DT.Rows.Count == 0) { throw new Exception("Invalid Path"); }
            if (DT.Rows.Count > 1) { throw new Exception("Odd DB Duplicate Paths Error"); }

            ID = DT.Rows[0]["ID"].ToInt(0);
            Parent_ID = DT.Rows[0]["Parent_ID"].ToInt(0);
            Topic_Title = DT.Rows[0]["Topic_Title"].ToString();
            Topic_Desc = DT.Rows[0]["Topic_Desc"].ToString();
            Version = DT.Rows[0]["Version"].ToInt();
            DateModified = DT.Rows[0]["DateModified"].ToString();
        }
    }
}
