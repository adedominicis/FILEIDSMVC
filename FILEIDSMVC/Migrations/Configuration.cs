namespace FILEIDSMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FILEIDSMVC.Models.ApplicationDbContext>
    {
        /// <summary>
        /// Configuración de base de datos de usuario desde aqui.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FILEIDSMVC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
