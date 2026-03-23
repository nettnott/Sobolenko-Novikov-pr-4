using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Практическая_работа_4_Новиков_Соболенко;

namespace UnitTestProject
{
    /// <summary>
    /// Модульные тесты для трёх математических функций класса <see cref="Funcs"/>.
    /// </summary>
    // Тесты для Func1:
    [TestClass]
    public class FuncsTests
    {
        /// <summary>
        /// Тест для <see cref="Funcs.Func1"/>.
        /// Проверяет корректность вычислений при различных входных данных.
        /// </summary>
        [TestMethod]
        public void TestFunc1()
        {

            double result1 = Funcs.Func1(1, 2, 0);
            Assert.AreEqual(0.0, result1, 1e-4, "Func1(1, 2, 0) должна вернуть 0");

 
            double result2 = Funcs.Func1(2, 1, 1);
            Assert.AreEqual(0.5, result2, 1e-4, "Func1(2, 1, 1) должна вернуть 0.5");


            double expected3 = -2.0 * (4.0 - Math.E / 2.0);
            double result3 = Funcs.Func1(4, Math.E, 0);
            Assert.AreEqual(expected3, result3, 1e-4, "Func1(4, e, 0) должна вернуть -2*(4−e/2)");

            double result4 = Funcs.Func1(0, 1, 0);
            Assert.AreEqual(0.0, result4, 1e-4, "Func1(0, 1, 0) должна вернуть 0");
        }

        // Тесты для Func2:

        /// <summary>
        /// Тест для <see cref="Funcs.Func2"/>.
        /// Проверяет каждую ветвь условной функции.
        /// </summary>
        [TestMethod]
        public void TestFunc2()
        {
            double result1 = Funcs.Func2(1, 3, 4);
            Assert.AreEqual(6.0, result1, 1e-4, "Func2(1, 3, 4): нечётный i, x>0 → должна вернуть 6");

            double result2 = Funcs.Func2(-1, 2, 9);
            Assert.AreEqual(3.0, result2, 1e-4, "Func2(-1, 2, 9): чётный i, x<0 → должна вернуть 3");

            double result3 = Funcs.Func2(1, 2, 9);
            Assert.AreEqual(Math.Sqrt(18), result3, 1e-4, "Func2(1, 2, 9): else-ветвь → должна вернуть √18");

            double result4 = Funcs.Func2(-1, 3, 4);
            Assert.AreEqual(Math.Sqrt(12), result4, 1e-4, "Func2(-1, 3, 4): else-ветвь → должна вернуть √12");
        }

        // Тесты для Func3:

        /// <summary>
        /// Тест для <see cref="Funcs.Func3"/>.
        /// Проверяет количество точек и корректность значений y.
        /// </summary>
        [TestMethod]
        [STAThread]
        public void TestFunc3()
        {
            double x0 = 0, xk = 2, dx = 1, b = 1;

            List<Point> points = Funcs.Func3(x0, xk, dx, b);

            Assert.AreEqual(3, points.Count, "Func3 должна вернуть 3 точки для x∈[0;2] с шагом 1");

            double expected0 = Math.Pow(0, 4) + Math.Cos(2 + Math.Pow(0, 3) - b);
            Assert.AreEqual(expected0, points[0].Y, 1e-4, "y при x=0 вычислена неверно");

            double expected1 = Math.Pow(1, 4) + Math.Cos(2 + Math.Pow(1, 3) - b);
            Assert.AreEqual(expected1, points[1].Y, 1e-4, "y при x=1 вычислена неверно");

            double expected2 = Math.Pow(2, 4) + Math.Cos(2 + Math.Pow(2, 3) - b);
            Assert.AreEqual(expected2, points[2].Y, 1e-4, "y при x=2 вычислена неверно");

            List<Point> emptyPoints = Funcs.Func3(5, 0, 1, 1);
            Assert.AreEqual(0, emptyPoints.Count, "Func3 должна вернуть пустой список при x0 > xk");
        }
    }
}
