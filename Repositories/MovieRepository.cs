using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using TrainingDotNetCoreMVC.Models;

namespace TrainingDotNetCoreMVC.Repositories
{
    public class MovieRepository
    {
        string ConnStr;
        public MovieRepository()
        {
            ConnStr = "Data Source=database/myDB.db";
        }

        public List<MovieModel> getMovieList()
        {
            var conn = new SqliteConnection(ConnStr);
            string cmdText = "select * from movie";
            var cmd = new SqliteCommand(cmdText, conn);
            conn.Open();
            SqliteDataReader reader = cmd.ExecuteReader();
            List<MovieModel> items = new List<MovieModel>();
            if (reader.Read())
            {
                var item = new MovieModel()
                {
                    id = reader.GetInt16(0),
                    title = reader.GetString(1),
                    coverImg = reader.GetString(2),
                    //releaseDate = DateTime.ParseExact(reader.Get String(3), "yyyy-MMM-dd", CultureInfo.InvariantCulture),
                    genre = reader.GetString(4),
                    duration = reader.GetInt32(5),
                    //createDate = DateTime.ParseExact(reader.GetString(6), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    //modifyDate = DateTime.ParseExact(reader.GetString(7), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                };
                items.Add(item);
            }
            conn.Close();
            return items;
        }
    }
}
