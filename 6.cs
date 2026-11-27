using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norbit.Tasks.LinqAnalytics
{
    public sealed class Category
    {
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }

    public sealed class Supplier
    {
        public Supplier(int id, string name, decimal rating)
        {
            Id = id;
            Name = name;
            Rating = rating;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Rating { get; private set; }
    }

    public sealed class Product
    {
        public Product(
            int id,
            string name,
            int categoryId,
            int? supplierId,
            decimal price,
            int stockQuantity,
            bool isActive,
            DateTime createdAt)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            SupplierId = supplierId;
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CategoryId { get; private set; }
        public int? SupplierId { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }

    public sealed class OrderItem
    {
        public OrderItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
    }

    public sealed class Order
    {
        public Order(int id, int customerId, DateTime createdAt, IReadOnlyList<OrderItem> items)
        {
            Id = id;
            CustomerId = customerId;
            CreatedAt = createdAt;
            Items = items;
        }

        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public IReadOnlyList<OrderItem> Items { get; private set; }
    }

    public sealed class ProductCatalogItem
    {
        public ProductCatalogItem(
            int id,
            string name,
            string categoryName,
            string supplierName,
            decimal price,
            decimal priceWithTax,
            string stockStatus,
            bool isActive)
        {
            Id = id;
            Name = name;
            CategoryName = categoryName;
            SupplierName = supplierName;
            Price = price;
            PriceWithTax = priceWithTax;
            StockStatus = stockStatus;
            IsActive = isActive;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CategoryName { get; private set; }
        public string SupplierName { get; private set; }
        public decimal Price { get; private set; }
        public decimal PriceWithTax { get; private set; }
        public string StockStatus { get; private set; }
        public bool IsActive { get; private set; }
    }

    public sealed class CategoryStockReport
    {
        public CategoryStockReport(
            string categoryName,
            int productCount,
            int activeProductCount,
            int inStockProductCount,
            int totalStockQuantity,
            decimal totalStockValue,
            decimal? averagePrice,
            decimal? minPrice,
            decimal? maxPrice)
        {
            CategoryName = categoryName;
            ProductCount = productCount;
            ActiveProductCount = activeProductCount;
            InStockProductCount = inStockProductCount;
            TotalStockQuantity = totalStockQuantity;
            TotalStockValue = totalStockValue;
            AveragePrice = averagePrice;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public string CategoryName { get; private set; }
        public int ProductCount { get; private set; }
        public int ActiveProductCount { get; private set; }
        public int InStockProductCount { get; private set; }
        public int TotalStockQuantity { get; private set; }
        public decimal TotalStockValue { get; private set; }
        public decimal? AveragePrice { get; private set; }
        public decimal? MinPrice { get; private set; }
        public decimal? MaxPrice { get; private set; }
    }

    public sealed class ProductSalesReport
    {
        public ProductSalesReport(
            int productId,
            string productName,
            int totalQuantity,
            int ordersCount,
            decimal revenue)
        {
            ProductId = productId;
            ProductName = productName;
            TotalQuantity = totalQuantity;
            OrdersCount = ordersCount;
            Revenue = revenue;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int TotalQuantity { get; private set; }
        public int OrdersCount { get; private set; }
        public decimal Revenue { get; private set; }
    }

    public sealed class StoreAnalyticsReport
    {
        public StoreAnalyticsReport(
            int totalProducts,
            int activeProducts,
            int outOfStockProducts,
            decimal totalStockValue,
            decimal? averageActiveProductPrice,
            IReadOnlyList<ProductCatalogItem> catalogPage,
            IReadOnlyList<CategoryStockReport> categories,
            IReadOnlyList<ProductSalesReport> topSellingProducts)
        {
            TotalProducts = totalProducts;
            ActiveProducts = activeProducts;
            OutOfStockProducts = outOfStockProducts;
            TotalStockValue = totalStockValue;
            AverageActiveProductPrice = averageActiveProductPrice;
            CatalogPage = catalogPage;
            Categories = categories;
            TopSellingProducts = topSellingProducts;
        }

        public int TotalProducts { get; private set; }
        public int ActiveProducts { get; private set; }
        public int OutOfStockProducts { get; private set; }
        public decimal TotalStockValue { get; private set; }
        public decimal? AverageActiveProductPrice { get; private set; }
        public IReadOnlyList<ProductCatalogItem> CatalogPage { get; private set; }
        public IReadOnlyList<CategoryStockReport> Categories { get; private set; }
        public IReadOnlyList<ProductSalesReport> TopSellingProducts { get; private set; }
    }

    public static class TestDataFactory
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category(1, "Ноутбуки"),
                new Category(2, "Периферия"),
                new Category(3, "Комплектующие"),
                new Category(4, "Сеть"),
                new Category(5, "Прочее") // пустая категория
            };
        }

        public static List<Supplier> GetSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier(1, "ТехноПоставка", 4.9m),
                new Supplier(2, "ОптКомп", 4.6m),
                new Supplier(3, "Дистрибьютор Плюс", 4.8m),
                new Supplier(4, "СмартТрейд", 4.3m),
                new Supplier(5, "Новый поставщик", 4.0m) // поставщик без товаров
            };
        }

        public static List<Product> GetProducts()
        {
            DateTime date = new DateTime(2026, 1, 15);

            return new List<Product>
            {
                new Product(1, "Ноутбук Asus ROG", 1, 1, 120000m, 3, true, date),
                new Product(2, "Ноутбук Lenovo IdeaPad", 1, 1, 65000m, 10, true, date.AddDays(1)),
                new Product(3, "Ноутбук HP Pavilion", 1, 2, 52000m, 8, true, date.AddDays(2)),
                new Product(4, "Ноутбук Acer Aspire", 1, null, 45000m, 4, true, date.AddDays(3)),
                new Product(5, "Ноутбук Dell Vostro", 1, 2, 38000m, 0, false, date.AddDays(4)),

                new Product(6, "Мышь Logitech G102", 2, 2, 2500m, 25, true, date.AddDays(5)),
                new Product(7, "Мышь Razer DeathAdder", 2, 3, 2500m, 15, true, date.AddDays(6)),
                new Product(8, "Клавиатура Defender", 2, 2, 2500m, 12, true, date.AddDays(7)),
                new Product(9, "Клавиатура HyperX", 2, 3, 7200m, 5, true, date.AddDays(8)),
                new Product(10, "Монитор LG 27", 2, 1, 24000m, 6, true, date.AddDays(9)),
                new Product(11, "Веб-камера Logitech", 2, null, 3800m, 0, true, date.AddDays(10)),

                new Product(12, "SSD Kingston 1TB", 3, 3, 8500m, 20, true, date.AddDays(11)),
                new Product(13, "ОЗУ Kingston 32GB", 3, 3, 11000m, 14, true, date.AddDays(12)),
                new Product(14, "Видеокарта RTX 4060", 3, 1, 48000m, 2, true, date.AddDays(13)),
                new Product(15, "Блок питания DeepCool", 3, 4, 6800m, 9, true, date.AddDays(14)),
                new Product(16, "Корпус Zalman", 3, 4, 5400m, 7, true, date.AddDays(15)),
                new Product(17, "Звуковая карта Asus", 3, 4, 3200m, 4, false, date.AddDays(16)),

                new Product(18, "Роутер TP-Link", 4, 4, 4900m, 11, true, date.AddDays(17)),
                new Product(19, "Коммутатор D-Link", 4, 2, 2100m, 18, true, date.AddDays(18)),
                new Product(20, "Wi-Fi адаптер TP-Link", 4, 2, 1200m, 3, true, date.AddDays(19)),
                new Product(21, "Кабель патч-корд 5м", 4, 4, 450m, 50, true, date.AddDays(20))
            };
        }

        public static List<Order> GetOrders()
        {
            DateTime date = new DateTime(2026, 2, 1);

            return new List<Order>
            {
                new Order(101, 1, date, new List<OrderItem>
                {
                    new OrderItem(2, 1, 65000m),
                    new OrderItem(6, 2, 2500m),
                    new OrderItem(12, 1, 8500m)
                }),
                new Order(102, 2, date.AddDays(1), new List<OrderItem>
                {
                    new OrderItem(10, 2, 24000m),
                    new OrderItem(9, 1, 7200m),
                    new OrderItem(6, 1, 2500m)
                }),
                new Order(103, 3, date.AddDays(2), new List<OrderItem>
                {
                    new OrderItem(1, 1, 120000m),
                    new OrderItem(13, 2, 11000m)
                }),
                new Order(104, 1, date.AddDays(3), new List<OrderItem>
                {
                    new OrderItem(12, 2, 8500m),
                    new OrderItem(12, 1, 8500m),
                    new OrderItem(18, 1, 4900m)
                }),
                new Order(105, 4, date.AddDays(4), new List<OrderItem>
                {
                    new OrderItem(14, 1, 48000m),
                    new OrderItem(15, 1, 6800m),
                    new OrderItem(16, 1, 5400m)
                }),
                new Order(106, 5, date.AddDays(5), new List<OrderItem>
                {
                    new OrderItem(3, 2, 52000m),
                    new OrderItem(8, 2, 2500m),
                    new OrderItem(19, 3, 2100m)
                }),
                new Order(107, 2, date.AddDays(6), new List<OrderItem>
                {
                    new OrderItem(2, 1, 65000m),
                    new OrderItem(4, 1, 45000m),
                    new OrderItem(20, 2, 1200m)
                }),
                new Order(108, 6, date.AddDays(7), new List<OrderItem>
                {
                    new OrderItem(10, 1, 24000m),
                    new OrderItem(12, 3, 8500m),
                    new OrderItem(7, 1, 2500m)
                })
            };
        }
    }

    public static class StoreAnalyticsService
    {
        public static StoreAnalyticsReport BuildReport(
            IEnumerable<Category> categories,
            IEnumerable<Supplier> suppliers,
            IEnumerable<Product> products,
            IEnumerable<Order> orders,
            decimal? minPrice,
            decimal? maxPrice,
            int? categoryId,
            string searchText,
            int pageNumber,
            int pageSize)
        {
            #region Валидация
            if (categories == null) throw new ArgumentNullException("categories");
            if (suppliers == null) throw new ArgumentNullException("suppliers");
            if (products == null) throw new ArgumentNullException("products");
            if (orders == null) throw new ArgumentNullException("orders");

            if (pageNumber < 1) throw new ArgumentOutOfRangeException("pageNumber");
            if (pageSize < 1) throw new ArgumentOutOfRangeException("pageSize");

            if (minPrice.HasValue && maxPrice.HasValue && minPrice.Value > maxPrice.Value)
            {
                throw new ArgumentException("minPrice не может быть больше maxPrice.");
            }

            if (!products.All(p => p != null && p.Price >= 0m) || products.Any(p => p.StockQuantity < 0))
            {
                throw new ArgumentException("Некорректные данные товара (отрицательная цена или остаток).");
            }

            bool hasBadOrderItems = orders
                .SelectMany(o => o.Items)
                .Any(i => i.Quantity <= 0 || i.UnitPrice < 0m);

            if (hasBadOrderItems)
            {
                throw new ArgumentException("Некорректные данные в строках заказа.");
            }
            #endregion

            IEnumerable<Product> filteredProducts = products;

            filteredProducts = filteredProducts.Where(p => p.IsActive);

            if (minPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice.Value);
            }

            if (categoryId.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                string search = searchText.Trim();
                filteredProducts = filteredProducts.Where(p =>
                    p.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            filteredProducts = filteredProducts.Where(p => p.StockQuantity > 0);

            var catalogQuery =
                from product in filteredProducts
                join category in categories
                    on product.CategoryId equals category.Id into catGroup
                from category in catGroup.DefaultIfEmpty()
                join supplier in suppliers
                    on product.SupplierId equals (int?)supplier.Id into supGroup
                from supplier in supGroup.DefaultIfEmpty()
                select new ProductCatalogItem(
                    product.Id,
                    product.Name,
                    category != null ? category.Name : "Неизвестная категория",
                    supplier != null ? supplier.Name : "Не указан",
                    product.Price,
                    decimal.Round(product.Price * 1.2m, 2, MidpointRounding.AwayFromZero),
                    GetStockStatus(product.StockQuantity),
                    product.IsActive);

            // OrderBy нужен перед Skip/Take, чтобы порядок элементов на страницах был предсказуемым.
            // ThenBy по Id дает стабильную сортировку, если у товаров совпали и цена, и название.
            // ToList() вызывается только в конце, чтобы не сохранять в память лишние элементы до пагинации.
            List<ProductCatalogItem> catalogPage = catalogQuery
                .OrderBy(item => item.Price)
                .ThenBy(item => item.Name)
                .ThenBy(item => item.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            List<CategoryStockReport> categoryReports = categories
                .GroupJoin(
                    products,
                    c => c.Id,
                    p => p.CategoryId,
                    (c, catProducts) =>
                    {
                        Product[] items = catProducts.ToArray();
                        return new CategoryStockReport(
                            c.Name,
                            items.Length,
                            items.Count(p => p.IsActive),
                            items.Count(p => p.StockQuantity > 0),
                            items.Sum(p => p.StockQuantity),
                            items.Sum(p => p.Price * p.StockQuantity),
                            items.Select(p => (decimal?)p.Price).Average(),
                            items.Select(p => (decimal?)p.Price).Min(),
                            items.Select(p => (decimal?)p.Price).Max());
                    })
                .OrderBy(r => r.CategoryName)
                .ToList();

            // SelectMany разворачивает списки позиций из всех заказов в одну общую последовательность.
            // Distinct нужен, чтобы один заказ не посчитался дважды, если товар в нем продублирован.
            // Обычный Join отсекает товары без продаж (например, кабель с Id=21), так как их нет в заказах.
            var orderLines = orders.SelectMany(
                order => order.Items,
                (order, item) => new
                {
                    OrderId = order.Id,
                    OrderDate = order.CreatedAt,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    LineTotal = item.Quantity * item.UnitPrice
                });

            List<ProductSalesReport> topSales = orderLines
                .Join(
                    products,
                    line => line.ProductId,
                    product => product.Id,
                    (line, product) => new
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        OrderId = line.OrderId,
                        Quantity = line.Quantity,
                        LineTotal = line.LineTotal
                    })
                .GroupBy(x => new { x.ProductId, x.ProductName })
                .Select(g => new ProductSalesReport(
                    g.Key.ProductId,
                    g.Key.ProductName,
                    g.Sum(x => x.Quantity),
                    g.Select(x => x.OrderId).Distinct().Count(),
                    g.Sum(x => x.LineTotal)))
                .OrderByDescending(r => r.Revenue)
                .ThenByDescending(r => r.TotalQuantity)
                .ThenBy(r => r.ProductName)
                .ThenBy(r => r.ProductId)
                .Take(5)
                .ToList();

            int totalProducts = products.Count();
            int activeProducts = products.Count(p => p.IsActive);
            int outOfStockProducts = products.Count(p => p.StockQuantity == 0);
            decimal totalStockValue = products.Sum(p => p.Price * p.StockQuantity);
            decimal? avgActivePrice = products
                .Where(p => p.IsActive)
                .Select(p => (decimal?)p.Price)
                .Average();

            return new StoreAnalyticsReport(
                totalProducts,
                activeProducts,
                outOfStockProducts,
                totalStockValue,
                avgActivePrice,
                catalogPage,
                categoryReports,
                topSales);
        }

        private static string GetStockStatus(int stockQuantity)
        {
            if (stockQuantity == 0) return "Нет в наличии";
            if (stockQuantity <= 5) return "Мало осталось";
            return "В наличии";
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Category> categories = TestDataFactory.GetCategories();
            List<Supplier> suppliers = TestDataFactory.GetSuppliers();
            List<Product> products = TestDataFactory.GetProducts();
            List<Order> orders = TestDataFactory.GetOrders();

            StoreAnalyticsReport report = StoreAnalyticsService.BuildReport(
                categories,
                suppliers,
                products,
                orders,
                minPrice: 1000m,
                maxPrice: 70000m,
                categoryId: null,
                searchText: null,
                pageNumber: 1,
                pageSize: 6);

            Console.WriteLine("Общие показатели:");
            Console.WriteLine("Всего товаров: " + report.TotalProducts);
            Console.WriteLine("Активных: " + report.ActiveProducts);
            Console.WriteLine("Нет в наличии: " + report.OutOfStockProducts);
            Console.WriteLine("Стоимость запасов: " + report.TotalStockValue);
            Console.WriteLine("Средняя цена активных: " +
                (report.AverageActiveProductPrice.HasValue ? report.AverageActiveProductPrice.Value.ToString("F2") : "нет"));

            Console.WriteLine("\nКаталог (страница 1):");
            foreach (var item in report.CatalogPage)
            {
                Console.WriteLine(string.Format("{0}. {1} | {2} | Поставщик: {3} | {4} руб. (с НДС: {5}) | {6}",
                    item.Id, item.Name, item.CategoryName, item.SupplierName, item.Price, item.PriceWithTax, item.StockStatus));
            }

            Console.WriteLine("\nОтчет по категориям:");
            foreach (var c in report.Categories)
            {
                Console.WriteLine(string.Format("{0}: товаров {1}, активных {2}, остаток {3} шт., сумма {4} руб., средняя цена: {5}",
                    c.CategoryName, c.ProductCount, c.ActiveProductCount, c.TotalStockQuantity, c.TotalStockValue,
                    c.AveragePrice.HasValue ? c.AveragePrice.Value.ToString("F2") : "null"));
            }

            Console.WriteLine("\nТоп-5 продаж:");
            foreach (var s in report.TopSellingProducts)
            {
                Console.WriteLine(string.Format("{0} (Id={1}): продано {2} шт., заказов {3}, выручка {4} руб.",
                    s.ProductName, s.ProductId, s.TotalQuantity, s.OrdersCount, s.Revenue));
            }

            // Проверки Any, All, FirstOrDefault, Single
            bool hasZeroStock = products.Any(p => p.StockQuantity == 0); // True
            bool allPricesValid = products.All(p => p.Price >= 0m); // True
            Product expensive = products.FirstOrDefault(p => p.Price > 100000m); // Ноутбук Asus ROG
            Category singleCategory = categories.Single(c => c.Id == 1); // Ноутбуки

            Console.WriteLine("\nПроверки:");
            Console.WriteLine("Есть товары с остатком 0: " + hasZeroStock);
            Console.WriteLine("Все цены >= 0: " + allPricesValid);
            Console.WriteLine("Товар дороже 100 000: " + (expensive != null ? expensive.Name : "нет"));
            Console.WriteLine("Категория с Id=1: " + singleCategory.Name);

            // Группировка по категории и ценовому диапазону с шагом 500
            Console.WriteLine("\nГруппировка по диапазонам цен (шаг 500):");
            var priceGroups = products
                .GroupBy(p => new
                {
                    CategoryId = p.CategoryId,
                    LowerBound = decimal.Floor(p.Price / 500m) * 500m
                })
                .OrderBy(g => g.Key.CategoryId)
                .ThenBy(g => g.Key.LowerBound)
                .Take(5);

            foreach (var g in priceGroups)
            {
                Console.WriteLine(string.Format("Категория {0}, от {1} руб.: {2} шт.",
                    g.Key.CategoryId, g.Key.LowerBound, g.Count()));
            }

            // Отложенное выполнение и AsQueryable / AsEnumerable
            List<Product> temp = new List<Product>(products);
            IEnumerable<Product> deferred = temp.Where(p => p.Price > 50000m);
            List<Product> snapshot = deferred.ToList();

            temp.Add(new Product(30, "Ноутбук MSI", 1, 1, 80000m, 2, true, new DateTime(2026, 2, 10)));
            Console.WriteLine(string.Format("\nВ снимке ToList: {0}, в отложенном запросе после добавления: {1}",
                snapshot.Count, deferred.Count()));

            // AsQueryable на списке в памяти не создает SQL-запрос, всё так же выполняется в памяти.
            // AsEnumerable переводит выполнение оставшейся части запроса на сторону приложения.
            IQueryable<Product> queryable = temp.AsQueryable().Where(p => p.IsActive);
            IEnumerable<Product> enumerable = queryable.AsEnumerable().Where(p => p.StockQuantity > 0);
            Console.WriteLine("Активных в наличии: " + enumerable.Count());
        }
    }
}