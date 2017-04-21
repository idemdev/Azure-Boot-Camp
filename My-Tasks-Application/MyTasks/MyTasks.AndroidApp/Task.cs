using System;

namespace MyTasks.AndroidApp
{
    /// <summary>
    /// Represents a simple Task item.
    /// </summary>
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
