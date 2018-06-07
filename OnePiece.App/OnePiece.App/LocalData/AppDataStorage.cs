using Newtonsoft.Json;
using OnePiece.App.DataModels;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace OnePiece.App.LocalData
{
    public interface IAppDataStorage
    {
        AppData GetAppData();
        void SaveAppData(AppData appData);

        List<Anime> GetAnimes();
        void SaveAnime(Anime anime);

        List<Manga> GetMangas();
        void SaveManga(Manga manga);
    }

    public class AppDataStorage : IAppDataStorage
    {
        private static readonly object _locker = new object();
        private readonly SQLiteConnection _dbContext;

        public AppDataStorage()
        {
            var sqliteProvider = DependencyService.Get<ISQLiteProvider>();

            _dbContext = sqliteProvider.GetConnection("AppDb");
            _dbContext.CreateTable<AppData>();
            _dbContext.CreateTable<Anime>();
            _dbContext.CreateTable<Manga>();
        }

        public AppData GetAppData()
        {
            var appData = _dbContext.Table<AppData>().FirstOrDefault();
            if (appData == null)
            {
                appData = new AppData();
                _dbContext.Insert(appData);
            }

            return appData;
        }

        public void SaveAppData(AppData appData)
        {
            _dbContext.Update(appData);
        }

        public List<Anime> GetAnimes()
        {
            var animes = _dbContext.Table<Anime>().ToList();

            return animes;
        }

        public void SaveAnime(Anime anime)
        {
            var animes = _dbContext.Table<Anime>().ToList();

            if (!animes.Any(x => x.Id == anime.Id))
            {
                _dbContext.Insert(anime);
            }
            else
            {
                _dbContext.Update(anime);
            }
        }

        public List<Manga> GetMangas()
        {
            var mangas = _dbContext.Table<Manga>().ToList();

            return mangas;
        }

        public void SaveManga(Manga manga)
        {
            var mangas = _dbContext.Table<Manga>().ToList();

            if (!mangas.Any(x => x.Id == manga.Id))
            {
                _dbContext.Insert(manga);
            }
            else
            {
                _dbContext.Update(manga);
            }
        }
    }

    public class AppData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
