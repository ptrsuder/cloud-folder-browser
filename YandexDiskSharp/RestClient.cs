using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YandexDiskSharp.Models;
using YandexDiskSharp.Utilities;

namespace YandexDiskSharp
{
    /// <summary>
    /// Rest клиент, предоставляющий доступ к REST API Яндекс.Диска.
    /// </summary>
    public class RestClient
    {
        #region ~Constants~

        /// <summary>
        /// Указывает на то, что ресурс расположен в каталоге приложения.
        /// </summary>
        public const string UriSchemeApp = "app:/";

        /// <summary>
        /// Указывает на то, что доступ к ресурсу осществляется через корневой каталог Диска.
        /// </summary>
        public const string UriSchemeDisk = "disk:/";

        /// <summary>
        /// Указывает на то, что ресурс расположен в каталоге "Корзина".
        /// </summary>
        public const string UriSchemeTrash = "trash:/";

        #endregion

        #region ~Constructor~

        /// <param name="
        /// ">OAuth-токен, выданный вашему приложению для доступа к Диску определенного пользователя.</param>
        public RestClient(string accessToken)
        {
            AccessToken = accessToken;
            Timeout = 100000;
            ReadWriteTimeout = 300000;
        }

        public RestClient()
        {            
            Timeout = 100000;
            ReadWriteTimeout = 300000;
        }

        #endregion

        #region ~Properties~

        /// <summary>
        /// OAuth-токен приложения для доступа к Диску определенного пользователя.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Возвращает или задает значение времени ожидания в миллисекундах для методов <see cref="HttpWebRequest.GetResponse()"/> и <see cref="HttpWebRequest.GetRequestStream()"/>.
        /// </summary>
        /// <value>Количество миллисекунд, ожидаемых до остановки запроса. По умолчанию установлено значение 100 000 миллисекунд (100 секунд).</value>
        /// <exception cref="ArgumentOutOfRangeException">Указанное значение меньше нуля и не равно <see cref="System.Threading.Timeout.Infinite"/>.</exception>
        public int Timeout { get; set; }

        /// <summary>
        /// Возвращает или задает время ожидания в миллисекундах при записи в поток или при чтении из него.
        /// </summary>
        /// <value>Количество миллисекунд до истечения срока действия записи или чтения. По умолчанию установлено значение 300 000 миллисекунд (5 минут).</value>
        /// <exception cref="InvalidOperationException"> Запрос уже был отправлен.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Значение, указанное для операции задания, меньше или равно нулю и не равно <see cref="System.Threading.Timeout.Infinite"/></exception>
        public int ReadWriteTimeout { get; set; }

        #endregion

        #region ~Methods~

        private HttpWebRequest CreateRequest(string requestUriString, string method)
        {
            HttpWebRequest result = (HttpWebRequest)WebRequest.Create(requestUriString);
            result.Method = method;
            result.Headers["Authorization"] = $"OAuth {AccessToken}";
            result.Timeout = Timeout;
            result.ReadWriteTimeout = ReadWriteTimeout;
            //result.Proxy = new WebProxy("93.92.204.173", Port:3128);
            //string g = result.ToString();
            return result;
        }

        private HttpWebRequest CreateAnonRequest(string requestUriString, string method)
        {
            HttpWebRequest result = (HttpWebRequest)WebRequest.Create(requestUriString);
            result.Method = method;
            //result.Headers["Authorization"] = $"OAuth {AccessToken}";
            result.Timeout = Timeout;
            result.ReadWriteTimeout = ReadWriteTimeout;
            //result.Proxy = new WebProxy("93.92.204.173", Port:3128);
            //string g = result.ToString();
            return result;
        }

        /// <summary>
        /// Данные о Диске пользователя.
        /// </summary>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Если запрос был обработан без ошибок, API возвращает данные о Диске в теле ответа, в объекте <see cref="Disk"/>. </returns>
        public Disk GetDisk(IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/?" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}",
                "GET").GetJsonTextReader())
                return new Disk(jsonReader);
        }

        /// <summary>
        /// Данные о Диске пользователя.
        /// </summary>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Если запрос был обработан без ошибок, API возвращает данные о Диске в теле ответа, в объекте <see cref="Disk"/>. </returns>
        public async Task<Disk> GetDiskAsync(IEnumerable<string> fields = null)
        {
            return await Task.Run(() => GetDisk(fields));
        }

        /// <summary>
        /// Запрашивает метаинформация о файле или папке.
        /// </summary>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="sort">Атрибут, по которому сортируется список ресурсов, вложенных в папку. Для сортировки в обратном порядке добавьте дефис к значению параметра, например: sort=-name.</param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Метаинформация включает в себя собственные свойства файлов и папок, а также свойства и содержимое вложенных папок.</returns>
        public Resource GetResource(string path, string sort = null, int limit = 20, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(sort != null ? $"&sort={sort}" : null)}" +
                $"{(limit != 20 ? $"&limit={limit}" : "")}" +
                $"{(offset != 0 ? $"&offset={offset}" : "")}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}" +
                $"{(previewSize != null ? $"&preview_size={previewSize}{(previewCrop != false ? $"&preview_crop={previewCrop.ToString()}" : null)}" : "")}",
                "GET").GetJsonTextReader())
                return new Resource(jsonReader);
        }

        /// <summary>
        /// Запрашивает метаинформация о файле или папке.
        /// </summary>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="sort">Атрибут, по которому сортируется список ресурсов, вложенных в папку. Для сортировки в обратном порядке добавьте дефис к значению параметра, например: sort=-name.</param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Метаинформация включает в себя собственные свойства файлов и папок, а также свойства и содержимое вложенных папок.</returns>
        public async Task<Resource> GetResourceAsync(string path, string sort = null, int limit = 20, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            return await Task.Run(() => GetResource(path, sort, limit, offset, fields, previewSize, previewCrop));
        }

        /// <summary>
        /// Метаинформация о файле или папке в корзине.
        /// </summary>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="sort">Атрибут, по которому сортируется список ресурсов, вложенных в папку. Для сортировки в обратном порядке добавьте дефис к значению параметра, например: sort=-name.</param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Если запрос был обработан без ошибок, API возвращает метаинформацию о запрошенном ресурсе в теле ответа, в объекте <see cref="Resource"/>.</returns>
        public Resource GetTrashResource(string path, string sort = null, int limit = 20, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/trash/resources?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(sort != null ? $"&sort={sort}" : null)}" +
                $"{(limit != 20 ? $"&limit={limit}" : "")}" +
                $"{(offset != 0 ? $"&offset={offset}" : "")}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}" +
                $"{(previewSize != null ? $"&preview_size={previewSize}{(previewCrop != false ? $"&preview_crop={previewCrop.ToString()}" : null)}" : "")}",
                "GET").GetJsonTextReader())
                return new Resource(jsonReader);
        }

        /// <summary>
        /// Метаинформация о файле или папке в корзине.
        /// </summary>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="sort">Атрибут, по которому сортируется список ресурсов, вложенных в папку. Для сортировки в обратном порядке добавьте дефис к значению параметра, например: sort=-name.</param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Если запрос был обработан без ошибок, API возвращает метаинформацию о запрошенном ресурсе в теле ответа, в объекте <see cref="Resource"/>.</returns>
        public async Task<Resource> GetTrashResourceAsync(string path, string sort = null, int limit = 20, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            return await Task.Run(() => GetTrashResource(path, sort, limit, offset, fields, previewSize, previewCrop));
        }

        /// <summary>
        /// Плоский список всех файлов.
        /// </summary>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="mediaType">Тип файлов, которые нужно включить в список. Диск определяет тип каждого файла при загрузке.</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Возвращает плоский список всех файлов на Диске в алфавитном порядке. Плоский список не учитывает структуру каталогов, поэтому в нем удобно искать файлы определенного типа, разбросанные по разным папкам. Диск определяет тип каждого файла при загрузке.</returns>
        public FilesResourceList GetFlatFilesList(int limit = 20, IEnumerable<string> mediaType = null, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            List<string> query = new List<string>();

            if (limit != 20)
                query.Add($"limit={limit.ToString()}");
            if (mediaType != null)
                query.Add($"media_type={string.Join(", ", mediaType)}");
            if (offset != 0)
                query.Add($"offset={offset.ToString()}");
            if (fields != null)
                query.Add($"fields={string.Join(",", fields)}");
            if (previewSize != null)
                query.Add($"preview_size={previewSize}");
            if (previewCrop != false)
                query.Add($"preview_crop={previewCrop.ToString()}");

            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/files{(query.Count > 0 ? $"?{string.Join("&", query)}" : null)}", 
                "GET").GetJsonTextReader())
                return new FilesResourceList(jsonReader);
        }

        /// <summary>
        /// Плоский список всех файлов.
        /// </summary>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="mediaType">Тип файлов, которые нужно включить в список. Диск определяет тип каждого файла при загрузке.</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Возвращает плоский список всех файлов на Диске в алфавитном порядке. Плоский список не учитывает структуру каталогов, поэтому в нем удобно искать файлы определенного типа, разбросанные по разным папкам. Диск определяет тип каждого файла при загрузке.</returns>
        public async Task<FilesResourceList> GetFlatFilesListAsync(int limit = 20, IEnumerable<string> mediaType = null, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            return await Task.Run(() => GetFlatFilesList(limit, mediaType, offset, fields, previewSize, previewCrop));
        }

        /// <summary>
        /// Последние загруженные файлы.
        /// </summary>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="mediaType">Тип файлов, которые нужно включить в список. Диск определяет тип каждого файла при загрузке.</param>
        /// <param name="fields">Список ключей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Если запрос был обработан без ошибок, возвращает метаинформацию о запрошенном количестве файлов в теле ответа, в объекте <see cref="LastUploadedResourceList"/>.</returns>
        public LastUploadedResourceList GetLastUploadedFilesList(int limit = 20, IEnumerable<string> mediaType = null, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            List<string> query = new List<string>();

            if (limit != 20)
                query.Add($"limit={limit.ToString()}");
            if (mediaType != null)
                query.Add($"media_type={string.Join(", ", mediaType)}");
            if (fields != null)
                query.Add($"fields={HttpUtility.UrlEncode(string.Join(",", fields))}");
            if (previewSize != null)
                query.Add($"preview_size={previewSize}");
            if (previewCrop != false)
                query.Add($"preview_crop={previewCrop.ToString()}");

            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/last-uploaded{(query.Count > 0 ? $"?{string.Join("&", query)}" : null)}",
                "GET").GetJsonTextReader())
                return new LastUploadedResourceList(jsonReader);
        }

        /// <summary>
        /// Последние загруженные файлы.
        /// </summary>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="mediaType">Тип файлов, которые нужно включить в список. Диск определяет тип каждого файла при загрузке.</param>
        /// <param name="fields">Список ключей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Если запрос был обработан без ошибок, возвращает метаинформацию о запрошенном количестве файлов в теле ответа, в объекте <see cref="LastUploadedResourceList"/>.</returns>
        public async Task<LastUploadedResourceList> GetLastUploadedFilesListAsync(int limit = 20, IEnumerable<string> mediaType = null, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            return await Task.Run(() => GetLastUploadedFilesList(limit, mediaType, fields, previewSize, previewCrop));
        }

        /// <summary>
        /// Добавление метаинформации для ресурса.
        /// </summary>
        /// <remarks>Для любого файла или папки, доступной на запись, можно задать дополнительные произвольные атрибуты. Эти атрибуты будут возвращаться в ответ на все запросы метаинформации о ресурсах (список всех файлов, последние загруженные и т. д.).</remarks>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="properties">Добавляемые атрибуты. Переданные атрибуты добавляются к уже имеющимся. Чтобы удалить какой-либо ключ, следует передать его со значением null</param>
        /// <returns>Возвращает метаинформацию о запрошенном ресурсе в теле ответа, в объекте <see cref="Resource"/>.</returns>
        //public Resource ResourcePatch(string path, IDictionary<string, string> properties, IEnumerable<string> fields = null)
        public Resource ResourcePatch(string path, IDictionary<string, string> properties, IEnumerable<string> fields = null)
        {
            var request = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(fields != null ? $"&fields={HttpUtility.UrlEncode(string.Join(",", fields))}" : "")}", 
                "PATCH");

            var propertyList = new List<string>();
            foreach (var property in properties)
                propertyList.Add((property.Value != null ) ? $"\"{property.Key}\":\"{property.Value}\"" : $"\"{property.Key}\":null");

            byte[] buffer = Encoding.UTF8.GetBytes($"{{\"custom_properties\":{{{string.Join(", ", propertyList)}}}}}");
            using (var stream = request.GetRequestStream())
                stream.Write(buffer, 0, buffer.Length);
            using (var jsonReader = request.GetJsonTextReader())
                return new Resource(jsonReader);
        }

        /// <summary>
        /// Добавление метаинформации для ресурса.
        /// </summary>
        /// <remarks>Для любого файла или папки, доступной на запись, можно задать дополнительные произвольные атрибуты. Эти атрибуты будут возвращаться в ответ на все запросы метаинформации о ресурсах (список всех файлов, последние загруженные и т. д.).</remarks>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="properties">Добавляемые атрибуты. Переданные атрибуты добавляются к уже имеющимся. Чтобы удалить какой-либо ключ, следует передать его со значением null</param>
        /// <returns>Возвращает метаинформацию о запрошенном ресурсе в теле ответа, в объекте <see cref="Resource"/>.</returns>
        public async Task<Resource> ResourcePatchAsync(string path, IDictionary<string, string> properties, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => ResourcePatch(path, properties, fields));
        }

        /// <summary>
        /// Запрос URL для загрузки.
        /// </summary>
        /// <remarks>Сообщив API Диска желаемый путь для загружаемого файла, вы получаете URL для обращения к загрузчику файлов.</remarks>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="overwrite">Признак перезаписи файла. Учитывается, если файл загружается в папку, в которой уже есть файл с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает сгенерированный URL для загрузки файла. Если в течение 30 минут этот URL не будет запрошен, он перестанет работать, и нужно будет запросить новую ссылку.</returns>
        public Link GetResourceUploadLink(string path, bool overwrite = false, IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/upload?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(overwrite != false ? $"&overwrite=true" : "")}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}", 
                "GET").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Запрос URL для загрузки.
        /// </summary>
        /// <remarks>Сообщив API Диска желаемый путь для загружаемого файла, вы получаете URL для обращения к загрузчику файлов.</remarks>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска.</param>
        /// <param name="overwrite">Признак перезаписи файла. Учитывается, если файл загружается в папку, в которой уже есть файл с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает сгенерированный URL для загрузки файла. Если в течение 30 минут этот URL не будет запрошен, он перестанет работать, и нужно будет запросить новую ссылку.</returns>
        public async Task<Link> GetResourceUploadLinkAsync(string path, bool overwrite = false, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => GetResourceUploadLink(path, overwrite, fields));
        }

        /// <summary>
        /// Скачивание файла из интернета на Диск.
        /// </summary>
        /// <remarks>Яндекс.Диск может скачать файл на Диск пользователя. Для этого следует передать в запросе URL файла и следить за ходом операции. Если при скачивании возникла ошибка, Диск не будет пытаться скачать файл еще раз.</remarks>
        /// <param name="url">Ссылка на скачиваемый файл.</param>
        /// <param name="path">Путь на Диске, по которому должен быть доступен скачанный файл.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="disableRedirects">Параметр помогает запретить редиректы по адресу, заданному в параметре <paramref name="url"/>.</param>
        /// <returns>Скачивание файла может занять неопределенное время. Если операция скачивания была запущена, API отвечает ссылкой на статус операции (в объекте <see cref="Link"/>).</returns>
        public Link UploadExternalResource(Uri url, string path, IEnumerable<string> fields = null, bool disableRedirects = false)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/upload?" +
                $"url={HttpUtility.UrlEncode(url.AbsoluteUri)}" +
                $"&path={HttpUtility.UrlEncode(path)}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}" +
                $"{(disableRedirects != false ? "disable_redirects=true" : null)}", 
                "GET").GetJsonTextReader())
                return new Link(jsonReader); 
        }

        /// <summary>
        /// Скачивание файла из интернета на Диск.
        /// </summary>
        /// <remarks>Яндекс.Диск может скачать файл на Диск пользователя. Для этого следует передать в запросе URL файла и следить за ходом операции. Если при скачивании возникла ошибка, Диск не будет пытаться скачать файл еще раз.</remarks>
        /// <param name="url">Ссылка на скачиваемый файл.</param>
        /// <param name="path">Путь на Диске, по которому должен быть доступен скачанный файл.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="disableRedirects">Параметр помогает запретить редиректы по адресу, заданному в параметре <paramref name="url"/>.</param>
        /// <returns>Скачивание файла может занять неопределенное время. Если операция скачивания была запущена, API отвечает ссылкой на статус операции (в объекте <see cref="Link"/>).</returns>
        public async Task<Link> UploadExternalResourceAsync(Uri url, string path, IEnumerable<string> fields = null, bool disableRedirects = false)
        {
            return await Task.Run(() => UploadExternalResource(url, path, fields, disableRedirects));
        }

        /// <summary>
        /// Запрос URL для скачивания.
        /// </summary>
        /// <remarks>Чтобы получить URL для непосредственной загрузки файла, необходимо передать API путь на Диске, по которому загруженный файл должен быть доступен.</remarks>
        /// <param name="path">Путь к скачиваемому файлу.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает сгенерированный URL для скачивания файла в объекте <see cref="Link"/></returns>
        public Link GetResourceDownloadLink(string path, IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/download?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}", 
                "GET").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Запрос URL для скачивания.
        /// </summary>
        /// <remarks>Чтобы получить URL для непосредственной загрузки файла, необходимо передать API путь на Диске, по которому загруженный файл должен быть доступен.</remarks>
        /// <param name="path">Путь к скачиваемому файлу.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает сгенерированный URL для скачивания файла в объекте <see cref="Link"/></returns>
        public async Task<Link> GetResourceDownloadLinkAsync(string path, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => GetResourceDownloadLink(path, fields));
        }

        /// <summary>
        /// Копирование файла или папки.
        /// </summary>
        /// <remarks>Копировать файлы и папки на Диске пользователя можно, указывая путь к ресурсу и требуемый путь к его копии.</remarks>
        /// <param name="from">Путь к копируемому ресурсу.</param>
        /// <param name="path">Путь к создаваемой копии ресурса.</param>
        /// <param name="overwrite">Признак перезаписи файла. Учитывается, если файл загружается в папку, в которой уже есть файл с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает ссылку на мета-информацию о созданном ресурсе для пустой папки или файла. Непустая папка может копироваться в течение произвольного времени, поэтому приложения должны самостоятельно следить за статусами запрошенных операций.</returns>
        public Link CopyResource(string from, string path, bool overwrite = false, IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/copy?" +
                $"from={HttpUtility.UrlEncode(from)}" +
                $"&path={HttpUtility.UrlEncode(path)}" +
                $"{(overwrite != false ? $"&overwrite=true" : "")}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}", 
                "POST").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Копирование файла или папки.
        /// </summary>
        /// <remarks>Копировать файлы и папки на Диске пользователя можно, указывая путь к ресурсу и требуемый путь к его копии.</remarks>
        /// <param name="from">Путь к копируемому ресурсу.</param>
        /// <param name="path">Путь к создаваемой копии ресурса.</param>
        /// <param name="overwrite">Признак перезаписи файла. Учитывается, если файл загружается в папку, в которой уже есть файл с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает ссылку на мета-информацию о созданном ресурсе для пустой папки или файла. Непустая папка может копироваться в течение произвольного времени, поэтому приложения должны самостоятельно следить за статусами запрошенных операций.</returns>
        public async Task<Link> CopyResourceAsync(string from, string path, bool overwrite = false, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => CopyResource(from, path, overwrite, fields));
        }

        /// <summary>
        /// Перемещение файла или папки.
        /// </summary>
        /// <remarks>Перемещать файлы и папки на Диске можно, указывая текущий путь к ресурсу и его новое положение.</remarks>
        /// <param name="from">Путь к перемещаемому ресурсу.</param>
        /// <param name="path">Путь к новому положению ресурса.</param>
        /// <param name="overwrite">Признак перезаписи файла. Учитывается, если файл загружается в папку, в которой уже есть файл с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает ссылку на мета-информацию о созданном ресурсе для пустой папки или файла. Непустая папка может перемещаться в течение произвольного времени, поэтому приложения должны самостоятельно следить за статусами запрошенных операций.</returns>
        public Link MoveResource(string from, string path, bool overwrite = false, IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/move?" +
                $"from={HttpUtility.UrlEncode(from)}" +
                $"&path={HttpUtility.UrlEncode(path)}" +
                $"{(overwrite != false ? $"&overwrite=true" : "")}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}", 
                "POST").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Перемещение файла или папки.
        /// </summary>
        /// <remarks>Перемещать файлы и папки на Диске можно, указывая текущий путь к ресурсу и его новое положение.</remarks>
        /// <param name="from">Путь к перемещаемому ресурсу.</param>
        /// <param name="path">Путь к новому положению ресурса.</param>
        /// <param name="overwrite">Признак перезаписи файла. Учитывается, если файл загружается в папку, в которой уже есть файл с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает ссылку на мета-информацию о созданном ресурсе для пустой папки или файла. Непустая папка может перемещаться в течение произвольного времени, поэтому приложения должны самостоятельно следить за статусами запрошенных операций.</returns>
        public async Task<Link> MoveResourceAsync(string from, string path, bool overwrite = false, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => MoveResource(from, path, overwrite, fields));
        }

        /// <summary>
        /// Удаление файла или папки.
        /// </summary>
        /// <remarks>Удалять файлы и папки на Диске пользователя можно, указывая путь к удаляемому ресурсу. Помните, что перемещение ресурсов в Корзину никак не влияет на доступное место на Диске. Чтобы освободить место, следует также удалять ресурсы из Корзины.</remarks>
        /// <param name="path">Путь к новому положению ресурса.</param>
        /// <param name="permanently">Признак безвозвратного удаления.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает ссылку на мета-информацию о созданном ресурсе для пустой папки или файла. Непустая папка может перемещаться в течение произвольного времени, поэтому приложения должны самостоятельно следить за статусами запрошенных операций.</returns>
        public Link DeleteResource(string path, bool permanently = false, IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(permanently != false ? $"&permanently=true" : "")}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}", 
                "DELETE").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Удаление файла или папки.
        /// </summary>
        /// <remarks>Удалять файлы и папки на Диске пользователя можно, указывая путь к удаляемому ресурсу. Помните, что перемещение ресурсов в Корзину никак не влияет на доступное место на Диске. Чтобы освободить место, следует также удалять ресурсы из Корзины.</remarks>
        /// <param name="path">Путь к новому положению ресурса.</param>
        /// <param name="permanently">Признак безвозвратного удаления.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Возвращает ссылку на мета-информацию о созданном ресурсе для пустой папки или файла. Непустая папка может перемещаться в течение произвольного времени, поэтому приложения должны самостоятельно следить за статусами запрошенных операций.</returns>
        public async Task<Link> DeleteResourceAsync(string path, bool permanently = false, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => DeleteResource(path, permanently, fields));
        }

        /// <summary>
        /// Создание папки.
        /// </summary>
        /// <remarks>Создавать папки на Диске можно, указывая требуемый путь к новой папке.</remarks>
        /// <param name="path">Путь к новому положению ресурса.</param>
        /// <param name="fields">Список ключей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>В объекте <see cref="Link"/>, возвращается ссылка на мета-информацию о созданном ресурсе.</returns>
        public Link CreateResource(string path, IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}", 
                "PUT").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Создание папки.
        /// </summary>
        /// <remarks>Создавать папки на Диске можно, указывая требуемый путь к новой папке.</remarks>
        /// <param name="path">Путь к новому положению ресурса.</param>
        /// <param name="fields">Список ключей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>В объекте <see cref="Link"/>, возвращается ссылка на мета-информацию о созданном ресурсе.</returns>
        public async Task<Link> CreateResourceAsync(string path, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => CreateResource(path, fields));
        }

        /// <summary>
        /// Публикация файла или папки.
        /// </summary>
        /// <remarks>Ресурс становится доступен по прямой ссылке.</remarks>
        /// <param name="path">Путь к публикуемому ресурсу.</param>
        /// <returns>Возвращает ссылку на мета-информацию о закрытом ресурсе в объекте <see cref="Link"/>.</returns>
        public Link PublishResource(string path)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/publish?" +
                $"path={HttpUtility.UrlEncode(path)}", 
                "PUT").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Публикация файла или папки.
        /// </summary>
        /// <remarks>Ресурс становится доступен по прямой ссылке.</remarks>
        /// <param name="path">Путь к публикуемому ресурсу.</param>
        /// <returns>Возвращает ссылку на мета-информацию о закрытом ресурсе в объекте <see cref="Link"/>.</returns>
        public async Task<Link> PublishResourceAsync(string path)
        {
            return await Task.Run(() => PublishResource(path));
        }

        /// <summary>
        /// Закрытие доступа к ресурсу.
        /// </summary>
        /// <remarks>Ресурс теряет атрибуты public_key и public_url, публичные ссылки на него перестают работать.</remarks>
        /// <param name="path">Путь к закрываемому ресурсу.</param>
        /// <returns>Возвращает ссылку на мета-информацию о закрытом ресурсе в объекте <see cref="Link"/>.</returns>
        public Link UnpublishResource(string path)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/unpublish?" +
                $"path={HttpUtility.UrlEncode(path)}", 
                "PUT").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Закрытие доступа к ресурсу.
        /// </summary>
        /// <remarks>Ресурс теряет атрибуты public_key и public_url, публичные ссылки на него перестают работать.</remarks>
        /// <param name="path">Путь к закрываемому ресурсу.</param>
        /// <returns>Возвращает ссылку на мета-информацию о закрытом ресурсе в объекте <see cref="Link"/>.</returns>
        public async Task<Link> UnpublishResourceAsync(string path)
        {
            return await Task.Run(() => UnpublishResource(path));
        }

        /// <summary>
        /// Список опубликованных ресурсов.
        /// </summary>
        /// <param name="limit">Количество опубликованных файлов, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="type">Тип ресурса.</param>
        /// <param name="fields">Список возвращаемых атрибутов.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Возвращает метаинформацию о запрошенном количестве файлов в теле ответа, в объекте <see cref="ResourceList"/>.</returns>
        public ResourceList ListPublicResources(int limit = 20, int offset = 0, string type = null, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            List<string> query = new List<string>();

            if (limit != 20)
                query.Add($"limit={limit.ToString()}");
            if (offset != 0)
                query.Add($"offset={offset.ToString()}");
            if (type != null)
                query.Add($"type={string.Join(", ", type)}");
            if (fields != null)
                query.Add($"fields={string.Join(",", fields)}");
            if (previewSize != null)
                query.Add($"preview_size={previewSize}");
            if (previewCrop != false)
                query.Add($"preview_crop={previewCrop.ToString()}");

            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/resources/public{(query.Count > 0 ? $"?{string.Join("&", query)}" : null)}", 
                "GET").GetJsonTextReader())
                return new ResourceList(jsonReader);
        }

        /// <summary>
        /// Список опубликованных ресурсов.
        /// </summary>
        /// <param name="limit">Количество опубликованных файлов, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="type">Тип ресурса.</param>
        /// <param name="fields">Список возвращаемых атрибутов.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Возвращает метаинформацию о запрошенном количестве файлов в теле ответа, в объекте <see cref="ResourceList"/>.</returns>
        public async Task<ResourceList> ListPublicResourcesAsync(int limit = 20, int offset = 0, string type = null, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            return await Task.Run(() => ListPublicResources(limit, offset, type, fields, previewSize, previewCrop));
        }

        /// <summary>
        /// Метаинформация о публичном ресурсе.
        /// </summary>
        /// <param name="publicKey">Ключ опубликованного ресурса или публичная ссылка на ресурс.</param>
        /// <param name="path">Относительный путь к ресурсу внутри публичной папки.</param>
        /// <param name="sort">Атрибут, по которому сортируется список ресурсов, вложенных в папку.</param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список ключей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Возвращает метаинформацию о запрошенном ресурсе в теле ответа, в объекте <see cref="ResourceList"/>.</returns>
        public ResourceList GetPublicResource(string publicKey, string path = null, string sort = null, int limit = 20, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            string te = $"https://cloud-api.yandex.net/v1/disk/public/resources?" +
                $"public_key={HttpUtility.UrlEncode(publicKey)}" +
                $"{(path != null ? $"&path={HttpUtility.UrlEncode(path)}" : null)}" +
                $"{(sort != null ? $"&sort={sort}" : null)}" +
                $"{(limit != 20 ? $"&limit={limit}" : null)}" +
                $"{(offset != 0 ? $"&offset={offset}" : null)}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}" +
                $"{(previewSize != null ? $"&preview_size={previewSize}{(previewCrop != false ? $"&preview_crop={previewCrop.ToString()}" : null)}" : "")}";

            using (var jsonReader = CreateAnonRequest($"https://cloud-api.yandex.net/v1/disk/public/resources?" +
                $"public_key={HttpUtility.UrlEncode(publicKey)}" +
                $"{(path != null ? $"&path={HttpUtility.UrlEncode(path)}" : null)}" +
                $"{(sort != null ? $"&sort={sort}" : null)}" +
                $"{(limit != 20 ? $"&limit={limit}" : null)}" +
                $"{(offset != 0 ? $"&offset={offset}" : null)}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}" +
                $"{(previewSize != null ? $"&preview_size={previewSize}{(previewCrop != false ? $"&preview_crop={previewCrop.ToString()}" : null)}" : "")}",
                "GET").GetJsonTextReader())
                return new ResourceList(jsonReader);
        }

        /// <summary>
        /// Метаинформация о публичном ресурсе.
        /// </summary>
        /// <param name="publicKey">Ключ опубликованного ресурса или публичная ссылка на ресурс.</param>
        /// <param name="path">Относительный путь к ресурсу внутри публичной папки.</param>
        /// <param name="sort">Атрибут, по которому сортируется список ресурсов, вложенных в папку.</param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="fields">Список ключей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="previewSize">Требуемый размер уменьшенного изображения (превью файла), ссылка на которое API возвращает в поле <see cref="Resource.Preview"/>.</param>
        /// <param name="previewCrop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра <paramref name="previewSize"/>.</param>
        /// <returns>Возвращает метаинформацию о запрошенном ресурсе в теле ответа, в объекте <see cref="ResourceList"/>.</returns>
        public async Task<ResourceList> GetPublicResourceAsync(string publicKey, string path = null, string sort = null, int limit = 20, int offset = 0, IEnumerable<string> fields = null, string previewSize = null, bool previewCrop = false)
        {
            return await Task.Run(() => GetPublicResource(publicKey, path, sort, limit, offset, fields, previewSize, previewCrop));
        }

        /// <summary>
        /// Скачивание публичного файла или папки.
        /// </summary>
        /// <remarks>Ресурс, опубликованный на Диске, можно скачать, зная его ключ или публичную ссылку на него. Из публичных папок также можно скачивать отдельные файлы.</remarks>
        /// <param name="publicKey">Ключ сохраняемого ресурса.</param>
        /// <param name="path">Путь к скачиваемому файлу. Следует указать, если в значении параметра <paramref name="publicKey"/> передан ключ публичной папки, в которой находится нужный файл.</param>
        /// <returns>Возвращает сгенерированный URL для скачивания файла в объекте <see cref="Link"/></returns>
        public Link GetPublicResourceDownloadLink(string publicKey, string path = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/public/resources/download?" +
                $"public_key={HttpUtility.UrlEncode(publicKey)}" +
                $"{(path != null ? $"&path={HttpUtility.UrlEncode(path)}" : "")}", 
                "GET").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Скачивание публичного файла или папки.
        /// </summary>
        /// <remarks>Ресурс, опубликованный на Диске, можно скачать, зная его ключ или публичную ссылку на него. Из публичных папок также можно скачивать отдельные файлы.</remarks>
        /// <param name="publicKey">Ключ сохраняемого ресурса.</param>
        /// <param name="path">Путь к скачиваемому файлу. Следует указать, если в значении параметра <paramref name="publicKey"/> передан ключ публичной папки, в которой находится нужный файл.</param>
        /// <returns>Возвращает сгенерированный URL для скачивания файла в объекте <see cref="Link"/></returns>
        public async Task<Link> GetPublicResourceDownloadLinkAsync(string publicKey, string path = null)
        {
            return await Task.Run(() => GetPublicResourceDownloadLink(publicKey, path));
        }

        /// <summary>
        /// Сохранение публичного файла в «Загрузки»
        /// </summary>
        /// <remarks>Файл, опубликованный на Диске, можно скопировать в папку «Загрузки» на Диске пользователя. Для этого нужно знать ключ файла или публичную ссылку на него. Также можно копировать отдельные файлы из публичной папки.</remarks>
        /// <param name="publicKey">Ключ сохраняемого файла.</param>
        /// <param name="name">Имя, под которым файл следует сохранить в папку «Загрузки».</param>
        /// <param name="path">Путь к скачиваемому файлу. Следует указать, если в значении параметра <paramref name="publicKey"/> передан ключ публичной папки, в которой находится нужный файл.</param>
        /// <param name="savePath">Путь к папке, в которую будет сохранен ресурс. По умолчанию «Загрузки».</param>
        /// <returns>Скачивание файла может занять неопределенное время. Если операция скачивания была запущена, API отвечает ссылкой на статус операции в объекте <see cref="Link"/>.</returns>

        public Link SaveToDiskPublicResource(string publicKey, string name, string path = null, string savePath = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/public/resources/save-to-disk?" +
                $"public_key={HttpUtility.UrlEncode(publicKey)}" +
                $"{(name != null ? $"&name={HttpUtility.UrlEncode(name)}" : "")}" +
                $"{(path != null ? $"&path={HttpUtility.UrlEncode(path)}" : "")}" +
                $"{(savePath != null ? $"&save_path={HttpUtility.UrlEncode(savePath)}" : "")}",
                "POST").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Сохранение публичного файла в «Загрузки»
        /// </summary>
        /// <remarks>Файл, опубликованный на Диске, можно скопировать в папку «Загрузки» на Диске пользователя. Для этого нужно знать ключ файла или публичную ссылку на него. Также можно копировать отдельные файлы из публичной папки.</remarks>
        /// <param name="publicKey">Ключ сохраняемого файла.</param>
        /// <param name="name">Имя, под которым файл следует сохранить в папку «Загрузки».</param>
        /// <param name="path">Путь к скачиваемому файлу. Следует указать, если в значении параметра <paramref name="publicKey"/> передан ключ публичной папки, в которой находится нужный файл.</param>
        /// <param name="savePath">Путь к папке, в которую будет сохранен ресурс. По умолчанию «Загрузки».</param>
        /// <returns>Скачивание файла может занять неопределенное время. Если операция скачивания была запущена, API отвечает ссылкой на статус операции в объекте <see cref="Link"/>.</returns>
        public async Task<Link> SaveToDiskPublicResourceAsync(string publicKey, string name, string path = null, string savePath = null)
        {
            return await Task.Run(() => SaveToDiskPublicResource(publicKey, name, path, savePath));
        }

        /// <summary>
        /// Очистка Корзины.
        /// </summary>
        /// <remarks>Файлы, перемещенные в Корзину, можно окончательно удалить. Корзина считается папкой на Диске, поэтому доступное на Диске место при этом увеличивается. Чтобы удалить из Корзины отдельный файл, можно указать путь к нему в запросе очистки.</remarks>
        /// <param name="path">Путь к удаляемому ресурсу относительно корневого каталога Корзины. Если параметр не задан, Корзина очищается полностью.</param>
        /// <returns>Очистка Корзины может занять неопределенное время. Яндекс.Диск возвращает ссылку на статус запущенной по запросу операции в объекте <see cref="Link"/>.</returns>
        public Link ClearTrash(string path = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/trash/resources/{(path != null ? $"?path={HttpUtility.UrlEncode(path)}" : null)}",
                "DELETE").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Очистка Корзины.
        /// </summary>
        /// <remarks>Файлы, перемещенные в Корзину, можно окончательно удалить. Корзина считается папкой на Диске, поэтому доступное на Диске место при этом увеличивается. Чтобы удалить из Корзины отдельный файл, можно указать путь к нему в запросе очистки.</remarks>
        /// <param name="path">Путь к удаляемому ресурсу относительно корневого каталога Корзины. Если параметр не задан, Корзина очищается полностью.</param>
        /// <returns>Очистка Корзины может занять неопределенное время. Яндекс.Диск возвращает ссылку на статус запущенной по запросу операции в объекте <see cref="Link"/>.</returns>
        public async Task<Link> ClearTrashAsync(string path = null)
        {
            return await Task.Run(() => ClearTrash(path));
        }

        /// <summary>
        /// Восстановление файла или папки из Корзины.
        /// </summary>
        /// <remarks>Перемещенный в Корзину ресурс можно восстановить на прежнем месте, указав путь к нему в корзине. Восстанавливаемый ресурс при этом можно переименовать. Если восстанавливаемый файл находился внутри папки, которая отсутствует на момент запроса, папка с таким именем будет создана в нужном месте.</remarks>
        /// <param name="path">Путь к восстанавливаемому ресурсу относительно корневого каталога Корзины.</param>
        /// <param name="name">Новое имя восстанавливаемого ресурса.</param>
        /// <param name="overwrite">Признак перезаписи. Учитывается, если ресурс восстанавливается в папку, в которой уже есть ресурс с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Восстановление ресурса из Корзины может занять неопределенное время. Возвращает ссылку на мета-информацию о созданном ресурсе в теле ответа, в объекте <see cref="Link"/>.</returns>
        public Link RestoreFromTrash(string path, string name, bool overwrite = false, IEnumerable<string> fields = null)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/trash/resources/restore?" +
                $"path={HttpUtility.UrlEncode(path)}" +
                $"{(name != null ? $"&name={name}" : "")}" +
                $"{(overwrite != false ? $"&overwrite=true" : "")}" +
                $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}",
                "PUT").GetJsonTextReader())
                return new Link(jsonReader);
        }

        /// <summary>
        /// Восстановление файла или папки из Корзины.
        /// </summary>
        /// <remarks>Перемещенный в Корзину ресурс можно восстановить на прежнем месте, указав путь к нему в корзине. Восстанавливаемый ресурс при этом можно переименовать. Если восстанавливаемый файл находился внутри папки, которая отсутствует на момент запроса, папка с таким именем будет создана в нужном месте.</remarks>
        /// <param name="path">Путь к восстанавливаемому ресурсу относительно корневого каталога Корзины.</param>
        /// <param name="name">Новое имя восстанавливаемого ресурса.</param>
        /// <param name="overwrite">Признак перезаписи. Учитывается, если ресурс восстанавливается в папку, в которой уже есть ресурс с таким именем.</param>
        /// <param name="fields">Список полей, которые следует включить в ответ. Ключи, не указанные в этом списке, будут отброшены при составлении ответа. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <returns>Восстановление ресурса из Корзины может занять неопределенное время. Возвращает ссылку на мета-информацию о созданном ресурсе в теле ответа, в объекте <see cref="Link"/>.</returns>
        public async Task<Link> RestoreFromTrashAsync(string path, string name, bool overwrite = false, IEnumerable<string> fields = null)
        {
            return await Task.Run(() => RestoreFromTrash(path, name, overwrite, fields));
        }

        /// <summary>
        /// Получить статус асинхронной операции.
        /// </summary>
        /// <param name="operationId">Идентификатор операции. Возвращается в составе URL запроса, если запрошенная операция может занять неопределенное время.</param>
        /// <returns>Если запрос был обработан без ошибок, API возвращает статус операции в теле ответа, в объекте <see cref="Operation"/>.</returns>
        public Operation GetOperationStatus(string operationId)
        {
            using (var jsonReader = CreateRequest($"https://cloud-api.yandex.net/v1/disk/trash/resources/restore?" +
                    $"operation_id={operationId}",
                    "GET").GetJsonTextReader())
                return new Operation(jsonReader);
        }

        /// <summary>
        /// Получить статус асинхронной операции.
        /// </summary>
        /// <param name="operationId">Идентификатор операции. Возвращается в составе URL запроса, если запрошенная операция может занять неопределенное время.</param>
        /// <returns>Если запрос был обработан без ошибок, API возвращает статус операции в теле ответа, в объекте <see cref="Operation"/>.</returns>
        public async Task<Operation> GetOperationStatusAsync(string operationId)
        {
            return await Task.Run(() => GetOperationStatus(operationId));
        }

        #endregion

    }
}
