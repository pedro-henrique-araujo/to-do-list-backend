namespace ToDoList.Dto
{
    public class Pagination<T>
    {
        public int Total { get; set; }

        public List<T> Items { get; set; }
    }
}
