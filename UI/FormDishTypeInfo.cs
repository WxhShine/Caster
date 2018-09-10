using System;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI {
    /// <summary>
    /// 菜品分类管理窗口
    /// </summary>
    public partial class FormDishTypeInfo : Form
    {
        public FormDishTypeInfo()
        {
            InitializeComponent();
        }
        DishTypeInfoBll dtiBll=new DishTypeInfoBll();
        private int rowIndex = -1;
        private DialogResult result = DialogResult.Cancel;

        private void FormDishTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            //设置列自动适应宽度
            //dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = dtiBll.GetList();
            //设置某行选中
            if (rowIndex >= 0)
            {
                dgvList.Rows[rowIndex].Selected = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //根据用户输入构造对象
            DishTypeInfo dti=new DishTypeInfo()
            {
                DTitle = txtTitle.Text
            };

            if (txtId.Text == "添加时无编号")
            {
                //添加
                if (dtiBll.Add(dti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍候重试");
                }
            }
            else
            {
                //修改
                dti.Id = int.Parse(txtId.Text);
                if (dtiBll.Edit(dti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍候重试");
                }
            }
            //清除控件值
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";

            this.result = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //清除控件值
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];

            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            btnSave.Text = "修改";

            //记录被点击的行的索引，用于刷新后再次选中
            rowIndex = e.RowIndex;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvList.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells[0].Value);

            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);

            if (result ==DialogResult.Cancel)
            {
                return;
            }

            if (dtiBll.Delete(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败，请稍候重试");
            }

            this.result = DialogResult.OK;
        }

        private void FormDishTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = this.result;
        }
    }
}
