namespace PizzaBox.Storing.Databases
{
  public interface IDatabaseInitializer<in TContext> where TContext : ApplicationDbContext
  {
    bool InitializeDatabase(TContext context);
  }
}