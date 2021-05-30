using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class Identification
    {
        public int Id { get; internal set; }

        public Identification(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
