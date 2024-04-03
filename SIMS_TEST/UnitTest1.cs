using SIMS_IT0602.Controllers;

namespace SIMS_TEST;

public class UnitTest1
{
    [Fact]
    public void Test_LoadTeacherSuccess()
    {
        //1. Arrange
        TeacherController sut = new TeacherController();
        //2. Act
        var result = sut.LoadTeacherFromFile("data.json");
        //3. Assert
        Assert.Equal(2, result?.Count);
    }
}
