using Newtonsoft.Json;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Адреса системных папок в Диске пользователя.
    /// </summary>
    public class SystemFolders
    {
        #region ~Constructor~

        internal SystemFolders(JsonTextReader jsonReader)
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "odnoklassniki":
                                Odnoklassniki = jsonReader.ReadAsString();
                                break;
                            case "google":
                                Google = jsonReader.ReadAsString();
                                break;
                            case "instagram":
                                Instagram = jsonReader.ReadAsString();
                                break;
                            case "vkontakte":
                                Vkontakte = jsonReader.ReadAsString();
                                break;
                            case "mailru":
                                MailRu = jsonReader.ReadAsString();
                                break;
                            case "downloads":
                                Downloads = jsonReader.ReadAsString();
                                break;
                            case "applications":
                                Applications = jsonReader.ReadAsString();
                                break;
                            case "facebook":
                                Facebook = jsonReader.ReadAsString();
                                break;
                            case "social":
                                Social = jsonReader.ReadAsString();
                                break;
                            case "screenshots":
                                Screenshots = jsonReader.ReadAsString();
                                break;
                            case "photostream":
                                Photostream = jsonReader.ReadAsString();
                                break;
                        }
                        break;
                    case JsonToken.EndObject:
                        if (jsonReader.Depth == depth)
                            return;
                        break;
                }
        }

        #endregion

        #region ~Methods~

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="SystemFolders"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="SystemFolders"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static SystemFolders Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new SystemFolders(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="SystemFolders"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="SystemFolders"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out SystemFolders result)
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
        /// Путь к папке "Социальные сети/Одноклассники".
        /// </summary>
        public string Odnoklassniki { get; }

        /// <summary>
        /// Путь к папке "Социальные сети/Google+".
        /// </summary>
        public string Google { get; }

        /// <summary>
        /// Путь к папке "Социальные сети/Instagram".
        /// </summary>
        public string Instagram { get; }

        /// <summary>
        /// Путь к папке "Социальные сети/ВКонтакте".
        /// </summary>
        public string Vkontakte { get; }

        /// <summary>
        /// Путь к папке "Социальные сети/Мой Мир".
        /// </summary>
        public string MailRu { get; }

        /// <summary>
        /// Путь к папке "Загрузки".
        /// </summary>
        public string Downloads { get; }

        /// <summary>
        /// Путь к папке "Приложения".
        /// </summary>
        public string Applications { get; }

        /// <summary>
        /// Путь к папке "Социальные сети/Facebook".
        /// </summary>
        public string Facebook { get; }

        /// <summary>
        /// Путь к папке "Социальные сети".
        /// </summary>
        public string Social { get; }

        /// <summary>
        /// Путь к папке "Скриншоты".
        /// </summary>
        public string Screenshots { get; }

        /// <summary>
        /// Путь к папке "Фотокамера".
        /// </summary>
        public string Photostream { get; }

        #endregion
    }
}
