using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigEMRCheckUp
{
    public class ConfigCls
    {
        //Config เครื่อง Production
       //public const string PathwayConnString = "Data Source=10.88.17.43;Initial Catalog=PathWay;Persist Security Info=True;User ID=sa;Password=sa1234";
       //public const string TodolistConnString = "Data Source=10.88.17.43;Initial Catalog=Todolist;Persist Security Info=True;User ID=sa;Password=sa1234";
        //Config เครื่อง Production End

        ////Config เครื่อง UAT
        public const string PathwayConnString = @"Data Source=10.88.190.147;Initial Catalog=PathWay;Persist Security Info=True;User ID=sa;Password=p0werDba";
        public const string TodolistConnString = @"Data Source=10.88.190.147;Initial Catalog=Todolist;Persist Security Info=True;User ID=sa;Password=p0werDba";
        ////Config เครื่อง UAT End
    }
}
