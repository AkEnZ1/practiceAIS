using System.Data.Entity;
using System.Data.SQLite;
using LogicAndModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для Entity Framework
    /// </summary>
    public class EmployeeContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр EmployeeContext
        /// </summary>
        public EmployeeContext() : base(new SQLiteConnection("Data Source=EmployeeDatabase.sqlite"), true)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EmployeeContext>());
        }

        /// <summary>
        /// Набор данных сотрудников
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
    }
}