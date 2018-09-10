using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI {
    /// <summary>
    /// 点菜窗口
    /// </summary>
    public partial class FormOrderDish : Form
    {
        public FormOrderDish()
        {
            InitializeComponent();
        }
        OrderInfoBll oiBll = new OrderInfoBll();

        private void FormOrderDish_Load(object sender, EventArgs e)
        {
            LoadDishType();
            LoadDishInfo();

            LoadDetailList();
        }

        private void LoadDishInfo()
        {
            //拼接查询条件 
            Dictionary<string,string> dic=new Dictionary<string, string>();
            if (txtTitle.Text != "")
            {
                dic.Add("dchar",txtTitle.Text);
            }
            if (ddlType.SelectedValue.ToString() != "0")
            {
                dic.Add("dtypeId",ddlType.SelectedValue.ToString());
            }

            //查询菜品，显示到表格中
            DishInfoBll diBll=new DishInfoBll();
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = diBll.GetList(dic);
        }

        private void LoadDishType()
        {
            DishTypeInfoBll dtiBll=new DishTypeInfoBll();
            var list=dtiBll.GetList();

            list.Insert(0,new DishTypeInfo()
            {
                Id = 0,
                DTitle = "全部"
            });

            ddlType.ValueMember = "id";
            ddlType.DisplayMember = "dtitle";
            ddlType.DataSource = list;
        }

        private void LoadDetailList()
        {
            int orderId = Convert.ToInt32(this.Tag);
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = oiBll.GetDetailList(orderId);

            GetTotalMoneyByOrderId();
        }

        private void GetTotalMoneyByOrderId()
        {
            int orderId = Convert.ToInt32(this.Tag);
            lblMoney.Text = oiBll.GetTotalMoneyByOrderId(orderId).ToString();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //点菜
            //订单编号
            int orderId = Convert.ToInt32(this.Tag);

            //菜单编号 
            int dishId = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);

            //执行点菜操作
            if (oiBll.OrderDishes(orderId, dishId))
            {
                //点菜成功
                LoadDetailList();
            }
        }

        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                //停止编辑数量列，才进行更改
                //获取修改的行
                var row = dgvOrderDetail.Rows[e.RowIndex];
                //获取oid
                int oid = Convert.ToInt32(row.Cells[0].Value);
                //获取数量
                int count = Convert.ToInt32(row.Cells[2].Value);
                //更新操作
                oiBll.UpdateCountByOid(oid, count);

                //重新计算总价
                GetTotalMoneyByOrderId();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            //获取订单编号
            int orderId = Convert.ToInt32(this.Tag);
            //获取总金额
            decimal money = Convert.ToDecimal(lblMoney.Text);
            //更新订单
            if (oiBll.SetOrderMoney(orderId, money))
            {
                MessageBox.Show("下单成功");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //获取编号
            int oid = Convert.ToInt32(dgvOrderDetail.SelectedRows[0].Cells[0].Value);
            //执行删除
            if (oiBll.DeleteDetailById(oid))
            {
                LoadDetailList();
            }
        }
    }
}
