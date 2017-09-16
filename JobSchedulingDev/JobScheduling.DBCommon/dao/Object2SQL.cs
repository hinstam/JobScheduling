using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace EG.Utility.DBCommon.dao
{
    public class Object2SQL
    {
        protected readonly static short TODO_INSERT = 1;
        protected readonly static short TODO_UPDATE = 2;
        protected readonly static short TODO_DELETE = 3;
        protected readonly static short TODO_SELECT = 4;

        protected object _entity { get; set; }
        protected Type _type { get; set; }
        protected short dbType { get; set; }

        public string Sql { get; set; }
        public short Todo { get; set; }

        public IList<String> ParameterNames { get; set; }
        public IList<Object> ParameterValues { get; set; }

        public IList<String> KeyNames { get; set; }
        public IList<Object> KeyValues { get; set; }
        public ADOTemplate adoTemplate { get; set; }


        protected String GetTableName()
        {
            
            string tableName = _type.GetAttributeValue((TableAttribute ta) => ta.Name);
            if (tableName == null)
            {
                return _type.Name ;
            }

            return tableName;
        }

        public void parse(object entity) {
            this._entity = entity;
            this._type = entity.GetType();


            //获得实体的属性集合 
            PropertyInfo[] props = _type.GetProperties();

            this.ParameterNames = new List<String>(props.Length);
            this.ParameterValues = new List<Object>(props.Length);

            this.KeyNames = new List<String>(1);
            this.KeyValues = new List<Object>(1);

            foreach (PropertyInfo prop in props)
            {
                string colName = prop.Name;
                object colValue = prop.GetValue(entity, null);
                bool isPK = false;

                //if is keyattribute return key
                object[] columnAtCCAS = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                ColumnAttribute colAttr = null;
                if (columnAtCCAS.Length > 0)
                {
                    colAttr = columnAtCCAS[0] as ColumnAttribute ;
                    if (colAttr.Name != null) {
                        colName = colAttr.Name;
                    }
                    isPK = colAttr.IsPrimaryKey ;

                    // 如果是oracle时，如果有seq定义，则取seq
                    if (Object2SQL.TODO_INSERT == Todo
                        && ADOTemplate.DB_TYPE_ORACLE == this.dbType
                        && colAttr.Expression != null)
                    {
                        String seqSql = String.Format("select {0}.nextval from dual", colAttr.Expression);

                        colValue = adoTemplate.getLong(seqSql);
                    }
                }

                if (isPK)
                {
                    this.KeyNames.Add(colName);
                    this.KeyValues.Add(colValue);
                }
                else
                {
                    this.ParameterNames.Add(colName);
                    this.ParameterValues.Add(colValue);
                }
            }
        }

        public ICollection<object> Conver2ObjectCollection(object entity) {
            if (entity is ICollection<long>) {
                ICollection<long> tempCollection = entity as ICollection<long>;

                IList<object> result = new List<object>(tempCollection.Count);

                foreach (long e in tempCollection) {
                    result.Add(e);
                }

                return result;
            }
            else if (entity is ICollection<int>)
            {
                ICollection<int> tempCollection = entity as ICollection<int>;

                IList<object> result = new List<object>(tempCollection.Count);

                foreach (int e in tempCollection)
                {
                    result.Add(e);
                }

                return result;
            }
            else if (entity is ICollection<double>)
            {
                ICollection<double> tempCollection = entity as ICollection<double>;

                IList<object> result = new List<object>(tempCollection.Count);

                foreach (double e in tempCollection)
                {
                    result.Add(e);
                }

                return result;
            }
            else if (entity is ICollection<float>)
            {
                ICollection<float> tempCollection = entity as ICollection<float>;

                IList<object> result = new List<object>(tempCollection.Count);

                foreach (double e in tempCollection)
                {
                    result.Add(e);
                }

                return result;
            }

            return entity as ICollection<object>;
        }

    }


    public class Object2Delete : Object2SQL
    {
        public string AsSql() {
            StringBuilder result = new StringBuilder();

            result.Append("delete from ");
            result.Append(this.GetTableName());
            result.Append(" where ");

            int size = this.KeyNames.Count ;
            for (int i = 0; i < size; i++)
            {
                if (i != 0) {
                    result.Append(" and ");
                }

                result.Append(this.KeyNames[i]);
                result.Append("=@");
                result.Append(this.KeyNames[i]);
            } 

            return result.ToString();
        }

        public String[] GetSqlParameterNames()
        {
            return this.KeyNames.ToArray<String>();
        }

        public Object[] GetSqlParameterValues()
        {
            return this.KeyValues.ToArray<Object>();
        }
    }


    public class Object2Get : Object2SQL
    {
        public string AsSql()
        {
            StringBuilder result = new StringBuilder();

            result.Append("select * from ");
            result.Append(this.GetTableName());
            result.Append(" where ");

            int size = this.KeyNames.Count;
            for (int i = 0; i < size; i++)
            {
                if (i != 0)
                {
                    result.Append(" and ");
                }

                result.Append(this.KeyNames[i]);
                result.Append("=@");
                result.Append(this.KeyNames[i]);
            }

            return result.ToString();
        }

        public String[] GetSqlParameterNames()
        {
            return this.KeyNames.ToArray<String>();
        }

        public Object[] GetSqlParameterValues()
        {
            return this.KeyValues.ToArray<Object>();
        }
    }

    public class Object2Insert : Object2SQL
    {
        public Object2Insert() : base() {
            this.Todo = Object2SQL.TODO_INSERT;
        }

        public string AsSql()
        {
            StringBuilder insertSQL = new StringBuilder();
            StringBuilder valueSQL = new StringBuilder();

            insertSQL.Append("insert into ").Append(this.GetTableName()).Append("(");
            valueSQL.Append(")values(");

            int size = this.KeyNames.Count;
            for (int i = 0; i < size; i++)
            {
                if (i != 0)
                {
                    insertSQL.Append(", ");
                    valueSQL.Append(", ");
                }

                insertSQL.Append(this.KeyNames[i]);
                valueSQL.Append("@").Append(this.KeyNames[i]);
            }

            size = this.ParameterNames.Count;
            for (int i = 0; i < size; i++)
            {
                insertSQL.Append(", ").Append(this.ParameterNames[i]);
                valueSQL.Append(", ").Append("@").Append(this.ParameterNames[i]);
            }

            return insertSQL.Append(valueSQL).Append(")").ToString();
        }

        public String[] GetSqlParameterNames()
        {
            String[] result = new String[this.KeyNames.Count + this.ParameterNames.Count] ;
            
            this.KeyNames.CopyTo(result, 0) ;
            this.ParameterNames.CopyTo(result, this.KeyNames.Count);
            
            return result;
        }

        public Object[] GetSqlParameterValues()
        {
            Object[] result = new Object[this.KeyValues.Count + this.ParameterValues.Count];

            this.KeyValues.CopyTo(result, 0);
            this.ParameterValues.CopyTo(result, this.KeyValues.Count);

            return result;
        }
    }




    public class Object2Update : Object2SQL
    {
        public string AsSql()
        {
            StringBuilder updateSQL = new StringBuilder();

            updateSQL.Append("update ").Append(this.GetTableName());

            int size = this.ParameterNames.Count;
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    updateSQL.Append(" set ");
                }
                else
                {
                    updateSQL.Append(", ");
                }
                updateSQL.Append(this.ParameterNames[i]).Append("=@").Append(this.ParameterNames[i]);
            }

            size = this.KeyNames.Count;
            for (int i = 0; i < size; i++)
            {
                if (i != 0)
                {
                    updateSQL.Append(", ");
                }
                else {
                    updateSQL.Append(" where ");
                }

                updateSQL.Append(this.KeyNames[i]).Append("=@").Append(this.KeyNames[i]);
            }

            return updateSQL.ToString();
        }

        public String[] GetSqlParameterNames()
        {
            String[] result = new String[this.KeyNames.Count + this.ParameterNames.Count];

            this.ParameterNames.CopyTo(result, 0);
            this.KeyNames.CopyTo(result, this.ParameterNames.Count);

            return result;
        }

        public Object[] GetSqlParameterValues()
        {
            Object[] result = new Object[this.KeyValues.Count + this.ParameterValues.Count];

            this.ParameterValues.CopyTo(result, 0);
            this.KeyValues.CopyTo(result, this.ParameterValues.Count);

            return result;
        }
    }

    public class Dictionary2Where : Object2SQL
    {
        private StringBuilder output = null;
        
        public void parse(object entity) {
            IDictionary<string, object> entityDict = entity as IDictionary<string, object>;

            output = new StringBuilder();

            if (entityDict == null || entityDict.Count == 0)
            {
                return;
            }
            output.Append(" where 1=1");

            this._entity = entity;


            //获得实体的属性集合 
            ICollection<string> props = entityDict.Keys;

            this.ParameterNames = new List<String>(props.Count);
            this.ParameterValues = new List<Object>(props.Count);

            foreach (string prop in props)
            {
                output.Append(" and ");
                
                string[] keyWithOper = prop.Split('$');
                string colName = keyWithOper[0];
                object colValue = entityDict[prop];
                
                if (null == colValue) {
                    continue;
                }

                string operatorName = "=";
                ICollection<object> colValues = this.Conver2ObjectCollection(colValue);
                bool isArray = (colValues != null);// colValue is ICollection<object>;
                if (keyWithOper.Length == 1)
                {
                    operatorName = isArray ? "in" : "=";
                }
                else
                {
                    operatorName = keyWithOper[1]; 
                }


                if (isArray)
                {
                    
                    if (colValues.Count != 0)
                    {
                        output.Append(colName).Append(" ").Append(operatorName).Append("(");
                        
                        int i = 0;
                        foreach (object colValueI in colValues)
                        {
                            if (i != 0)
                            {
                                output.Append(", ");
                            }
                            string colNameI = colName + "_" + (i++);
                            output.Append("@").Append(colNameI);
                            this.ParameterNames.Add(colNameI);
                            this.ParameterValues.Add(colValueI);
                        }

                        output.Append(")");
                    }
                }
                else
                {
                    output.Append(colName).Append(operatorName);

                    output.Append("@").Append(colName);
                    this.ParameterNames.Add(colName);
                    this.ParameterValues.Add(colValue);
                }


            }
        }


        public string AsSql()
        {
            return output.ToString();
        }

        public String[] GetSqlParameterNames()
        {
            return ParameterNames.ToArray();
        }

        public Object[] GetSqlParameterValues()
        {
            return this.ParameterValues.ToArray();
        }
    }
}
