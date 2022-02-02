using ACT.Core.Extensions;
using System;
using System.Collections.Generic;

namespace ACT.Applications.WebContentManager.Structs
{
    public class Database_MenuItem : CoreClass
    {
        public List<Database_MenuItem> Children = new List<Database_MenuItem>();

        public int ID;
        public int Parent_ID;
        public string Menu_ID;
        public string Menu_Text;
        public string Page_ID;
        public int DisplayOrder;
        public int Version;
        public string DateModified;

        public Database_MenuItem(string PageID, string MenuID)
        {
            var DT = Globals.DB.GetTableData("Select * from MenuItems Where Page_ID = '" + PageID + "' AND Menu_ID = '" + MenuID + "' Order By DisplayOrder");

            if (DT.Rows.Count == 0) { throw new Exception("Invalid Path"); }
            if (DT.Rows.Count > 1) { throw new Exception("Odd DB Duplicate Paths Error"); }

            ID = DT.Rows[0]["ID"].ToInt(0);
            Parent_ID = DT.Rows[0]["Parent_ID"].ToInt(0);
            Menu_ID = DT.Rows[0]["Menu_ID"].ToString();
            Menu_Text = DT.Rows[0]["Menu_Text"].ToString();
            Page_ID = DT.Rows[0]["Page_ID"].ToString();
            DisplayOrder = DT.Rows[0]["DisplayOrder"].ToInt(0);
            Version = DT.Rows[0]["Version"].ToInt(0);
            DateModified = DT.Rows[0]["DateModified"].ToString();

            var DTC = Globals.DB.GetTableData("Select * from MenuItems Where Parent_ID = " + ID.ToString() + " Order By DisplayOrder ");
            if (DTC.Rows.Count > 0)
            {
                foreach (System.Data.DataRow dataRow in DTC.Rows)
                {
                    Children.Add(new Database_MenuItem(dataRow["Page_ID"].ToString(), MenuID));
                }
            }
        }

        public Database_MenuItem(string MenuID)
        {
            var DT = Globals.DB.GetTableData("Select * from MenuItems Where Parent_ID IS NULL AND Menu_ID = '" + Menu_ID + "' Order By DisplayOrder");

            if (DT.Rows.Count == 0) { throw new Exception("Invalid MenuID"); }

            foreach (System.Data.DataRow DR in DT.Rows)
            {
                var _MnuItem = new Database_MenuItem(DR["Page_ID"].ToString(), MenuID);
                Children.Add(_MnuItem);
            }

        }
    }
}
