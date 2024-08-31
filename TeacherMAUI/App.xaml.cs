using TeacherMAUI.Services;

namespace TeacherMAUI
{
    public partial class App : Application
    {
        public static DatabaseHelper Database { get; private set; }
        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "items.db3");
            Database = new DatabaseHelper(dbPath);


            Routing.RegisterRoute(nameof(ExeiEditPage), typeof(ExeiEditPage));

            MainPage = new AppShell();
        }

        //static DatabaseHelper database;

        //public static DatabaseHelper Database //gets the database when the app launches
        //{
        //    get
        //    {
        //        if (database == null) //checks if the database is null
        //        {
        //            database = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3"));
        //            //initialises database in local folder if it doesn't exist already
        //        }
        //        return database;
        //    }
        //}
    }
}
