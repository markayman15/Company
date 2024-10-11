namespace Company.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public string Name { get; set; }
        public DateOnly Creation_Date { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}
