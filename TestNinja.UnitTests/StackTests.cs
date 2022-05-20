using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class StackTests
    {
        [Test]
        public void Push_ArgIsNull_ThrowArgNullException()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }
        [Test]
        public void Push_ArgIsNotNull_AddTheObjectToTheStack()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            stack.Push("a");
            Assert.That( stack.Count, Is.EqualTo(1));
        }
    }
}
