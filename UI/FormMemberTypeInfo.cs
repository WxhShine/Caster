using System;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI {
    /// <summary>
    /// 会员分类窗口
    /// </summary>
    public partial class FormMemberTypeInfo : Form
    {
        public FormMemberTypeInfo()
        {
            InitializeComponent();
        }
        //调用业务逻辑层对象
        MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();

        private DialogResult result = DialogResult.Cancel;

        private void FormMemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            //禁止自动生成列
            dgvList.AutoGenerateColumns = false;
            //指定数据源为查询结果
            dgvList.DataSource = mtiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //接收用户输入的值，构造对象
            MemberTypeInfo mti = new MemberTypeInfo()
            {
                MTitle = txtTitle.Text,
                MDiscount = Convert.ToDecimal( txtDiscount.Text)
            };

            if (txtId.Text.Equals("添加时无编号"))
            {
                //添加
                
                //调用添加方法
                if (mtiBll.Add(mti))
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
                mti.Id = int.Parse(txtId.Text);
                //调用修改的方法
                if (mtiBll.Edit(mti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍候重试");
                }
            }
            //将控件还原
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";

            result = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //将控件还原
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取点击的行
            var row = dgvList.Rows[e.RowIndex];
            //将行中列的值赋给文本框
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtDiscount.Text = row.Cells[2].Value.ToString();
            btnSave.Text = "修改";

            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //获取选中行的编号
            var row = dgvList.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells[0].Value);
            //确认
            DialogResult result= MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //进行删除
            if (mtiBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败，请稍候重试");
            }

            this.result = DialogResult.OK;
        }

        private void FormMemberTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = result;
        }
    }
}
