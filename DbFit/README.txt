dbfit http://dbfit.github.io/dbfit/
PostgreSQL database schema test server setup
	Prior to version 10

As root
Add
local   MyCo            all                                     trust
host    MyCo            all             127.0.0.1/32            trust
host    MyCo            all             ::1/128                 trust
To /var/lib/pgsql/data/pg_hba.conf

Restart
/etc/init.d/postgresql restart

PostgreSQL database schema test server setup
	After version 9

su - postgres
mkdir -p postgres/data
initdb postgres/data
pg_ctl -D postgres/data -l logfile start

createdb -E UNICODE MyCo

psql MyCo
MyCo=#
CREATE USER test;
ALTER ROLE test WITH LOGIN;
CREATE SCHEMA Business;
GRANT ALL ON SCHEMA Business TO test;
ALTER USER test SET search_path TO Business;
\q

You may need to edit <git root>/Business/Makefile PostgreSQLServer host entry.

cd <git root>/Business
make pgsqldb

You may need to edit the Connect line in <git root>/Business/DbFit/BusinessSchema/PostgreSqlSuite/SetUp/content.txt

symbolic link <git root>/Business/DbFit/BusinessSchema to <dbfit install directory>/dbfit/FitNesseRoot/BusinessSchema
cd <dbfit install directory>
./startFitnesse.sh

http://localhost:8085/BusinessSchema
Top of a test page
!path lib/*.jar
!|dbfit.PostgresTest|
!|Connect|localhost|test|dbfit|MyCo|

For PostgreSQL tests
http://localhost:8085/BusinessSchema.PostgreSqlSuite?suite

Access the database directly
psql MyCo test
