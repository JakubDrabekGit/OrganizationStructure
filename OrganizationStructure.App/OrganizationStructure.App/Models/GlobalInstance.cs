using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Models
{
    public class GlobalInstance
    {
        private static GlobalInstance instance;

        public static GlobalInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalInstance();
                }
                return instance;
            }
        }

        public MainModel Model { get; set; } = new MainModel();

        public void LoadModel(MainModel model)
        {
            Model.Persons.Clear();
            Model.Roles.Clear();
            Model.StructureModel = null;

            foreach (var item in model.Persons)
            {
                Model.Persons.Add(item);
            }

            foreach (var item in model.Roles)
            {
                Model.Roles.Add(item);
            }

            Model.StructureModel = model.StructureModel;
        }
    }
}
