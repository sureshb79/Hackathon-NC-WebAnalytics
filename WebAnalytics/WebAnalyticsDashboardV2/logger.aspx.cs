using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAnalyticsDashboardV2
{
    public partial class logger : System.Web.UI.Page
    {
        protected string sAppID = "", sPageID = "";
        protected string sPageTitle = "", sRefererURL = "";
        protected string sRegion = "", sCity = "", sCountry = "";
        protected string sSessionID = "", sClientIP = "", sHostAddress = "", sPageURL = "";
        protected string sAction = "", sActionName = "", sLoadTime = "", sIdleTime = "";
        protected string sLanguage = "", sBrowser = "";
        protected string sLatitude = "", sLongitude = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["appid"] != null && Request.QueryString["appid"] != "")
            {
                sAppID = Request.QueryString["appid"].ToString();
            }
            if (Request.QueryString["pgId"] != null && Request.QueryString["pgId"] != "")
            {
                sPageID = Request.QueryString["pgId"].ToString();
            }
            if (Request.QueryString["pgtl"] != null && Request.QueryString["pgtl"] != "")
            {
                sPageTitle = Request.QueryString["pgtl"].ToString();
            }
            if (Request.QueryString["ref"] != null && Request.QueryString["ref"] != "")
            {
                sRefererURL = Request.QueryString["ref"].ToString();
            }
            if (Request.QueryString["rgn"] != null && Request.QueryString["rgn"] != "")
            {
                sRegion = Request.QueryString["rgn"].ToString();
            }
            if (Request.QueryString["city"] != null && Request.QueryString["city"] != "")
            {
                sCity = Request.QueryString["city"].ToString();
            }
            if (Request.QueryString["ctry"] != null && Request.QueryString["ctry"] != "")
            {
                sCountry = Request.QueryString["ctry"].ToString();
            }

            //    protected string sSessionID="", sClientIP="",sFullURL="";
            //protected string sAction="", sActionName="",sLoadTime="",sIdleTime="";
            if (Request.QueryString["uid"] != null && Request.QueryString["uid"] != "")
            {
                sSessionID = Request.QueryString["uid"].ToString();
            }
            else
            {
                sSessionID = Session.SessionID;
            }
            if (Request.QueryString["ip"] != null && Request.QueryString["ip"] != "")
            {
                sClientIP = Request.QueryString["ip"].ToString();
            }
            if (Request.QueryString["hst"] != null && Request.QueryString["hst"] != "")
            {
                sHostAddress = Request.QueryString["hst"].ToString();
            }
            if (Request.QueryString["pgnm"] != null && Request.QueryString["pgnm"] != "")
            {
                sPageURL = Request.QueryString["pgnm"].ToString();
            }
            if (Request.QueryString["ac"] != null && Request.QueryString["ac"] != "")
            {
                sAction = Request.QueryString["ac"].ToString();
            }
            if (Request.QueryString["acn"] != null && Request.QueryString["acn"] != "")
            {
                sActionName = Request.QueryString["acn"].ToString();
            }
            if (Request.QueryString["pglt"] != null && Request.QueryString["pglt"] != "")
            {
                sLoadTime = Request.QueryString["pglt"].ToString();
            }
            if (Request.QueryString["pgit"] != null && Request.QueryString["pgit"] != "")
            {
                sIdleTime = Request.QueryString["pgit"].ToString();
            }
            if (Request.QueryString["Language"] != null && Request.QueryString["Language"] != "")
            {
                sLanguage = Request.QueryString["Language"].ToString();
            }
            if (Request.QueryString["dc"] != null && Request.QueryString["dc"] != "")
            {
                sBrowser = Request.QueryString["dc"].ToString();
            }
            //protected string sLatitue="", sLongtitude="";
            if (Request.QueryString["lat"] != null && Request.QueryString["lat"] != "")
            {
                sLatitude = Request.QueryString["lat"].ToString();
            }
          
            SendRequest(sAppID, sPageID, sPageTitle, sRefererURL, sRegion, sCity, sCountry, sSessionID,
                sClientIP, sHostAddress, sPageURL, sAction, sActionName, sLoadTime, sIdleTime, sLanguage,
                sBrowser, sLatitude, sLongitude);
        }


        private void SendRequest(string AppID, string PageID, string PageTitle, string RefererURL,
                                   string Region, string City, string Country, string SessionID, string ClientIP,
                                   string Hostaddress, string PageURL, string Action, string ActionName,
                                   string LoadTime, string IdleTime, string Language, string Browser,
                                   string Latitude, string Longitude
                                   )
        {
            string constring = @"Data Source=(localdb)\v11.0;Initial Catalog=WebAnalytics;Persist Security Info=True;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(constring))
            {

                //string cmdText = "INSERT INTO PageStats(AppID,PageID) values(" + AppID + "," + PageID + ")";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.AddPageStat";
                SqlParameter param = new SqlParameter();
                if (!string.IsNullOrEmpty(AppID))
                {
                    param = new SqlParameter("@AppID", SqlDbType.VarChar, 50);
                    param.Value = AppID;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(PageID))
                {
                    param = new SqlParameter("@PageID", SqlDbType.VarChar, 50);
                    param.Value = PageID;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(PageTitle))
                {
                    param = new SqlParameter("@PageTitle", SqlDbType.VarChar, 500);
                    param.Value = PageTitle;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(RefererURL))
                {
                    param = new SqlParameter("@RefererURL", SqlDbType.VarChar, 1000);
                    param.Value = RefererURL;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(Region))
                {
                    param = new SqlParameter("@Region", SqlDbType.VarChar, 250);
                    param.Value = Region;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(City))
                {
                    param = new SqlParameter("@City", SqlDbType.VarChar, 250);
                    param.Value = City;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(Country))
                {
                    param = new SqlParameter("@Country", SqlDbType.VarChar, 250);
                    param.Value = Country;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(SessionID))
                {
                    param = new SqlParameter("@SessionID", SqlDbType.VarChar, 200);
                    param.Value = SessionID;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(ClientIP))
                {
                    param = new SqlParameter("@ClientIP", SqlDbType.VarChar, 100);
                    param.Value = ClientIP;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(Hostaddress))
                {
                    param = new SqlParameter("@Hostaddress", SqlDbType.VarChar, 1000);
                    param.Value = Hostaddress;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(PageURL))
                {
                    param = new SqlParameter("@PageURL", SqlDbType.VarChar, 250);
                    param.Value = PageURL;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(Action))
                {
                    param = new SqlParameter("@Action", SqlDbType.VarChar, 50);
                    param.Value = Action;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(ActionName))
                {
                    param = new SqlParameter("@ActionName", SqlDbType.VarChar, 250);
                    param.Value = ActionName;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(LoadTime))
                {
                    param = new SqlParameter("@LoadTime", SqlDbType.VarChar,20);
                    param.Value = LoadTime;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(IdleTime))
                {
                    param = new SqlParameter("@IdleTime", SqlDbType.VarChar,20);
                    param.Value = IdleTime;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(Language))
                {
                    param = new SqlParameter("@Language", SqlDbType.VarChar, 250);
                    param.Value = Language;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(Browser))
                {
                    param = new SqlParameter("@Browser", SqlDbType.VarChar, 1000);
                    param.Value = Browser;
                    cmd.Parameters.Add(param);
                }
                if (!string.IsNullOrEmpty(Latitude))
                {
                    param = new SqlParameter("@latlang", SqlDbType.VarChar,200);
                    param.Value = Latitude;
                    cmd.Parameters.Add(param);
                }
               
                //param = new SqlParameter("@PageID", SqlDbType.VarChar, 50);
                //param.Value = PageID;
                //cmd.Parameters.Add(param);
                //param = new SqlParameter("@PageID", SqlDbType.VarChar, 50);
                //param.Value = PageID;
                //cmd.Parameters.Add(param);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}