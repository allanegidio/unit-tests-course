using Moq;
using UnitTestsCourse.Mocking;
using Xunit;

namespace UnitTestsCourseTests.Mocking
{
    public class EmployeeControllerTests
    {
        [Fact]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDB()
        {
            var dbMock = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(dbMock.Object);

            controller.DeleteEmployee(1);

            dbMock.Verify(db => db.DeleteEmployee(1));
        }
    }
}