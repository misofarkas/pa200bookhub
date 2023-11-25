using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Author
{
    public class AuthorCreateUpdateDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
