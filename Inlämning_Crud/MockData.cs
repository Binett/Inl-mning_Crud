namespace Inlämning_Crud
{
    class MockData
    {
        internal static void FillDataTable()
        {
            var db = new SQLDatabase();
            db.ExecuteSQL(@"insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Lillie', 'Binett', '2015', '0', 3, 4);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Felicia', 'Binett', '2014', '0', 3, 4);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Cecilia', 'Binett', '1989', '0', 10, 11);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Tobias', 'Binett', '1989', '0', 6, 7);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Sara', 'Binett', '1989', '0', 6, 7);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Lena', 'Binett', '1965', '0', 8, 9);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Leif', 'Johansson', '1965', '0', 0, 0);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Sigrid', 'Binett', '1948', '0', 0, 0);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Lars', 'Binett', '1945', '0', 0, 0);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Carina', 'Sverin', '1960', '0', 0, 0);
                            insert into Family (firstName, lastName, born, died, motherId, fatherId) values ('Lars', 'Sverin', '1960', '0', 0, 0);                                
                            ");
        }
    }
}
