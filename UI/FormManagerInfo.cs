using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI {
    public partial class FormManagerInfo : Form {
        private MangerInfoBLL BLL = new MangerInfoBLL();
        public FormManagerInfo() {
            InitializeComponent();
            dgvList.AutoGenerateColumns = false;
        }

        private void FormManagerInfo_Load(object sender, EventArgs e) {
            LoadList();
        }

        private void LoadList() {
            var list = BLL.GetList();
            dgvList.DataSource = list;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            ManagerInfo mi = new ManagerInfo {
                MName = txtName.Text,
                MPwd = txtPwd.Text,
                MType = rb1.Checked ? 1 : 0
            };
            if (txtId.Text.Equals("添加时无编号")) {
                if (!BLL.Add(mi)) MessageBox.Show("添加失败，请稍后重试");
            }
            else {
                mi.MId = int.Parse(txtId.Text);
                if (!BLL.Edit(mi)) MessageBox.Show("修改失败，请稍后重试");
            }
            LoadList();
            txtName.Text = string.Empty;
            txtPwd.Text = string.Empty;
            rb2.Checked = true;
            btnSave.Text = "添加";
            txtId.Text = "添加时无编号";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value.ToString().Equals("1")) rb1.Checked = true;
            else rb2.Checked = true;
            txtPwd.Text = "是否修改";
            btnSave.Text = "修改";
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            txtName.Text = string.Empty;
            txtPwd.Text = string.Empty;
            rb2.Checked = true;
            btnSave.Text = "添加";
            txtId.Text = "添加时无编号";
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            var rows = dgvList.SelectedRows;
            if (rows.Count > 0) {
                DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                if(result == DialogResult.Cancel) { return; }
                int id = int.Parse(rows[0].Cells[0].Value.ToString());
                if (!BLL.Remove(id)) MessageBox.Show("删除失败");
                LoadList();
            }
            else {
                MessageBox.Show("请选择要删除行");
            }
        }
    }
}
