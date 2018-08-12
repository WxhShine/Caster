using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// OrderDetailInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderDetailInfo
	{
		public OrderDetailInfo()
		{}
		#region Model
		private int _oid;
		private int? _orderid;
		private int? _dishid;
		private int? _count;
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
		public int? OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DishId
		{
			set{ _dishid=value;}
			get{return _dishid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		#endregion Model

	}
}

