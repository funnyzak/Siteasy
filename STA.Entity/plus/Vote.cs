using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using STA.Common;
namespace STA.Entity.Plus
{
    public class VoteInfo
    {
        private int id = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private DateTime startDate = DateTime.Now;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        private DateTime endDate = DateTime.Now;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        private int isMore = 0;

        public int IsMore
        {
            get { return isMore; }
            set { isMore = value; }
        }
        private int isView = 0;


        public int IsView
        {
            get { return isView; }
            set { isView = value; }
        }
        private int isEnable = 0;

        public int IsEnable
        {
            get { return isEnable; }
            set { isEnable = value; }
        }
        private int interval = 0;


        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }
        private string items = string.Empty;

        public string Items
        {
            get { return items; }
            set { items = value; }
        }

        public string ItemPattern
        {
            get
            {
                return @"<item id='(\d+)' count='(\d+)'>([\s\S]+?)</item>";
            }
        }

        public List<VoteItem> VoteList
        {
            get
            {
                List<VoteItem> list = new List<VoteItem>();
                Regex r = new Regex(ItemPattern, RegexOptions.IgnoreCase);
                foreach (Match m in r.Matches(items))
                {
                    VoteItem item = new VoteItem();
                    item.Id = TypeParse.StrToInt(m.Groups[1].Value, 0);
                    item.Count = TypeParse.StrToInt(m.Groups[2].Value, 0);
                    item.Content = m.Groups[3].Value;
                    list.Add(item);
                }
                return list;
            }
        }

        public int VoteTotalCount
        {
            get
            {
                int count = 0;
                foreach (VoteItem item in VoteList)
                {
                    count += item.Count;
                }
                return count;
            }
        }

        public override string ToString()
        {
            string ret = string.Empty;
            foreach (VoteItem item in VoteList)
            {
                ret += item.ToString() + "\r\n";
            }
            return ret;
        }

    }
    public class VoteItem
    {
        private int id = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int count = 0;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private string content = string.Empty;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private double precent = 0;

        public double Precent
        {
            get { return precent; }
            set { precent = value; }
        }

        public override string ToString()
        {
            return string.Format("<item id='{0}' count='{1}'>{2}</item>", id.ToString(), count.ToString(), content);
        }
    }

    public class VoteCompare : IComparer<VoteItem>
    {
        public int Compare(VoteItem x, VoteItem y)
        {
            return y.Count.CompareTo(x.Count);
        }
    }
}
