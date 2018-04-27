using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataCenter;

namespace express
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Search_KeyWords> ListSearch_KeyWords { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            aliDataContext db = new aliDataContext();
            GridView1.DataSource = db.ProductListing.ToList();
            GridView1.DataBind();
            

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ListSearch_KeyWords == null) 
            {
                aliDataContext db = new aliDataContext();
                ListSearch_KeyWords = db.Search_KeyWords.ToList();
            }
            Label lbKeyWordsId = (Label)e.Row.FindControl("lbKeyWordsId");
            if (lbKeyWordsId != null)
            {
                Search_KeyWords obj = ListSearch_KeyWords.Find(delegate(Search_KeyWords target) { return target.Id.ToString() == lbKeyWordsId.Text; });
                if (obj != null) 
                {
                    lbKeyWordsId.Text = obj.KeyWords;
                }
            }
            
        }
    }
}