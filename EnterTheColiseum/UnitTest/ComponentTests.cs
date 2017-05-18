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
            //Arrange
            GameObject gameObject = new GameObject(Vector2.Zero);
            SpriteRenderer component = new SpriteRenderer(gameObject, "CollisionTexture", 0.5f, 0.5f);

            //Act
            gameObject.AddComponent(component);

            //Assert
            Assert.AreSame(gameObject.GetComponent("SpriteRenderer"), component);
        }
    }
}
