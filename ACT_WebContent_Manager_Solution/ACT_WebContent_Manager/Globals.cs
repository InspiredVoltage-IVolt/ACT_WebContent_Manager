namespace ACT.Applications.WebContentManager
{
    public static class Globals
    {
        public static Data.SqlLite_Engine DB;

        public static void InitDB(string DatabaseName) { DB = new Data.SqlLite_Engine(DatabaseName); }

    }
}
