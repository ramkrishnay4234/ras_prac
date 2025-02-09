using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace ras_prac
{
    public partial class insert : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }
        private void BindGridView()
        {
            string query = "SELECT * FROM dummy_image";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                FormDataGrid.DataSource = dt;
                FormDataGrid.DataBind();
            }
        }
        private string SaveImage(HttpPostedFile file)
        {
            string folderPath = Server.MapPath("~/images/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folderPath, fileName);
            file.SaveAs(filePath);
            return "~/images/" + fileName;
        }
        private void SaveFormData(string fname, string image)
        {
            string query = "INSERT INTO dummy_image (fname, image) " +
                           "VALUES (@fname, @image)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@image", image);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fname = Fname.Text;
            string image = SaveImage(imagepath.PostedFile);
            SaveFormData(fname, image);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void FormDataGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(FormDataGrid.DataKeys[e.RowIndex].Value);
            string query = "DELETE FROM dummy_image WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            BindGridView();
        }
    }
}