using SQLite;
using TeacherMAUI.Models;

namespace TeacherMAUI.Services
{
    public class DatabaseService
    {
        private const string DB_NAME = "sqlite_db_1.db3";
        readonly SQLiteAsyncConnection _connection; //establishes database connection

        //public DatabaseService(string dbPath)
        //{
        //    bool dbExists = File.Exists(dbPath); //boolean that checks if there is a file that exists in dbPath
        //    //_database = new SQLiteAsyncConnection(dbPath);// establishes new database connection to dbpath
        //    _database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));// establishes new database connection to dbpath

        //    if (!dbExists) //checks if a database DOESNT exist
        //    {
        //        //creates tables when db does not exist
        //        Task.Run(async () => CreateTables(_database));
        //    }
        //}

        public DatabaseService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));// establishes new database connection to dbpath
            
            // Creating tables
            CreateTables(_connection);
        }

        private static async void CreateTables(SQLiteAsyncConnection dbConnection)
        {
            await dbConnection.CreateTableAsync<Lesson>();//creates table for Lesson

            await dbConnection.CreateTableAsync<Tmima>();//creates table for Tmima

            await dbConnection.CreateTableAsync<Efhmeria>();// creates table for Efhmeria

            await dbConnection.CreateTableAsync<Exei>(); //creates table for Exei
        }

        public Task<List<Lesson>> GetLessonsAsync() //establishing getlist for ui feedback for table lesson
        {
            return _connection.Table<Lesson>().ToListAsync();
        }

        public Task<int> SaveLessonAsync(Lesson lesson)  //establishing save for insertion of row in table lesson
        {
            return _connection.InsertAsync(lesson);
        }
        public Task<List<Tmima>> GetTmimasAsync()  //establishing getlist for ui feedback for table tmima
        {
            return _connection.Table<Tmima>().ToListAsync();
        }

        public Task<int> SaveTmimaAsync(Tmima tmima) //establishing save for insertion of row in table tmima
        {
            return _connection.InsertAsync(tmima);
        }

        public Task<List<Efhmeria>> GetEfhmeriasAsync()  //establishing getlist for ui feedback for table efhmeria
        {
            return _connection.Table<Efhmeria>().ToListAsync();
        }

        public Task<int> SaveEfhmeriaAsync(Efhmeria efhmeria) //establishing save for insertion of row in table efhmeria
        {
            return _connection.InsertAsync(efhmeria);
        }
        public Task<List<Exei>> GetExeisAsync()  //establishing getlist for ui feedback for table efhmeria
        {
            return _connection.Table<Exei>().ToListAsync();
        }

        public Task<int> SaveExeiAsync(Exei exei) //establishing save for insertion of row in table efhmeria
        {
            return _connection.InsertAsync(exei);
        }

    }


}
