namespace ACT.Applications.WebContentManager
{
    public class CoreClass
    {
        public CoreClass()
        {
            if (Globals.DB == null) { Globals.InitDB("act_help_data"); }
        }
    }
}
