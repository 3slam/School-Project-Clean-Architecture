using FluentAssertions;

namespace XUnitTest
{
    public class UnitTestExamples
    {
        [Fact]
        public void Test1()
        {
            var x = 5;
            x.Should().Be(5);
        }

        [Fact]
        public void Test2()
        {
            var x = "Eslam";
            x.Should().NotBeEmpty();
        }
    }
}