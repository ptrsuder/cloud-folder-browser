using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Список последних добавленных на Диск файлов, отсортированных по дате загрузки (от поздних к ранним).
    /// </summary>
    public class LastUploadedResourceList : IList<Resource>
    {
        #region ~Constructor~

        internal LastUploadedResourceList()
        {
            items = new List<Resource>();
            //Items = new ReadOnlyCollection<Resource>(items);
        }

        internal LastUploadedResourceList(JsonTextReader jsonReader) : this()
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
            {
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "items":
                                jsonReader.Read();
                                while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
                                    items.Add(new Resource(jsonReader));
                                break;
                            case "limit":
                                limit = jsonReader.ReadAsInt32().Value;
                                break;
                        }
                        break;
                    case JsonToken.EndObject:
                        if (jsonReader.Depth == depth)
                            return;
                        break;
                }
            }
        }

        public Resource this[int index] { get => items[index]; }
        
        #endregion

        #region ~Fields~

        protected List<Resource> items;
        protected int limit;

        #endregion

        #region ~Properties~

        //public ReadOnlyCollection<Resource> Items { get; }

        public int Count => items.Count;

        public bool IsReadOnly => true;

        /// <summary>
        /// Максимальное количество элементов в массиве <see cref="Items"/>, заданное в запросе.
        /// </summary>
        public int Limit => limit;

        #endregion

        #region ~Methods~

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="LastUploadedResourceList"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="LastUploadedResourceList"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static LastUploadedResourceList Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new LastUploadedResourceList(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="LastUploadedResourceList"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="LastUploadedResourceList"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out LastUploadedResourceList result)
        {
            try
            {
                result = Parse(s);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public bool Contains(Resource item)
        {
            return items.Contains(item);
        }

        public void CopyTo(Resource[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public int IndexOf(Resource item)
        {
            return items.IndexOf(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region ~IList~

        Resource IList<Resource>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void ICollection<Resource>.Add(Resource item)
        {
            throw new NotImplementedException();
        }

        void ICollection<Resource>.Clear()
        {
            throw new NotImplementedException();
        }

        void IList<Resource>.Insert(int index, Resource item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<Resource>.Remove(Resource item)
        {
            throw new NotImplementedException();
        }

        void IList<Resource>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
