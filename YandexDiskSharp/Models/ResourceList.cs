using Newtonsoft.Json;
using System;

namespace YandexDiskSharp.Models
{
    public class ResourceList : FilesResourceList
    {
        protected ResourceList() { }

        public ResourceList(ResourceList rl)
        {
            this.sort = rl.sort;
            this.publicKey = rl.publicKey;
            this.path = rl.path;
            this.total = rl.total;
            this.items = new System.Collections.Generic.List<Resource>();
            foreach(Resource item in rl.items)
            {
                this.items.Add(new Resource(item));
            }

        }

        internal ResourceList(JsonTextReader jsonReader) : base()
        {
            int depth = jsonReader.Depth;
            while (jsonReader.Read())
            {
                switch (jsonReader.TokenType)
                {
                    case JsonToken.PropertyName:
                        switch (jsonReader.Value.ToString())
                        {
                            case "sort":
                                sort = jsonReader.ReadAsString();
                                break;
                            case "public_key":
                                publicKey = jsonReader.ReadAsString();
                                break;
                            case "items":
                                jsonReader.Read();
                                while (jsonReader.Read() && jsonReader.TokenType != JsonToken.EndArray)
                                    items.Add(new Resource(jsonReader));
                                break;
                            case "path":
                                path = jsonReader.ReadAsString();
                                break;
                            case "limit":
                                limit = jsonReader.ReadAsInt32().Value;
                                break;
                            case "offset":
                                offset = jsonReader.ReadAsInt32().Value;
                                break;
                            case "total":
                                total = jsonReader.ReadAsInt32().Value;
                                break;
                            case "name":
                                name = jsonReader.ReadAsString();
                                break;
                            case "created":
                                created = (DateTime)jsonReader.ReadAsDateTime();
                                break;
                            case "modified":
                                created = (DateTime)jsonReader.ReadAsDateTime();
                                break;
                            case "type":
                                System.Diagnostics.Debug.WriteLine($"type = {jsonReader.ReadAsString()}");
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


        #region ~Fields~

        protected string name;
        protected string sort;
        protected string publicKey;
        protected string path;
        protected int total;
        protected System.DateTime created;
        protected System.DateTime modified;

        #endregion

        #region ~Properties~

        /// <summary>
        /// Поле, по которому отсортирован список.
        /// </summary>
        public string Sort => sort;

        /// <summary>
        /// Ключ опубликованного ресурса.
        /// </summary>
        /// <remarks>
        /// Включается только в ответ на запрос метаинформации о публичной папке.
        /// </remarks>
        public string PublicKey => publicKey;

        /// <summary>
        /// Путь к папке, чье содержимое описывается в данном объекте <see cref="ResourceList"/>.
        /// </summary>
        /// <remarks>
        /// Для публичной папки значение атрибута всегда равно «/».
        /// </remarks>
        public string Path => path;

        /// <summary>
        /// Общее количество ресурсов в папке.
        /// </summary>
        public int Total => total;

        public string Name => name;

        public System.DateTime Created => created;

        public System.DateTime Modified => modified;

        public System.Collections.Generic.List<Resource> Items => items;

        #endregion
    }
}
