using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FundoRepositoryLayer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Remainder { get; set; }

        public string Color { get; set; }

        public string Image {  get; set; }

        public bool IsArchive { get; set; }

        public bool IsPin { get; set; }

        public bool IsTrash { get; set; }

        [ForeignKey("User")]
        //foreign key of userid the notes belong to
        public long UsertId { get; set; }
        [JsonIgnore]

        //navigational property holds complete details of user
        public virtual UserEntity User { get; set; }




    }
}
