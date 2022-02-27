using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Driver : IParticipant
    {
        public string Name { get; set; }

        public int Points { get; set; } 

        public IEquipment Equipment { get; set; }

        public TeamColors TeamColor { get; set; }


        public Driver(string Name, int Points, IEquipment Equipment, TeamColors TeamColor)
        {
            this.Name = Name;
            this.Points = Points;
            this.Equipment = Equipment;
            this.TeamColor = TeamColor;
        }
    }
}
