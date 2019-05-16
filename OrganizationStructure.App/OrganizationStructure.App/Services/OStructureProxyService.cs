using OrganizationStructure.App.Services.DataObjects;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Services
{
    public class OStructureProxyService
    {
        const string BaseUrlAppKey = "OrganizationStructure.WebApp.BaseUrl";
        const string LoginNameAppKey = "OrganizationStructure.WebApp.LoginName";
        const string LoginPasswordAppKey = "OrganizationStructure.WebApp.LoginPassword";

        const string ResourceName = "OrganizationStructure";
        
        private string _BaseUrl;
        private string _LoginName;
        private string _LoginPassword;
        public string BaseUrl
        {
            get
            {
                if (_BaseUrl == null)
                {
                    _BaseUrl = ConfigurationManager.AppSettings[BaseUrlAppKey];
                }
                return _BaseUrl;
            }
        }
        
        public string LoginName
        {
            get
            {
                if (_LoginName == null)
                {
                    _LoginName = ConfigurationManager.AppSettings[LoginNameAppKey];
                }
                return _LoginName;
            }
        }
        
        public string LoginPassword
        {
            get
            {
                if (_LoginPassword == null)
                {
                    _LoginPassword = ConfigurationManager.AppSettings[LoginPasswordAppKey];
                }
                return _LoginPassword;
            }
        }

        public OrganizationStructuresModel GetOrganizationStructures()
        {
            var client = new RestClient(BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(LoginName, LoginPassword);

            var request = new RestRequest(ResourceName, Method.GET);
            IRestResponse<OrganizationStructuresModel> response = client.Execute<OrganizationStructuresModel>(request);
            return response.Data;
        }
        public OrganizationStructureModel GetOrganizationStructure(Guid id)
        {
            var client = new RestClient(BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(LoginName, LoginPassword);

            var request = new RestRequest($"{ResourceName}/{id}", Method.GET);
            IRestResponse<OrganizationStructureModel> response = client.Execute<OrganizationStructureModel>(request);
            return response.Data;
        }

        public void SaveOrganizationStructure(OrganizationStructureModel model)
        {
            var client = new RestClient(BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(LoginName, LoginPassword);
            
            var request = new RestRequest(ResourceName, Method.POST);
            request.AddObject(model);
            var response = client.Execute(request);
        }
    }
}
