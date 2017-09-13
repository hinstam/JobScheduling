using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.CommModel
{
    public class ResultModel
    {
        bool _IsSuccess = true;
        int _Affected = 0;

        public bool IsSuccess
        {
            get { return _IsSuccess; }
            set { _IsSuccess = value; }
        }

        public int Affected
        {
            get { return _Affected; }
            set { _Affected = value; }
        }

        public string Exception { get; set; }

        public string TextData { get; set; }
    }


    public class ResultModel<T>
    {
        bool _IsSuccess = true;

        public List<T> EntityList { get; set; }

        public bool IsSuccess
        {
            get { return _IsSuccess; }
            set { _IsSuccess = value; }
        }

        public string Exception { get; set; }

        public string TextData { get; set; }

        public int Affected
        {
            get
            {
                if (EntityList != null)
                    return EntityList.Count();
                return 0;
            }
        }
    }


    public class SQLModel
    {
        public StringBuilder InsertSQL { get; set; }

        public StringBuilder DeleteSQL { get; set; }

        public StringBuilder UpdateSQL { get; set; }

        public StringBuilder DeleteCCASSQL { get; set; }

        public StringBuilder UpdateCCASSQL { get; set; }
    }

}
