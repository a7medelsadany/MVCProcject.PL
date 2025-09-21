namespace MVCProcject.PL.ViewModels.DepartmentsViewModels
{
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public string code { get; set; } = string.Empty!;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
    }
}
