
namespace ToDoManager.Domain.Common
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        protected Entity() { }

        protected Entity(int id)
        {
            Id = id;
        }
    }
}
