using OrganizationStructure.WebApp.Controllers.Base;
using OrganizationStructure.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrganizationStructure.WebApp.Controllers
{
    public class DataController : BaseApiController
    {

        private Bll.OStructureService _OStructureService;
        public Bll.OStructureService OStructureService
        {
            get
            {
                if (_OStructureService == null)
                {
                    _OStructureService = new Bll.OStructureService();
                }
                return _OStructureService;
            }
        }

        [HttpGet]
        [Route("OrganizationStructure")]
        public OrganizationStructuresModel GetOrganizationStructure()
        {
            return OStructureService.LoadOrganizationStructures();
        }

        [HttpPost]
        [Route("OrganizationStructure")]
        public void SaveOrganizationStructure([FromBody]OrganizationStructureModel organizationStructure)
        {
            OStructureService.SaveOrganizationStructure(organizationStructure);
        }
    }
}
