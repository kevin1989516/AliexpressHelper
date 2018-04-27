using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using AliexpressHelper.Data;

namespace AliexpressHelper
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            //ipad 2 digitizer
            //alieexpressDataContext db = new alieexpressDataContext();
            //var list = db.Search_KeyWords.ToList();
            //dataGridView1.DataSource = list;
        }

        string GetHtml(string url) 
        {
            
            url = System.Web.HttpUtility.UrlEncode(url);
            HttpHelper.post("data=", "http://localhost/newrelictest/WebForm1.aspx?action=setdata");
            HttpHelper.post(string.Format("url=" + url), "http://localhost/newrelictest/WebForm1.aspx?action=seturl");

            string data = HttpHelper.get("http://localhost/newrelictest/WebForm1.aspx?action=getdata");
            while (data == string.Empty)
            {
                System.Threading.Thread.Sleep(1000);
                data = HttpHelper.get("http://localhost/newrelictest/WebForm1.aspx?action=getdata");
            }

            return data;
        }

        void Search(string keywords, int KeyWordsId) 
        {
            string key = string.Empty;
            foreach(string k in keywords.Split(' '))
            {
                if(string.IsNullOrEmpty(key))
                {
                    key += k;
                }
                else
                {
                    key += "+"+k;
                }
                
            }

            string url = "https://www.aliexpress.com/wholesale?catId=0&g=n&SearchText=" + key;
            //string html = HttpHelper.get(url);
            string html = GetHtml(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//*[@id=\"hs-list-items\"]");

            if (items == null)
            {
                for (int i = 0; i < 10;i++ )
                {
                    System.Threading.Thread.Sleep(1000);
                    html = GetHtml(url);
                    doc.LoadHtml(html);
                    items = doc.DocumentNode.SelectNodes("//*[@id=\"hs-list-items\"]");
                    if(items!=null)
                    {
                        break;
                    }
                }
            }
            else 
            {
                string topProduct = items[0].InnerHtml;


                items = doc.DocumentNode.SelectNodes("//*[@id=\"hs-below-list-items\"]");
                html = items[0].InnerHtml;
                html = html.Replace("<script type=\"text/x-handlebars-template\" id=\"lazy-render\" class=\"lazy-render\">", "");
                html = html.Replace("</script>", "");
                html = html.Replace("<ul>", "");
                html = "<ul>" + topProduct + html;

                doc.LoadHtml(html);

                items = doc.DocumentNode.SelectNodes("ul/li");

                List<ProductListing> list = GetProduct(items, KeyWordsId);

                //foreach(ProductInfo info in list)
                //{
                //    GetHtml("https://"+info.ProudctLink);
                //}
            }

            
        }

        public List<ProductListing> GetProduct(HtmlNodeCollection items, int KeyWordsId) 
        {
            List<ProductListing> list = new List<ProductListing>();
            int i = 1;
            foreach (HtmlNode item in items)
            {
                try
                {
                    ProductListing product = new ProductListing();
                    product.ProductOrder = i;
                    product.KeyWordsId = KeyWordsId;
                    product.CreateTime = DateTime.Now;
                    product.CreateYear = DateTime.Now.Year;
                    product.CreateMonth = DateTime.Now.Month;
                    product.CreateDay = DateTime.Now.Day;
                    product.CompanyName = item.SelectSingleNode("div[1]/div/div[1]/div/span/a").InnerHtml;

                    string Price = item.SelectSingleNode("div[1]/div/div[2]/span/span[1]").InnerHtml;
                    Price = Price.Replace("US $", "");
                    if (Price.Contains("-"))
                    {
                        string[] p = Price.Split('-');
                        product.PriceFrom = decimal.Parse(p[0].Trim());
                        product.PriceTo = decimal.Parse(p[1].Trim());
                    }
                    else
                    {
                        product.PriceFrom = decimal.Parse(Price);
                        product.PriceTo = decimal.Parse(Price);
                    }

                    product.ProudctLink = "https://"+item.SelectSingleNode("div[1]/div/div[1]/h3/a").Attributes[1].Value.Replace("//", "");
                    HtmlNode FeedbackNode = item.SelectSingleNode("div[1]/div/div[2]/div[1]/a");
                    if (FeedbackNode != null)
                    {
                        product.Feedback = int.Parse(FeedbackNode.InnerHtml.Replace("Feedback(", "").Replace(")", ""));
                    }

                    HtmlNode OrdersQtyNode = item.SelectSingleNode("div[1]/div/div[2]/div[1]/span[3]/a/em");
                    if (OrdersQtyNode != null)
                    {
                        product.OrdersQty = int.Parse(OrdersQtyNode.InnerHtml.Replace("Orders  (", "").Replace(" Order  (", "").Replace(")", ""));
                    }
                    
                    list.Add(product);

                    db.ProductListings.InsertOnSubmit(product);
                    db.SubmitChanges();
                    i++;
                }
                catch(Exception ex) 
                {
                    
                }
            }

            return list;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        alieexpressDataContext db = new alieexpressDataContext();

        private void button1_Click(object sender, EventArgs e)
        {

            this.Enabled = false;
            try
            {

                List<Search_KeyWord> list = db.Search_KeyWords.ToList();
                foreach(Search_KeyWord keywords in list)
                {
                    Search(keywords.KeyWords, keywords.Id);
                }

                dataGridView1.DataSource = db.ProductListings.ToList();
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
            this.Enabled = true;
        }
    }




}
