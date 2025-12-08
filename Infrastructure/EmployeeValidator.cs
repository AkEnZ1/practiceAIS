using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Shared.Interfaces;

namespace Infrastructure.Validation
{
    /// <summary>
    /// Валидатор для данных сотрудников
    /// </summary>
    public class EmployeeValidator : IValidator
    {
        public bool ValidateName(string name, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(name))
            {
                error = "Имя не может быть пустым";
                return false;
            }

            if (name.Length < 2)
            {
                error = "Имя должно содержать минимум 2 символа";
                return false;
            }

            if (name.Length > 100)
            {
                error = "Имя не может превышать 100 символов";
                return false;
            }

            // Проверка на допустимые символы (только буквы, пробелы и дефисы)
            if (!Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$"))
            {
                error = "Имя может содержать только буквы, пробелы и дефисы";
                return false;
            }

            return true;
        }

        public bool ValidateWorkExperience(int workExp, out string error)
        {
            error = null;

            if (workExp < 0)
            {
                error = "Опыт работы не может быть отрицательным";
                return false;
            }

            if (workExp > 50)
            {
                error = "Опыт работы не может превышать 50 лет";
                return false;
            }

            return true;
        }

        public bool ValidateEmployeeIndex(int index, int totalEmployees, out string error)
        {
            error = null;

            if (index < 0)
            {
                error = "Индекс не может быть отрицательным";
                return false;
            }

            if (index >= totalEmployees)
            {
                error = $"Индекс {index} выходит за пределы списка (всего сотрудников: {totalEmployees})";
                return false;
            }

            return true;
        }

        public bool ValidateEmployeeData(string name, int workExp, out List<string> errors)
        {
            errors = new List<string>();

            if (!ValidateName(name, out var nameError))
                errors.Add(nameError);

            if (!ValidateWorkExperience(workExp, out var expError))
                errors.Add(expError);

            return errors.Count == 0;
        }
    }
}