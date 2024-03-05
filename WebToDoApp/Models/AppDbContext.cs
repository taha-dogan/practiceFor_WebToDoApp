﻿using Microsoft.EntityFrameworkCore;

namespace WebToDoApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
