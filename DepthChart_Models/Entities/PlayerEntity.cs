using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChart_Models.Entities
{
    public class PlayerEntity
    {
        public int PlayerId { get; set; }
        public int PlayerNumber { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string PlayerPosition { get; set; }
    }
}
