﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Milestone3
{
    public partial class AssocRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register(object sender, EventArgs e)
        {
            String conStr = WebConfigurationManager.ConnectionStrings["Sports"].ToString();

            SqlConnection conn = new SqlConnection(conStr);

            conn.Open();

            String na = name.Text;
            String us = username.Text;
            String pass = password.Text;

            SqlCommand addAssoc = new SqlCommand("addAssociationManager", conn);
            addAssoc.CommandType = CommandType.StoredProcedure;
            addAssoc.Parameters.Add(new SqlParameter("@name", na));
            addAssoc.Parameters.Add(new SqlParameter("@username", us));
            addAssoc.Parameters.Add(new SqlParameter("@password", pass));


            addAssoc.ExecuteNonQuery();
            conn.Close();

            //Response.Redirect("AssocActions.aspx");

        }

        
    }
}