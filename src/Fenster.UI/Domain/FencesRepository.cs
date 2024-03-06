using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenster.UI.Domain
{
    public class FencesRepository
    {
        List<Fence> _fences = new List<Fence> 
        { 
            new Fence() 
            { 
                Name = "Documents",
                Height = 200,
                Width = 200,
                Top = 20,
                Left = 20,
            },
            new Fence() 
            {
                Name = "Documents",
                Height = 200,
                Width = 200,
                Top = 1020,
                Left = 1020,
            },
        };

        public IEnumerable<Fence> Fences => _fences;
    }
}
