using OrganizationStructure.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizationStructure.WebApp.Bll
{
    public class OStructureService
    {
        private Dal.OStructureRepository _OStructureRepository;
        public Dal.OStructureRepository OStructureRepository
        {
            get
            {
                if (_OStructureRepository == null)
                {
                    _OStructureRepository = new Dal.OStructureRepository();
                }
                return _OStructureRepository;
            }
        }

        public void SaveOrganizationStructure(OrganizationStructureModel organizationStructure)
        {
            OStructureRepository.SaveOrganizationStructure(organizationStructure.Id.ToString(), organizationStructure.ModelJson);
        }

        public OrganizationStructuresModel LoadOrganizationStructures()
        {
            OrganizationStructuresModel structures = new OrganizationStructuresModel();

            foreach (var structure in OStructureRepository.LoadOrganizationStructures())
            {
                structures.OrganizationStructures.Add(new OrganizationStructureModel() { Id = structure.Key, ModelJson = structure.Value });
            }

            return structures;
        }

        public OrganizationStructureModel LoadOrganizationStructure(Guid id)
        {
            var structure = OStructureRepository.LoadOrganizationStructures().FirstOrDefault(s => s.Key == id);

            return new OrganizationStructureModel() { Id = structure.Key, ModelJson = structure.Value };
        }
    }
}