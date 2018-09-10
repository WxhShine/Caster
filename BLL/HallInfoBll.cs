using System.Collections.Generic;
using CaterDal;
using CaterModel;

namespace CaterBll {
    /// <summary>
    /// 厅包业务层
    /// </summary>
    public partial class HallInfoBll {
        private HallInfoDal hiDal;

        public HallInfoBll() {
            hiDal = new HallInfoDal();
        }

        /// <summary>
        /// 获取厅包列表
        /// </summary>
        /// <returns></returns>
        public List<HallInfo> GetList() {
            return hiDal.GetList();
        }

        /// <summary>
        /// 添加厅包
        /// </summary>
        /// <param name="hi">实体</param>
        /// <returns></returns>
        public bool Add(HallInfo hi) {
            return hiDal.Insert(hi) > 0;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="hi">实体</param>
        /// <returns></returns>
        public bool Edit(HallInfo hi) {
            return hiDal.Update(hi) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        public bool Remove(int id) {
            return hiDal.Delete(id) > 0;
        }
    }
}
