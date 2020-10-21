using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    class GroupPhase
    {
        public List<Group> Groups { get; set; }

        public GroupPhase(List<Group> groups)
        {
            Groups = groups;
        }
    }
}
