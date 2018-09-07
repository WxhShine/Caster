namespace CaterModel {
    public partial class MemberInfo
   {
       public string MTypeTitle { get; set; }
       public decimal MDiscount { get; set; }
   }

    public partial class DishInfo
    {
        public string DTypeTitle { get; set; }
    }

    public partial class TableInfo
    {
        public string HallTitle { get; set; }
    }

    public partial class OrderDetailInfo
    {
        public string DTitle { get; set; }
        public decimal DPrice { get; set; }
    }
}
