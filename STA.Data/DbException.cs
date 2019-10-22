using System;
using STA.Common;
using System.Text;

namespace STA.Data
{
    public class DbException : STAException
    {
        public DbException(string message)
            : base(message)
        {
        }

        public int Number
        {
            get { return 0 ; }
        }

       
    }
}
