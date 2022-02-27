﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Car : IEquipment
    {
        public int Quality { get; set; }

        public int Performance { get; set; }

        public int Speed { get; set; }

        public bool IsBroken { get; set; }

        public Car(int Quality, int Performance, int Speed, bool IsBroken)
        {
            this.Quality = Quality; 
            this.Performance = Performance;
            this.IsBroken = IsBroken;
            this.Speed = Speed;
        }
    }
}
