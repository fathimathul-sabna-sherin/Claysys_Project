using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using CrudAsp.Models;
using System.Data;

namespace CrudAsp.Database
{
    public class Signup_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["aspConnectionstring"].ToString();

        //Get all signup details
        public List<Signup> GetAllDetails()
        {
            List<Signup> signupList = new List<Signup>();

                using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spMVCSignup";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtSignup = new DataTable();

                con.Open();
                da.Fill(dtSignup);
                con.Close();

                foreach (DataRow dr in  dtSignup.Rows)
                {
                    signupList.Add(new Signup
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date,                     
                        Phone = dr["Phone"].ToString(),
                        Email = dr["Email"].ToString(),
                        Address = dr["Address"].ToString()

                    }) ;
                }


            }

                return signupList;
        }

        //Insert employee
        public bool InsertEmployee(Signup signup)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spMVCSignupInsert",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", signup.FirstName);
                cmd.Parameters.AddWithValue("@LastName", signup.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", signup.DateOfBirth);
                cmd.Parameters.AddWithValue("@Phone", signup.Phone);
                cmd.Parameters.AddWithValue("@Email", signup.Email);
                cmd.Parameters.AddWithValue("@Address", signup.Address);

                con.Open();
                id = cmd.ExecuteNonQuery();
                con.Close();

            }
            if(id>0)
            {
                return true;
            }
            else
            { 
                return false; 
            }
        }

        //Get signup details by id
        public List<Signup> GetEmployeeById(int Id)
        {
            List<Signup> signupList = new List<Signup>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spMVCGetAllSignup";
                cmd.Parameters.AddWithValue("@Id",Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtSignup = new DataTable();

                con.Open();
                da.Fill(dtSignup);
                con.Close();

                foreach (DataRow dr in dtSignup.Rows)
                {
                    signupList.Add(new Signup
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date,
                        Phone = dr["Phone"].ToString(),
                        Email = dr["Email"].ToString(),
                        Address = dr["Address"].ToString()

                    });
                }


            }

            return signupList;
        }

        //Update employee
        public bool UpdateEmployee(Signup signup)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spMVCSignupUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", signup.Id);
                cmd.Parameters.AddWithValue("@FirstName", signup.FirstName);
                cmd.Parameters.AddWithValue("@LastName", signup.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", signup.DateOfBirth);
                cmd.Parameters.AddWithValue("@Phone", signup.Phone);
                cmd.Parameters.AddWithValue("@Email", signup.Email);
                cmd.Parameters.AddWithValue("@Address", signup.Address);

                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();

            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Employee
        public string DeleteEmployee(int id)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spMVCDeleteEmployee", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters.Add("@OutputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@OutputMessage"].Value.ToString();
                con.Close();

            }
            return result;
        }


    }

}