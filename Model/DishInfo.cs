using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// 菜品 实体 
	/// </summary>
	[Serializable]
	public partial class DishInfo
	{
		public DishInfo()
		{}
		#region Model
		private int _did;
		private string _dtitle;
		private int? _dtypeid;
		private decimal? _dprice;
		private string _dchar;
		private bool _disdelete;
		/// <summary>
		/// 
		/// </summary>
		public int DId
		{
			set{ _did=value;}
			get{return _did;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string DTitle
		{
			set{ _dtitle=value;}
			get{return _dtitle;}
		}
		/// <summary>
		/// 菜品类型
		/// </summary>
		public int? DTypeId
		{
			set{ _dtypeid=value;}
			get{return _dtypeid;}
		}
		/// <summary>
		/// 价格
		/// </summary>
		public decimal? DPrice
		{
			set{ _dprice=value;}
			get{return _dprice;}
		}
		/// <summary>
		/// 首字母
		/// </summary>
		public string DChar
		{
			set{ _dchar=value;}
			get{return _dchar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool DIsDelete
		{
			set{ _disdelete=value;}
			get{return _disdelete;}
		}
		#endregion Model

	}
}

