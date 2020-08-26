

using System;
using System.ComponentModel.DataAnnotations;

namespace MT.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
           Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        private DateTime? _createAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public DateTime? CreateAt
        {          
            get { return _createAt; }
            set { _createAt = (value == null ? DateTime.UtcNow : value); }
        }

               
    }
}
