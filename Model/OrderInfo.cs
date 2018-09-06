using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// 订单 实体 
	/// </summary>
	[Serializable]
	public partial class OrderInfo
	{
		public OrderInfo()
		{}
		#region Model
		private int _oid;
		private int? _memberid;
		private DateTime? _odate;
		private decimal? _omoney;
		private bool _ispay;
		private int? _tableid;
		private decimal? _discount;
		/// <summary>
		/// 
		/// </summary>
		public int OId
		{
			set{ _oid=value;}
			get{return _oid;}
		}
        /// <summary>
        /// 会员编号
        /// </summary>
        public int? MemberId
		{
			set{ _memberid=value;}
			get{return _memberid;}
		}
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime? ODate
		{
			set{ _odate=value;}
			get{return _odate;}
		}
		/// <summary>
		/// 消费金额
		/// </summary>
		public decimal? OMoney
		{
			set{ _omoney=value;}
			get{return _omoney;}
		}
		/// <summary>
		/// 是否结账
		/// </summary>
		public bool IsPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 桌号
		/// </summary>
		public int? TableId
		{
			set{ _tableid=value;}
			get{return _tableid;}
		}
		/// <summary>
		/// 折扣
		/// </summary>
		public decimal? Discount
		{
			set{ _discount=value;}
			get{return _discount;}
		}
		#endregion Model

	}
}

