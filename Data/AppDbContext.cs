using Microsoft.EntityFrameworkCore;
using SistemaCliente.Models;

namespace SistemaCliente.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // Tabela Clientes
    public DbSet<Cliente> Clientes { get; set; }
    // Tabela Contatos
    public DbSet<Contato> Contatos { get; set; }
    // Tabela Enderecos
    public DbSet<Endereco> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Endereco>()
            .HasKey(e => e.ClienteId);
        
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nome).IsRequired();

        // Relação entre Cliente e Endereco no db
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Endereco) //Apenas um cliente para um endereço
            .WithOne(e => e.Cliente) //Apenas um endereço para um Cliente
            .HasForeignKey<Endereco>(e => e.ClienteId) // Chave estrangeira
            .OnDelete(DeleteBehavior.Cascade);

        // Relação entre Cliente e Contatos no db
        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Contatos) //Muitos contatos para um cliente
            .WithOne(ct => ct.Cliente) // Contato só pode ter 1 cliente
            .HasForeignKey(ct => ct.ClienteId) // Chave estrangeira
            .OnDelete(DeleteBehavior.Cascade);
    }
}
