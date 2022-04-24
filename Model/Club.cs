﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Club
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("description")]
        [Required(ErrorMessage = "Required fields")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }
        [JsonProperty("category")]
        public Category Category { get; set; }
        public int? CategoryId { get; set; }

    }
}
