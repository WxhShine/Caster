using System;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI {
    /// <summary>
    /// 厅包管理窗口
    /// </summary>
    public partial class FormHallInfo : Form
    {
        public FormHallInfo()
        {
            InitializeComponent();

            hiBll=new HallInfoBll();
        }

        private HallInfoBll hiBll;

        public event Action MyUpdateForm;

        private void FormHallInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = hiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HallInfo hi=new HallInfo()
            {
                HTitle = txtTitle.Text
            };

            if (txtId.Text == "添加时无编号")
            {
                //添加
                if (hiBll.Add(hi))
                {
                    LoadList();
                }
            }
            else
            {
                //修改
                hi.Id = int.Parse(txtId.Text);
                if (hiBll.Edit(hi))
                {
                    LoadList();
                }
            }

            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";

            MyUpdateForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            if (hiBll.Remove(id))
            {
                LoadList();
            }

            //调用事件，事件中的方法都被执行
            MyUpdateForm();
        }
    }
}
