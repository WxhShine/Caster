using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// 会员等级 实体
	/// </summary>
	[Serializable]
	public partial class MemberTypeInfo
	{
		public MemberTypeInfo()
		{}
		#region Model
		private int _mid;
		private string _mtitle;
		private decimal? _mdiscount;
		private bool _misdelete;
		/// <summary>
		/// 编号
		/// </summary>
		public int MId
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 会员类型名称
		/// </summary>
		public string MTitle
		{
			set{ _mtitle=value;}
			get{return _mtitle;}
		}
		/// <summary>
		/// 会员折扣
		/// </summary>
		public decimal? MDiscount
		{
			set{ _mdiscount=value;}
			get{return _mdiscount;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public bool MIsDelete
		{
			set{ _misdelete=value;}
			get{return _misdelete;}
		}
		#endregion Model

	}
}

