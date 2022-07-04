using Microsoft.EntityFrameworkCore;
using TesteBancoMaster.Infra.Entities;

namespace TesteBancoMaster.Infra.Data
{
    public class ViagensContext : DbContext, IUnitOfWork
    {
        public ViagensContext() { }

        public ViagensContext(DbContextOptions<ViagensContext> options) : base(options) { }

        public DbSet<Viagem> Viagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Todas as propriedades string sem valor maximo de caracteres vão ser colocadas em varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            #region Seeds
            modelBuilder.Entity<Viagem>().HasData(new Viagem
            {
                Id = 1,
                Origem = "GRU",
                Destino = "BRC",
                ValorPassagem = 10
            });

            modelBuilder.Entity<Viagem>().HasData(new Viagem
            {
                Id = 2,
                Origem = "BRC",
                Destino = "SCL",
                ValorPassagem = 5
            });

            modelBuilder.Entity<Viagem>().HasData(new Viagem
            {
                Id = 3,
                Origem = "GRU",
                Destino = "CDG",
                ValorPassagem = 75
            });

            modelBuilder.Entity<Viagem>().HasData(new Viagem
            {
                Id = 4,
                Origem = "GRU",
                Destino = "SCL",
                ValorPassagem = 20
            });

            modelBuilder.Entity<Viagem>().HasData(new Viagem
            {
                Id = 5,
                Origem = "GRU",
                Destino = "ORL",
                ValorPassagem = 56
            });

            modelBuilder.Entity<Viagem>().HasData(new Viagem
            {
                Id = 6,
                Origem = "ORL",
                Destino = "CDG",
                ValorPassagem = 5
            });

            modelBuilder.Entity<Viagem>().HasData(new Viagem
            {
                Id = 7,
                Origem = "SCL",
                Destino = "ORL",
                ValorPassagem = 20
            });
            #endregion
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
