using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP229_F2016_MidTerm_300876654.Models;

namespace COMP229_F2016_MidTerm_300876654
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodo();
            }
        }

        protected void GetTodo()
        {
            // populate the form with existing data from db
            int TodoId = Convert.ToInt32(Request.QueryString["TodoId"]);

            // connect to the EF DB
            using (TodoContext db = new TodoContext())
            {
                // get todolist data from the query string
               Todo updatedTodo = (from x in db.Todos
                                          where x.TodoId== TodoId
                                          select x).FirstOrDefault();

             //if not emptyfill in the previous data
                if (updatedTodo != null)
                {
                    TodoDescriptionTextBox.Text = updatedTodo.TodoDescription;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                   TodoCompletedCheckbox.Text = updatedTodo.Completed.ToString();
               
                }
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            using (TodoContext db = new TodoContext())
            {
                // create new todo object
                // save a new record

                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // resulting query  has id in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoId"]);

                    // get the todo 
                    newTodo = (from x in db.Todos
                                  where x.TodoId == TodoID
                                  select x).FirstOrDefault();
                }

                // add new todo record
                newTodo.TodoDescription = TodoDescriptionTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
                newTodo.Completed = Convert.ToBoolean(TodoCompletedCheckbox);
          

                // using linq to add

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // save our change
                db.SaveChanges();

                // Redirect back to the updated todo page
                Response.Redirect("TodoList.aspx");
            }
        }
    }

    }
