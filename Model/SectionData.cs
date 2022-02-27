using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class SectionData
    {
        public IParticipant Left { get; set; }

        public IParticipant Right { get; set; }

        public int DistanceLeft { get; set; }

        public int DistanceRight { get; set; }

        public SectionData(IParticipant Left, IParticipant Right, int DistanceLeft, int DistanceRight)
        {
            this.DistanceLeft = DistanceLeft;
            this.DistanceRight = DistanceRight;
            this.Left = Left;
            this.Right = Right;
        }

    }
}
