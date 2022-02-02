using ACT.Core.Extensions;
using System;

namespace ACT.Applications.WebContentManager.Structs
{
    public class Database_Page : CoreClass
    {
        public int ID;
        public string Path;
        public string Meta_Title;
        public string Meta_Description;
        public string Meta_Keywords;
        public string Meta_Author;
        public string Robots;
        public string Page_Template_Path;
        public string Javascript_Header_Template_Path;
        public string Javascript_Footer_Template_Path;
        public int Version;
        public string DateModified;

        public Database_Page(string Path)
        {
            var DT = Globals.DB.GetTableData("Select * from Pages Where Path = '" + Path.Replace("'", "''") + "' Order By Version Desc LIMIT 1");

            if (DT.Rows.Count == 0) { throw new Exception("Invalid Path"); }
            if (DT.Rows.Count > 1) { throw new Exception("Odd DB Duplicate Paths Error"); }

            ID = DT.Rows[0]["ID"].ToInt(0);
            Path = DT.Rows[0]["Path"].ToString();
            Meta_Title = DT.Rows[0]["Meta_Title"].ToString();
            Meta_Description = DT.Rows[0]["Meta_Description"].ToString();
            Meta_Keywords = DT.Rows[0]["Meta_Keywords"].ToString();
            Meta_Author = DT.Rows[0]["Meta_Author"].ToString();
            Robots = DT.Rows[0]["Robots"].ToString();
            Page_Template_Path = DT.Rows[0]["Page_Template_Path"].ToString();
            Javascript_Header_Template_Path = DT.Rows[0]["Javascript_Header_Template_Path"].ToString();
            Javascript_Footer_Template_Path = DT.Rows[0]["Javascript_Footer_Template_Path"].ToString();
            Version = DT.Rows[0]["Version"].ToInt();
            DateModified = DT.Rows[0]["DateModified"].ToString();
        }
    }
}
