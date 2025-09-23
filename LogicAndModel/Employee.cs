using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAndModel
{
    public enum VacancyType
    {
        Head,
        Intern,
        Manager
    }

    public class Employee
    {
        public int WorkExp { get; set; }
        public string Name { get; set; }
        public VacancyType Vacancy { get; set; }

        /// <summary>
        /// Возвращает строковое представление информации о сотруднике.
        /// </summary>
        /// <returns>
        /// Строка в формате: "Имя: {Name}, Должность: {Vacancy}, Опыт: {WorkExp} лет".
        /// </returns>
        public override string ToString()
        {
            return $"Имя: {Name}, Должность: {Vacancy}, Опыт: {WorkExp} лет";
        }
    }
}
