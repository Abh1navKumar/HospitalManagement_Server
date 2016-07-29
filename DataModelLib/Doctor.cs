using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLib
{
    public class Doctor : User
    {
        private string region;
       
        public string Region
        {
            get
            {
                return region;
            }

            set
            {
                region = value;
            }
        }
    }
}
