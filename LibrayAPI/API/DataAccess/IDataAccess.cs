using API.Models;
using System.Data;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;

namespace API.DataAccess
{
    public interface IDataAccess
    {
        int CreateUser(User user);
        bool IsEmailAvailable(string email);
        bool AuthenticateUser(string email, string password, out User? user);

        public IList<Book> GetAllBooks()
        {
            IEnumerable<Book> books = null;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = "select * from Books;";
                books = connection.Query<Book>(sql);

                foreach (var book in books)
                {
                    sql = "select * from BookCategories where Id=" + book.CategoryId;
                    book.Category = connection.QuerySingle<BookCategory>(sql);
                }
            }

            return books.ToList();
        }

        bool OrderBook(int userId, int bookId);
    }
}


