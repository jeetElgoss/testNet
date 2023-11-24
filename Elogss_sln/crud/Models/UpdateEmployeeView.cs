namespace crud.Models
{
    public class UpdateEmployeeView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public string Department { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
