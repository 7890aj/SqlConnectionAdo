using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SqlConnectionAdo.Models
{
    public class EmployeeDBContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public List<Employee> GetEmployee()
        {   //cmd.ExecuteReader() =use only select query, cmd.execute non query = use 1-insert,update,delete   Execute Scaler = use only aggrigate function 1-count(),Min,Max(),Avg(),Sum()
            //sql command 3 properties 1-Command text 2-Connection 3-CommandType
            List<Employee> EmployeesList = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            //string query = "select*from tbl_Emoloyee ";
            string query = "SP_GetEmployees";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = query;
            //cmd.Connection = con;
            con.Open();
            SqlDataReader dr=cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.Id =Convert.ToInt32(dr.GetValue(0).ToString());
                emp.Name = dr.GetValue(1).ToString();
                emp.Gender = dr.GetValue(2).ToString();
                emp.Salary =Convert.ToInt32( dr.GetValue(3).ToString());
                emp.Age =Convert.ToInt32(dr.GetValue(4).ToString());
                emp.City = dr.GetValue(5).ToString();
                EmployeesList.Add(emp);
            }
            con.Close();
            return EmployeesList; 
        }


        public bool AddEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SP_AddEmployee",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name",emp.Name);
            cmd.Parameters.AddWithValue("@Gender",emp.Gender);
            cmd.Parameters.AddWithValue("@Age",emp.Age);
            cmd.Parameters.AddWithValue("@Salary",emp.Salary);
            cmd.Parameters.AddWithValue("@City",emp.City);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Gender", emp.Gender);
            cmd.Parameters.AddWithValue("@Age", emp.Age);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@City", emp.City);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteEmployee(int id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SP_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // use aggrigate function 
        public bool EmployeeMaxSallary(int id)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select count(Salary) from tbl_Emoloyee";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i =(int) cmd.ExecuteScalar();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //sql data reader class = its is read only and forword only
        //SqlDataReader dr = cmd.ExecuteReader();
        public List<Employee> Employeesgetmethode()
        {
            List<Employee> EmployeesList = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from tbl_Emoloyee";
            SqlCommand cmd = new SqlCommand(query,con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())//proerty of data reader 1-dr.Read(),2-dr.Hasrows dr.iscosed return true or false
            {
                Employee emp = new Employee();
                emp.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                emp.Name = dr.GetValue(1).ToString();
                emp.Gender = dr.GetValue(2).ToString();
                emp.Salary = Convert.ToInt32(dr.GetValue(3).ToString());
                emp.Age = Convert.ToInt32(dr.GetValue(4).ToString());
                emp.City = dr.GetValue(5).ToString();
                EmployeesList.Add(emp);
            }
            con.Close();
            return EmployeesList;
        }


        //get data from two tables
        //public List<Employee> GetDataFromTWoTables()
        //{
        //    List<Employee> EmployeesList = new List<Employee>();
        //    SqlConnection con = new SqlConnection(cs);
        //    string query = "select * from tbl_Emoloyee; select* from tbl_Student";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();

        //    while (dr.Read())//proerty of data reader 1-dr.Read(),2-dr.Hasrows dr.iscosed return true or false
        //    {
        //        Employee emp = new Employee();
        //        emp.Id = Convert.ToInt32(dr.GetValue(0).ToString());
        //        emp.Name = dr.GetValue(1).ToString();
        //        emp.Gender = dr.GetValue(2).ToString();
        //        emp.Salary = Convert.ToInt32(dr.GetValue(3).ToString());
        //        emp.Age = Convert.ToInt32(dr.GetValue(4).ToString());
        //        emp.City = dr.GetValue(5).ToString();
        //        EmployeesList.Add(emp);
        //    }
        //    if (dr.NextResult())
        //    {
        //        while (dr.Read())
        //        {
        //            Student std = new Student();
        //            std.Id = Convert.ToInt32(dr.GetValue(0).ToString());
        //            std.Name = dr.GetValue(1).ToString();
        //            std.Standard = dr.GetValue(2).ToString();
        //            std.FName = (dr.GetValue(3).ToString());
        //            std.Age = Convert.ToInt32(dr.GetValue(4).ToString());
        //            std.city = dr.GetValue(5).ToString();
        //        }
        //    }
        //    con.Close();
        //    return EmployeesList;
        //}


        //Sql data adapter Disconnected Data access in this have two class 1-DataSet 2-DataTable
        //1-DataSet -*-use in multiple table on workd and relations tables  * sql command not use in Sql Data Adapter
        //2-DataTable- use in working single table
        public List<Employee> dataAdaptergetdata()
        {
            List<Employee> EmpList = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from tbl_Emoloyee";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Employee emp = new Employee();
                
            }
            return EmpList;
        }

        //next tutorial reaf sql data adapter
    }
}