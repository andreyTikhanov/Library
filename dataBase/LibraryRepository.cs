using MySql.Data.MySqlClient;
using newTestLibrary.Model;
using System.Data;
using System.IO;
using System.Windows;

namespace newTestLibrary.dataBase
{
    public class LibraryRepository
    {
        private readonly string _fileConnection = "connectionString.txt";

        private static string readConnection(string fileConnection)
        {
            if(!File.Exists(fileConnection)) 
            {
                throw new Exception ("Connection file not found ");
            }
            string conn = File.ReadAllText(fileConnection);
            if(conn == null)
            {
                throw new Exception("Connection string is empty");
            }
            return conn;
        }
        private MySqlConnection _connection;
        public LibraryRepository()
        {
            _connection = new MySqlConnection(readConnection(_fileConnection));
        }
        private bool OpenConn()
        {
            try
            {
                _connection.Open();
                return true;
                
            }catch (Exception ex)
            {
                throw new Exception();
                
            }
        }
        private bool CloseConn()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void AddBook(Book book)
        {
            if (book == null) return;
            if (!OpenConn()) return;
            try {
                string query = "insert into books values (null, @tit, @aut, @dat, @gen)";
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@tit", book.Title);
                cmd.Parameters.AddWithValue("@aut", book.Author);
                cmd.Parameters.AddWithValue("@dat", book.Release);
                cmd.Parameters.AddWithValue("@gen", book.Genre);
                cmd.ExecuteNonQuery();
            }catch (Exception ex) { }
            finally { CloseConn(); }
        }
        public void DeleteBook(Book book)
        {
            if (book == null && !OpenConn()) return;
            try
            {
                string query = "delete from books where id = @id";
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", book.Id);
                cmd.ExecuteNonQuery();
            }catch { }
            finally { CloseConn(); }
        }
        public void AddUser(User user)
        {
            try
            {
                if (user == null || !OpenConn()) return;
                string query = "insert into users values (null, @name, @pass, @phone)";
                MySqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@pass", user.Password);
                cmd.Parameters.AddWithValue("@phone", user.Phone);
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally { CloseConn(); }
        }
        public User GetUser(string name, string password)
        {
            User user = null;
            string query = "select * from users where name = @name && pass = @pass";
            MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.ExecuteNonQuery();
            using(MySqlDataReader  reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new User();
                    user.Id=reader.GetInt32(0);
                    user.Name=reader.GetString(1);
                    user.Password=reader.GetString(2);
                    user.Phone=reader.GetString(3);
                }
                reader.Close();
            }
            return user;
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                if (!OpenConn()) return null;
                string query = "SELECT * FROM users";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User u = new User()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Password = reader.GetString(2),
                            Phone = reader.GetString(3),
                        };
                        users.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении пользователей: {ex.Message}");
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return users;
        }


    }
}

