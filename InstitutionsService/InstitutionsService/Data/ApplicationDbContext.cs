using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InstitutionsService.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Classifier> Classifiers { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionContact> InstitutionContacts { get; set; }
        public DbSet<InstitutionReplication> InstitutionReplications { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institution>()
                .HasMany(i => i.Replications)
                .WithOne(ir => ir.Institution)
                .HasForeignKey(ir => ir.InstitutionId);
           
            modelBuilder.Entity<Institution>()
              .HasOne(i => i.Address)
              .WithMany()
              .HasForeignKey(i => i.AddressId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
