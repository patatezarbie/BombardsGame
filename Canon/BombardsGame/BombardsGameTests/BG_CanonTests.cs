using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombardsGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace BombardsGame.Tests
{
    [TestClass()]
    public class BG_CanonTests
    {
        [TestMethod()]
        public void MoveTest()
        {
            BG_Cannon c1 = new BG_Cannon(5.0f, Color.Black, new BG_Location(0,0));
            int targetValueX = 50;
            int targetValueY = 100;
            c1.Move(new BG_Location(targetValueX, targetValueY));

            Assert.AreEqual(c1.Location.PosX, targetValueX);
            Assert.AreEqual(c1.Location.PosY, targetValueY);
            
        }

        [TestMethod()]
        public void ajustAngleTest()
        {
            BG_Cannon c1 = new BG_Cannon(0.0f, Color.Black, new BG_Location(0, 0));

            Assert.AreEqual(c1.Rotation, 0.0f);

            c1.AdjustAngle(5.0f);

            Assert.AreEqual(c1.Rotation, 5.0f);

            // 5 + 360 = 365
            c1.AdjustAngle(360.0f);

            Assert.AreEqual(c1.Rotation, 5.0f);

            c1.AdjustAngle(-10.0f);

            Assert.AreEqual(c1.Rotation, 355.0f);
        }        
    }
}
