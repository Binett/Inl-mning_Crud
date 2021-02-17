using System;
using System.Collections.Generic;
using System.Text;

namespace Inlämning_Crud
{
    class MockData
    {
        internal static void FillDataTable()
        {
            var db = new SQLDatabase();
            db.ExecuteSQL(@"insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Tobias', 'Binett', '1989', '0', 2, 3);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Lena', 'Binett', '1965', '0', 0, 0);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Leif', 'Johansson', '1961', '0', 0, 0);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Sara', 'Binett', '1987', '0', 2, 3);");
        }
    }
}
