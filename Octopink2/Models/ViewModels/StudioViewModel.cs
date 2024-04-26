using System.Collections.Generic;

namespace Octopink2.Models.ViewModels
{
    public class StudioViewModel
    {
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}