using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class Name
    {
        public string Title { get; internal set; }

        public Name(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
