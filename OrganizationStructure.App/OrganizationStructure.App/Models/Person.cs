using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Models
{
    public class Person : ViewModels.Base.BaseViewModel, IComparable<Person>, IModel
    {
        private int _Id;
        private string _FirstName;
        private string _LastName;
        private int? roleId;

        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }

        public int? RoleId
        {
            get { return roleId; }
            set
            {
                roleId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RoleName));
            }
        }

        public string RoleName
        {
            get
            {
                if (RoleId != null)
                {
                    return GlobalInstance.Instance.Model.Roles.Where(x => x.Id == RoleId).Select(x => x.Name).FirstOrDefault();
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public void UpdateModel()
        {
            OnPropertyChanged("");
        }

        public int CompareTo(Person other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}
