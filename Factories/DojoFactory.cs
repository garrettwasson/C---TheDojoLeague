using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Data;
using DojoLeague.Models;
using Dapper;

namespace DojoLeague.Factories
{
    public class DojoFactory
    {
        private MySqlOptions _mySqlOptions;
        public DojoFactory(IOptions<MySqlOptions> options)
        {
            _mySqlOptions = options.Value;
        }
        internal IDbConnection Connection 
        {
            get
            {
                return new MySqlConnection(_mySqlOptions.ConnectionString);
            }
        }
        // create dojo
        public void CreateDojo(Dojo dojo)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO dojos (DojoName, DojoLocation, DojoDescription, CreatedAt, UpdatedAt) VALUES(@DojoName, @DojoLocation, @DojoDescription, NOW(), NOW());";
                dbConnection.Open();
                dbConnection.Execute(query, dojo);
            }
        }
        // get all dojos 
        public List<Dojo> GetAllDojos()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM dojos;";
                dbConnection.Open();
                return dbConnection.Query<Dojo>(query).ToList();
            }
        }
        // get single dojo with all current members 
        public Dojo GetDojo(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var query = $"SELECT * FROM dojos WHERE ID = {id}; SELECT * FROM ninjas WHERE DojoID = {id};";
                using (var multi = dbConnection.QueryMultiple(query, id))
                {
                    var dojo = multi.Read<Dojo>().Single();
                    dojo.ninjas = multi.Read<Ninja>().ToList();
                    return dojo;
                }
            }
        }
        // get all rogue ninjas
        public List<Ninja> GetRogueNinjas()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = $"SELECT * FROM ninjas WHERE DojoID IS NULL;";
                dbConnection.Open();
                return dbConnection.Query<Ninja>(query).ToList();
            }
        }
        // banish ninja
        public void BanishNinja(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = $"UPDATE ninjas SET DojoID = NULL WHERE ID = {id};";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
        // recruit ninja
        public void RecruitNinja(int dojoID, int ninjaID)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = $"UPDATE ninjas SET DojoID = {dojoID} WHERE ID = {ninjaID};";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
    }
}