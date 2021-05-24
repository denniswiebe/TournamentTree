using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class TournamentLogTest
    {
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void GetLetterByNumberTest()
        {
            // Prüfen, ob bei 2 auch ein B kommt
            Assert.AreEqual(CellFinder.GetLetterByNumber(2), "B");

            // Methode mit falscher Zahl aufrufen, sodass dort die Exception geworfen wird
            // Der Test darf nicht schiefgehen
            CellFinder.GetLetterByNumber(1000);
        }
    }
}
