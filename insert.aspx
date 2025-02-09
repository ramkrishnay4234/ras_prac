<%@ Page Title="" Language="C#" MasterPageFile="~/ras.Master" AutoEventWireup="true" CodeBehind="insert.aspx.cs" Inherits="ras_prac.insert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="container-fluid">
     <div class="container">
         <div class="row">
             <div class="col-md-6" style="align-content: center;">
                 <asp:TextBox ID="Fname" Class="input" runat="server" Required="true" placeholder="Enter full name"></asp:TextBox><br />
                 <asp:FileUpload ID="imagepath" runat="server" CssClass="input" /><br />
                 <br />
                 <div>
                     <asp:Button ID="Button1" CssClass="btn btn-sm btn-success" runat="server" Text="Update" OnClick="Button1_Click" required="True" />
                 </div>
             </div>
             <div class="col-md-6">
                 <asp:GridView ID="FormDataGrid" runat="server" DataKeyNames="id" OnRowDeleting="FormDataGrid_RowDeleting" AutoGenerateColumns="False" class="table table-hover">
                     <Columns>
                         <asp:BoundField DataField="fname" HeaderText="Full Name" SortExpression="fname" />
                         <asp:TemplateField HeaderText="CEO/Founder Image">
                             <ItemTemplate>
                                 <img src='<%# ResolveUrl(Eval("image").ToString()) %>' alt="Image" style="width: 100px; height: auto;" />
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:CommandField ControlStyle-CssClass="btn btn-danger" ShowDeleteButton="True" HeaderText="Delete" DeleteText="Delete" ButtonType="Button" />
                     </Columns>
                 </asp:GridView>
             </div>
         </div>
     </div>
 </div>

</asp:Content>
