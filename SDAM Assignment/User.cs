using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SDAM_Assignment
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }

        protected static string connectionString = "server=localhost;user=root;password=;database=marketplace;";

        public User() { }

        public virtual void OpenDashboard()
        {
            MessageBox.Show("Opening default user dashboard...");
        }

        public static List<User> LoadUsersByRole(string role)
        {
            List<User> users = new List<User>();

            using (var conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name, email, phone_no, role FROM users WHERE role = @r";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@r", role);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string userRole = reader.GetString("role");

                        if (userRole == "Admin")
                        {
                            users.Add(new Admin
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Email = reader.GetString("email"),
                                Phone = reader.GetString("phone_no"),
                                Role = userRole
                            });
                        }
                        else if (userRole == "Seller")
                        {
                            users.Add(new Seller
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Email = reader.GetString("email"),
                                Phone = reader.GetString("phone_no"),
                                Role = userRole
                            });
                        }
                        else if (userRole == "Buyer")
                        {
                            users.Add(new Buyer
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Email = reader.GetString("email"),
                                Phone = reader.GetString("phone_no"),
                                Role = userRole
                            });
                        }
                        else
                        {
                            users.Add(new User
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Email = reader.GetString("email"),
                                Phone = reader.GetString("phone_no"),
                                Role = userRole
                            });
                        }
                    }
                }
            }

            return users;
        }

        public static User Login(string email, string password, string role)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM users WHERE email = @em AND password = @pw AND role = @role";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@em", email);
                cmd.Parameters.AddWithValue("@pw", password);
                cmd.Parameters.AddWithValue("@role", role);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user;
                        switch (role.ToLower())
                        {
                            case "admin":
                                user = new Admin();
                                break;
                            case "seller":
                                user = new Seller();
                                break;
                            case "buyer":
                                user = new Buyer();
                                break;
                            default:
                                return null;
                        }

                        user.Id = Convert.ToInt32(reader["id"]);
                        user.Name = reader["name"].ToString();
                        user.Email = reader["email"].ToString();
                        user.Phone = reader["phone_no"].ToString();
                        user.Role = role;
                        return user;
                    }
                }
            }

            return null;
        }

        public static string Register(string name, string email, string phone, string role, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                return "Please fill in all fields.";
            }

            if (password != confirmPassword)
            {
                return "Passwords do not match.";
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                // Check if email already exists
                string checkQuery = "SELECT COUNT(*) FROM users WHERE email = @e";
                using (var checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@e", email);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        return "Email already registered. Please use a different one.";
                    }
                }

                // Insert user
                string insertQuery = "INSERT INTO users (name, email, phone_no, role, password) " +
                                     "VALUES (@name, @email, @phone, @role, @password)";
                using (var cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@role", role.ToLower());
                    cmd.Parameters.AddWithValue("@password", password); // 🔐 Note: No encryption used

                    cmd.ExecuteNonQuery();
                }

                return "success";
            }
        }
    }
}

