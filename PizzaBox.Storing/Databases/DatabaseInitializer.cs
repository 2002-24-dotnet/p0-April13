namespace PizzaBox.Storing.Databases
{
  public class DatabaseInitializer : IDatabaseInitializer<ApplicationDbContext>
  {
    public bool InitializeDatabase(ApplicationDbContext context)
    {
      //context.Database.Create();
      //context.Database.Delete();
      return context.Database.CanConnect();
    }

  }
}