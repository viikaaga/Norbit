using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Norbit.Tasks.Delegates
{
    /// <summary>
    /// Пользовательский делегат для расчёта итоговой стоимости одной позиции заказа.
    /// </summary>
    /// <param name="item">Товарная позиция.</param>
    /// <param name="transformedUnitPrice">Преобразованная цена за единицу товара.</param>
    /// <returns>Итоговая стоимость позиции.</returns>
    public delegate decimal ItemTotalCalculator(OrderItem item, decimal transformedUnitPrice);

    /// <summary>
    /// Представляет товарную позицию в заказе.
    /// </summary>
    public sealed class OrderItem
    {
        /// <summary>
        /// Инициализирует новый экземпляр товарной позиции.
        /// </summary>
        /// <param name="name">Наименование товара.</param>
        /// <param name="price">Цена за единицу.</param>
        /// <param name="quantity">Количество.</param>
        public OrderItem(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Цена за единицу товара.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Количество единиц товара.
        /// </summary>
        public int Quantity { get; private set; }
    }

    /// <summary>
    /// Данные события завершения обработки заказа.
    /// </summary>
    public sealed class OrderProcessedEventArgs : EventArgs
    {
        /// <summary>
        /// Инициализирует новый экземпляр аргументов события обработки заказа.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="total">Итоговая сумма заказа.</param>
        /// <param name="processedItemsCount">Количество обработанных позиций.</param>
        /// <param name="processedAt">Время завершения обработки.</param>
        public OrderProcessedEventArgs(
            int orderId,
            decimal total,
            int processedItemsCount,
            DateTimeOffset processedAt)
        {
            OrderId = orderId;
            Total = total;
            ProcessedItemsCount = processedItemsCount;
            ProcessedAt = processedAt;
        }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; private set; }

        /// <summary>
        /// Итоговая стоимость заказа.
        /// </summary>
        public decimal Total { get; private set; }

        /// <summary>
        /// Количество включённых в расчёт позиций.
        /// </summary>
        public int ProcessedItemsCount { get; private set; }

        /// <summary>
        /// Дата и время обработки заказа.
        /// </summary>
        public DateTimeOffset ProcessedAt { get; private set; }
    }

    /// <summary>
    /// Класс для обработки заказов интернет-магазина.
    /// </summary>
    public sealed class OrderProcessor
    {
        /// <summary>
        /// Событие, возникающее после успешной обработки заказа.
        /// </summary>
        public event EventHandler<OrderProcessedEventArgs> OrderProcessed;

        /// <summary>
        /// Выполняет обработку заказа, фильтрацию позиций, расчёт стоимости и логирование этапов.
        /// </summary>
        /// <param name="orderId">Номер заказа (положительное число).</param>
        /// <param name="items">Список позиций заказа.</param>
        /// <param name="shouldProcess">Предикат проверки включения товара в расчёт.</param>
        /// <param name="transformPrice">Функция преобразования цены (скидка, налог, округление).</param>
        /// <param name="calculateItemTotal">Делегат расчёта стоимости позиции.</param>
        /// <param name="log">Делегат для журналирования этапов обработки.</param>
        /// <returns>Итоговая стоимость заказа.</returns>
        public decimal Process(
            int orderId,
            IEnumerable<OrderItem> items,
            Predicate<OrderItem> shouldProcess,
            Func<decimal, decimal> transformPrice,
            ItemTotalCalculator calculateItemTotal,
            Action<string> log = null)
        {
            #region Валидация входных аргументов

            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException("orderId", "Номер заказа должен быть положительным числом.");
            }
            if (items == null)
            {
                throw new ArgumentNullException("items", "Коллекция товаров не может быть null.");
            }
            if (shouldProcess == null)
            {
                throw new ArgumentNullException("shouldProcess", "Предикат фильтрации не может быть null.");
            }
            if (transformPrice == null)
            {
                throw new ArgumentNullException("transformPrice", "Функция преобразования цены не может быть null.");
            }
            if (calculateItemTotal == null)
            {
                throw new ArgumentNullException("calculateItemTotal", "Делегат расчёта стоимости позиции не может быть null.");
            }

            #endregion

            WriteLog(log, string.Format("Начало обработки заказа #{0}.", orderId));

            decimal totalOrderAmount = 0m;
            int processedCount = 0;

            foreach (OrderItem item in items)
            {
                if (item == null)
                {
                    throw new ArgumentException("Позиция заказа не может быть null.", "items");
                }
                if (item.Price < 0)
                {
                    throw new ArgumentOutOfRangeException("items", string.Format("Цена товара '{0}' не может быть отрицательной.", item.Name));
                }
                if (item.Quantity <= 0)
                {
                    throw new ArgumentOutOfRangeException("items", string.Format("Количество товара '{0}' должно быть положительным.", item.Name));
                }

                // 1. Проверяем, разрешено ли включать товар в расчёт
                if (!shouldProcess(item))
                {
                    WriteLog(log, string.Format("Товар '{0}' (цена: {1:F2}) исключён из расчёта.", item.Name, item.Price));
                    continue;
                }

                // 2. Преобразуем цену единицы товара
                decimal transformedPrice = transformPrice(item.Price);
                WriteLog(log, string.Format("Товар '{0}': цена преобразована с {1:F2} до {2:F2} руб.", item.Name, item.Price, transformedPrice));

                // 3. Рассчитываем стоимость позиции
                decimal itemTotal = calculateItemTotal(item, transformedPrice);
                WriteLog(log, string.Format("Товар '{0}' (x{1}): итоговая стоимость позиции = {2:F2} руб.", item.Name, item.Quantity, itemTotal));

                totalOrderAmount += itemTotal;
                processedCount++;
            }

            WriteLog(log, string.Format("Завершение обработки заказа #{0}. Обработано позиций: {1}, Итого: {2:F2} руб.",
                orderId, processedCount, totalOrderAmount));

            // 4. Формируем аргументы и вызываем событие
            OrderProcessedEventArgs args = new OrderProcessedEventArgs(
                orderId,
                totalOrderAmount,
                processedCount,
                DateTimeOffset.Now);

            OnOrderProcessed(args);

            return totalOrderAmount;
        }

        /// <summary>
        /// Вызывает событие завершения обработки заказа с изоляцией ошибок подписчиков.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        private void OnOrderProcessed(OrderProcessedEventArgs e)
        {
            EventHandler<OrderProcessedEventArgs> handlers = OrderProcessed;
            if (handlers == null)
            {
                return;
            }

            foreach (EventHandler<OrderProcessedEventArgs> handler in handlers.GetInvocationList())
            {
                try
                {
                    handler(this, e);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Ошибка обработчика события: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Вспомогательный метод для безопасного вызова делегата журналирования.
        /// </summary>
        private static void WriteLog(Action<string> log, string message)
        {
            if (log != null)
            {
                log(message);
            }
        }
    }

    /// <summary>
    /// Основной класс приложения для демонстрации работы с делегатами и событиями.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Обработка заказов интернет-магазина\n");

            List<OrderItem> items = new List<OrderItem>
            {
                new OrderItem("Ноутбук", 50000m, 1),
                new OrderItem("Игровая мышь", 2500m, 2),
                new OrderItem("Батарейка АА", 50m, 4), // Не пройдет фильтр минимальной цены
                new OrderItem("Клавиатура", 4000m, 1)
            };

            OrderProcessor processor = new OrderProcessor();

            // 1. Мультикаст-делегат для логирования (консоль + журнал аудита)
            Action<string> logger = LogToConsole;
            logger += LogToAudit;

            // 2. Замыкание: захватываем локальную переменную minAllowedPrice в предикате
            decimal minAllowedPrice = 100m;
            Predicate<OrderItem> shouldProcess = item => item.Price >= minAllowedPrice;

            // 3. Лямбда Func: скидка 10% и округление до 2 знаков
            Func<decimal, decimal> applyDiscount = price => Math.Round(price * 0.90m, 2);

            // 4. Пользовательский делегат: цена со скидкой * количество
            ItemTotalCalculator calculator = (item, unitPrice) => unitPrice * item.Quantity;

            // 5. Подписка на событие (сохраняем обработчик в переменную для последующей отписки)
            EventHandler<OrderProcessedEventArgs> notificationHandler = (sender, args) =>
            {
                Console.WriteLine(string.Format(
                    "\nЗаказ #{0} успешно оформлен в {1:HH:mm:ss}. Позиций: {2}, Сумма к оплате: {3:F2} руб.",
                    args.OrderId, args.ProcessedAt, args.ProcessedItemsCount, args.Total));
            };

            // Обработчик с исключением для демонстрации изоляции ошибок в цепочке подписчиков
            EventHandler<OrderProcessedEventArgs> faultyHandler = (sender, args) =>
            {
                throw new InvalidOperationException("Сбой внешнего сервиса аналитики.");
            };

            processor.OrderProcessed += faultyHandler;
            processor.OrderProcessed += notificationHandler;

            // Запуск обработки первого заказа
            decimal finalTotal = processor.Process(
                101,
                items,
                shouldProcess,
                applyDiscount,
                calculator,
                logger);

            Console.WriteLine("\nИтоговый результат метода Process: " + finalTotal.ToString("F2", CultureInfo.InvariantCulture) + " руб.\n");

            // 6. Демонстрация отписки от события и изменения переменной замыкания
            processor.OrderProcessed -= faultyHandler;
            processor.OrderProcessed -= notificationHandler;
            minAllowedPrice = 3000m; // Меняем порог в замыкании (теперь отсеется и мышь)

            Console.WriteLine("Повторный запуск");
            processor.Process(102, items, shouldProcess, applyDiscount, calculator, LogToConsole);
        }

        private static void LogToConsole(string message)
        {
            Console.WriteLine(message);
        }

        private static void LogToAudit(string message)
        {
            Console.WriteLine(message);
        }
    }
}