namespace Inlämning_Crud
{
    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Born { get; set; } = default;
        public int Died { get; set; } = default;
        public int Mother { get; set; } = default;
        public int Father { get; set; } = default;
    }
}
