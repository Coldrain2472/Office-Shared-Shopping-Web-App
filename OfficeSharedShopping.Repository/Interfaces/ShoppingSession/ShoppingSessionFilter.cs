using System.Data.SqlTypes;

namespace OfficeSharedShopping.Repository.Interfaces.ShoppingSession
{
    public class ShoppingSessionFilter
    {
        public SqlInt32? CreatedByEmployeeId { get; set; }

        public SqlBoolean? IsActive { get; set; }

        public SqlInt32? StoreId { get; set; }
    }
}
