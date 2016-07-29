using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLib
{
    public class Admin : User
    {
        private int privileges;
        
        public int Privileges
        {
            get
            {
                return privileges;
            }

            set
            {
                privileges = value;
            }
        }
        
    }
}
