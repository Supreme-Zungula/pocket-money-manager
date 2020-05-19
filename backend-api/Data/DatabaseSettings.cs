using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
     public class DatabaseSettings 
    {
        public static string ConnectionString 
        { 
            set {} 
            get { return "mongodb://localhost:27017"; }
        }
        public static string DatabaseName 
        {
            set {} 
            get { return "PocketMoneyDB"; } 
        }
        //public static string CollectionName { get; set; }
    }
/* 
    public interface IDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    } */
}
