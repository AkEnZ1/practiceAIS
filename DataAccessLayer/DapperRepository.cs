// DataAccessLayer/DapperRepository.cs
using Dapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий для работы с сотрудниками using Dapper
    /// </summary>
    public class DapperRepository : IRepository<Employee>
    {
        private readonly string _connectionString;

        /// <summary>
        /// Инициализирует новый экземпляр DapperRepository
        /// </summary>
        public DapperRepository()
        {
            string basePath = FindSolutionRoot();
            string dbPath = Path.Combine(basePath, "EmployeeDatabase.sqlite");
            _connectionString = $"Data Source={dbPath}";

            InitializeDatabase();
        }

        /// <summary>
        /// Находит корневую директорию решения
        /// </summary>
        /// <returns>Путь к корневой директории решения</returns>
        private string FindSolutionRoot()
        {
            DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory?.FullName ?? Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Инициализирует базу данных и создает таблицу при необходимости
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                  
                    var createTableSql = @"
                        CREATE TABLE IF NOT EXISTS Employees (
                            ID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Vacancy INTEGER NOT NULL,
                            WorkExp INTEGER NOT NULL
                        )";

                    connection.Execute(createTableSql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка инициализации базы данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавляет нового сотрудника в базу данных
        /// </summary>
        /// <param name="entity">Сотрудник для добавления</param>
        public void Add(Employee entity)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"INSERT INTO Employees (Name, Vacancy, WorkExp) 
                       VALUES (@Name, @Vacancy, @WorkExp);
                       SELECT last_insert_rowid()";
                    entity.ID = connection.Query<int>(sql, new
                    {
                        Name = entity.Name,
                        Vacancy = (int)entity.Vacancy,
                        WorkExp = entity.WorkExp
                    }).First();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception($"Ошибка базы данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Удаляет сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var sql = "DELETE FROM Employees WHERE ID = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="entity">Сотрудник с обновленными данными</param>
        public void Update(Employee entity)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var sql = @"UPDATE Employees 
                           SET Name = @Name, Vacancy = @Vacancy, WorkExp = @WorkExp 
                           WHERE ID = @ID";
                connection.Execute(sql, entity);
            }
        }

        /// <summary>
        /// Получает сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Найденный сотрудник или null</returns>
        public Employee GetById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Employee>("SELECT * FROM Employees WHERE ID = @Id", new { Id = id });
            }
        }

        /// <summary>
        /// Получает всех сотрудников из базы данных
        /// </summary>
        /// <returns>Коллекция всех сотрудников</returns>
        public IEnumerable<Employee> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Employee>("SELECT * FROM Employees");
            }
        }
    }
}