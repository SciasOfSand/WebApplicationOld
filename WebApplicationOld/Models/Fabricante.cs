using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplicationOld.Models
{
    [Table("fabricante", Schema ="public")]
    public class Fabricante
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        [JsonIgnore]
        public List<Produto> produtos { get; set; }
    }
}