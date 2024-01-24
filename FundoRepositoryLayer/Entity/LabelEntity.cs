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
    public  class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }

        public string LabelName { get; set; }

        //foreign key the label is belongs to noteid
        [ForeignKey("Note")]
        public long NoteId { get; set; }
        [JsonIgnore]

        //navigational property holds complete details of note
        public virtual NoteEntity Note { get; set; }



        [ForeignKey("User")]
        //foreign key of userid the notes belong to
        public long UsertId { get; set; }
        [JsonIgnore]

        //navigational property holds complete details of user
        public virtual UserEntity User { get; set; }
    }
}
