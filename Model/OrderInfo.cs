using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// OrderInfo:实体类(属性说明自动提取数据库字段的描述信息)
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
		/// 
		/// </summary>
		public int? MemberId
		{
			set{ _memberid=value;}
			get{return _memberid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ODate
		{
			set{ _odate=value;}
			get{return _odate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OMoney
		{
			set{ _omoney=value;}
			get{return _omoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TableId
		{
			set{ _tableid=value;}
			get{return _tableid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Discount
		{
			set{ _discount=value;}
			get{return _discount;}
		}
		#endregion Model

	}
}

