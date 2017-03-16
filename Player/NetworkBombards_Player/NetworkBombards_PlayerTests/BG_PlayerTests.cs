using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkBombards_Player;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace NetworkBombards_Player.Tests
{
    [TestClass()]
    public class BG_PlayerTests
    {
        [TestMethod()]
        public void BG_PlayerTest()
        {
            string DefaultName = "noname";
            BG_Player player = new BG_Player();
            Assert.AreEqual(DefaultName, player.Name);
            Assert.AreEqual(false, player.IsPlaying);
        }

        [TestMethod()]
        public void BG_PlayerTest1()
        {
            string name = "player";
            BG_Player player = new BG_Player(name);
            Assert.AreEqual(name, player.Name);
            Assert.AreEqual(false, player.IsPlaying);
            Assert.IsNotNull(player.History);
        }

        [TestMethod()]
        public void shootTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ResetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConnectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddHistoryTest()
        {
            /*BG_Player player = new BG_Player();
            BG_Hit hit = new BG_Hit(new BG_Location(0,0), BG_Cannon);
            player.AddHistory(hit)*/
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveFromStringTest()
        {
            string input = "";
        }
    }
}
