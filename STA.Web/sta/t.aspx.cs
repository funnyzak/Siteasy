using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using System.Text.RegularExpressions;
using System.Xml;

using STA.Core.Collect;
using System.Data;
using System.Drawing;
using System.IO;

namespace STA.Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.StatusCode = 302;
            //Response.RedirectLocation = "/index.aspx?action=2";
            //STA.Core.Collect.WebCollect.CollectPre(Collects.GetWebCollect(1), 0);

            //STA.Core.Collect.DbCollect.CollectPre(1, 5);
            //STA.Core.Collect.DbCollect.Collect();

            //Response.Write(Utils.test);
            //Response.Write("斯蒂芬\r斯蒂芬");

            //Response.Write("斯蒂芬\r\n\r斯蒂芬");

            //Response.Write(string.Format("{1}{0}", "1", "2"));
            //FileUtil.CreateFolder(Utils.GetMapPath("/sdfdsf/"));
            //FileUtil.MoveFile(Utils.GetMapPath("/123.txt"), Utils.GetMapPath("/sdfdsf/234.txt"), true);
            //Response.Write(Utils.ConvertFileSize(2294444));
            //Thumbnail.MakeThumbnailImage(Utils.GetMapPath("/test.jpg"), Utils.GetMapPath("/test_3.jpg"), 300, 100);
            //Response.Write(Utils.MD5("123456dsfjsdlkfjewr54w5789").Length.ToString());
            //Response.Write(Utils.DataTableToJSON(DbHelper.ExecuteDataset("select * from sta_tags").Tables[0]).ToString());
            //Response.Write(Tag.AddTag("测试",1).ToString());
            //Response.Write(Tag.AddTag("测试", 2).ToString());
            //Response.Write(Tag.AddTag("测试", 2).ToString());
            //Response.Write(Tag.AddTag("测试", 0).ToString());
            //Response.Write(Tag.AddTag("测3试", 0).ToString());
            //Response.Write(DbHelper.QueryDetail);
            //FileUtil.CreateFile(Utils.GetMapPath("/sdfsdf.txxt"), Encoding.Default.GetBytes("你好"));
            //Response.Write(Utils.ConvertE("你好吗朋友士大夫玩儿玩儿想大方送").ToLower());
            //string a = "abcdefg";
            //Response.Write(a.Substring(0, a.IndexOf("def")));
            //Response.Write(DateTime.Now.Ticks.ToString());

            //UIDataProvide.GetUITable("content", "type=channel id=61 num=1 fields=id,title,img order=0 property=a,p cache=1 page=2");
            //DataTable dt = DbHelper.ExecuteDataset("select * from contents where id > 1688").Tables[0];
            //foreach (DataRow dr in dt.Rows)
            //{
            //    int id = TypeParse.StrToInt(dr["id"]);
            //    ContentInfo info = Contents.GetContent(id);
            //    if (info.Savepath.EndsWith("/"))
            //        info.Savepath = info.Savepath.Substring(0, info.Savepath.Length - 1);
            //    info.Property = ",";
            //    if (dr["isrecommend"].ToString() == "1")
            //        info.Property += "r,";
            //    if (dr["ishot"].ToString() == "1")
            //        info.Property += "a,";
            //    if (dr["istopline"].ToString() == "1")
            //        info.Property += "h,";
            //    if (dr["newtype"].ToString() == "1")
            //        info.Property += "p,";
            //    Contents.EditContent(info);
            //}
            //Response.Write("ok");


            //DataTable dt = Databases.ExecuteTable("Data Source=.;User ID=sa;Password=123456;Initial Catalog=ctaa2;Pooling=true", "select * from contents");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    int id = TypeParse.StrToInt(dr["id"]);
            //    ContentInfo info = Contents.GetContent(id);
            //    if (dr["extchannels"].ToString() != "")
            //    {
            //        info.Extchannels = dr["extchannels"].ToString();
            //        Contents.EditContent(info);
            //    }
            //}
            //Response.Write("ok");

            //List<FileItem> list = FileUtil.GetFiles(Utils.GetMapPath("../admin"), "aspx");
            //foreach (FileItem file in list)
            //{
            //    Response.Write(string.Format("<Content Include=\"{0}\" />\r\n", file.FullName.Replace(Utils.GetMapPath("/"), "")));
            //}
            //DataTable dt = new DataTable();
            //dt.Dispose();
            //dt.Dispose();

            //string txt = FileUtil.ReadFile(Utils.GetMapPath("/t.txt"));
            //foreach (string s in txt.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            //{
            //    Response.Write(string.Format("<option value=\"{0}\">{0}</option>\r\n",s.Trim()));
            //}

            //DataTable dt = Contents.GetContentTableByWhere(100, "title", "");
            //DataRow dr = dt.NewRow();
            //dr["title"] = "''士大夫";
            //dt.Rows.Add(dr);
            //string tstr = "''''士大夫";
            //Response.Write(dt.Select("title='" + tstr + "'").Length.ToString());
            //System.Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            //for (int i = 0; i < 10; i++)
            //{
            //    Response.Write(random.Next(0, 10).ToString() + "_");
            //}


            //DataTable dt = DbHelper.ExecuteDataset("select COUNT(*) as ct,[value] from sta_selects where ename = 'nativeplace'  group by [value] order by count(*) desc").Tables[0];
            //foreach (DataRow dr in dt.Rows)
            //{
            //    int count = TypeParse.StrToInt(dr["ct"]);
            //    decimal val = TypeParse.StrToDecimal(dr["value"].ToString().Trim());
            //    if (count <= 1 || val % 1 == 0) continue;

            //    DataTable dt2 = DbHelper.ExecuteDataset(string.Format("select id from sta_selects where ename = 'nativeplace' and [value] = '{0}'", val)).Tables[0];
            //    foreach (DataRow idr in dt2.Rows)
            //    {
            //        SelectInfo sinfo = Selects.GetSelect(TypeParse.StrToInt(idr["id"]));
            //        val += 0.001M;
            //        sinfo.Value = val.ToString();
            //        Selects.EditSelect(sinfo);
            //    }
            //}

            //string input = string.Empty;
            //string pattern = @"http://(?<domain>[^\/]*)";
            //input = HttpContext.Current.Request.Url.ToString();
            //Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            //Response.Write(STARequest.GetCurrentFullHost());

            //Response.Write(Utils.UrlDecode(Request.Cookies["cartgoods"].Value));
            //Response.Write(string.Format("{0},{1},{2}", DateTime.Now, DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString()));
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //Response.Write(Regex.Replace(txt1.Text, txt2.Text, txt3.Text));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Response.Write(Utils.GetMapPath(TextBox1.Text));
            //XmlDocument ihtdoc = XMLUtil.LoadDocument(Utils.GetMapPath("/config/tpl.config"));
            //Response.Write((ihtdoc.SelectSingleNode("tpl/page/item" + TextBox1.Text) == null).ToString());
            //Response.Write(Utils.GetPageContent(new Uri(TextBox1.Text), Encoding.UTF8));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //Response.Write(txtDesc.Text);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //ConvertHTML2 d = new ConvertHTML2();
            //d.Convert();
            //ConvertHTML.HTMLConvertImage(TextBox2.Text);


            //Bitmap m_Bitmap = WebSiteThumbnail.GetWebSiteThumbnail(TextBox2.Text, 600, 600, 600, 600);
            //MemoryStream ms = new MemoryStream();
            //m_Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);//JPG、GIF、PNG等均可
            //byte[] buff = ms.ToArray();
            //Response.BinaryWrite(buff);


            //Bitmap m_Bitmap = WebSiteThumbnail.GetWebSiteThumbnail(TextBox2.Text, 600, 600, 600, 600);
            //HttpContext.Current.Response.ContentType = "image/Png";
            //m_Bitmap.Save(this.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);//JPG、GIF、PNG等均可

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //Regex r = new Regex(@"(\[([a-z0-9\{\}_]{2,})\])", RegexOptions.IgnoreCase);
            //foreach (Match m in r.Matches(txtSource.Text))
            //{
            //    txtSource.Text = txtSource.Text.Replace(m.Groups[0].ToString(), "`" + m.Groups[2].ToString() + "`");

            //}
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            //foreach (string s in Utils.SplitString(TextBox3.Text, "/n"))
            //{
            //    string[] ss = s.Split('/');
            //    Utils.DownFile(s, Utils.GetMapPath("/files/cimg/"), ss[ss.Length]);
            //}
        }


    }
}