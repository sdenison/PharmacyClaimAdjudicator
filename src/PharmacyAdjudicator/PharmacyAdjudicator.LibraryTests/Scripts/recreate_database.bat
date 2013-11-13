rem sqlcmd -S localhost\SQLEXPRESS -i Scripts\Create_database_with_test_data.sql
sqlcmd -S localhost\SQLEXPRESS -i Scripts\PharmacyAdjFromDatabase.edmx.sql
sqlcmd -S localhost\SQLEXPRESS -i Scripts\Create_test_data.sql