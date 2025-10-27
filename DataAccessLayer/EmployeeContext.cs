using System.Data.Entity;
using System.Data.SQLite;
using DomainModel;

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

        /// <summary>
        /// Конфигурация модели
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Базовая конфигурация
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>().HasKey(e => e.ID);
            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Vacancy).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.WorkExp).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}