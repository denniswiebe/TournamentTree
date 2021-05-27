using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    class TournamentTree
    {
        private IElimination _elimination;

        public TournamentTree()
        {

        }

        public TournamentTree(IElimination elimination)
        {
            _elimination = elimination;
        }

        public void SetElimination(IElimination elimination)
        {
            _elimination = elimination;
        }

        public void StartTreeGenerator()
        {
            _elimination.StartElimination();
        }
    }
}