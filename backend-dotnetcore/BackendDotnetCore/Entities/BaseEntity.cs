using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    public abstract class BaseEntity
    {
        public int id;
        public DateTime createdDate;
        public DateTime modifiedDate;
        public string createdBy;
        public string modifiedBy;

        public int getId() { return this.id; }
        public void setId(int id)
        {
            this.id = id;
        }
        public DateTime getCreatedDate() { return createdDate; }
        public void setCreatedDate(DateTime createdDate) { this.createdDate = createdDate; }
        public void setCreateBy(string createdBy) { this.createdBy = createdBy; }
        public string getCreatedBy(string createdBy) { return this.createdBy; }

        public BaseEntity()
        {

        }


    }
}
