using Newtonsoft.Json;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Плоский список всех файлов на Диске в алфавитном порядке.
    /// </summary>
    public class FilesResourceList : LastUploadedResourceList
    {
        #region ~Constructot~

        internal FilesResourceList() { }

        internal FilesResourceList(JsonTextReader jsonReader)
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
                            case "offset":
                                offset = jsonReader.ReadAsInt32().Value;
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

        #region ~Fields~

        protected int offset;

        #endregion

        #region ~Methods~

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="FilesResourceList"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="FilesResourceList"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static new FilesResourceList Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new FilesResourceList(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="FilesResourceList"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="FilesResourceList"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out FilesResourceList result)
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
        /// Смещение начала списка от первого ресурса в папке.
        /// </summary>
        public int Offset => offset;

        #endregion

    }
}
