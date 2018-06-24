using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientJourneyTest
{
    class Permission
    {
        private DateTime dateFrom;
        private DateTime dateTo;
        private string address;

        public Permission(DateTime dateFrom, DateTime dateTo, string address)
        {
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.address = address;
        }

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }

        public DateTime DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
