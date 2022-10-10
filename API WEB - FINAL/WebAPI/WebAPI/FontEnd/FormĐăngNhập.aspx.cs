﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data;

namespace WebApplication1
{
    public partial class FormĐăngNhập : System.Web.UI.Page
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {

                HttpCookie cookie = Request.Cookies["userlogin"];
                if(cookie != null)
                {
                    Response.Redirect("index.html", false);                    
                }
                   
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //Move to Register Page if do not have user and pass 
            Response.Redirect("FormĐăngkí.aspx");
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("userlogin");
            cookie["Username"] = txtUserAccount.Text;
            cookie["Password"] =  txtPasswordAccount.Text;
            Response.Cookies.Add(cookie);

            


            // Connect to database
            String connStr = @"Data Source=LOCNE\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);


            try
            {
                // Query to database
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                // Using StoreProcedure and add Parameter for store sp
                cmd = new SqlCommand("sp_LoginCheck", conn);
                cmd.Parameters.AddWithValue("@user", txtUserAccount.Text);
                cmd.Parameters.AddWithValue("@password", txtPasswordAccount.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                // Check result and Show box
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    txtPasswordAccount.Text = txtUserAccount.Text = "";
                    Response.Redirect("index.html", false);


                    cookie.Expires = DateTime.Now.AddDays(30);


                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}