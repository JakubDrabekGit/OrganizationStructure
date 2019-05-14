using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Models
{
    public class Role : ViewModels.Base.BaseViewModel, IComparable<Role>, IModel
    {
        private int _Id;
        private string _Name;
        private string _Description;

        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged();
            }
        }

        public void UpdateModel()
        {
            OnPropertyChanged("");
        }

        public override string ToString()
        {
            return $"{Name} {Description}";
        }

        public int CompareTo(Role other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}