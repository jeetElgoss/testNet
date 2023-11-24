using crud.Data;
using crud.Models;
using crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        /// <summary>
        /// Pj- created Action..
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var employees =  await mvcDemoDbContext.Employees.Where(x => x.Status== "Active").ToListAsync();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeView addEmployeeRequest ) {

            var status = "Active";
            var employee = new Employee()
            {
                Id = new Guid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Department = addEmployeeRequest.Department,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Status = status,
                CreatedOn = DateTime.Now
        };   
            await mvcDemoDbContext.Employees.AddAsync( employee );
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
            

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync( x => x.Id == id );
            if ( employee != null )
            {
                var viewEmployee = new UpdateEmployeeView()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    CreatedOn = employee.CreatedOn
                };
                return View(viewEmployee);
            }
            return RedirectToAction("Index");            
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeView updateEmployeeView)
        {

            var employee = await mvcDemoDbContext.Employees.FindAsync(updateEmployeeView.Id);
            if (employee != null)
            {

                employee.Id = updateEmployeeView.Id;
                employee.Name = updateEmployeeView.Name;
                employee.Email = updateEmployeeView.Email;
                employee.Department = updateEmployeeView.Department;
                employee.Salary= updateEmployeeView.Salary;
                employee.DateOfBirth= updateEmployeeView.DateOfBirth;
                mvcDemoDbContext.Employees.Update(employee);
                mvcDemoDbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id ==id && x.Status=="Active");
            if (data != null) {
                
                data.Status = "Inactive";
                mvcDemoDbContext.Employees.Update(data);
                mvcDemoDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else { 
                return RedirectToAction("Index");
            }
        }
    }
}
