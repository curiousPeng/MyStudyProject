using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;

namespace SQLCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始读取表信息");
            var sqlConnStr = "User ID=db_erp;Password=erp.2019;Data Source=172.16.10.220:1521/xe";
            //1.读出数据库信息
            List<TableFileds> Tables = GetUserAllTables(sqlConnStr);
            List<string> IgnoreTable = new List<string>
            {

            };//要忽略的表名放这里面,大写的表名。
            Console.WriteLine("数据表信息读取完毕，开始读取表数据并生成文件");
            string outPutPath = "E:\\output\\erp\\sql\\createFile\\";
            int currentPage = 1;
            int pageSize = 10000;
            //1.读出所有表
            //2.遍历所有表，读取表字段信息
            //3.读出表数据，转JToken，根据数组索引方式获取数据
            //4.生成sql语句。
            foreach (var table in Tables)
            {
                if (IgnoreTable.Contains(table.table_name))
                {
                    Console.WriteLine($"表{table.table_name}设置为不读取数据，跳过！");
                    continue;
                }
                Console.WriteLine($"开始读取表{table.table_name}的数据");
                var rowNum = GetTableRowsNum(table.table_name, sqlConnStr);
                if (rowNum < 1)
                {
                    Console.WriteLine($"表{table.table_name}拥有0条数据，跳过！");
                    continue;
                }
                Console.WriteLine($"表{table.table_name}拥有{rowNum}条数据");
                var TotalPages = rowNum / pageSize;
                if (rowNum % pageSize > 0)
                {
                    TotalPages += 1;
                }
                Console.WriteLine($"分为{TotalPages}次读取数据,每次读取{pageSize}条");
                var sb = new StringBuilder();
                bool havaTitle = false;
                for (var i = 0; i < TotalPages; i++)
                {
                    Console.WriteLine($"开始读取第{currentPage}次");
                    var list = GetList(table.table_name, sqlConnStr, currentPage, pageSize);
                    if (list.Count > 0)
                    {
                        List<TableColumns> columns = GetTableColumns(table.table_name, sqlConnStr);
                        var columnStr = "(";
                        foreach (var column in columns)
                        {
                            //if (column.data_type == "NUMBER")
                            //{
                            //    if (column.data_length == 1)
                            //    {
                            //        column.data_type = "NUMBER(1)";
                            //    }
                            //    column.data_type = MapCsharpType(column.data_type);
                            //}
                            columnStr += string.Format("{0}{1}", column.column_name, ",");
                        }
                        columnStr = string.Format("{0}{1}", columnStr.Remove(columnStr.LastIndexOf(","), 1), ")");



                        for (var j = 0; j < list.Count; j++)
                        {
                            var valueStr = "VALUES (";
                            var values = JsonConvert.DeserializeObject<JToken>(JsonConvert.SerializeObject(list[i]));
                            foreach (var column in columns)
                            {
                                var value = values[column.column_name].ToString();
                                if (column.data_type == "NUMBER")
                                {
                                    valueStr += string.Format("{0}{1}", value, ",");
                                }
                                else if (column.data_type == "DATE")
                                {
                                    valueStr += string.Format("{0}{1}", "TO_DATE('" + value + "', 'SYYYY/MM/DD HH24:MI:SS')", ",");
                                }
                                else
                                {
                                    valueStr += string.Format("{0}{1}", "'" + value + "'", ",");
                                }
                            }
                            valueStr = string.Format("{0}{1}", valueStr.Remove(valueStr.LastIndexOf(","), 1), ")");
                            if (!havaTitle)
                            {
                                sb.AppendLine(string.Format("INSERT ALL INTO {0} {1} {2}", table.table_name, columnStr, valueStr));
                                havaTitle = true;
                            }
                            else
                            {
                                sb.AppendLine(string.Format("           INTO {0} {1} {2}", table.table_name, columnStr, valueStr));
                            }
                        }

                    }
                    Console.WriteLine($"第{currentPage}次生成完毕");
                    currentPage += 1;
                }

                File.AppendAllText(Path.Combine(outPutPath, $"{table.table_name}.sql"), sb.ToString());
                sb.Clear();
                Console.WriteLine($"表{table.table_name}，数据生成完毕");
            }
            //2.生成文件并保存。 
        }

        /// <summary>
        /// 获取数据表名和备注
        /// </summary>
        /// <returns></returns>
        private static List<TableFileds> GetUserAllTables(string conn_str)
        {
            var sql = new StringBuilder();
            sql.Append("select a.TABLE_NAME, b.COMMENTS from user_tables a, user_tab_comments b");
            sql.Append(" where a.TABLE_NAME = b.TABLE_NAME order by TABLE_NAME");
            List<TableFileds> ret;
            using (var conn = GetOpenConnection(conn_str))
            {
                ret = conn.Query<TableFileds>(sql.ToString()).ToList();
            }

            return ret;
        }
        /// <summary>
        /// 获取到表的字段名，类型，长度和介绍
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static List<TableColumns> GetTableColumns(string tableName, string conn_str)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT a.column_name,a.data_type,a.data_length,b.comments,a.nullable");
            sql.Append(" FROM user_tab_columns a, user_col_comments b ");
            sql.Append(" WHERE a.table_name = b.table_name and b.column_name=a.column_name and a.table_name = :tableName");
            List<TableColumns> ret;
            using (var conn = GetOpenConnection(conn_str))
            {
                ret = conn.Query<TableColumns>(sql.ToString(), new { @tableName = tableName }).ToList();
            }

            return ret;
        }
        private static int GetTableRowsNum(string tableName, string conn_str)
        {
            var sql = new StringBuilder();
            sql.Append("select NUM_ROWS from all_tables where \"TABLE_NAME\"= :tableName");
            int ret;
            using (var conn = GetOpenConnection(conn_str))
            {
                ret = conn.QueryFirst<int>(sql.ToString(), new { @tableName = tableName });
            }

            return ret;
        }
        private static List<object> GetList(string tableName, string conn_str, int currentPage, int pageSize)
        {
            var startNum = (currentPage - 1) * pageSize;
            var endNum = currentPage * pageSize;
            var sql = string.Format("SELECT * FROM (SELECT t.*,ROWNUM AS rowno FROM {0} t WHERE ROWNUM <= {2}) table_alias WHERE table_alias.rowno > {1} ", tableName, startNum, endNum);
            using (var conn = GetOpenConnection(conn_str))
            {
                var list = conn.Query<object>(sql).ToList();
                return list;
            }
        }
        private static OracleConnection GetOpenConnection(string conn_str)
        {
            var connection = new OracleConnection(conn_str);
            connection.Open();
            return connection;
        }
        private static string MapCsharpType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return dbtype;
            string csharpType = "object";
            switch (dbtype)
            {
                case "BFILE": csharpType = "byte[]"; break;
                case "BLOB": csharpType = "byte[]"; break;
                case "CHAR": csharpType = "string"; break;
                case "CLOB": csharpType = "string"; break;
                case "DATE": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "LONG": csharpType = "string"; break;
                case "LONG RAW": csharpType = "byte[]"; break;
                case "INTEGER": csharpType = "decimal"; break;
                case "FLOAT": csharpType = "decimal"; break;
                case "NCHAR": csharpType = "string"; break;
                case "NCLOB": csharpType = "string"; break;
                case "NUMBER": csharpType = "decimal"; break;
                case "NVARCHAR2": csharpType = "string"; break;
                case "RAW": csharpType = "byte[]"; break;
                case "ROWID": csharpType = "string"; break;
                case "TIMESTAMP": csharpType = "DateTime"; break;
                case "VARCHAR2": csharpType = "string"; break;
                case "INTERVAL DAY TO  SECOND": csharpType = "TimeSpan"; break;
                case "INTERVAL YEAR TO  MONTH": csharpType = "int"; break;
                case "NUMBER(1)": csharpType = "bool"; break;
                default: csharpType = "object"; break;
            }
            return csharpType;
        }
        static ConsoleProgressBar GetProgressBar()
        {
            return new ConsoleProgressBar(System.Console.CursorLeft, System.Console.CursorTop, 50, ProgressBarType.Character);
        }
    }
}
