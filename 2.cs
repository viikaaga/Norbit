using System;
using System.Text;

namespace Norbit.Tasks.TypeOfData
{
    /// <summary>
    /// Класс для отрисовки геометрических фигур в консоли.
    /// </summary>
    public class DiamondDrawer
    {
        /// <summary>
        /// Формирует строку с изображением контура ромба из символов 'X'.
        /// </summary>
        /// <param name="n">Длина диагонали ромба (положительное нечётное число).</param>
        /// <returns>Строковое представление ромба.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если <paramref name="n"/> меньше или равно 0.</exception>
        /// <exception cref="ArgumentException">Выбрасывается, если <paramref name="n"/> четное число.</exception>
        public static string BuildDiamond(int n)
        {
            #region Валидация
            ValidateDimension(n);
            #endregion

            int center = n / 2;
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Манхэттенское расстояние от текущей точки (i, j) до центра матрицы
                    int distanceFromCenter = Math.Abs(i - center) + Math.Abs(j - center);

                    // Точка принадлежит контуру ромба, если расстояние равно радиусу (center)
                    if (distanceFromCenter == center)
                    {
                        // Центр ромба всегда должен оставаться пустым (важно для случая N = 1)
                        if (i == center && j == center)
                        {
                            stringBuilder.Append(' ');
                        }
                        else
                        {
                            stringBuilder.Append('X');
                        }
                    }
                    else
                    {
                        stringBuilder.Append(' ');
                    }
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Выводит ромб на экран консоли.
        /// </summary>
        /// <param name="n">Длина диагонали (положительное нечётное число).</param>
        public static void DrawDiamond(int n)
        {
            Console.Write(BuildDiamond(n));
        }

        /// <summary>
        /// Проверяет корректность размера ромба.
        /// </summary>
        /// <param name="n">Размер диагонали.</param>
        private static void ValidateDimension(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Размер ромба должен быть строго больше 0.");
            }

            if (n % 2 == 0)
            {
                throw new ArgumentException("Размер диагонали ромба должен быть нечётным числом.", nameof(n));
            }
        }
    }

    /// <summary>
    /// Точка входа для тестирования программы.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Тест 1: Пример из условия (N = 5)
            Console.WriteLine("Ромб для N = 5:");
            DiamondDrawer.DrawDiamond(5);

            Console.WriteLine();

            // Тест 2: Минимальный ромб (N = 3)
            Console.WriteLine("Ромб для N = 3:");
            DiamondDrawer.DrawDiamond(3);

            Console.WriteLine();

            // Тест 3: Большой ромб (N = 7)
            Console.WriteLine("Ромб для N = 7:");
            DiamondDrawer.DrawDiamond(7);
        }
    }
}