using WebToDoApp.Models;

namespace WebToDoApp
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.ToDoItems.Any())
            {
                context.ToDoItems.AddRange(
                    new ToDoItem { Id = Guid.NewGuid(), JobName = "Çimleri biç" },
                    new ToDoItem { Id = Guid.NewGuid(), JobName = "Ödevlerini yap" },
                    new ToDoItem { Id = Guid.NewGuid(), JobName = "Traş ol" },
                    new ToDoItem { Id = Guid.NewGuid(), JobName = "Web Programlama Öğren" },
                    new ToDoItem { Id = Guid.NewGuid(), JobName = "İş Bul" },
                    new ToDoItem { Id = Guid.NewGuid(), JobName = "Evlen" }
                );
                context.SaveChanges();
            }
        }
    }
}
