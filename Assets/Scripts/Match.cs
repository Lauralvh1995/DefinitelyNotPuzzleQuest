using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Match
    {

        HashSet<Cell> match;

        //add origin to set
        //check up, left, down and right from origin
        //if same gem type, add to set
        //repeat for new elements in set
        //if no new elements are added, check length
        //if set.size > 2, return true

        void CheckNeighbours(Cell c)
        {
            Cell foundcell;

            //match.Add(foundcell);
        }
    }
}
