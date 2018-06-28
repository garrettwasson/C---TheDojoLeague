using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Data;
using DojoLeague.Models;
using Dapper;

namespace DojoLeague.Factories
{
    public class NinjaFactory 
    {
        private MySqlOptions _mySqlOptions;
        public NinjaFactory(IOptions<MySqlOptions> options)
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
        // Create ninja
        public void CreateNinja(Ninja ninja)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO ninjas (DojoID, NinjaName, NinjaLevel, NinjaDescription, CreatedAt, UpdatedAt) VALUES (@DojoID, @NinjaName, @NinjaLevel, @NinjaDescription, NOW(), NOW());";
                dbConnection.Open();
                dbConnection.Execute(query, ninja);
            }
        }
        // Get all ninjas and associated dojos
        public IEnumerable<Ninja> GetAllNinjas()
        {
            using(IDbConnection dbConnection = Connection)
            {
                var query = "SELECT * FROM ninjas LEFT JOIN dojos ON ninjas.DojoID = dojos.ID;";
                dbConnection.Open();
                var allNinjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => {ninja.dojos = dojo; return ninja;});
                return allNinjas;
            }
        }
        // Get all dojos for ninja form 
        public List<Dojo> GetAllDojos()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM dojos;";
                dbConnection.Open();
                return dbConnection.Query<Dojo>(query).ToList();
            }
        }
        // Get ninja by ID 
        public IEnumerable<Ninja> GetNinja(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT * FROM ninjas LEFT JOIN dojos ON ninjas.DojoID = dojos.ID WHERE ninjas.ID = {id};";
                dbConnection.Open();
                var singleNinja = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => {ninja.dojos = dojo; return ninja;});
                return singleNinja;
            }
        }
    }
}