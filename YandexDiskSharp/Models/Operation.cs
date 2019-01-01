using Newtonsoft.Json;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Статус операции.
    /// </summary>
    public class Operation
    {
        #region ~Constructor~

        internal Operation(JsonTextReader jsonReader)
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
            {
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "status":
                                switch (jsonReader.ReadAsString())
                                {
                                    case "success":
                                        Status = OperationStatus.Success;
                                        break;
                                    case "failure":
                                        Status = OperationStatus.Failure;
                                        break;
                                    case "in-progress":
                                        Status = OperationStatus.InProgress;
                                        break;
                                }
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
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Operation"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="Operation"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static Operation Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new Operation(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Operation"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="Operation"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out Operation result)
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
        /// Статус операции.
        /// </summary>
        public OperationStatus Status { get; }
        
        #endregion
    }
}
