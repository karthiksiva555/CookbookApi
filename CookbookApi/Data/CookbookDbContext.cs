using CookbookApi.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CookbookApi.Data;

public sealed class CookbookDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    
    public DbSet<Ingredient> Ingredients { get; set; }
    
    public CookbookDbContext(DbContextOptions<CookbookDbContext> options): base(options)
    {
        Recipes = Set<Recipe>();
        Ingredients = Set<Ingredient>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Cookbook.db" };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);

        optionsBuilder.UseSqlite(connection);
    }
        

}