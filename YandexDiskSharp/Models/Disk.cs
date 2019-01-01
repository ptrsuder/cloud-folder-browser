using Newtonsoft.Json;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Данные о свободном и занятом пространстве на Диске.
    /// </summary>
    public class Disk
    {
        #region ~Constructor~

        internal Disk(JsonTextReader jsonReader)
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "max_file_size":
                                MaxFileSize = long.Parse(jsonReader.ReadAsString());
                                break;
                            case "total_space":
                                TotalSpace = long.Parse(jsonReader.ReadAsString());
                                break;
                            case "trash_size":
                                TrashSize = long.Parse(jsonReader.ReadAsString());
                                break;
                            case "is_paid":
                                IsPaid = jsonReader.ReadAsBoolean().Value;
                                break;
                            case "used_space":
                                UsedSpace = long.Parse(jsonReader.ReadAsString());
                                break;
                            case "system_folders":
                                SystemFolders = new SystemFolders(jsonReader);
                                break;
                            case "revision":
                                Revision = long.Parse(jsonReader.ReadAsString());
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
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Disk"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="Disk"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static Disk Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new Disk(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Disk"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="Disk"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out Disk result)
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
        /// Максимальный поддерживаемый размер файла.
        /// </summary>
        public long MaxFileSize { get; }

        /// <summary>
        /// Общий объем диска (байт).
        /// </summary>
        public long TotalSpace { get; }

        /// <summary>
        /// Общий размер файлов в Корзине (байт). Входит в <see cref="UsedSpace"/>.
        /// </summary>
        public long TrashSize { get; }

        /// <summary>
        /// Признак наличия купленного места.
        /// </summary>
        public bool IsPaid { get; }

        /// <summary>
        /// Используемый объем диска (байт).
        /// </summary>
        public long UsedSpace { get; }

        /// <summary>
        /// Адреса системных папок в Диске пользователя.
        /// </summary>
        public SystemFolders SystemFolders { get; }

        /// <summary>
        /// Текущая ревизия Диска.
        /// </summary>
        public long Revision { get; }
        
        #endregion
    }
}
