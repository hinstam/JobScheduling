using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.CommModel
{
    public class ArtistModel
    {
        private string _Artist_Code;
        private string _EnglishName;
        private string _ChineseName;
        private string _Company_Code;
        private string _Deptment_Code;
        private string _Artist_Type_Code;

        public string Artist_Code
        {
            get { return _Artist_Code; }
            set { _Artist_Code = value; }
        }

        public string EnglishName
        {
            get { return _EnglishName; }
            set { _EnglishName = value; }
        }

        public string ChineseName
        {
            get { return _ChineseName; }
            set { _ChineseName = value; }
        }

        public string Company_Code
        {
            get { return _Company_Code; }
            set { _Company_Code = value; }
        }

        public string Deptment_Code
        {
            get { return _Deptment_Code; }
            set { _Deptment_Code = value; }
        }

        public string Artist_Type_Code
        {
            get { return _Artist_Type_Code; }
            set { _Artist_Type_Code = value; }
        }
    }
}
