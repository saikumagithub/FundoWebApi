using FundoRepositoryLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoRepositoryLayer.Context
{
    public  class FundoContext:DbContext
    {
        public FundoContext(DbContextOptions options):base(options)
        { 
        
        
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<NoteEntity> Notes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<CollaboratorEntity> Collaborators { get; set; }

    }
}
