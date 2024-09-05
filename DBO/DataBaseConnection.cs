using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using BankAPI.Models;

namespace BankAPI.DBO
{
    //Using DataBaseContext parent class to connect to the data base.
    public class DataBaseConnection: DbContext
    {
        // The data model list of all the elements.
        public DbSet<BankModel> Banks { get; set; }
        public DataBaseConnection(DbContextOptions<DataBaseConnection> options):base(options)
        {
			try
			{
                //use as conection stream.
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                // If connection is done
                if(dbCreator != null)
                {

                    // If you can't connect you create the data base.
                    if (!dbCreator.CanConnect())
                        dbCreator.Create();
                    // if it has no tables create them.
                    if(!dbCreator.HasTables())
                        dbCreator.CreateTables();
                }
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
        }
    }
}
