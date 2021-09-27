using System;
using System.Collections.Generic;

namespace LudoRevamp
{
    class Fields
    {
        public List<Pawn> occupants = new List<Pawn>();
        public int field;
        public int xPos;
        public int yPos;
        public Fields(int field, int xPos, int yPos)
        {
            this.field = field;
            this.xPos = xPos;
            this.yPos = yPos;
        }


    }
}
