//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class abd1Entities : DbContext
    {
        public abd1Entities()
            : base("name=abd1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        private static abd1Entities _context;
        public static abd1Entities GetContext()
        {
            if (_context == null)
                _context = new abd1Entities();
            return _context;
        }
        public virtual DbSet<PRAuto> PRAuto { get; set; }
        public virtual DbSet<PRClient> PRClient { get; set; }
        public virtual DbSet<PRProcat> PRProcat { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypeRole> TypeRole { get; set; }
        public virtual DbSet<user> user { get; set; }
    }
}
