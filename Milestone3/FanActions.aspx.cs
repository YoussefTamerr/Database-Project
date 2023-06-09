﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace Milestone3
{
    public partial class fan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logOut(object sender, EventArgs e)
        {
            Response.Redirect("loginPage.aspx");
        }

        protected void availableTicket(object sender, EventArgs e)
        {
            String conStr = WebConfigurationManager.ConnectionStrings["Sports"].ToString();

            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();

            HtmlGenericControl titlesRow = new HtmlGenericControl("div");

            HtmlGenericControl t1 = new HtmlGenericControl("span");
            HtmlGenericControl t2 = new HtmlGenericControl("span");
            HtmlGenericControl t3 = new HtmlGenericControl("span");
            HtmlGenericControl t4 = new HtmlGenericControl("span");

            HtmlGenericControl rowsContainer = new HtmlGenericControl("div");


            t1.InnerText = "Host";
            t2.InnerText = "Guest";
            t3.InnerText = "Stadium";
            t4.InnerText = "Location";

            titlesRow.Controls.Add(t1);
            titlesRow.Controls.Add(t2);
            titlesRow.Controls.Add(t3);
            titlesRow.Controls.Add(t4);

            rowsContainer.Controls.Add(titlesRow);

            String date = dtF.Text;

            HtmlGenericControl lbl1 = new HtmlGenericControl("div");
            lbl1.InnerText = "Please enter valid data.";

            if (date == "" )
            {
                AvailableTicketsContainer.Controls.Add(lbl1);
            }
            else
            {


                String srr = "SELECT DISTINCT c1.name host, c2.name guest, s.name stadium, s.location FROM Match m INNER JOIN Ticket t ON t.match_id = m.match_id INNER JOIN Stadium s ON m.stadium_id = s.sid INNER JOIN Club c1 ON c1.club_id = host_club_id INNER JOIN Club c2 ON c2.club_id = guest_club_id WHERE t.status = 1 AND 'x' < m.start_time AND CURRENT_TIMESTAMP < m.start_time\r\n".Replace("x", date);

                SqlCommand rr = new SqlCommand(srr, conn);
                rr.CommandType = CommandType.Text;
                SqlDataReader rdr = rr.ExecuteReader();
                while (rdr.Read())
                {
                    HtmlGenericControl tmp = new HtmlGenericControl("div");

                    String host = rdr.GetString(rdr.GetOrdinal("host"));
                    String guest = rdr.GetString(rdr.GetOrdinal("guest"));
                    String stadium = rdr.GetString(rdr.GetOrdinal("stadium"));
                    String location = rdr.GetString(rdr.GetOrdinal("location"));

                    HtmlGenericControl h = new HtmlGenericControl("div");
                    HtmlGenericControl g = new HtmlGenericControl("div");
                    HtmlGenericControl s = new HtmlGenericControl("div");
                    HtmlGenericControl l = new HtmlGenericControl("div");

                    h.InnerText = host;
                    g.InnerText = guest;
                    s.InnerText = stadium;
                    l.InnerText = location;

                    tmp.Controls.Add(h);
                    tmp.Controls.Add(g);
                    tmp.Controls.Add(s);
                    tmp.Controls.Add(l);

                    rowsContainer.Controls.Add(tmp);

                }
                AvailableTicketsContainer.Controls.Add(rowsContainer);
                rdr.Close();
                rr.ExecuteNonQuery();
            }
            conn.Close();
        }

        protected void purchaseTicket(object sender, EventArgs e)
        {
            String conStr = WebConfigurationManager.ConnectionStrings["Sports"].ToString();

            SqlConnection conn = new SqlConnection(conStr);

            conn.Open();

            String id = Session["fan"].ToString();
            String host = hostFan.Text;
            String guest = guestFan.Text;

            HtmlGenericControl lbl1 = new HtmlGenericControl("div");
            lbl1.InnerText = "Please enter valid data.";

            if (host == "" || guest =="")
            {
                faCLR.Controls.Add(lbl1);
            }
            else
            {

                SqlCommand count = conn.CreateCommand();
                count.CommandText = "SELECT COUNT(tid) from Ticket where status = 0";
                Int32 countSold = Convert.ToInt32(count.ExecuteScalar());


                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT TOP (1) m.start_time FROM match m INNER JOIN club c1 ON m.host_club_id = c1.club_id INNER JOIN club c2 ON m.guest_club_id = c2.club_id WHERE c1.name ='" + host + "' AND c2.name = '" + guest + "'";

                String start = cmd.ExecuteScalar().ToString();




                SqlCommand purTic = new SqlCommand("purchaseTicket", conn);
                purTic.CommandType = CommandType.StoredProcedure;
                purTic.Parameters.Add(new SqlParameter("@fan_id", id));
                purTic.Parameters.Add(new SqlParameter("@host_name", host));
                purTic.Parameters.Add(new SqlParameter("@guest_name", guest));
                purTic.Parameters.Add(new SqlParameter("@start_time", start));


                purTic.ExecuteNonQuery();

                Int32 countSoldAfter = Convert.ToInt32(count.ExecuteScalar());
                HtmlGenericControl cnt = (HtmlGenericControl)Page.FindControl("purchaseConfirmation");
                HtmlGenericControl lbl = new HtmlGenericControl("div");


                if (countSoldAfter > countSold)
                {
                    lbl.InnerText = "Transaction Completed!";
                }
                else
                {
                    lbl.InnerText = "Transaction Failed :(";

                }
                cnt.Controls.Add(lbl);

                loginPage.EmptyTextBoxes(faCLR);
            }

            conn.Close();

            //Response.Redirect("AssocActions.aspx");

        }
    }
}