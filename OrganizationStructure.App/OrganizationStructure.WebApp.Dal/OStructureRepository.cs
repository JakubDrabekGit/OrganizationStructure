using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.WebApp.Dal
{
    public class OStructureRepository
    {
        private const string FilesFolder = "Files";

        public void SaveOrganizationStructure(string fileName, string oStructureModelJSON)
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, FilesFolder);

            CheckExistsFolder(basePath);

            var path = Path.Combine(basePath, GetFileName(fileName));
            File.WriteAllText(path, oStructureModelJSON);
        }


        public Dictionary<Guid, string> LoadOrganizationStructure()
        {
            Dictionary<Guid, string> result = new Dictionary<Guid, string>();

            string path = Path.Combine(Environment.CurrentDirectory, FilesFolder);

            if (Directory.Exists(path))
            {
                string[] filePaths = Directory.GetFiles(path, "*.json");

                foreach (var filePath in filePaths)
                {
                    var json = File.ReadAllText(filePath);
                    string fileName = Path.GetFileName(filePath);
                    result.Add(Guid.Parse(fileName), json);
                }
            }

            return result;
        }

        private void CheckExistsFolder(string path)
        {
            if (!Directory.Exists(FilesFolder))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string GetFileName(string fileName)
        {
            return $"{fileName}.json";
        }
    }
}
