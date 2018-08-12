using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
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
