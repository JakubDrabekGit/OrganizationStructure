using OrganizationStructure.WebApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OrganizationStructure.WebApp.Controllers.Base
{
    [BasicAuthentication]
    public class BaseApiController : ApiController
    {
    }
}