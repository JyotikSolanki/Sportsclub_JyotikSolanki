using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Sportsclub.Models
{
    public class MemberModel
    {
        // Database connection string
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Database1;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        // Properties representing member data with validation attributes
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your First Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Phone Number.")]
        [Phone(ErrorMessage = "Please Enter a Valid Phone Number.")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email.")]
        [EmailAddress(ErrorMessage = "Please Enter a Valid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Date of Birth.")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please Select Your Gender.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Select a Time.")]
        public string Time { get; set; }

        [Required(ErrorMessage = "Please Enter Emergency Contact Name.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please Enter Relationship.")]
        public string Relationship { get; set; }

        [Required(ErrorMessage = "Please Enter Emergency Contact Number.")]
        [Phone(ErrorMessage = "Please Enter a Valid Phone Number.")]
        public string ContactNumber { get; set; }

        // Method to insert member data into the database
        public bool insert(MemberModel member)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Member_Form (firstname, lastname, phoneno, email, dob, gender, time, contactname, relationship, contactnumber) VALUES (@firstname, @lastname, @phoneno, @email, @dob, @gender, @time, @contactname, @relationship, @contactnumber)", con))
                    {
                        cmd.Parameters.AddWithValue("@firstname", member.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", member.LastName);
                        cmd.Parameters.AddWithValue("@phoneno", member.PhoneNo);
                        cmd.Parameters.AddWithValue("@email", member.Email);
                        cmd.Parameters.AddWithValue("@dob", member.DOB);
                        cmd.Parameters.AddWithValue("@gender", member.Gender);
                        cmd.Parameters.AddWithValue("@time", member.Time);
                        cmd.Parameters.AddWithValue("@contactname", member.ContactName);
                        cmd.Parameters.AddWithValue("@relationship", member.Relationship);
                        cmd.Parameters.AddWithValue("@contactnumber", member.ContactNumber);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        return i >= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                Console.WriteLine("An error occurred while inserting member data: " + ex.Message);
                return false;
            }
        }
    }
}
