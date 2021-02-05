using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Task2
{
    public partial class _Default : Page
    {
        public static List<Books> BooksList = new List<Books>();

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = BooksList;
            if (!IsPostBack)
                GridView1.DataBind();
        }

        public class Books
        {
            public string Name { get; set; }
            public string Author { get; set; }

            public Books(string name, string author)
            {
                this.Name = name;
                this.Author = author;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var newBook = new Books(Convert.ToString(booksName.Text), Convert.ToString(booksAuthor.Text));
            BooksList.Add(newBook);


            GridView1.DataSource = BooksList;
            GridView1.DataBind();

            booksName.Text = string.Empty;
            booksAuthor.Text = string.Empty;
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var bookForUpdate = BooksList[e.RowIndex];
            if (bookForUpdate != null)
            {
                var row = GridView1.Rows[e.RowIndex];
                var bookName = (row.FindControl("TextBox1") as TextBox).Text;
                var bookAuthor = (row.FindControl("TextBox2") as TextBox).Text;

                bookForUpdate.Author = (string)bookAuthor;
                bookForUpdate.Name = (string)bookName;
            }
            
            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BooksList.Remove(BooksList[e.RowIndex]);

            GridView1.DataSource = BooksList;
            GridView1.DataBind();
        }
    }
}