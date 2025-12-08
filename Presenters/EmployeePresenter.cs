using System;
using System.Collections.Generic;
using BusinessLogic.Interfaces;
using DomainModel;
using Shared.Interfaces;

namespace Presenters
{
    /// <summary>
    /// Главный презентер приложения, реализующий паттерн MVP.
    /// Координирует взаимодействие между View и бизнес-логикой,
    /// обрабатывает события пользовательского интерфейса и управляет
    /// потоком данных в приложении.
    /// </summary>
    /// <remarks>
    /// Является центральным координатором в архитектуре MVP:
    /// - Получает события от View через интерфейс IEmployeeView
    /// - Обрабатывает бизнес-логику через фасад ILogic
    /// - Обновляет View результатами операций
    /// - Управляет жизненным циклом представления
    /// </remarks>
    public class EmployeePresenter : IApplicationPresenter
    {
        private IEmployeeView _view;
        private readonly ILogic _logic;

        /// <summary>
        /// Инициализирует новый экземпляр EmployeePresenter
        /// </summary>
        /// <param name="logic">Фасад бизнес-логики приложения</param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если logic равен null
        /// </exception>
        public EmployeePresenter(ILogic logic)
        {
            _logic = logic ?? throw new ArgumentNullException(nameof(logic));
        }

        /// <summary>
        /// Присоединяет представление к презентеру и инициализирует подписку на события
        /// </summary>
        /// <param name="view">Представление для присоединения</param>
        /// <exception cref="InvalidOperationException">
        /// Выбрасывается, если представление уже присоединено
        /// </exception>
        public void AttachView(IEmployeeView view)
        {
            if (_view != null)
                throw new InvalidOperationException("View уже присоединен к Presenter");

            _view = view;
            SubscribeToViewEvents();
            Initialize();
        }

        /// <summary>
        /// Отсоединяет представление от презентера и отменяет подписки на события
        /// </summary>
        /// <remarks>
        /// Освобождает ресурсы и предотвращает утечки памяти.
        /// Должен вызываться при завершении работы с представлением.
        /// </remarks>
        public void DetachView()
        {
            if (_view == null) return;

            UnsubscribeFromViewEvents();
            _view = null;
        }

        /// <summary>
        /// Инициализирует презентер и загружает начальные данные
        /// </summary>
        /// <remarks>
        /// Вызывается автоматически после присоединения View.
        /// Загружает список сотрудников и отображает приветственное сообщение.
        /// </remarks>
        public void Initialize()
        {
            RefreshEmployeeList();
            _view.ShowMessage("Система управления сотрудниками запущена");
        }

        /// <summary>
        /// Подписывается на события представления
        /// </summary>
        /// <remarks>
        /// Устанавливает обработчики для всех событий, которые может генерировать View.
        /// Вызывается автоматически при присоединении View.
        /// </remarks>
        private void SubscribeToViewEvents()
        {
            if (_view == null) return;

            _view.OnAddEmployee += HandleAddEmployee;
            _view.OnUpdateEmployee += HandleUpdateEmployee;
            _view.OnDeleteEmployee += HandleDeleteEmployee;
            _view.OnEmployeeSelected += HandleEmployeeSelected;
            _view.OnCalculateSalary += HandleCalculateSalary;
            _view.OnAddWorkExperience += HandleAddWorkExperience;
            _view.OnShowStatistics += HandleShowStatistics;
            _view.OnFilterByVacancy += HandleFilterByVacancy;
            _view.OnShowAllEmployees += HandleShowAllEmployees;
            _view.OnFindByIndex += HandleFindByIndex;
        }

        /// <summary>
        /// Отписывается от событий представления
        /// </summary>
        /// <remarks>
        /// Удаляет обработчики событий для предотвращения утечек памяти.
        /// Вызывается автоматически при отсоединении View.
        /// </remarks>
        private void UnsubscribeFromViewEvents()
        {
            if (_view == null) return;

            _view.OnAddEmployee -= HandleAddEmployee;
            _view.OnUpdateEmployee -= HandleUpdateEmployee;
            _view.OnDeleteEmployee -= HandleDeleteEmployee;
            _view.OnEmployeeSelected -= HandleEmployeeSelected;
            _view.OnCalculateSalary -= HandleCalculateSalary;
            _view.OnAddWorkExperience -= HandleAddWorkExperience;
            _view.OnShowStatistics -= HandleShowStatistics;
            _view.OnFilterByVacancy -= HandleFilterByVacancy;
            _view.OnShowAllEmployees -= HandleShowAllEmployees;
            _view.OnFindByIndex -= HandleFindByIndex;
        }

        /// <summary>
        /// Обновляет список сотрудников в представлении
        /// </summary>
        /// <remarks>
        /// Получает актуальный список сотрудников из бизнес-логики
        /// и передает его в представление для отображения.
        /// Обрабатывает исключения и отображает сообщения об ошибках.
        /// </remarks>
        private void RefreshEmployeeList()
        {
            try
            {
                var employees = _logic.GetEmployees();
                _view.RefreshEmployeeList(employees);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при загрузке сотрудников: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие добавления нового сотрудника
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="workExp">Опыт работы в годах</param>
        /// <param name="vacancy">Должность сотрудника</param>
        /// <remarks>
        /// Выполняет валидацию входных данных и вызывает соответствующий
        /// метод бизнес-логики. Обновляет список сотрудников после успешного добавления.
        /// </remarks>
        private void HandleAddEmployee(string name, int workExp, VacancyType vacancy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    _view.ShowError("Имя не может быть пустым!");
                    return;
                }

                if (workExp < 0)
                {
                    _view.ShowError("Опыт работы должен быть неотрицательным числом!");
                    return;
                }

                _logic.AddEmployee(name, workExp, vacancy);
                RefreshEmployeeList();
                _view.ShowMessage("Сотрудник добавлен!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при добавлении сотрудника: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие обновления данных сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <param name="name">Новое имя сотрудника</param>
        /// <param name="vacancy">Новая должность</param>
        /// <param name="workExp">Новый опыт работы</param>
        /// <remarks>
        /// Вызывает метод бизнес-логики для обновления данных сотрудника.
        /// Проверяет результат операции и обновляет список при успешном выполнении.
        /// </remarks>
        private void HandleUpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            try
            {
                var success = _logic.UpdateEmployee(index, name, vacancy, workExp);
                if (success)
                {
                    RefreshEmployeeList();
                    _view.ShowMessage("Сотрудник обновлен!");
                }
                else
                {
                    _view.ShowError("Неверный индекс сотрудника!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при обновлении сотрудника: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие удаления сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <remarks>
        /// Вызывает метод бизнес-логики для удаления сотрудника.
        /// Обновляет список сотрудников после успешного удаления.
        /// </remarks>
        private void HandleDeleteEmployee(int index)
        {
            try
            {
                _logic.DeleteEmployee(index);
                RefreshEmployeeList();
                _view.ShowMessage("Сотрудник удален!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при удалении сотрудника: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие выбора сотрудника
        /// </summary>
        /// <param name="index">Индекс выбранного сотрудника</param>
        /// <remarks>
        /// Получает подробную информацию о сотруднике по индексу
        /// и отображает ее в представлении.
        /// </remarks>
        private void HandleEmployeeSelected(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                _view.ShowEmployeeDetails(employee);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при выборе сотрудника: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие расчета зарплаты сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <remarks>
        /// Получает сотрудника по индексу, рассчитывает его зарплату
        /// через бизнес-логику и отображает результат в представлении.
        /// </remarks>
        private void HandleCalculateSalary(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                var salary = _logic.CalculateSalary(employee);
                _view.ShowMessage($"Зарплата сотрудника {employee.Name}: {salary:C}");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при расчете зарплаты: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие добавления стажа сотруднику
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <remarks>
        /// Добавляет один год опыта работы выбранному сотруднику.
        /// Отображает сообщение с информацией о предыдущем и новом стаже.
        /// Обновляет список сотрудников.
        /// </remarks>
        private void HandleAddWorkExperience(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                int oldExp = employee.WorkExp;
                _logic.AddWorkExp(employee);
                RefreshEmployeeList();
                _view.ShowMessage($"Сотруднику {employee.Name} добавлен 1 год стажа!\nБыло: {oldExp} лет\nСтало: {employee.WorkExp} лет");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при добавлении стажа: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие отображения статистики
        /// </summary>
        /// <remarks>
        /// Собирает статистическую информацию через бизнес-логику
        /// и форматирует ее для отображения в представлении.
        /// Включает общее количество сотрудников, средний опыт,
        /// распределение по должностям, общий бюджет на зарплаты
        /// и информацию о самом опытном сотруднике.
        /// </remarks>
        private void HandleShowStatistics()
        {
            try
            {
                var totalEmployees = _logic.GetTotalEmployees();
                var avgExperience = _logic.GetAverageExperience();
                var distribution = _logic.GetVacancyDistribution();
                var mostExperienced = _logic.GetMostExperiencedEmployee();
                var totalBudget = _logic.GetTotalSalaryBudget();

                var statistics = $"=== СТАТИСТИКА ПО СОТРУДНИКАМ ===\n\n" +
                               $"Общее количество сотрудников: {totalEmployees}\n" +
                               $"Средний опыт работы: {avgExperience:F1} лет\n" +
                               $"Общий бюджет на зарплаты: {totalBudget:F2} руб.\n\n" +
                               $"Распределение по должностям:\n";

                foreach (var item in distribution)
                {
                    statistics += $"  {GetVacancyRussianName(item.Key)}: {item.Value} чел.\n";
                }

                if (mostExperienced != null)
                {
                    var salary = _logic.CalculateSalary(mostExperienced);
                    statistics += $"\nСамый опытный сотрудник: {mostExperienced.Name}\n" +
                                 $"  Должность: {GetVacancyRussianName(mostExperienced.Vacancy)}\n" +
                                 $"  Опыт работы: {mostExperienced.WorkExp} лет\n" +
                                 $"  Зарплата: {salary:F2} руб.";
                }

                _view.ShowMessage(statistics);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при получении статистики: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие фильтрации сотрудников по должности
        /// </summary>
        /// <param name="vacancy">Тип должности для фильтрации</param>
        /// <remarks>
        /// Получает список сотрудников с указанной должностью
        /// и отображает его в представлении.
        /// Отображает количество найденных сотрудников.
        /// </remarks>
        private void HandleFilterByVacancy(VacancyType vacancy)
        {
            try
            {
                var employees = _logic.GetEmployeesByVacancy(vacancy);
                _view.RefreshEmployeeList(employees);
                _view.ShowMessage($"Показаны {employees.Count} {GetVacancyRussianName(vacancy).ToLower()}(ов)");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при фильтрации: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает событие отображения всех сотрудников
        /// </summary>
        /// <remarks>
        /// Сбрасывает фильтры и отображает полный список сотрудников.
        /// Вызывает метод обновления списка с отображением информационного сообщения.
        /// </remarks>
        private void HandleShowAllEmployees()
        {
            RefreshEmployeeList();
            _view.ShowMessage("Показаны все сотрудники");
        }

        /// <summary>
        /// Обрабатывает событие поиска сотрудника по индексу
        /// </summary>
        /// <param name="index">Индекс искомого сотрудника</param>
        /// <remarks>
        /// Находит сотрудника по указанному индексу и отображает
        /// подробную информацию о нем. Если сотрудник не найден,
        /// отображается соответствующее сообщение об ошибке.
        /// </remarks>
        private void HandleFindByIndex(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                _view.ShowMessage($"Найден сотрудник: {employee}");
                _view.ShowEmployeeDetails(employee);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при поиске: {ex.Message}");
            }
        }

        /// <summary>
        /// Преобразует тип должности в русское название
        /// </summary>
        /// <param name="vacancy">Тип должности</param>
        /// <returns>Локализованное название должности</returns>
        /// <remarks>
        /// Используется для отображения понятных пользователю названий
        /// должностей в интерфейсе приложения.
        /// </remarks>
        private string GetVacancyRussianName(VacancyType vacancy)
        {
            switch (vacancy)
            {
                case VacancyType.Head: return "Руководитель";
                case VacancyType.Manager: return "Менеджер";
                case VacancyType.Intern: return "Стажер";
                default: return "Неизвестно";
            }
        }
    }
}