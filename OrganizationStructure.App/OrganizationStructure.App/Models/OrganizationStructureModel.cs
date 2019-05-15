using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Models
{
    public class OrganizationStructureModel //: ViewModels.Base.BaseViewModel
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public ObservableCollection<StructureModel> RootStructure { get; set; }
    }
}