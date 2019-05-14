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

        //    private Guid _Id;
        //    private string _CompanyName;
        //    private DateTime _CreatedDate;
        //    private DateTime _LastUpdatedDate;
        //    private ObservableCollection<StructureModel> _RootStructure;

        //    public Guid Id
        //    {
        //        get { return _Id; }
        //        set
        //        {
        //            SetProperty(ref _Id, value);
        //        }
        //    }

        //    public string CompanyName
        //    {
        //        get { return _CompanyName; }
        //        set
        //        {
        //            SetProperty(ref _CompanyName, value);
        //        }
        //    }
        //    public DateTime CreatedDate
        //    {
        //        get { return _CreatedDate; }
        //        set
        //        {
        //            SetProperty(ref _CreatedDate, value);
        //        }
        //    }

        //    public DateTime LastUpdatedDate
        //    {
        //        get { return _LastUpdatedDate; }
        //        set
        //        {
        //            SetProperty(ref _LastUpdatedDate, value);
        //        }
        //    }

        //    public ObservableCollection<StructureModel> RootStructure
        //    {
        //        get { return _RootStructure; }
        //        set
        //        {
        //            SetProperty(ref _RootStructure, value);
        //        }
        //    }
    }


}
