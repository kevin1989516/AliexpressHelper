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
            if (!IsPostBack) 
            {
                aliDataContext db = new aliDataContext();
                GridView1.DataSource = db.ProductListing.ToList();
                GridView1.DataBind();
            }


            

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            aliDataContext db = new aliDataContext();
            var list = from b in db.ProductListing select b;
            if (!string.IsNullOrEmpty(txtCompany.Text))
            {
                list = list.Where(c => c.CompanyName == txtCompany.Text);
            }
            if (!string.IsNullOrEmpty(txtKewWords.Text))
            {
                if (ListSearch_KeyWords == null)
                {
                    ListSearch_KeyWords = db.Search_KeyWords.ToList();
                }
                Search_KeyWords obj = ListSearch_KeyWords.Find(delegate(Search_KeyWords target)
                { return target.Id.ToString() == txtKewWords.Text; });
                int KeyWordsId = 0;
                if (obj != null)
                {
                    KeyWordsId = obj.Id;
                }
                if(KeyWordsId > 0)
                {
                    list = list.Where(c => c.KeyWordsId == KeyWordsId);
                }

                
            }
            GridView1.DataSource = list.ToList();
            GridView1.DataBind();
        }
    }
}