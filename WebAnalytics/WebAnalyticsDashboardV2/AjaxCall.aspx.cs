using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;

namespace WebAnalyticsDashboardV2
{
    public partial class AjaxCall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetBarData()
        {
            //string constring = @"Data Source=(localdb)\v11.0;Initial Catalog=WebAnalytics;Persist Security Info=True;Integrated Security=true;";
            string retval = "test";
            DataHelper dh = new DataHelper();
            DataSet ds = new DataSet();
            dh.AddParameterToSQLCommand("@compapp1", SqlDbType.VarChar, 100);
            dh.AddParameterToSQLCommand("@compapp2", SqlDbType.VarChar, 100);
            string qrytxt = "select Count(*) AS [COUNT],AppID, CONVERT(VARCHAR(3),DateName( month , DateAdd( month , DATEPART(mm,HitDateTime) , 0 ) - 1 )) + '-'+CONVERT(VARCHAR(10),DATEPART(year,HitDateTime)) AS [TIMELINE] FROM Pagestats WHERE Action = 'load' GROUP BY APPID,CONVERT(VARCHAR(3),DateName( month , DateAdd( month , DATEPART(mm,HitDateTime) , 0 ) - 1 ))+'-'+CONVERT(VARCHAR(10),DATEPART(year,HitDateTime))";
            ds = dh.GetDatasetByCommand("AppCompareBar", CommandType.StoredProcedure);
            retval = JsonConvert.SerializeObject(ds);
            return retval;
        }

        [WebMethod]
        public static string GetDoughNutData()
        {
            //string constring = @"Data Source=(localdb)\v11.0;Initial Catalog=WebAnalytics;Persist Security Info=True;Integrated Security=true;";
            string retval = "";
            DataHelper dh = new DataHelper();
            DataSet ds = new DataSet();
            //dh.AddParameterToSQLCommand("@compapp1", SqlDbType.VarChar, 100);
            //dh.AddParameterToSQLCommand("@compapp2", SqlDbType.VarChar, 100);
            string qrytxt = "select Count(*) AS value,Region As label FROM Pagestats WHERE Country='US' AND  Action = 'load' AND AppID='VzPortal' GROUP BY Region";
            ds = dh.GetDatasetByCommand(qrytxt, CommandType.Text);
            retval = JsonConvert.SerializeObject(ds);
            return retval;
        }

        [WebMethod]
        public static string GetLineData()
        {
            //string constring = @"Data Source=(localdb)\v11.0;Initial Catalog=WebAnalytics;Persist Security Info=True;Integrated Security=true;";
            string retval = "test";
            DataHelper dh = new DataHelper();
            DataSet ds = new DataSet();
            dh.AddParameterToSQLCommand("@compapp1", SqlDbType.VarChar, 100);
            dh.AddParameterToSQLCommand("@compapp2", SqlDbType.VarChar, 100);
            ds = dh.GetDatasetByCommand("AreaChartComapreApps", CommandType.StoredProcedure);
            retval = JsonConvert.SerializeObject(ds);
            return retval;
        }


        [WebMethod]
        public static string GetGridData()
        {
            //string constring = @"Data Source=(localdb)\v11.0;Initial Catalog=WebAnalytics;Persist Security Info=True;Integrated Security=true;";
            string retval = "";
            DataHelper dh = new DataHelper();
            DataSet ds = new DataSet();
            //dh.AddParameterToSQLCommand("@compapp1", SqlDbType.VarChar, 100);
            //dh.AddParameterToSQLCommand("@compapp2", SqlDbType.VarChar, 100);
            string qrytxt = "select Top 100 AppID,PageID,Action,ActionName,Count(*) AS Hits,AVG(CONVERT(INT,ISNULL(LoadTime,'0'))) [AvgTime in ms] from pagestats  Group by Action,ActionName,PageID,AppID ORDER BY Hits DESC";
            ds = dh.GetDatasetByCommand(qrytxt, CommandType.Text);
            retval = JsonConvert.SerializeObject(ConvertDataTableToHtml(ds.Tables[0]));
            return retval;
        }


        [WebMethod]
        public static string GetMapData()
        {
            //string constring = @"Data Source=(localdb)\v11.0;Initial Catalog=WebAnalytics;Persist Security Info=True;Integrated Security=true;";
            string retval = "";
            DataHelper dh = new DataHelper();
            DataSet ds = new DataSet();
            //dh.AddParameterToSQLCommand("@compapp1", SqlDbType.VarChar, 100);
            //dh.AddParameterToSQLCommand("@compapp2", SqlDbType.VarChar, 100);
            string qrytxt = "select TOP 1 AVG(CONVERT(INT,ISNULL(LoadTime,'0'))) [AvgTimeinms],SubString(latlang,0,CHARINDEX(',',latlang))  AS latitude ,SubString(latlang,CHARINDEX(',',latlang)+1,len(latlang))  AS longitude from pagestats Group BY SubString(latlang,0,CHARINDEX(',',latlang)),SubString(latlang,CHARINDEX(',',latlang)+1,len(latlang)) ORDER BY [AvgTimeinms] DESC";
            ds = dh.GetDatasetByCommand(qrytxt, CommandType.Text);
            retval = JsonConvert.SerializeObject(ds);
            return retval;
        }



        public static string ConvertDataTableToHtml(DataTable targetTable)
        {
            string htmlString = "";
            if (targetTable != null)
            {
                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.Append("<table class='table table-bordered table-hover table-striped'>");
                htmlBuilder.Append("<thead>");
                //htmlBuilder.Append("AppID");
                //htmlBuilder.Append("PageID");
                //htmlBuilder.Append("Action");
                //htmlBuilder.Append("ActionName");
                //htmlBuilder.Append("Hits");
                //htmlBuilder.Append("AvgTime in ms");
                //htmlBuilder.Append("</tr></thead>");

                //Create Header Row
                htmlBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn targetColumn in targetTable.Columns)
                {
                    htmlBuilder.Append("<td align='left' valign='top'>");
                    htmlBuilder.Append(targetColumn.ColumnName);
                    htmlBuilder.Append("</td>");
                }
                htmlBuilder.Append("</tr>");
                htmlBuilder.Append("</thead>");
                //Create Data Rows
                foreach (DataRow myRow in targetTable.Rows)
                {
                    htmlBuilder.Append("<tr align='left' valign='top'>");
                    foreach (DataColumn targetColumn in targetTable.Columns)
                    {
                        htmlBuilder.Append("<td align='left' valign='top'>");
                        htmlBuilder.Append(myRow[targetColumn.ColumnName].ToString());
                        htmlBuilder.Append("</td>");
                    }
                    htmlBuilder.Append("</tr>");
                }

                //Create Bottom Portion of HTML Document
                htmlBuilder.Append("</table>");
                htmlBuilder.Append("</body>");
                htmlBuilder.Append("</html>");

                //Create String to be Returned
                htmlString = htmlBuilder.ToString();
            }
            return htmlString;
        }

    }
}