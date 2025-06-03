using System.Data.SqlTypes;

namespace OfficeSharedShopping.Repository.Interfaces.SessionRequest
{
    public class SessionRequestUpdate
    {
        public SqlInt32? Quantity { get; set; }

        public SqlDecimal? MaxPrice { get; set; }

        public SqlInt32? ProductId { get; set; }
    }
}
