using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLib
{
    public class Report
    {
        private string reportId;
        private string id;
        private string dateTime;

        public string ReportId
        {
            get
            {
                return reportId;
            }

            set
            {
                reportId = value;
            }
        }

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

        public string DateTime
        {
            get
            {
                return dateTime;
            }

            set
            {
                dateTime = value;
            }
        }
    }
}
