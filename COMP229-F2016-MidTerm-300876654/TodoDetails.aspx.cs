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
                // populate a student object instance with the StudentID from 
                // the URL parameter
               Todo updatedTodo = (from x in db.Todos
                                          where x.TodoId== TodoId
                                          select x).FirstOrDefault();

                // map the student properties to the form control
                if (updatedTodo != null)
                {
                    TodoDescriptionTextBox.Text = updatedTodo.TodoDescription;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                 //   TodoCompletedCheckbox.Text = updatedTodo.Completed.ToString();
               
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
                // use the student model to create a new student object and 
                // save a new record

                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // our URL has a STUDENTID in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoId"]);

                    // get the current student from EF db
                    newTodo = (from x in db.Todos
                                  where x.TodoId == TodoID
                                  select x).FirstOrDefault();
                }

                // add form data to the new student record
                newTodo.TodoDescription = TodoDescriptionTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
               //  newTodo.Completed = Convert.ToBoolean(TodoCompletedCheckbox);
          

                // use LINQ to ADO.NET to add / insert new student into the db

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated students page
                Response.Redirect("TodoList.aspx");
            }
        }
    }

    }
