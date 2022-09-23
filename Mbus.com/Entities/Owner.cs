using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mbus.com.Entities
{
    public class Owner
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Bus> BusOwned { get; set; } = new List<Bus>();
    }
}
