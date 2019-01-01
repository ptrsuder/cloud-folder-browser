using Newtonsoft.Json;
using System;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Cодержит URL для запроса метаданных ресурса.
    /// </summary>
    public class Link
    {
        #region ~Constructor~

        internal Link(JsonTextReader jsonReader)
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
            {
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "href":
                                Href = new Uri(jsonReader.ReadAsString());
                                break;
                            case "method":
                                Method = jsonReader.ReadAsString();
                                break;
                            case "templated":
                                Templated = jsonReader.ReadAsBoolean().Value;
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

        #endregion

        #region ~Methods~

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Link"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="Link"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static Link Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new Link(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Link"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="Link"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out Link result)
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

        #endregion

        #region ~Properties~

        /// <summary>
        /// URL. Может быть шаблонизирован, см. ключ <seealso cref="Templated"/>.
        /// </summary>
        public Uri Href { get; }

        /// <summary>
        /// HTTP-метод для запроса URL из ключа href.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Признак URL, который был шаблонизирован согласно RFC 6570. 
        /// </summary>
        public bool Templated { get; }

        #endregion
    }
}
