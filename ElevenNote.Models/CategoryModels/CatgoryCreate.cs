using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.CategoryModels
{
    public class CatgoryCreate
    {
        [Required]
        public string Name { get; set; }        
        
        public DateTimeOffset CreatedUtc { get; set; }

    }
}
