using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLib
{
    public class Patient
    {
        private string id;
        private string docId;
        private string name;
        private int age;
       
        //Doctor doc ;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string DocId
        {
            get
            {
                return docId;
            }

            set
            {
                docId = value;
            }
        }
    }
}
