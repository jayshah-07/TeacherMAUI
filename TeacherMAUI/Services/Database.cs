﻿using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherMAUI.Models;

namespace TeacherMAUI.Services
{
    public class Database
    {

        readonly SQLiteAsyncConnection _database; //establishes database connection
        public Database(string dbPath)
        {
            bool dbExists = File.Exists(dbPath); //boolean that checks if there is a file that exists in dbPath
            _database = new SQLiteAsyncConnection(dbPath);// establishes new database connection to dbpath
             

            if (!dbExists) //checks if a database DOESNT exist
            {
                Task.Run(async () =>
                {

                    CreateTables(_database); //creates tables when db does not exist
                });

            }

            
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
            return _database.Table<Lesson>().ToListAsync();
        }

        public Task<int> SaveLessonAsync(Lesson lesson)  //establishing save for insertion of row in table lesson
        {
            return _database.InsertAsync(lesson);
        }
        public Task<List<Tmima>> GetTmimasAsync()  //establishing getlist for ui feedback for table tmima
        {
            return _database.Table<Tmima>().ToListAsync();
        }

        public Task<int> SaveTmimaAsync(Tmima tmima) //establishing save for insertion of row in table tmima
        {
            return _database.InsertAsync(tmima);
        }

        public Task<List<Efhmeria>> GetEfhmeriasAsync()  //establishing getlist for ui feedback for table efhmeria
        {
            return _database.Table<Efhmeria>().ToListAsync();
        }

        public Task<int> SaveEfhmeriaAsync(Efhmeria efhmeria) //establishing save for insertion of row in table efhmeria
        {
            return _database.InsertAsync(efhmeria);
        }

    }


}
