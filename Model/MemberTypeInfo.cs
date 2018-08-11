using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// MemberTypeInfo:实体类(属性说明自动提取数据库字段的描述信息)
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
		/// 
		/// </summary>
		public int MId
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MTitle
		{
			set{ _mtitle=value;}
			get{return _mtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MDiscount
		{
			set{ _mdiscount=value;}
			get{return _mdiscount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool MIsDelete
		{
			set{ _misdelete=value;}
			get{return _misdelete;}
		}
		#endregion Model

	}
}

