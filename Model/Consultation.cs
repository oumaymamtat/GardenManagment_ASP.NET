﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Consultation
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [JsonProperty("dateC")]
        public DateTime DateC { get; set; }
        [Required]
        [JsonProperty("description")]
        public String Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [JsonProperty("weight")]
        public double Weight { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [JsonProperty("height")]
        public double Height { get; set; }
        [JsonProperty("doctor")]
        public User Doctor { get; set; }
        [JsonProperty("folderMedical")]
        public FolderMedical FolderMedical { get; set; }
        public int? FolderMedicalId { get; set; }


    }
}
