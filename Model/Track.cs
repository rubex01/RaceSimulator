using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }

        public LinkedList<Section> Sections { get; set; }

        public Track(string Name, LinkedList<Section> Sections)
        {
            this.Name = Name;
            this.Sections = Sections;
        }
    }
}
