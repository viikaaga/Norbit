using System;
using System.Globalization;
using System.Text;

namespace Norbit.Tasks.TypeOfData
{
    /// <summary>
    /// Класс для выполнения расчетов по финансовым задачам.
    /// </summary>
    public class CompoundInterestCalculator
    {
        /// <summary>
        /// Формирует строку с расчетом сложных процентов по годам.
        /// </summary>
        /// <param name="initialDeposit">Начальный вклад (положительное число).</param>
        /// <param name="years">Количество лет (положительное целое число).</param>
        /// <param name="interestRate">Годовая процентная ставка (положительное число).</param>
        /// <returns>Строка с поэтапным расчетом накоплений.</returns>
        public static string CalculateCompoundInterest(decimal initialDeposit, int years, decimal interestRate)
        {
            #region Валидация

            CheckIsPositive(initialDeposit, nameof(initialDeposit), "Стартовый размер вклада должен быть больше 0.");
            CheckIsPositive(years, nameof(years), "Количество лет должно быть больше 0.");
            CheckIsPositive(interestRate, nameof(interestRate), "Процентная ставка должна быть больше 0.");

            #endregion

            var stringBuilder = new StringBuilder();
            decimal currentAmount = initialDeposit;

            for (int year = 1; year <= years; year++)
            {
                // Формула сложного процента за 1 год
                currentAmount += currentAmount * (interestRate / 100m);

                // InvariantCulture гарантирует точку в качестве разделителя (1100.00), как в условии
                stringBuilder.AppendLine($"Год {year}: {currentAmount.ToString("F2", CultureInfo.InvariantCulture)} руб.");
            }

            // Убираем последний перевод строки, чтобы вывод был чистым
            return stringBuilder.ToString().TrimEnd();
        }

        /// <summary>
        /// Проверяет, что значение аргумента строго больше нуля.
        /// </summary>
        private static void CheckIsPositive(decimal value, string paramName, string errorMessage)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, errorMessage);
            }
        }
    }
}
