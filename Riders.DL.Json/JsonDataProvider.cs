using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Riders.Common;

namespace Riders.DL.Json
{
    internal abstract class JsonDataProvider<T> : DataProvider<T> where T:IIdentifieable
    {
        protected abstract T FromJson(dynamic json);
        protected abstract dynamic ToJson(T obj);

        protected DirectoryInfo StoreDirectory { get; }
        protected DataContext Context { get; }

        protected JsonDataProvider(string homeDirectory, DataContext dataContext)
        {
            Context = dataContext;
            StoreDirectory = new DirectoryInfo(Path.Combine(homeDirectory, typeof(T).Name.ToLower()));

            if (!StoreDirectory.Exists)
                StoreDirectory.Create();
        }

        public override IQueryable<T> Query => LoadObjects().AsQueryable();

        private IEnumerable<T> LoadObjects()
        {
            foreach (var file in StoreDirectory.EnumerateFiles())
            {
                yield return LoadObject(file);
            }
        }

        private readonly JsonConverter jsonConverter = new ExpandoObjectConverter();
        private T LoadObject(FileInfo file)
        {
            using (var reader = new StreamReader(file.OpenRead()))
            {
                dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(reader.ReadToEnd(), jsonConverter);
                var obj = FromJson(json);
                obj.Id = ExtractId(file);
                return obj;
            }
        }

        protected override T Save(T obj)
        {
            obj.Id = GetNextId();
            return SaveToFile(obj);
        }

        private long? GetNextId()
        {
            return StoreDirectory
                       .EnumerateFiles()
                       .Select(ExtractId)
                       .DefaultIfEmpty(0)
                       .Max() + 1;
        }

        private long ExtractId(FileInfo file)
        {
            var match = Regex.Match(file.Name, @"(\d+)\.json");
            return match.Success
                ? Int64.Parse(match.Groups[1].Value)
                : 0;

        }

        protected override T Update(T obj)
        {
            return SaveToFile(obj);
        }

        private T SaveToFile(T obj)
        {
            var targetFile = new FileInfo(Path.Combine(StoreDirectory.FullName, $"{obj.Id.Value}.json"));
            using (var writer = new StreamWriter(targetFile.OpenWrite()))
            {
                var json = ToJson(obj);
                writer.Write(JsonConvert.SerializeObject(json, jsonConverter));
            }
            return obj;
        }
    }
}
