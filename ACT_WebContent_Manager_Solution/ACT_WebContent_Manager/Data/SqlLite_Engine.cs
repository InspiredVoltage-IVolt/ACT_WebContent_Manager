using ACT.Core.Extensions;
using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace ACT.Applications.WebContentManager.Data
{

    public class SqlLite_Engine
    {
        private string _StoredDBName = "";

        public SqlLite_Engine(string DatabaseName)
        {
            if (DatabaseName.NullOrEmpty()) { DatabaseName = "act_help_data.sqlite3"; }

            _StoredDBName = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat().FindFileReturnPath(DatabaseName.EnsureEndsWith("sqlite3"));

            if (Open() == false) { throw new Exception("Error Locating Database"); }
            else { if (Close() == false) { throw new Exception("Odd Error Unable To Close Database"); } }
        }

        SqliteConnection _DatabaseConnection = new SqliteConnection();

        public SqliteConnection DBConn { get => _DatabaseConnection; set => _DatabaseConnection = value; }

        public bool Open()
        {
            DBConn.ConnectionString = _StoredDBName;
            DBConn.Open();

            if (DBConn.State != ConnectionState.Open) { return false; }
            return true;
        }
        public bool Close()
        {
            if (DBConn.State != System.Data.ConnectionState.Open) { return false; }
            else { DBConn.Close(); }

            return true;
        }

        /// <summary>
        /// If Table Name is Null or Blank SQL Statement Expected to Be Full SQL Statement
        /// Else SqlStatement Expected to Be Where Statement
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SQLStatement"></param>
        /// <returns></returns>
        public DataTable GetTableData(string TableName, string SQLStatement = "")
        {
            #region Parameter Validation
            if (TableName.NullOrEmpty())
            {
                if (SQLStatement.NullOrEmpty())
                {
                    return null;
                }
            }
            #endregion

            if (TableName.NullOrEmpty() == false)
            {
                string SQL = "Select * from " + TableName + " ";
                if (SQLStatement.NullOrEmpty() == false)
                {
                    SQL += SQLStatement;
                }
                var CMD = DBConn.CreateCommand();
                CMD.CommandText = SQL;
                var RDR = CMD.ExecuteReader();

                DataTable _T = new DataTable();
                _T.Load(RDR);
                Close();
                return _T;
            }
            else
            {
                Open();
                string SQL = "Select * from " + TableName + " ";
                if (SQLStatement.NullOrEmpty() == false)
                {
                    SQL += SQLStatement;
                }
                var CMD = DBConn.CreateCommand();
                CMD.CommandText = SQL;
                var RDR = CMD.ExecuteReader();

                DataTable _T = new DataTable();
                _T.Load(RDR);
                Close();
                return _T;
            }
        }

    }
}
