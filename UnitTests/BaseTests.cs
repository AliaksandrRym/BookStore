using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStore.UnitTests
{
    [TestClass]
    public class BaseTests
    {
        protected Fixture _fixture;

        public BaseTests() 
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }  
    }
}
