using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplicationOld.Models
{
    [Table("produto", Schema = "public")]
    public class Produto
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }

        [ForeignKey("fabricante")]
        public int id_fabricante { get; set; }
        [JsonIgnore]
        public Fabricante fabricante { get; set; }
    }
}