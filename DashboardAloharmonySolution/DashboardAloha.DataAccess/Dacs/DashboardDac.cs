using DasboardAloha.Entities.Configuration.ModelsConfiguration;
using DashboardAloha.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;
using DasboardAloha.Entities.Helpers;
using MongoDB.Bson.Serialization;
using DasboardAloha.Entities.Models;
using System.Xml.Linq;
using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Dacs
{
    public class DashboardDac : IDashboardDac
    {
        #region CONSTRUCTOR
        private readonly ConfigurationDashboardAPI ConfigurationDashboardAPI;
        private readonly ConnectionStringsModel ConnectionStringsModel;
        private readonly IConnectionDac ConnectionDac;
        public DashboardDac(IConfiguration Configuration,
            IConnectionDac ConnectionDac)
        {
            this.ConnectionDac = ConnectionDac;

            var settings = Configuration.GetSection("ConfigurationDashboardAPI");
            this.ConfigurationDashboardAPI = settings.Get<ConfigurationDashboardAPI>();

            settings = Configuration.GetSection("ConnectionStringsModel");
            this.ConnectionStringsModel = settings.Get<ConnectionStringsModel>();
        }
        #endregion

        #region METHODS    
        public Task<string> GetCountUsersRegistersXMembership()
        {
            try
            {
                var client = new MongoClient(this.ConnectionDac.CnMongo());
                var database = client.GetDatabase(this.ConfigurationDashboardAPI.NameBDDefault);

                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("user");

                var result = collection.Aggregate().Group(new BsonDocument
                {
                   { "_id", "$membership" },
                   { "count", new BsonDocument { { "$sum", 1 } } }
                }).ToList();

                var userProfileCounts = result
                .Select(doc => new UserProfileCount { Profile = ConvertValueHelper.ConvertStringValue(doc["_id"]), Count = doc["count"].AsInt32 })
                .ToList();

                return Task.FromResult(JsonConvert.SerializeObject(userProfileCounts));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
        public Task<string> GetCountUsersRegisters()
        {
            try
            {
                var client = new MongoClient(this.ConnectionDac.CnMongo());
                var database = client.GetDatabase(this.ConfigurationDashboardAPI.NameBDDefault);

                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("user");

                var result = collection.Aggregate().Group(new BsonDocument
                {
                   { "_id", "$profile" },
                   { "count", new BsonDocument { { "$sum", 1 } } }
                }).ToList();

                var userProfileCounts = result
                .Select(doc => new UserProfileCount { Profile = doc["_id"].AsString, Count = doc["count"].AsInt32 })
                .ToList();

                return Task.FromResult(JsonConvert.SerializeObject(userProfileCounts));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
        public Task<string> GetCountUsersActive()
        {
            try
            {
                List<UserModel> usersList = this.LoadUsersCollection().Result;

                List<HistoryTrackModel> historyTracksList = this.LoadHistoryTracksCollection().Result;

                List<HistoryViewModel> historyViewsList = this.LoadHistoryViewsCollection().Result;

                foreach (HistoryTrackModel historytrack in historyTracksList)
                {
                    ObjectId idTrack = new(historytrack.IdTrack.ToString());

                    historytrack.HistoryViews = historyViewsList.Where(ht => ht.IdTrack == idTrack &&
                    ht.CreatedAt.ToString("yyyy-MM-dd") == historytrack.CreatedAt.ToString("yyyy-MM-dd")).ToList();
                }

                foreach (UserModel user in usersList)
                {
                    ObjectId idUser = new(user.Id.ToString());

                    user.HistoryTracks = historyTracksList.Where(ht => ht.IdUser == idUser).ToList();
                }
              
                return Task.FromResult(JsonConvert.SerializeObject(usersList));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
        public Task<List<UserModel>> GetAllUsersRegisters()
        {
            try
            {
                List<UserModel> usersList = this.LoadUsersCollection().Result;

                List<HistoryTrackModel> historyTracksList = this.LoadHistoryTracksCollection().Result;

                List<HistoryViewModel> historyViewsList = this.LoadHistoryViewsCollection().Result;

                foreach (HistoryTrackModel historytrack in historyTracksList)
                {
                    ObjectId idTrack = new(historytrack.IdTrack.ToString());

                    historytrack.HistoryViews = historyViewsList.Where(ht => ht.IdTrack == idTrack &&
                    ht.CreatedAt.ToString("yyyy-MM-dd HH:mm") == historytrack.CreatedAt.ToString("yyyy-MM-dd HH:mm")).ToList();

                    if (historytrack.HistoryViews.Count > 0) 
                    {
                        historytrack.Total_time_elapsed = historytrack.HistoryViews.Sum(x => x.TimeElapsed);

                        var typesMusicUser = historytrack.HistoryViews.Select(x => x.MusicType).Select(xname =>
                        new TypeMusicUserModel { Id = historytrack.IdUser, Type = xname }).ToList();

                        historytrack.TypesMusicUserModel = typesMusicUser;
                    }
                }

                foreach (UserModel user in usersList)
                {
                    ObjectId idUser = new(user.Id.ToString());

                    user.HistoryTracks = historyTracksList.Where(ht => ht.IdUser == idUser).ToList();

                    user.TypesMusicUserModel = user.HistoryTracks
                     .SelectMany(ht => ht.TypesMusicUserModel)
                     .Distinct()
                     .ToList();

                    if (user.HistoryTracks.Count > 0)
                    {
                        user.Total_time_elapsed = user.HistoryTracks.Sum(x => x.Total_time_elapsed);                       
                    }                 
                }

                return Task.FromResult(usersList);
            }
            catch (Exception)
            {
                return Task.FromResult(new List<UserModel>());
            }
        }
        public Task<List<UserModel>> LoadUsersCollection()
        {
            try
            {
                var client = new MongoClient(this.ConnectionDac.CnMongo());
                var database = client.GetDatabase(this.ConfigurationDashboardAPI.NameBDDefault);

                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("user");

                var results = collection.Find(new BsonDocument()).ToList();

                List<UserModel> usersCollectionList = results.Select(bsonDocument =>
                    BsonSerializer.Deserialize<UserModel>(bsonDocument)).ToList();

                return Task.FromResult(usersCollectionList);
            }
            catch (Exception)
            {
                return Task.FromResult(new List<UserModel>());
            }
        }
        public Task<List<HistoryTrackModel>> LoadHistoryTracksCollection()
        {
            try
            {
                var client = new MongoClient(this.ConnectionDac.CnMongo());
                var database = client.GetDatabase(this.ConfigurationDashboardAPI.NameBDDefault);

                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("historytrack");

                var results = collection.Find(new BsonDocument()).ToList();

                List<HistoryTrackModel> historyTrackCollectionList = results.Select(bsonDocument =>
                    BsonSerializer.Deserialize<HistoryTrackModel>(bsonDocument)).ToList();

                return Task.FromResult(historyTrackCollectionList);
            }
            catch (Exception)
            {
                return Task.FromResult(new List<HistoryTrackModel>());
            }
        }
        public Task<List<HistoryViewModel>> LoadHistoryViewsCollection()
        {
            try
            {
                var client = new MongoClient(this.ConnectionDac.CnMongo());
                var database = client.GetDatabase(this.ConfigurationDashboardAPI.NameBDDefault);

                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("historyviews");

                var results = collection.Find(new BsonDocument()).ToList();

                List<HistoryViewModel> historyViewsCollectionList = results.Select(bsonDocument =>
                    BsonSerializer.Deserialize<HistoryViewModel>(bsonDocument)).ToList();

                return Task.FromResult(historyViewsCollectionList);
            }
            catch (Exception)
            {
                return Task.FromResult(new List<HistoryViewModel>());
            }
        }
        public Task<List<TypeMusicModel>> LoadTypesMusicCollection()
        {
            try
            {
                var client = new MongoClient(this.ConnectionDac.CnMongo());
                var database = client.GetDatabase(this.ConfigurationDashboardAPI.NameBDDefault);

                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("types");

                var results = collection.Find(new BsonDocument()).ToList();

                List<TypeMusicModel> typesCollectionList = results.Select(bsonDocument =>
                    BsonSerializer.Deserialize<TypeMusicModel>(bsonDocument)).ToList();

                return Task.FromResult(typesCollectionList);
            }
            catch (Exception)
            {
                return Task.FromResult(new List<TypeMusicModel>());
            }
        }
        public Task<List<HistoryPaymentModel>> LoadHistoryPaymentCollection()
        {
            try
            {
                var client = new MongoClient(this.ConnectionDac.CnMongo());
                var database = client.GetDatabase(this.ConfigurationDashboardAPI.NameBDDefault);

                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("historypayment");

                var results = collection.Find(new BsonDocument()).ToList();

                List<HistoryPaymentModel> historyCollectionList = results.Select(bsonDocument =>
                    BsonSerializer.Deserialize<HistoryPaymentModel>(bsonDocument)).ToList();

                return Task.FromResult(historyCollectionList);
            }
            catch (Exception)
            {
                return Task.FromResult(new List<HistoryPaymentModel>());
            }
        }
        public class UserProfileCount
        {
            public UserProfileCount()
            {
                this.Profile = string.Empty;
            }
            [BsonElement("_id")]
            public string Profile { get; set; }
            [BsonElement("count")]
            public int Count { get; set; }
        }
        #endregion
    }
}
