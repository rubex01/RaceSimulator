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

        public Track(string Name, LinkedList<Section> sections)
        {
            this.Name = Name;
            this.Sections = sections;
        }

        public Track(string Name, SectionTypes[] sections)
        {
            this.Name = Name;
            this.Sections = SectionTypesArrayToSectionLinkedList(sections);
        }

        public LinkedList<Section> SectionTypesArrayToSectionLinkedList(SectionTypes[] sectionTypes)
        {
            var newLinkedList = new LinkedList<Section>();
            foreach (SectionTypes type in sectionTypes)
            {
                newLinkedList.AddLast(new Section(type));
            }
            return newLinkedList;
        }
    }
}
