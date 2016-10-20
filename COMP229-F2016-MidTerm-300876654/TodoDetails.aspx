<%@ Page Title="Todo Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoDetails.aspx.cs" Inherits="COMP229_F2016_MidTerm_300876654.TodoDetails" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Todo List Details</h1>
                <h5>All Fields are required</h5>
                <br />

                <div class="form-group">
                    <label class="control-label" for="TodoDescriptionTextBox">Description</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoDescriptionTextBox"
                        placeholder="Description" required="true"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label class="control-label" for="TodoNotesTextBox">Notes</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNotesTextBox"
                        placeholder="Notes" required="true"></asp:TextBox>
                </div>

            
                <div class="form-group"> 
                    <label class="control-label" for="TodoCompletedCheckbox">Completed</label>
                    <asp:CheckBox  runat="server" CssClass="form-control" ID="TodoCompletedCheckbox"
                        required="true"></asp:Checkbox></div>
      

                

                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server"
                        UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                    <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server"
                        OnClick="SaveButton_Click" />
                </div>
            </div>
        </div>
    </div> 

</asp:Content>
