using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Sportsclub.Models
{
    public class Signupmodel
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\jyotik s\\source\\repos\\Sportsclub_new\\Sportsclub\\App_Data\\Database.mdf\";Integrated Security=True";

        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name.")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Please Enter Your last name.")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email.")]
        [EmailAddress(ErrorMessage = "Please Enter a Valid Email Address.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Passwrod must be same as Password.")]
        public string confirmpassword { get; set; }

        public List<Signupmodel> GetData()
        {
            List<Signupmodel> lstSign = new List<Signupmodel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM sign_in", con))
                    {
                        DataSet ds = new DataSet();
                        apt.Fill(ds);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            lstSign.Add(new Signupmodel
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                firstname = dr["firstname"].ToString(),
                                lastname = dr["lastname"].ToString(),
                                email = dr["email"].ToString(),
                                password = dr["password"].ToString(),
                                confirmpassword = dr["confirmpassword"].ToString(),

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
            }
            return lstSign;
        }

        public Signupmodel GetData(int id)
        {
            Signupmodel sign = new Signupmodel();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM sign_in WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                sign.Id = Convert.ToInt32(dr["Id"]);
                                sign.firstname = dr["firstname"].ToString();
                                sign.lastname = dr["lastname"].ToString();
                                sign.email = dr["email"].ToString();
                                sign.password = dr["password"].ToString();
                                sign.confirmpassword = dr["confirmpassword"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
            }
            return sign;
        }

        public bool Insert(Signupmodel Sign)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO sign_in (firstname, lastname, email, password, confirmpassword) VALUES (@firstname, @lastname, @email, @password, @confirmpassword)", con))
                    {
                        cmd.Parameters.AddWithValue("@firstname", Sign.firstname);
                        cmd.Parameters.AddWithValue("@lastname", Sign.lastname);
                        cmd.Parameters.AddWithValue("@email", Sign.email);
                        cmd.Parameters.AddWithValue("@password", Sign.password);
                        cmd.Parameters.AddWithValue("@confirmpassword", Sign.confirmpassword);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        return i >= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting data: " + ex.Message);
                return false;
            }
        }
    }
}
