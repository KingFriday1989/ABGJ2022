using DoubleAgent.Data;
using DoubleAgent.Helpers;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DoubleAgent.Tests.Helpers
{
    [Category("Double Agent")]
    public sealed class AlgorithmTests : A.Tests.TestRunner
    {
        protected override Task RunTests()
        {
            TestWalkForward();
            TestWalkBackward();
            TestWalkRight();
            TestWalkLeft();

            return Task.CompletedTask;
        }

        private void TestWalkForward()
        {
            Assert.AreEqual(WalkingDirections.WalkForward, Algorithms.GetWalkingDirection(0, 1, 0, 1)); //North
            Assert.AreEqual(WalkingDirections.WalkForward, Algorithms.GetWalkingDirection(0, -1, 0, -1)); //South
            Assert.AreEqual(WalkingDirections.WalkForward, Algorithms.GetWalkingDirection(1, 0, 1, 0)); //East
            Assert.AreEqual(WalkingDirections.WalkForward, Algorithms.GetWalkingDirection(-1, 0, -1, 0)); //West
        }

        private void TestWalkBackward()
        {
            Assert.AreEqual(WalkingDirections.WalkBackward, Algorithms.GetWalkingDirection(0, 1, 0, -1)); //North
            Assert.AreEqual(WalkingDirections.WalkBackward, Algorithms.GetWalkingDirection(0, -1, 0, 1)); //South
            Assert.AreEqual(WalkingDirections.WalkBackward, Algorithms.GetWalkingDirection(1, 0, -1, 0)); //East
            Assert.AreEqual(WalkingDirections.WalkBackward, Algorithms.GetWalkingDirection(-1, 0, 1, 0)); //West
        }

        private void TestWalkRight()
        {
            Assert.AreEqual(WalkingDirections.WalkRight, Algorithms.GetWalkingDirection(0, 1, -1, 0)); //North
            Assert.AreEqual(WalkingDirections.WalkRight, Algorithms.GetWalkingDirection(0, -1, 1, 0)); //South
            Assert.AreEqual(WalkingDirections.WalkRight, Algorithms.GetWalkingDirection(1, 0, 0, 1)); //East
            Assert.AreEqual(WalkingDirections.WalkRight, Algorithms.GetWalkingDirection(-1, 0, 0, -1)); //West
        }

        private void TestWalkLeft()
        {
            Assert.AreEqual(WalkingDirections.WalkLeft, Algorithms.GetWalkingDirection(0, 1, 1, 0)); //North
            Assert.AreEqual(WalkingDirections.WalkLeft, Algorithms.GetWalkingDirection(0, -1, -1, 0)); //South
            Assert.AreEqual(WalkingDirections.WalkLeft, Algorithms.GetWalkingDirection(1, 0, 0, -1)); //East
            Assert.AreEqual(WalkingDirections.WalkLeft, Algorithms.GetWalkingDirection(-1, 0, 0, 1)); //West
        }
    }
}