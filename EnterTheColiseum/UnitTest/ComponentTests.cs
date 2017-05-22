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
        public void GetComponentTest()
        {
            //Arrange
            GameObject gameObject = new GameObject(Vector2.Zero);
            Component c;

            //Act
            c = gameObject.GetComponent("Transform");

            //Assert
            Assert.AreSame(c, gameObject.GetComponent("Transform"));
        }
        [TestMethod]
        public void AddMultipleComponentsAndGetOne()
        {
            //Arrange
            GameObject gameObject = new GameObject(Vector2.Zero);
            Transform c = new Transform(gameObject, Vector2.Zero);
            Transform b = new Transform(gameObject, Vector2.Zero);

            //Act
            gameObject.AddComponent(c);
            gameObject.AddComponent(b);

            //Assert
            Assert.AreNotSame(b, gameObject.GetComponent("Transform"));
            //Specific components of the same type cannot be differentiated. 
            //One component of each type per GameObject at most.
        }
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
