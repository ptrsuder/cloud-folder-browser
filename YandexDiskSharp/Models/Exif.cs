using Newtonsoft.Json;
using System;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Метаданные медиафайла (EXIF).
    /// </summary>
    public class Exif
    {
        #region ~Constructor~

        internal Exif(JsonTextReader jsonReader)
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
            {
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "private_resource":
                                DateTime = jsonReader.ReadAsDateTime().Value;
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
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Exif"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="Exif"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static Exif Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new Exif(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Exif"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="Exif"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out Exif result)
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
        /// Дата съёмки.
        /// </summary>
        public DateTime DateTime { get; }

        #endregion
    }
}
