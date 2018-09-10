using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI {
    /// <summary>
    /// 菜品管理窗口
    /// </summary>
    public partial class FormDishInfo : Form
    {
        public FormDishInfo()
        {
            InitializeComponent();
        }
        private DishInfoBll diBll=new DishInfoBll();

        private void FormDishInfo_Load(object sender, EventArgs e)
        {
            LoadTypeList();
            LoadList();
            
        }

        private void LoadList()
        {
            //拼接条件
            Dictionary<string,string> dic=new Dictionary<string, string>();
            if (txtTitleSearch.Text != "")
            {
                dic.Add("dtitle",txtTitleSearch.Text);
            }
            if (ddlTypeSearch.SelectedValue.ToString() != "0")
            {
                dic.Add("DTypeId",ddlTypeSearch.SelectedValue.ToString());
            }


            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = diBll.GetList(dic);
        }

        private void LoadTypeList()
        {
            DishTypeInfoBll dtiBll=new DishTypeInfoBll();
            

            #region 绑定查询的下拉列表
            List<DishTypeInfo> list = dtiBll.GetList();
            //向list中插入数据
            list.Insert(0, new DishTypeInfo()
            {
                Id = 0,
                DTitle = "全部"
            });

            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.ValueMember = "id";//对应于SelectedValue属性
            ddlTypeSearch.DisplayMember = "dtitle";//用于显示的值 
            #endregion

            #region 绑定添加的下拉列表

            ddlTypeAdd.DataSource = dtiBll.GetList();
            ddlTypeAdd.DisplayMember = "dtitle";
            ddlTypeAdd.ValueMember = "id";

            #endregion
        }

        private void txtTitleSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = "";
            ddlTypeSearch.SelectedIndex = 0;//全部
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //收集用户输入信息
            DishInfo di=new DishInfo()
            {
                DTitle = txtTitleSave.Text,
                DChar = txtChar.Text,
                DPrice = Convert.ToDecimal(txtPrice.Text),
                DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue)
            };

            if (txtId.Text == "添加时无编号")
            {
                #region 添加

                if (diBll.Add(di))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("逗b，怎么加的");
                }
                #endregion
            }
            else
            {
                #region 修改

                di.Id = int.Parse(txtId.Text);
                if (diBll.Update(di))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("你是猴子请来的救兵吗？");
                }
                #endregion
            }

            #region 恢复控件

            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            txtPrice.Text = "";
            txtChar.Text = "";
            ddlTypeAdd.SelectedIndex = 0;

            #endregion
        }

        private void txtTitleSave_Leave(object sender, EventArgs e)
        {
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            txtPrice.Text = "";
            txtChar.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            txtChar.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormDishTypeInfo formDti=new FormDishTypeInfo();
            DialogResult result = formDti.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadTypeList();
                LoadList();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (diBll.Remove(id))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("****");
                }
            }
        }
    }
}
