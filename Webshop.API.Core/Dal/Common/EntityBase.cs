using System.ComponentModel.DataAnnotations;

namespace Webshop.API.Core.Dal.Common
{
    public class EntityBase
    {
        [Key]
        public int Identifier { get; set; }
    }
}
