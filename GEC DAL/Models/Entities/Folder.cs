using System;
using System.Collections.Generic;
using System.Text;

namespace GEC_DataLayer.Models.Entities
{
    public class Folder
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Folder()
        {

        }

        public Folder(long? pId, string pName, string pDescription)
        {
            if (string.IsNullOrWhiteSpace(pName))
            {
                throw new ArgumentException("Name is required");
            }
            Id = pId;
            Name = pName;
            Description = pDescription;
        }
    }
}
