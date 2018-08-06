using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL {
    public class MangerInfoBLL {
        private ManagerInfoDAL DAL = new ManagerInfoDAL();
        public List<ManagerInfo> GetList() {
            return DAL.GetList();
        }

        public bool Add(ManagerInfo mi) {
            return DAL.Insert(mi)>0;
        }

        public bool Edit(ManagerInfo mi) {
            return DAL.Update(mi) > 0;
        }

        public bool Remove(int id) {
            return DAL.Delete(id)>0;
        }
    }
}