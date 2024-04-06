using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Sportsclub.Models
{
    public class ContactModel
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\jyotik s\\source\\repos\\Sportsclub_new\\Sportsclub\\App_Data\\Database.mdf\";Integrated Security=True";

        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email.")]
        [EmailAddress(ErrorMessage = "Please Enter a Valid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Phone Number.")]
        [Phone(ErrorMessage = "Please Enter a Valid Phone Number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please Enter Your Subject.")]
        public string Subject { get; set; }

        public string Message { get; set; }

        public List<ContactModel> GetData()
        {
            List<ContactModel> lstCont = new List<ContactModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM Contact_Us", con))
                {
                    DataSet ds = new DataSet();
                    apt.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lstCont.Add(new ContactModel
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["name"].ToString(),
                            Email = dr["email"].ToString(),
                            Phone = dr["pnumber"].ToString(),
                            Subject = dr["subject"].ToString(),
                            Message = dr["message"].ToString(),

                        });
                    }
                }
            }
            return lstCont;
        }

        public ContactModel GetData(int id)
        {
            ContactModel cont = new ContactModel();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Contact_Us WHERE Id = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cont.Id = Convert.ToInt32(dr["Id"]);
                            cont.Name = dr["name"].ToString();
                            cont.Email = dr["email"].ToString();
                            cont.Phone = dr["pnumber"].ToString();
                            cont.Subject = dr["subject"].ToString();
                            cont.Message = dr["message"].ToString();
                        }
                    }
                }
            }
            return cont;
        }

        public bool Insert(ContactModel Cont)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Contact_Us (name, email, pnumber, subject, message) VALUES (@Name, @Email, @Phone, @Subject, @Message)", con))
                {
                    cmd.Parameters.AddWithValue("@Name", Cont.Name);
                    cmd.Parameters.AddWithValue("@Email", Cont.Email);
                    cmd.Parameters.AddWithValue("@Phone", Cont.Phone);
                    cmd.Parameters.AddWithValue("@Subject", Cont.Subject);
                    cmd.Parameters.AddWithValue("@Message", Cont.Message);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i >= 1;
                }
            }
        }
    }
}
