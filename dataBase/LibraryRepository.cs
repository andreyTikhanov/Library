using MySql.Data.MySqlClient;
using newTestLibrary.Model;
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
                MessageBox.Show("Ok");
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
    }
}
