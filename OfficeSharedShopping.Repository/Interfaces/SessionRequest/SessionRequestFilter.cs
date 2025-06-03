using System.Data.SqlTypes;

namespace OfficeSharedShopping.Repository.Interfaces.SessionRequest
{
    public class SessionRequestFilter
    {
        public SqlInt32? SessionId { get; set; }
    }
}
