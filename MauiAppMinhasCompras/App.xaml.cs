using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public static SQLiteDatabaseHelper Database { get; private set; } = null!;

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "minhascompras.db3"
            );

            Database = new SQLiteDatabaseHelper(dbPath);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}