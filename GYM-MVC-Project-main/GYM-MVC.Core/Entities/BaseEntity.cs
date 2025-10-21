using System.ComponentModel.DataAnnotations;

namespace GYM.Domain.Entities {

    public class BaseEntity {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EntryDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? UpdateDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}