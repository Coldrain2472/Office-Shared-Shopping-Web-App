using System.Data.SqlTypes;

namespace OfficeSharedShopping.Repository.Interfaces.ShoppingSession
{
    public class ShoppingSessionUpdate
    {
        public SqlBoolean? IsActive { get; set; }

        public SqlDateTime? Deadline { get; set; }
    }
}
