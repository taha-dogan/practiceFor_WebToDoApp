using WebToDoApp.Models;

namespace WebToDoApp.Services
{
    public class ToDoItemService
    {
        private readonly AppDbContext _context;

        public ToDoItemService(AppDbContext context)
        {
            _context = context;
        }

        public List<ToDoItem> GetAllToDoItems()
        {
            return _context.ToDoItems.ToList();
        }

        public ToDoItem GetToDoItemById(Guid id)
        {
            return _context.ToDoItems.FirstOrDefault(p => p.Id == id);
        }

        public void AddToDoItem(ToDoItem addedItem)
        {
            _context.ToDoItems.Add(addedItem);
            _context.SaveChanges();
        }

        public void UpdateToDoItem(ToDoItem updatedItem)
        {
            var existingItem = _context.ToDoItems.FirstOrDefault(p => p.Id == updatedItem.Id);

            if (existingItem != null)
            {
                existingItem.JobName = updatedItem.JobName;
                _context.SaveChanges();
            }
        }

        public void DeleteToDoItem(Guid id)
        {
            var existingItem = _context.ToDoItems.FirstOrDefault(p => p.Id == id);

            if (existingItem != null)
            {
                _context.ToDoItems.Remove(existingItem);
                _context.SaveChanges();
            }
        }
    }
}
