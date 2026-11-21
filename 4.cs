using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Norbit.Tasks.Collections
{
    /// <summary>
    /// Представляет структуру данных "Умный стек" на базе массива.
    /// </summary>
    /// <typeparam name="T">Тип элементов в стеке.</typeparam>
    public class SmartStack<T> : IEnumerable<T>
    {
        #region Константы и поля

        private const int DefaultCapacity = 4;
        private T[] _items;
        private int _count;

        #endregion

        #region Свойства и индексатор

        /// <summary>
        /// Количество элементов в стеке
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Ёмкость
        /// </summary>
        public int Capacity
        {
            get { return _items.Length; }
        }

        /// <summary>
        /// Индексатор для доступа к элементам стека по глубине
        /// </summary>
        /// <param name="depth">Глубина элемента.</param>
        /// <returns>Элемент стека на указанной глубине.</returns>
        public T this[int depth]
        {
            get
            {
                if (depth < 0 || depth >= _count)
                {
                    throw new ArgumentOutOfRangeException("depth", "Индекс глубины выходит за границы стека.");
                }
                return _items[_count - 1 - depth];
            }
            set
            {
                if (depth < 0 || depth >= _count)
                {
                    throw new ArgumentOutOfRangeException("depth", "Индекс глубины выходит за границы стека.");
                }
                _items[_count - 1 - depth] = value;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор без параметров.
        /// </summary>
        public SmartStack()
        {
            _items = new T[DefaultCapacity];
            _count = 0;
        }

        /// <summary>
        /// Конструктор с указанной начальной ёмкостью массива.
        /// </summary>
        /// <param name="capacity">Начальная ёмкость стека.</param>
        public SmartStack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", "Ёмкость стека не может быть отрицательной.");
            }

            _items = new T[capacity];
            _count = 0;
        }

        /// <summary>
        /// Конструктор, копирующий элементы из переданной коллекции.
        /// Последний элемент коллекции становится вершиной стека.
        /// </summary>
        /// <param name="collection">Исходная коллекция элементов.</param>
        public SmartStack(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "Коллекция не может быть null.");
            }

            int size = 0;
            foreach (T item in collection)
            {
                size++;
            }

            _items = new T[size];
            _count = 0;

            foreach (T item in collection)
            {
                _items[_count] = item;
                _count++;
            }
        }

        #endregion

        #region Основные методы стека

        /// <summary>
        /// Добавляет элемент на вершину стека. При нехватке места удваивает ёмкость массива.
        /// </summary>
        /// <param name="item">Добавляемый элемент.</param>
        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
                Resize(newCapacity);
            }

            _items[_count] = item;
            _count++;
        }

        /// <summary>
        /// Добавляет на вершину стека элементы коллекции (последний элемент коллекции станет вершиной стека).
        /// </summary>
        /// <param name="collection">Коллекция элементов для добавления.</param>
        public void PushRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "Коллекция не может быть null.");
            }

            foreach (T item in collection)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Удаляет и возвращает элемент с вершины стека.
        /// </summary>
        /// <returns>Элемент с вершины стека.</returns>
        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Невозможно извлечь элемент: стек пуст.");
            }

            _count--;
            T topItem = _items[_count];
            _items[_count] = default(T); // Очистка ссылки для сборщика мусора
            return topItem;
        }

        /// <summary>
        /// Возвращает элемент с вершины стека без его удаления.
        /// </summary>
        /// <returns>Элемент с вершины стека.</returns>
        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Невозможно просмотреть элемент: стек пуст.");
            }

            return _items[_count - 1];
        }

        /// <summary>
        /// Проверяет наличие элемента в стеке.
        /// </summary>
        /// <param name="item">Искомый элемент.</param>
        /// <returns>true, если элемент найден; иначе false.</returns>
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < _count; i++)
            {
                if (comparer.Equals(_items[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Изменяет размер внутреннего массива.
        /// </summary>
        /// <param name="newCapacity">Новая ёмкость массива.</param>
        private void Resize(int newCapacity)
        {
            T[] newArray = new T[newCapacity];
            for (int i = 0; i < _count; i++)
            {
                newArray[i] = _items[i];
            }
            _items = newArray;
        }

        #endregion

        #region Реализация IEnumerable

        /// <summary>
        /// Возвращает перечислитель, осуществляющий обход от вершины стека к его основанию.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        /// <summary>
        /// Возвращает необобщенный перечислитель.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    /// <summary>
    /// Основной класс программы для демонстрации работы SmartStack.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Демонстрация работы Умного стека\n");

            // Создание пустого стека (ёмкость по умолчанию = 4)
            SmartStack<int> stack = new SmartStack<int>();
            Console.WriteLine(string.Format("Создан пустой стек. Count: {0}, Capacity: {1}", stack.Count, stack.Capacity));

            // Добавление элементов через Push и проверка удвоения ёмкости
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);
            stack.Push(50); // На 5-м элементе ёмкость удвоится с 4 до 8
            Console.WriteLine(string.Format("После добавления 5 элементов. Count: {0}, Capacity: {1}", stack.Count, stack.Capacity));

            // Проверка Peek и Pop
            Console.WriteLine("Вершина стека (Peek): " + stack.Peek());
            Console.WriteLine("Извлечен элемент (Pop): " + stack.Pop());
            Console.WriteLine(string.Format("После Pop. Count: {0}, Capacity (не уменьшилась): {1}", stack.Count, stack.Capacity));

            // Проверка PushRange
            int[] extraItems = new int[] { 60, 70, 80 };
            stack.PushRange(extraItems);
            Console.WriteLine("После PushRange({ 60, 70, 80 }) новая вершина: " + stack.Peek());

            // Проверка Contains
            Console.WriteLine("Содержит 30? " + stack.Contains(30));
            Console.WriteLine("Содержит 999? " + stack.Contains(999));

            // Проверка индексатора по глубине
            Console.WriteLine("Элемент на глубине 0: " + stack[0]);
            Console.WriteLine("Элемент на глубине " + (stack.Count - 1) + " (основание): " + stack[stack.Count - 1]);

            // Обход через foreach
            Console.Write("Обход стека: ");
            foreach (int item in stack)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}