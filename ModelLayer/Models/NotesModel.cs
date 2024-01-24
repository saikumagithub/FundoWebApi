using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public  class NotesModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Remainder { get; set; }


        public string Color { get; set; }


        public string Image { get; set; }

        public bool IsArchive { get; set; }

        public bool IsPin { get; set; }

        public bool IsTrash { get; set; }

    }
}
