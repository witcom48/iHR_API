using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC
{
    public class Config
    {

        static public string Database = "HRM";
        static public string Userid = "sa";
        static public string Server = ".\\SQLEXPRESS";
        static public string Password = "Sql2019*";
        //static public string Database = "HRM";
        //static public string Userid = "sa";

        //static public string Server = ".\\SQLEXPRESS";
        //static public string Password = "Sql2019*";


        //static public string Server = "DESKTOP-8DH5RMN";
        //static public string Password = "Sql2019*";

        static public string FormatDateSQL = "MM/dd/yyyy";

        static public string PathFileImport = "D:\\Temp\\HR365";
        static public string PathFileExport = "D:\\Temp\\HR365\\Export";
        
    }
}
