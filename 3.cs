using System;
using System.Globalization;

namespace Norbit.Tasks.OOP
{
    /// <summary>
    /// Описывает товар.
    /// </summary>
    public class Product
    {
        #region Поля
        private string _name;
        private string _manufacturer;
        private decimal _price;
        private int _shelfLifeDays;
        private DateTime _manufactureDate;
        #endregion

        #region Свойства
        public string Name {
            get { return _name; }
            set {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Название не может быть пустым");
                _name = value;
            }
        }

        public string Manufacturer {
            get { return _manufacturer; }
            set {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Производитель не может быть пустым");
                _manufacturer = value;
            }
        }

        public decimal Price {
            get { return _price; }
            set { if (value <= 0) throw new ArgumentOutOfRangeException(); _price = value; }
        }

        public int ShelfLifeDays {
            get { return _shelfLifeDays; }
            set { if (value <= 0) throw new ArgumentOutOfRangeException(); _shelfLifeDays = value; }
        }

        public DateTime ManufactureDate {
            get { return _manufactureDate; }
            set { if (value > DateTime.Now) throw new ArgumentException("Дата в будущем"); _manufactureDate = value; }
        }
        #endregion

        public Product(string name, string manufacturer, decimal price, int shelfLifeDays, DateTime manufactureDate)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            ShelfLifeDays = shelfLifeDays;
            ManufactureDate = manufactureDate;
        }

        /// <summary>
        /// Переопределение ToString для вывода инфо о товаре.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Товар: {0} | Производитель: {1} | Цена: {2:F2} руб. | Дата пр-ва: {3:dd.MM.yyyy}", 
                Name, Manufacturer, Price, ManufactureDate);
        }
    }

    /// <summary>
    /// Основной класс программы.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Установка кодировки для нормального отображения русского языка
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(" Программа: Товароведение ");

            try {
                Console.Write("Введите название товара: ");
                string name = Console.ReadLine();

                Console.Write("Введите производителя: ");
                string manufacturer = Console.ReadLine();

                Console.Write("Введите цену: ");
                decimal price = decimal.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);

                Console.Write("Срок годности (дней): ");
                int days = int.Parse(Console.ReadLine());

                Console.Write("Дата производства (дд.мм.гггг): ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

                // Создаем объект
                Product prod = new Product(name, manufacturer, price, days, date);

                Console.WriteLine("\nРезультат:");
                Console.WriteLine(prod.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}