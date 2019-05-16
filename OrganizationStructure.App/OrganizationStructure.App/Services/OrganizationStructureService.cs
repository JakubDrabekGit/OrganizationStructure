using Newtonsoft.Json;
using OrganizationStructure.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Services
{
    public class OrganizationStructureService
    {
        private FileService _FileService;

        public FileService FileService
        {
            get
            {
                if (_FileService == null)
                {
                    _FileService = new FileService();
                }
                return _FileService;
            }
        }

        private OStructureProxyService _ProxyService;

        public OStructureProxyService ProxyService
        {
            get
            {
                if (_ProxyService == null)
                {
                    _ProxyService = new OStructureProxyService();
                }
                return _ProxyService;
            }
        }

        internal void CreateOrganizationStructure(string companyName)
        {
            GlobalInstance.Instance.Model.CreateOrganizationStructure(companyName);
        }

        internal void SaveToLocal()
        {
            UpdateDateModel();
            FileService.SaveDataToLocal(GlobalInstance.Instance.Model);
        }

        internal void SaveToServer()
        {
            SaveToLocal();
            ProxyService.SaveOrganizationStructure(new DataObjects.OrganizationStructureModel
            {
                Id = GlobalInstance.Instance.Model.StructureModel.Id,
                ModelJson = JsonConvert.SerializeObject(GlobalInstance.Instance.Model),
            });
        }
        private void UpdateDateModel()
        {
            GlobalInstance.Instance.Model.StructureModel.LastUpdatedDate = DateTime.Now;
        }

        internal List<MainModel> LoadFromServer()
        {
            List<MainModel> mainModels = new List<MainModel>();

            DataObjects.OrganizationStructuresModel modelsServer = ProxyService.GetOrganizationStructures();

            foreach (var model in modelsServer.OrganizationStructures)
            {
                mainModels.Add(JsonConvert.DeserializeObject<MainModel>(model.ModelJson));
            }
            return mainModels;
        }

        internal List<MainModel> LoadDataFromLocal()
        {
            List<MainModel> models = FileService.LoadDataFromLocal();

            return models;
        }

        internal List<OrganizationStructureModel> LoadStructureModelsFromLocal()
        {
            List<OrganizationStructureModel> structureModels = new List<OrganizationStructureModel>();
            List<MainModel> models = FileService.LoadDataFromLocal();

            foreach (var model in models)
            {
                structureModels.Add(model.StructureModel);
            }
            return structureModels;
        }

        internal List<OrganizationStructureModel> LoadStructureModelsFromServer()
        {
            List<OrganizationStructureModel> structureModels = new List<OrganizationStructureModel>();
            DataObjects.OrganizationStructuresModel modelsServer = ProxyService.GetOrganizationStructures();

            if (modelsServer != null)
            {
                foreach (var model in modelsServer.OrganizationStructures)
                {
                    structureModels.Add(JsonConvert.DeserializeObject<MainModel>(model.ModelJson).StructureModel);
                }
            }
            return structureModels;
        }

        internal void LoadModelToApp(Guid structureId)
        {
            var model = FileService.LoadModelToApp(structureId);

            GlobalInstance.Instance.LoadModel(model);
        }

        internal void LoadModelToAppModelFromServer(Guid structureId)
        {
            List<OrganizationStructureModel> structureModels = new List<OrganizationStructureModel>();
            DataObjects.OrganizationStructuresModel modelsServer = ProxyService.GetOrganizationStructure(structureId);


            if (modelsServer != null && modelsServer.OrganizationStructures != null)
            {
                var model = modelsServer.OrganizationStructures.FirstOrDefault();

                GlobalInstance.Instance.LoadModel(JsonConvert.DeserializeObject<MainModel>(model.ModelJson));
            }
        }

        #region Person


        internal void AddPerson(Person person)
        {
            GlobalInstance.Instance.Model.AddPerson(person);
        }

        internal void DeletePerson(int personId)
        {
            GlobalInstance.Instance.Model.RemovePerson(personId);
        }
        internal Person GetPerson(int? personId)
        {
            return GlobalInstance.Instance.Model.Persons.FirstOrDefault(x => x.Id == personId);
        }
        internal string GetPersonFullName(int? personId)
        {
            string fullName = "";
            Person person = GetPerson(personId);

            if(person != null)
            {
                fullName = person.FullName;
            }
            return fullName;
        }

        internal int GetNewPersonId()
        {
            return GlobalInstance.Instance.Model.GetNewPersonId();
        }

        #endregion

        #region Role
        
        internal void AddRole(Role role)
        {
            GlobalInstance.Instance.Model.AddRole(role);
        }
        internal void DeleteRole(int roleId)
        {
            GlobalInstance.Instance.Model.RemoveRoles(roleId);
        }

        internal Role GetRole(int? roleId)
        {
            return GlobalInstance.Instance.Model.Roles.FirstOrDefault(x => x.Id == roleId);
        }

        internal string GetPersonRole(int? PersonId)
        {
            string roleName = String.Empty;
            var person = GlobalInstance.Instance.Model.Persons.FirstOrDefault(x => x.Id == PersonId);
            if (person != null)
            {
                roleName = person.RoleName;
            }
            return roleName;
        }

        internal int GetNewRoleId()
        {
            return GlobalInstance.Instance.Model.GetNewRoleId();
        }
        #endregion


        internal int GetNewStructureId()
        {
            return GlobalInstance.Instance.Model.GetNewStructureId();
        }

        internal void DeleteStructure(StructureModel structureModel)
        {
            GlobalInstance.Instance.Model.DeleteStructure(structureModel);
        }

        internal StructureModel GetStructureModel(int structureId)
        {
            return GlobalInstance.Instance.Model.GetStructureModel(structureId);
        }

        internal List<StructureModel> GetAllStructures()
        {
            return GlobalInstance.Instance.Model.GetAllStructures();
        }
    }
}
