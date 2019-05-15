using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace OrganizationStructure.WebApp.Dal
{
    public class OStructureRepository
    {
        //private const string FilesFolder = "Files";
        const string BasePathFilesAppKey = "PathFiles";

        private string _BasePathFiles;
        public string BasePathFiles
        {
            get
            {
                if (_BasePathFiles == null)
                {
                    _BasePathFiles = ConfigurationManager.AppSettings[BasePathFilesAppKey];
                }
                return _BasePathFiles;
            }
        }

        public void SaveOrganizationStructure(string fileName, string oStructureModelJSON)
        {
            //string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FilesFolder);

            CheckExistsFolder(BasePathFiles);

            var path = Path.Combine(BasePathFiles, GetFileName(fileName));
            File.WriteAllText(path, oStructureModelJSON);
        }


        public Dictionary<Guid, string> LoadOrganizationStructures()
        {
            Dictionary<Guid, string> result = new Dictionary<Guid, string>();

            
            //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FilesFolder);

            if (Directory.Exists(BasePathFiles))
            {
                string[] filePaths = Directory.GetFiles(BasePathFiles, "*.json");

                foreach (var filePath in filePaths)
                {
                    var json = File.ReadAllText(filePath);
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    result.Add(Guid.Parse(fileName), json);
                }
            }

            return result;
        }

        private void CheckExistsFolder(string path)
        {
            if (!Directory.Exists(path))
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