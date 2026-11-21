using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Norbit.Tasks.TypeOfData
{
    /// <summary>
    /// Калькулятор вклада.
    /// </summary>
    public class DepositCalculator
    {
        /// <summary>
        /// Вычисляет сложный процент по годам.
        /// </summary>
        /// <param name="initialDeposit">Стартовый размер вклада.</param>
        /// <param name="years">Количество лет.</param>
        /// <param name="interestRate">Годовая процентная ставка.</param>
        /// <returns>Список кортежей (год, сумма накоплений).</returns>
        public static List<(int year, double deposit)> CalculateCompoundInterest(
            double initialDeposit, int years, double interestRate)
        {
            #region Валидация
            CheckIsPositive(initialDeposit, "Стартовый размер вклада ожидается > 0.");
            CheckIsPositive(years, "Кол-во лет ожидается > 0.");
            CheckIsPositive(interestRate, "Ставка ожидается > 0.");
            #endregion

            double amount = initialDeposit;
            var result = new List<(int year, double deposit)>();

            for (int i = 1; i <= years; i++)
            {
                amount += amount / 100 * interestRate;
                result.Add((i, amount));
            }

            return result;
        }

        /// <summary>
        /// Формирует и возвращает строку с расчетом сложных процентов по годам.
        /// </summary>
        /// <param name="initialDeposit">Начальный вклад (положительное число).</param>
        /// <param name="years">Количество лет (положительное целое число).</param>
        /// <param name="interestRate">Годовая процентная ставка (положительное число).</param>
        /// <returns>Строка с расчетом накоплений по годам.</returns>
        public static string GetCompoundInterestString(
            double initialDeposit, int years, double interestRate)
        {
            var calculations = CalculateCompoundInterest(initialDeposit, years, interestRate);
            var stringBuilder = new StringBuilder();

            foreach (var item in calculations)
            {
                // CultureInfo.InvariantCulture гарантирует точку (1100.00), а не запятую (1100,00)
                string formattedAmount = item.deposit.ToString("F2", CultureInfo.InvariantCulture);
                stringBuilder.AppendLine($"Год {item.year}: {formattedAmount} руб.");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        /// <summary>
        /// Проверяет, что <paramref name="value"/> положительное.
        /// </summary>
        /// <param name="value">Проверяемое значение.</param>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        /// <exception cref="ArgumentOutOfRangeException">Если <paramref name="value"/> меньше или равно нулю.</exception>
        private static void CheckIsPositive(double value, string errorMessage)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), errorMessage);
            }
        }
    }

    /// <summary>
    /// Точка входа в программу для тестирования.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Тестовые данные из условия задачи
            double initialDeposit = 1000;
            int years = 3;
            double interestRate = 10;

            string report = DepositCalculator.GetCompoundInterestString(initialDeposit, years, interestRate);
            
            Console.WriteLine(report);
        }
    }
}