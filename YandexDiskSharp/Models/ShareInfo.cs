using Newtonsoft.Json;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Информация об общей папке.
    /// </summary>
    public class ShareInfo
    {
        #region ~Constructor~

        internal ShareInfo(JsonTextReader jsonReader)
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
            {
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "is_root":
                                IsRoot = jsonReader.ReadAsBoolean().Value;
                                break;
                            case "is_owned":
                                IsOwned = jsonReader.ReadAsBoolean().Value;
                                break;
                            case "rights":
                                Rights = jsonReader.ReadAsString();
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
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="ShareInfo"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="ShareInfo"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static ShareInfo Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new ShareInfo(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="ShareInfo"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="ShareInfo"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out ShareInfo result)
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
        /// Признак того, что папка является корневой в группе.
        /// </summary>
        public bool IsRoot { get; }

        /// <summary>
        /// Признак, что текущий пользователь является владельцем общей папки.
        /// </summary>
        public bool IsOwned { get; }

        /// <summary>
        /// Права доступа.
        /// </summary>
        public string Rights { get; }

        #endregion
    }
}
