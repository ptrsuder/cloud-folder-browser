using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace YandexDiskSharp.Models
{
    /// <summary>
    /// Описание ресурса, мета-информация о файле или папке. Включается в ответ на запрос метаинформации.
    /// </summary>
    public class Resource
    {
        #region ~Constructor~

        internal Resource() { }

        [JsonConstructor]
        public Resource(string Name, DateTime Created, DateTime Modified, long Size)
        {
            this.publicKey = "";
            this.name = Name;
            this.created = Created;
            this.modified = Modified;
            this.path = "";
            this.mimeType = "";
            this.size = Size;
            this.mediaType = "";
            //this.preview = Preview;
            //this.publicUrl = "";
        }

        public Resource(string PublicKey, string Name, DateTime Created, DateTime Modified, string Path, string MimeType, long Size, string MediaType, Uri PublicUrl)
        {
            this.publicKey = PublicKey;
            this.name = Name;
            this.created = Created;            
            this.modified = Modified;
            this.path = Path;            
            this.mimeType = MimeType;
            this.size = Size;
            this.mediaType = MediaType;
            //this.preview = Preview;
            this.publicUrl = PublicUrl;
        }


        public Resource(Resource r)
        {
            this.publicKey = r.publicKey;
            this.name = r.name;
            this.created = r.created;
            this.publicUrl = r.publicUrl;
            this.originPath = r.originPath;
            this.modified = r.modified;
            this.path = r.path;
            this.md5 = r.md5;
            this.sha256 = r.sha256;
            this.revision = r.revision;
            this.type = r.type;
            this.mimeType = r.mimeType;
            this.size = r.size;
            this.mediaType = r.mediaType;
            this.preview = r.preview;
            this.resourceId = r.resourceId;
            this.commentIds = r.commentIds;
            this.share = r.share;
            this.viewsCount = r.viewsCount;
        }

        internal Resource(JsonTextReader jsonReader)
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
            {
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "public_key":
                                publicKey = jsonReader.ReadAsString();
                                break;
                            case "_embedded":
                                embedded = new ResourceList(jsonReader);
                                break;
                            case "name":
                                name = jsonReader.ReadAsString();
                                break;
                            case "created":
                                created = jsonReader.ReadAsDateTime().Value;
                                break;
                            case "custom_properties":
                                IDictionary<string, string> customProperties = new Dictionary<string, string>();
                                int depth2 = jsonReader.Depth;
                                while (jsonReader.Read())
                                {
                                    if (jsonReader.TokenType == JsonToken.PropertyName)
                                        customProperties.Add(jsonReader.Value.ToString(), jsonReader.ReadAsString());
                                    else if (jsonReader.TokenType == JsonToken.EndObject)
                                        if (jsonReader.Depth == depth2)
                                            break;
                                }
                                customProperties = new ReadOnlyDictionary<string, string>(customProperties);
                                break;
                            case "public_url":
                                publicUrl = new Uri(jsonReader.ReadAsString());
                                break;
                            case "origin_path":
                                originPath = jsonReader.ReadAsString();
                                break;
                            case "modified":
                                modified = jsonReader.ReadAsDateTime().Value;
                                break;
                            case "path":
                                path = jsonReader.ReadAsString();
                                break;
                            case "md5":
                                md5 = jsonReader.ReadAsString();
                                break;
                            case "type":
                                type = jsonReader.ReadAsString() == "file" ? Type.file : Type.dir;
                                break;
                            case "mime_type":
                                mimeType = jsonReader.ReadAsString();
                                break;
                            case "size":
                                size = long.Parse(jsonReader.ReadAsString());
                                break;
                            case "sha256":
                                sha256 = jsonReader.ReadAsString();
                                break;
                            case "revision":
                                revision = long.Parse(jsonReader.ReadAsString());
                                break;
                            case "media_type":
                                mediaType = jsonReader.ReadAsString();
                                break;
                            case "preview":
                                preview = new Uri(jsonReader.ReadAsString());
                                break;
                            case "resource_id":
                                resourceId = jsonReader.ReadAsString();
                                break;
                            case "comment_ids":
                                commentIds = new CommentIds(jsonReader);
                                break;
                            case "share":
                                share = new ShareInfo(jsonReader);
                                break;
                            case "views_count":
                                viewsCount = jsonReader.ReadAsInt32();
                                break;
                            case "owner":
                                owner = new UserPublicInformation(jsonReader);
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

        protected ResourceList embedded { get; set; }
        protected string publicKey;
        protected string name;
        protected DateTime created;
        protected ReadOnlyDictionary<string, string> customProperties;
        protected Uri publicUrl { get; set; }
        protected string originPath;
        protected DateTime modified;
        protected string path;
        protected string md5;
        protected string sha256;
        protected long revision;
        protected Type type;
        protected string mimeType;
        protected long size;
        protected string mediaType;
        protected Uri preview;
        protected string resourceId;
        protected CommentIds commentIds;
        protected ShareInfo share;
        protected int? viewsCount;
        protected UserPublicInformation owner;

        #endregion

        #region ~Methods~

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Resource"/>.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <returns>Класс <see cref="Resource"/>, эквивалентный json ответу, содержащемуся в параметре s.</returns>
        public static Resource Parse(string s)
        {
            using (var stringReader = new StringReader(s))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return new Resource(jsonReader);
            }
        }

        /// <summary>
        /// Преобразует строковое представление json ответа в эквивалентный ему класс <see cref="Resource"/>. Возвращает значение, указывающее, успешно ли выполнено преобразование.
        /// </summary>
        /// <param name="s">Строка, содержащая преобразуемый json ответ.</param>
        /// <param name="result">При возвращении этим методом содержиткласс <see cref="Resource"/>, эквивалентный json ответу, содержащемуся в параметре s, если преобразование выполнено успешно, или null, если оно завершилось сбоем. Преобразование завершается сбоем, если параметр s равен null или <see cref="System.String.Empty"/> или не находится в правильном формате. Этот параметр передается неинициализированным; любое значение, первоначально предоставленное в объекте result, будет перезаписано.</param>
        /// <returns>Значение true, если параметр s успешно преобразован; в противном случае — значение false.</returns>
        public static bool TryParse(string s, out Resource result)
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
        /// Ресурсы, непосредственно содержащиеся в папке (содержит объект <see cref="ResourceList"/>).
        /// </summary>
        /// <remarks>
        /// Включается в ответ только при запросе метаинформации о папке.
        /// </remarks>      
        [JsonIgnore]
        public ResourceList Embedded => embedded;

        /// <summary>
        /// Ключ опубликованного ресурса.
        /// </summary>
        /// <remarks>
        /// Включается в ответ только если указанный файл или папка опубликован.
        /// </remarks>
        public string PublicKey => publicKey;

        /// <summary>
        /// Имя ресурса.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Дата и время создания ресурса.
        /// </summary>
        public DateTime Created => created;

        #warning добавить ссылку на метод
        /// <summary>
        /// Объект со всеми атрибутами, заданными с помощью запроса <see cref="YandexDiskApi.PatchResources(string, IDictionary{string, string}, string[])"/>. 
        /// </summary>
        /// <remarks>
        /// Содержит только ключи вида имя:значение (объекты или массивы содержать не может).
        /// </remarks>
        [JsonIgnore]
        public ReadOnlyDictionary<string, string> CustomProperties => customProperties;

        /// <summary>
        /// Ссылка на опубликованный ресурс.
        /// </summary>
        /// <remarks>
        /// Включается в ответ только если указанный файл или папка опубликован.
        /// </remarks>        
        public Uri PublicUrl
        {
            get { return publicUrl;  }
            set { publicUrl = value;  }
        }

        /// <summary>
        /// Путь к ресурсу до перемещения в Корзину.
        /// </summary>
        /// <remarks>
        /// Включается в ответ только для запроса метаинформации о ресурсе в Корзине.
        /// </remarks>
        [JsonIgnore]
        public string OriginPath => originPath;

        /// <summary>
        /// Дата и время изменения ресурса.
        /// </summary>
        public DateTime Modified => modified;

        /// <summary>
        /// Полный путь к ресурсу на Диске.
        /// </summary>
        /// <remarks>
        /// В метаинформации опубликованной папки пути указываются относительно самой папки. Для опубликованных файлов значение ключа всегда «/».
        /// Для ресурса, находящегося в Корзине, к атрибуту может быть добавлен уникальный идентификатор.
        /// <example>
        /// trash:/foo_1408546879
        /// </example>
        /// С помощью этого идентификатора ресурс можно отличить от других удаленных ресурсов с тем же именем.
        /// </remarks>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// MD5-хэш файла.
        /// </summary>
        public string Md5 => md5;

        /// <summary>
        /// SHA256-хэш файла.
        /// </summary>
        public string Sha256 => sha256;

        /// <summary>
        /// Ревизия Диска в которой этот ресурс был изменён последний раз.
        /// </summary>
        public long Revision => revision;

        /// <summary>
        /// Тип ресурса.
        /// </summary>
        public Type Type => type;

        /// <summary>
        /// MIME-тип файла.
        /// </summary>
        public string MimeType => mimeType;

        /// <summary>
        /// Размер файла.
        /// </summary>
        public long Size => size;

        /// <summary>
        /// MIME-тип файла.
        /// </summary>
        public string MediaType => mediaType;

        /// <summary>
        /// Ссылка на уменьшенное изображение из файла(превью). Включается в ответ только для файлов поддерживаемых графических форматов.
        /// </summary>
        /// <remarks>
        /// Запросить превью можно только с OAuth-токеном пользователя, имеющего доступ к самому файлу.
        /// </remarks>
        public Uri Preview => preview;

        /// <summary>
        /// Идентификатор ресурса.
        /// </summary>
        public string ResourceId => resourceId;

        /// <summary>
        /// Идентификаторы комментариев.
        /// </summary>
        [JsonIgnore]
        public CommentIds CommentIds => commentIds;

        /// <summary>
        /// Информация об общей папке.
        /// </summary>
        [JsonIgnore]
        public ShareInfo Share => share;

        /// <summary>
        /// Счетчик просмотров публичного ресурса.
        /// </summary>
        [JsonIgnore]
        public int? ViewsCount => viewsCount;

        /// <summary>
        /// Владелец опубликованного ресурса.
        /// </summary>
        [JsonIgnore]
        public UserPublicInformation Owner => owner;

        #endregion
    }
}
