using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoBak
{
    public class SqlDumper
    {
        string sOriginDB;
        public string sBackUpDB;
        string sUpConn;
        string sDownConn;
        string sDownDBConn;
        public string sSqlFile;


        public SqlDumper()
        {
            sOriginDB = "mjtradierdb";
            sBackUpDB = "mjtradierdb_bk";
            sUpConn = $"server=221.149.119.60;port=2023;user=meancl;pwd=1234;database={sOriginDB}";
            sDownConn = $"server=localhost;user=root;pwd=root;database={sBackUpDB}";
            sDownDBConn = "server=localhost;user=root;port=3306;password=root";
            sSqlFile = @"./backup.sql";
        }

        #region mainServer DB download
        public void Backup()
        {
            

            //using (MySqlConnection conn = new MySqlConnection(sUpConn))
            //{
            //    using (MySqlCommand cmd = new MySqlCommand())
            //    {
            //        using (MySqlBackup mainServer = new MySqlBackup(cmd))
            //        {
            //            DropDB(sBackUpDB);

            //            try
            //            {
            //                cmd.Connection = conn;
            //                conn.Open();
            //                Console.WriteLine("backUp 커넥션 오픈");

            //                try
            //                {
            //                    mainServer.ExportToFile(sSqlFile);
            //                    Console.WriteLine("backUp작업 정상종료");
            //                }
            //                catch(Exception ex)
            //                {
            //                    Console.WriteLine("backUp작업 에러");
            //                    Environment.Exit(-111);
            //                }
            //                conn.Close();
            //                Console.WriteLine("backUp 커넥션 클로즈");
            //            }
            //            catch(Exception ex)
            //            {
            //                Console.WriteLine("conn - backUp 에러");
            //            }
            //        }
            //    }
            //}

        }
        #endregion

        #region serveServer DB upload
        public void Restore()
        {
            //using (MySqlConnection conn = new MySqlConnection(sDownConn))
            //{
            //    using (MySqlCommand cmd = new MySqlCommand())
            //    {
            //        using (MySqlBackup mb = new MySqlBackup(cmd))
            //        {
            //            CreateDB(sBackUpDB);

            //            try
            //            {
            //                cmd.Connection = conn;
            //                conn.Open();
            //                Console.WriteLine("restore 커넥션 오픈");
            //                try
            //                {
            //                    mb.ImportFromFile(sSqlFile);
            //                    Console.WriteLine("restore작업 정상종료");
            //                }
            //                catch (Exception ex)
            //                {
            //                    Console.WriteLine("restore작업 에러");
            //                    Environment.Exit(-122);
            //                }
            //                conn.Close();
            //                Console.WriteLine("restore 커넥션 클로즈");
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine("conn - restore 에러");
            //            }


            //        }
            //    }
            //}
        }
        #endregion

        #region DB 생성
        public void CreateDB(string sDbName)
        {
            using (var conn = new MySqlConnection(sDownDBConn))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {sDbName} DEFAULT CHARACTER SET utf8mb4";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("db 생성 성공");
                }
            }
        }
        #endregion

        #region DB 삭제 
        public void DropDB(string sDbName)
        {
            using (var conn = new MySqlConnection(sDownDBConn))
            {
                using (var cmd = conn.CreateCommand())
                {

                    conn.Open();
                    cmd.CommandText = $"DROP DATABASE IF EXISTS {sDbName}";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("db 삭제 성공");
                }
            }
        }
        #endregion


    }
}
