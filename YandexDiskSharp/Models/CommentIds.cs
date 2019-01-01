using Newtonsoft.Json;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Идентификаторы комментариев.
    /// </summary>
    public class CommentIds
    {
        #region ~Constructor~

        [JsonConstructor]
        public CommentIds(string PrivateResource, string PublicResource)
        {
            this.PrivateResource = PrivateResource;
            this.PublicResource = PublicResource;
        }

        internal CommentIds(JsonTextReader jsonReader)
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
                                PrivateResource = jsonReader.ReadAsString();
                                break;
                            case "public_resource":
                                PublicResource = jsonReader.ReadAsString();
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
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="CommentIds"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="CommentIds"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static CommentIds Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new CommentIds(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="CommentIds"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="CommentIds"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out CommentIds result)
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
        /// Идентификатор комментариев для приватных ресурсов.
        /// </summary>
        public string PrivateResource { get; }

        /// <summary>
        /// Идентификатор комментариев для публичных ресурсов.
        /// </summary>
        public string PublicResource { get; }

        #endregion
    }
}
