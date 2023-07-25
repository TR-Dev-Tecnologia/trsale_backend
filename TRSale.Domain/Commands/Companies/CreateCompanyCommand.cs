using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TRSale.Domain.Commands.Companies
{
    public class CreateCompanyCommand
    {
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public Guid UserId { get; set; }

    }
}