using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Практическая_работа_4_Новиков_Соболенко;

namespace UnitTestProject
{
    /// <summary>
    /// Тренировочный тестовый класс для изучения методов объекта Assert.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            // Assert.AreEqual — проверяет равенство
            Assert.AreEqual(4, 2 + 2);

            // Assert.AreNotEqual — проверяет неравенство
            Assert.AreNotEqual(5, 2 + 2);

            // Assert.IsTrue — проверяет истинность условия
            Assert.IsTrue(10 > 5);

            // Assert.IsFalse — проверяет ложность условия
            Assert.IsFalse(3 > 10);

            // Assert.IsNull — проверяет, что объект равен null
            object obj = null;
            Assert.IsNull(obj);

            // Assert.IsNotNull — проверяет, что объект не равен null
            object obj2 = new object();
            Assert.IsNotNull(obj2);
        }
    }
}
