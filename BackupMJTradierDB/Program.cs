using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupMJTradierDB
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlDumper sqlDumper = new SqlDumper();
            sqlDumper.Backup();
            sqlDumper.Restore();
            File.Delete(sqlDumper.sSqlFile);
        }
    }
}
