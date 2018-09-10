using System.Collections.Generic;
using CaterDal;
using CaterModel;

namespace CaterBll {
    /// <summary>
    /// 菜品类型业务层
    /// </summary>
    public partial class DishTypeInfoBll
    {
        private DishTypeInfoDal dtiDal=new DishTypeInfoDal();

        public List<DishTypeInfo> GetList()
        {
            return dtiDal.GetList();
        }

        public bool Add(DishTypeInfo dti)
        {
            return dtiDal.Insert(dti) > 0;
        }

        public bool Edit(DishTypeInfo dti)
        {
            return dtiDal.Update(dti) > 0;
        }

        public bool Delete(int id)
        {
            return dtiDal.Delete(id) > 0;
        }
    }
}
