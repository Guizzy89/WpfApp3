using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Models;

namespace WpfApp3.Data
{
    public class ApplicationDbContext : DbContext           // Контекст базы данных
    {
        public ApplicationDbContext() { }                             // Пустой конструктор, обязателен для работы с SQLite в Wpf.app

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } // Конструктор с параметрами для передачи опций

        public DbSet<Products> Products { get; set; }                           // Таблица продуктов
        public DbSet<ProductTypes> ProductTypes { get; set; }                   // Таблица типов продуктов
        public DbSet<Workshops> Workshops { get; set; }                         // Таблица цехов
        public DbSet<ProductWorkshops> ProductWorkshops { get; set; }           // Таблица связей продуктов цехов
        public DbSet<MaterialTypes> MaterialTypes { get; set; }                 // Таблица типов материалов

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Настройка подключения к базе данных
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=WpfApp3.db"); // Подключение к базе данных SQLite
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MaterialTypes>(entity =>
            {
                entity.HasKey(e => e.Id);                           // Первчиный ключ
                entity.Property(e => e.Name).IsRequired();          // Название материала обязательно
                entity.Property(e => e.LosePercent).IsRequired();   // Процент потерь обязателен
            });

            modelBuilder.Entity<Workshops>(entity =>
            {
                entity.HasKey(e => e.Id);                           // Первчиный ключ
                entity.Property(e => e.Name).IsRequired();          // Название цеха обязательно
                entity.Property(e => e.WorkshopType).IsRequired();  // Тип цеха обязателен
                entity.Property(e => e.StuffCount).IsRequired();    // Количество сотрудников обязательно
            });

            modelBuilder.Entity<ProductWorkshops>(entity=>
            {
                entity.HasKey(e => e.Id);                           // Первчиный ключ
                entity.Property(e => e.Name).IsRequired();          // Название обязательно
                entity.Property(e => e.ManufacturingInHours).IsRequired(); // Время изготовления обязательно

                entity.HasOne(e => e.Workshop)                      // Связь с цехом
                      .WithMany(w => w.ProductWorkshops)            // Один цех может быть связан со многими ProductWorkshops, т.к. в WorkShops есть ICollection<ProductWorkshops>
                      .HasForeignKey(e => e.WorkshopId)             // Внешний ключ
                      .OnDelete(DeleteBehavior.Cascade);            // При удалении цеха удаляются связанные ProductWorkshops
            });

            modelBuilder.Entity<ProductTypes>(entity =>
            {
                entity.HasKey(e => e.Id);                           // Первчиный ключ     
                entity.Property(e => e.Name).IsRequired();          // Название типа продукта обязательно
                entity.Property(e => e.Coefficient).IsRequired();   // Коэффициент обязательно
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Article);                      // Первчиный ключ
                entity.Property(e => e.Name).IsRequired();          // Название продукта обязательно
                entity.Property(e => e.MinimalCost).IsRequired();   // Минимальная стоимость продукта обязательна

                entity.HasOne(e => e.ProductType)                   // Связь с типом продукта
                      .WithMany(pt => pt.Products)                  // Один тип продукта может быть связан со многими продуктами, т.к. в ProductTypes есть ICollection<Products>
                      .HasForeignKey(e => e.ProductTypeId)          // Внешний ключ
                      .OnDelete(DeleteBehavior.Cascade);            // При удалении типа продукта удаляются связанные продукты

                entity.HasOne(e => e.MaterialType)                  // Связь с типом материала
                        .WithMany(mt => mt.Products)                // Один тип материала может быть связан со многими продуктами, т.к. в MaterialTypes есть ICollection<Products>
                        .HasForeignKey(e => e.MaterialTypeId)       // Внешний ключ
                        .OnDelete(DeleteBehavior.Cascade);          // При удалении типа материала удаляются связанные продукты
            });
        }
    }
}
