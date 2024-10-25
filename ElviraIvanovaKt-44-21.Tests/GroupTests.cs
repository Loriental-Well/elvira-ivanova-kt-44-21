using ElviraIvanovaKt_44_21.Models;

namespace ElviraIvanovaKt_44_21.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_KT4421_True()
        {
            //arrange
            var testGroup = new Group
            {
                GroupName = "KT-31-20"
            };

            //act
            var result = testGroup.IsValidGroupName();

            //assert
            Assert.True(result);
        }
    }
}
