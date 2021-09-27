using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoRevamp
{
    class Pawn
    {
        public string name { get; set; }
        public int field { get; set; }
        public int moveLength { get; set; }
        public int team { get; set; }

        public int xPos { get; set; }
        public int xDef { get; set; }
        
        public int yPos { get; set; }
        public int yDef { get; set; }

        public Pawn(string name, int xDef, int yDef, int team)
        {
            this.name = name;

            this.xDef = xDef;
            this.yDef = yDef;

            xPos = this.xDef;
            yPos = this.yDef;
            this.team = team;
        }

        //public bool hasWon()
        //{
        //    return (moveLength == 56);
        //}

        //Checks whether the piece is home
        public bool isHome()
        {
            return (xPos == xDef && yPos == yDef);
        }

        public bool hasWon;

        public bool isHomeStretch;
    }
}
