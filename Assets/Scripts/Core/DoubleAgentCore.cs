using Helpers.Extensions;

namespace DoubleAgent
{
    public abstract class DoubleAgentCore : Core.Behaviour
    {
        public const string Namespace = "Double Agent";

        //-------------- Move to Behaviour
        public void UnParent() => transform.UnParent();
    }
}