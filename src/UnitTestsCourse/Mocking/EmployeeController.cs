using Microsoft.EntityFrameworkCore;

namespace UnitTestsCourse.Mocking
{
    public class EmployeeController
    {
        private IEmployeeStorage _db;

        public EmployeeController()
        {
            _db = new EmployeeStorage();
        }

        public ActionResult DeleteEmployee(int id)
        {
            _db.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }

    public class RedirectResult : ActionResult { }
}