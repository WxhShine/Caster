using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// 会员 实体
	/// </summary>
	[Serializable]
	public partial class MemberInfo
	{
		public MemberInfo()
		{}
		#region Model
		private int _mid;
		private int? _mtypeid;
		private string _mname;
		private string _mphone;
		private decimal? _mmoney;
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
		/// 会员类型
		/// </summary>
		public int? MTypeId
		{
			set{ _mtypeid=value;}
			get{return _mtypeid;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string MName
		{
			set{ _mname=value;}
			get{return _mname;}
		}
		/// <summary>
		/// 手机号
		/// </summary>
		public string MPhone
		{
			set{ _mphone=value;}
			get{return _mphone;}
		}
		/// <summary>
		/// 账户余额
		/// </summary>
		public decimal? MMoney
		{
			set{ _mmoney=value;}
			get{return _mmoney;}
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

