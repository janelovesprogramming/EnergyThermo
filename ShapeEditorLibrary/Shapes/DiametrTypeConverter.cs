using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace ShapeEditorLibrary.Shapes
{
    class DiametrTypeConverter : StringConverter
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;

        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // false - можно вводить вручную// true - только выбор из спискаreturntrue;

            return true;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            try
            {
                List<string> myList = new List<string>();
                        // PostgeSQL-style connection string
                string connstring = String.Format("Server = 127.0.0.1; Port = 5432; User Id = postgres; Password =; Database = energy_thermo; ");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "select * from steel_tubes";
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];


                for (int i = 0; i < dt.Rows.Count; i++)
                    myList.Add(dt.Rows[i].Field<int>("dy").ToString());
                StandardValuesCollection svc = new StandardValuesCollection(myList);
                // since we only showing the result we don't need connection anymore
                conn.Close();
                return new StandardValuesCollection(svc);
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                throw;
            }
            
            
            

        }
    }
}
