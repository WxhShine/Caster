using System.Collections.Generic;
using CaterDal;
using CaterModel;

namespace CaterBll {
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
        /// <summary>
        /// 设置餐桌状态
        /// </summary>
        /// <param name="tableId">桌号</param>
        /// <param name="isfree">是否空闲</param>
        public void SetSate(int tableId,bool isfree) {
            tiDal.SetState(tableId, isfree);
        }
    }
}
