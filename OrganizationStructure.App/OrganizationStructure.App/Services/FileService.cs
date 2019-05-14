using Newtonsoft.Json;
using OrganizationStructure.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Services
{
    public class FileService
    {
        private const string FilesFolder = "Files";

        public void SaveDataToLocal(MainModel mainModel)
        {
            string json = JsonConvert.SerializeObject(mainModel);
            string basePath = Path.Combine(Environment.CurrentDirectory, FilesFolder);

            CheckExistsFolder(basePath);

            var path = Path.Combine(basePath, GetFileName(mainModel));
            File.WriteAllText(path, json);
        }

        public List<MainModel> LoadDataFromLocal()
        {
            List<MainModel> models = new List<MainModel>();
            string path = Path.Combine(Environment.CurrentDirectory, FilesFolder);

            if (Directory.Exists(path))
            {
                string[] filePaths = Directory.GetFiles(path, "*.json");

                foreach (var filePath in filePaths)
                {
                    var json = File.ReadAllText(filePath);
                    MainModel model = JsonConvert.DeserializeObject<MainModel>(json);
                    models.Add(model);
                }
            }

            return models;
        }

        public MainModel LoadModelToApp(Guid structureId)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, FilesFolder, GetFileName(structureId));

            var json = File.ReadAllText(filePath);
            MainModel model = JsonConvert.DeserializeObject<MainModel>(json);

            return model;
        }

        private string GetFileName(MainModel mainModel)
        {
            return $"{mainModel.StructureModel.Id}.json";
        }

        private string GetFileName(Guid strucutreId)
        {
            return $"{strucutreId}.json";
        }

        private void CheckExistsFolder(string path)
        {
            if (!Directory.Exists(FilesFolder))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
