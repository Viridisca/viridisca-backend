using System.Data.Common;

namespace Viridisca.Common.Application.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
