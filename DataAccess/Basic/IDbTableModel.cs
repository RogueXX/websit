using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mor.DataAccess
{

    public interface IDbTableModel
    {
        string GetTableName();
        string[] GetPrimaryKeys();
    }
}
