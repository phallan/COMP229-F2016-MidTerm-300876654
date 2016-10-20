using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using COMP229_F2016_MidTerm_300876654.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP229_F2016_MidTerm_300876654
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoId"; // default sort column
                Session["SortDirection"] = "ASC";

                // Get the student data
                this.GetTodo();

            }
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int selectedRow = e.RowIndex;

           
            int todoId = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoId"]);

           
            using (TodoContext db = new TodoContext())
            {
               
                Todo deletedTodo = (from todoR in db.Todos
                                          where todoR.TodoId == todoId
                                          select todoR).FirstOrDefault();

              
                db.Todos.Remove(deletedTodo);

              
                db.SaveChanges();

              
                this.GetTodo();
            }


        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the Gridview
            this.GetTodo();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {// get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // refresh the GridView
            this.GetTodo();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";

        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < TodoGridView.Columns.Count - 1; index++)
                    {
                        if (TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
        private void GetTodo()
        {
            using (TodoContext db = new TodoContext())
            {
                string SortString = Session["SortColumn"].ToString() + " " +
                    Session["SortDirection"].ToString();

                // query the Student Table using EF and LINQ
                var todos = (from todo in db.Todos
                                select todo);

                // bind the result to the Students GridView
                TodoGridView.DataSource = todos.AsQueryable().OrderBy(SortString).ToList();
                TodoGridView.DataBind();
            }
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the new Page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the GridView
            this.GetTodo();
        }
    }
}