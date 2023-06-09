﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms.DataVisualization.Charting;

namespace Milestone3
{
    public partial class FanRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerFan(object sender, EventArgs e)
        {
            String conStr = WebConfigurationManager.ConnectionStrings["Sports"].ToString();

            SqlConnection conn = new SqlConnection(conStr);

            conn.Open();

            String na = fanName.Text;
            String us = fanUsername.Text;
            String pass = fanPassword.Text;
            String nat = fanNat.Text;
            String phone = fanPhone.Text;
            String bd = fanBirthday.Text;
            String address = fanAddress.Text;

            HtmlGenericControl lbl = new HtmlGenericControl("div");
            lbl.InnerText = "Please enter valid data.";
            if (na == "" || us == "" || pass == "" || nat == "" ||
                phone == "" || bd == "" || address == "")
            {
                fID.Controls.Add(lbl);
            }
            else
            {
                SqlCommand addFan = new SqlCommand("addFan", conn);
                addFan.CommandType = CommandType.StoredProcedure;
                addFan.Parameters.Add(new SqlParameter("@fan_name", na));
                addFan.Parameters.Add(new SqlParameter("@username", us));
                addFan.Parameters.Add(new SqlParameter("@password", pass));
                addFan.Parameters.Add(new SqlParameter("@national_id", nat));
                addFan.Parameters.Add(new SqlParameter("@birthdate", bd));
                addFan.Parameters.Add(new SqlParameter("@address", address));
                addFan.Parameters.Add(new SqlParameter("@phone_number", phone));


                addFan.ExecuteNonQuery();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT national_id FROM fan WHERE username = 'x'".Replace("x", us);

                String result = cmd.ExecuteScalar().ToString();

                Session["fan"] = result;
                Response.Redirect("loginPage.aspx");

            }
            conn.Close();
        }

       
    }
}

