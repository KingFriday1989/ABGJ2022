using Helpers.Extensions;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.TestTools;

namespace A.Tests
{
    /// <summary>
    /// Base class used for Simple Pass Test Runners
    /// </summary>
    public abstract class TestRunner : RuntimeTestBase
    {
        /// <summary>
        /// Run the runtime tests.
        /// </summary>
        /// <returns>Completed task of running tasks.</returns>
        [UnityTest]
        public IEnumerator RunSimplePassTests()
        {
            var task = RunTests();
            yield return task.AsIEnumerator();
        }

        /// <summary>
        /// Run test on simple passes
        /// </summary>
        /// <returns>Completed task after tests have finished</returns>
        protected abstract Task RunTests();
    }

    /// <summary>
    /// Base class used for all Runtime Test Runners.
    /// </summary>
    /// <typeparam name="B">Type of Behaviour to test</typeparam>
    public abstract class TestRunner<B> : RuntimeTestBase
    where B : Core.Behaviour
    {
        private B TestBehaviour;

        /// <summary>
        /// Run the runtime tests.
        /// </summary>
        /// <returns>Completed task of running tasks.</returns>
        [UnityTest]
        public IEnumerator RunTests()
        {
            TestBehaviour = CreateGameObject<B>("@Test Runner");
            //var task = RunTests(TestBehaviour);
            yield return RunTests(TestBehaviour).AsIEnumerator(); //task.AsIEnumerator();
            if (TestBehaviour)
                DestroyImmediate(TestBehaviour);
        }

        ///// <summary>
        ///// Create the test runner and call the RunTests function.
        ///// </summary>
        //protected async void Intiialize()
        //{
        //    TestBehaviour = CreateGameObject<B>("@Test Runner");
        //    await RunTests(TestBehaviour);
        //    if(TestBehaviour)
        //        DestroyImmediate(TestBehaviour);
        //}

        /// <summary>
        /// Run test on the behaviour
        /// </summary>
        /// <param name="behaviour">Type of Behaviour to test</param>
        /// <returns>Completed task after tests have finished</returns>
        protected abstract Task RunTests(B behaviour);
    }
}