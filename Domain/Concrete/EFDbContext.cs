using Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(): base("name=EFDbContext")
        { }

       // public DbSet<Book> Books { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { set; get; }
        public DbSet<Review> Reviews { set; get; }
        public DbSet<ImagesWithResolutions> ImagesWithResolutions { set; get; }
        // public DbSet<Categories> Categories { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<ImagesWithResolutions>().
                HasMany(i=>i.Images).
                WithOptional().
                HasForeignKey(i=>i.ImagesWithResolutionsId).
                WillCascadeOnDelete(true);

            //modelBuilder.Entity<Image>().
            //    HasKey(i => new { i.ImageID, i.ImagesWithResolutionsId }).
            //    Property(i => i.ImageID).
            //    HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Product>().
                HasMany(p => p.ImagesWithResolutions).
                WithOptional().
                HasForeignKey(i => i.productId).
                WillCascadeOnDelete(true);

            //modelBuilder.Entity<ImagesWithResolutions>().
            //    HasKey(i => new { i.ImagesWithResolutionsID, i.productId }).
            //    Property(i => i.ImagesWithResolutionsID).
            //    HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
