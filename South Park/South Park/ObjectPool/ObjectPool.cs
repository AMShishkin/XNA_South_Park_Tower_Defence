// Класс реализующий пулл объектов и доступ по индексу.

using System.Collections.Generic;

namespace South_Park
{
    class ObjectPool<T> where T : class
    {
        private Stack<T> data;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ObjectPool()
        {
            data = new Stack<T>();
        }


        /// <summary>
        /// Возвращает колличестов обьектов в коллекции
        /// </summary>
        public int Count
        {
            get { return data.Count; }
        }


        /// <summary>
        /// Добавляет обьект в коллекцию
        /// </summary>
        /// <param name="obj"></param>
        public void Push(T obj)
        {
            data.Push(obj);
            data.TrimExcess();
        }

        

        public T Pop()
        {
            return data.Pop();
        }


        /// <summary>
        /// Очистка пула
        /// </summary>
        public void Clear()
        {
            data.Clear();
        }
    }
}