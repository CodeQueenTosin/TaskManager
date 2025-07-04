namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        public string? Id { get; set; }  

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public DateTime StartDate { get; set; }

        public int AllottedTime { get; set; }  

        public int ElapsedTime { get; set; }   

        public bool TaskStatus { get; set; }  

        public DateTime EndDate => StartDate.AddDays(ElapsedTime);

        public DateTime DueDate => StartDate.AddDays(AllottedTime);

        public int DaysOverdue => !TaskStatus ? Math.Max(ElapsedTime - AllottedTime, 0) : 0;

        public int DaysLate => TaskStatus ? Math.Max(AllottedTime - ElapsedTime, 0) : 0;
    }


}
