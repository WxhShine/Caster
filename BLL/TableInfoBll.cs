using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    /// <summary>
    /// 餐桌业务层
    /// </summary>
    public partial class TableInfoBll
    {
        private TableInfoDal tiDal=new TableInfoDal();

        /// <summary>
        /// 获取所有餐桌
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            return tiDal.GetList(dic);
        }

        public bool Add(TableInfo ti)
        {
            return tiDal.Insert(ti) > 0;
        }

        public bool Edit(TableInfo ti)
        {
            return tiDal.Update(ti) > 0;
        }

        public bool Remove(int id)
        {
            return tiDal.Delete(id) > 0;
        }
    }
}
