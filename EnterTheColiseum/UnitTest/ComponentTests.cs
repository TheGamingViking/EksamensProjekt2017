using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterTheColiseum;
using Microsoft.Xna.Framework;

namespace UnitTest
{
    [TestClass]
    public class ComponentTests
    {
        [TestMethod]
        public void AddComponentTest()
        {
            //Arrangement
            GameObject obj = new GameObject(Vector2.Zero);
            Collider component = new Collider(obj);

            //Act
            obj.AddComponent(component);

            //Assert
            Assert.AreSame(obj.GetComponent("Collider"), component);
        }
    }
}
