using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Models
{
    public class MainModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public OrganizationStructureModel StructureModel = new OrganizationStructureModel();
        public ObservableCollection<Role> Roles = new ObservableCollection<Role>();
        public ObservableCollection<Person> Persons = new ObservableCollection<Person>();

        public MainModel()
        {
            Roles.CollectionChanged += CollectionChanged;
            Persons.CollectionChanged += CollectionChanged;
            //DataModel model = JsonConvert.DeserializeObject<DataModel>(json);
            //this.Structures = model.Structures;
            //this.Roles = model.Roles;
            //this.Persons = model.Persons;


            //this.Persons = new ObservableCollection<Person>()
            //{
            //    new Person()
            //    {
            //        Id = 1,
            //        FirstName = "Jan",
            //        LastName = "Petrik",
            //         RoleId = 1
            //    },
            //    new Person()
            //    {
            //        Id = 2,
            //        FirstName = "Pepa",
            //        LastName = "Zdepa",
            //         RoleId = 2,

            //    },
            //    new Person()
            //    {
            //        Id = 3,
            //        FirstName = "Viktor",
            //        LastName = "Hugo",
            //        RoleId = 1
            //    }
            //};
            //this.Roles = new ObservableCollection<Role>()
            //{
            //    new Role()
            //    {
            //        Id =  1,
            //        Name = "Director",
            //        Description = "Director is..."
            //    },

            //    new Role()
            //    {
            //        Id =  2,
            //        Name = "Manager",
            //        Description = "Manager is..."
            //    }
            //};
            //this.StructureModel = new OrganizationStructureModel()
            //{
            //    Id = Guid.NewGuid(),
            //    CompanyName = "Artin company",
            //    RootStructure = new StructureModel()
            //    {
            //        Id = 1,
            //        Name = "Structure model",
            //        PersonId = 1,
            //        SubStructures = new ObservableCollection<StructureModel>()
            //        {
            //            new StructureModel()
            //            {
            //                 Id = 1,
            //                Name = "Structure model2",
            //                PersonId = 1,
            //            }
            //        }
            //    }
            //};

        }

        public void CreateOrganizationStructure(string companyName)
        {
            this.Persons.Clear();
            this.Roles.Clear();
            //StructureModel = new OrganizationStructureModel()
            //{
            //    Id = Guid.NewGuid(),
            //    CompanyName = companyName,
            //};

            this.StructureModel = new OrganizationStructureModel()
            {
                Id = Guid.NewGuid(),
                CompanyName = companyName,
                CreatedDate = DateTime.Now,
                //RootStructure = new ObservableCollection<StructureModel>()
                //    {
                //        new StructureModel()
                //        {
                //            Id = 2,
                //            Name = "Structure model",
                //            PersonId = 1,
                //            SubStructures = new ObservableCollection<StructureModel>()
                //            {
                //                new StructureModel()
                //                {
                //                     Id = 3,
                //                    Name = "Structure model2",
                //                    PersonId = 2,
                //                }
                //            }
                //        }
                //    }

            };
        }



        public int GetNewPersonId()
        {
            int maxId;
            if (this.Persons.Count > 0)
            {
                maxId = this.Persons.Max().Id++;
            }
            else
            {
                maxId = 1;
            }

            return maxId;
        }

        public int GetNewRoleId()
        {
            int maxId;
            if (this.Roles.Count > 0)
            {
                maxId = this.Roles.Max().Id++;
            }
            else
            {
                maxId = 1;
            }

            return maxId;
        }

        public void DeleteStructure(StructureModel structureModel)
        {
            if (StructureModel.RootStructure.Contains(structureModel) /*|| structureModel.IsRoot*/)
            {
                StructureModel.RootStructure.Remove(structureModel);
            }

            List<StructureModel> structures = GetAllStructures();

            var parent = structures.FirstOrDefault(s => s.SubStructures.Contains(structureModel));
            if (parent != null)
            {
                parent.SubStructures.Remove(structureModel);
            }
        }

        internal StructureModel GetStructureModel(int structureId)
        {
            var result = FindStructure(structureId, this.StructureModel.RootStructure);

            return result;
        }

        private StructureModel FindStructure(int structureId, ObservableCollection<StructureModel> subStructures)
        {
            StructureModel result = null;
            foreach (var item in subStructures)
            {
                if (item.Id == structureId)
                {
                    return item;
                }
                if (item.SubStructures.Count > 0)
                {
                    result = FindStructure(structureId, item.SubStructures);
                    if (result != null)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public int GetNewStructureId()
        {
            int newId, maxId;
            var completeStructure = GetAllStructures();

            if (completeStructure?.Count > 0)
            {
                maxId = completeStructure.Select(x => x.Id).Max();
                newId = ++maxId;
            }
            else
            {
                newId = 1;
            }
            return newId;

            //int newId, maxId;
            //if (this.StructureModel.RootStructure.Count > 0)
            //{
            //    maxId = this.StructureModel.RootStructure.Select(x => x.Id).Max();
            //    newId = maxId++;
            //}
            //else
            //{
            //    newId = 1;
            //}
            //return newId;
        }

        public List<StructureModel> GetAllStructures()
        {
            if (this.StructureModel.RootStructure != null)
            {
                List<StructureModel> completeStructure = GetSubStructure(this.StructureModel.RootStructure);

                return completeStructure;
            }
            else
            {
                return null;
            }
        }

        List<StructureModel> GetSubStructure(ObservableCollection<StructureModel> subStructures)
        {
            List<StructureModel> allStructures = new List<StructureModel>();
            foreach (var subStructure in subStructures)
            {
                allStructures.Add(subStructure);
                if (subStructure.SubStructures.Count > 0)
                {
                    allStructures.AddRange(GetSubStructure(subStructure.SubStructures));
                }
            }
            return allStructures;
        }

        public void AddPerson(Person person)
        {
            Persons.Add(person);
        }

        public void RemovePerson(int personId)
        {
            var person = Persons.Select(x => x).Where(x => x.Id == personId).FirstOrDefault();
            if (person != null)
            {
                Persons.Remove(person);
            }
        }

        public void AddRole(Role role)
        {
            Roles.Add(role);
        }

        public void RemoveRoles(int roleId)
        {
            var role = Roles.Select(x => x).Where(x => x.Id == roleId).FirstOrDefault();
            if (role != null)
            {
                Roles.Remove(role);
            }
        }

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, null);
        }
    }
}
