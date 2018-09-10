using System.Collections.Generic;
using CaterDal;
using CaterModel;

namespace CaterBll {
    /// <summary>
    /// 菜品信息业务层
    /// </summary>
    public partial class DishInfoBll
    {
        private DishInfoDal diDal=new DishInfoDal();

        public List<DishInfo> GetList(Dictionary<string,string> dic)
        {
            return diDal.GetList(dic);
        }

        public bool Add(DishInfo di)
        {
            return diDal.Insert(di) > 0;
        }

        public bool Update(DishInfo di)
        {
            return diDal.Update(di) > 0;
        }

        public bool Remove(int id)
        {
            return diDal.Delete(id) > 0;
        }
    }
}
