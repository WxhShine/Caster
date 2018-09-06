using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
	/// <summary>
	/// 餐桌 实体 
	/// </summary>
	[Serializable]
	public partial class TableInfo
	{
		public TableInfo()
		{}
		#region Model
		private int _tid;
		private string _ttitle;
		private int? _thallid;
		private bool _tisfree;
		private bool _tisdelete;
		/// <summary>
		/// 
		/// </summary>
		public int TId
		{
			set{ _tid=value;}
			get{return _tid;}
		}
        /// <summary>
        /// 餐桌名称
        /// </summary>
        public string TTitle
		{
			set{ _ttitle=value;}
			get{return _ttitle;}
		}
        /// <summary>
        /// 厅包信息
        /// </summary>
        public int? THallId
		{
			set{ _thallid=value;}
			get{return _thallid;}
		}
        /// <summary>
        /// 是否空闲
        /// </summary>
        public bool TIsFree
		{
			set{ _tisfree=value;}
			get{return _tisfree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool TIsDelete
		{
			set{ _tisdelete=value;}
			get{return _tisdelete;}
		}
		#endregion Model

	}
}

