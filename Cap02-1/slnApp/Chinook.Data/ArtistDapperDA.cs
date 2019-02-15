using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;

namespace Chinook.Data
{
    public class ArtistDapperDA:BaseConnection
    {
        public int GetCount()
        {
            var result = 0;

            var sql = "SELECT COUNT(1) FROM Artist";
            /*1: Create una instancia sql DbC   onnection*/
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result= cn.Query<int>(sql).First();
                
            }

            return result;
        }

        public List<Artist> GetArtists()
        {
            var result = new List<Artist>();
            var sql = "SELECT ArtistId,Name FROM Artist";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Query<Artist>(sql).ToList();

            }

            return result;
        }



        public List<Artist> GetArtistsWithSP(string filterByName)
        {
            var result = new List<Artist>();
            var sql = "usp_GetArtist";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {

                result = cn.Query<Artist>(sql
                    , new { pNombre = filterByName }
                    , commandType:CommandType.StoredProcedure).ToList();

            }

            return result;
        }

        public List<Artist> GetArtists(string filterByName)
        {
            var result = new List<Artist>();
            var sql = "SELECT ArtistId,Name FROM Artist WHERE Name like @name";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<Artist>(sql
                    , new { name = filterByName }).ToList();
                

            }

            return result;
        }



        public int InsertArtits(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn 
                = new SqlConnection(GetConnection()))
            {
               result =  cn.Query<int>("usp_InsertArtist",
                    new { Name = entity.Name },
                    commandType: CommandType.StoredProcedure).Single();
                    


            }

            return result;

        }

        public int InsertArtistWithOutput(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn
                = new SqlConnection(GetConnection()))
            {

                var param = new DynamicParameters();
                param.Add("Name", entity.Name);
                param.Add("ID", dbType: DbType.Int32, direction: ParameterDirection.Output);


                cn.Query("usp_InsertArtistWithOutput",
                    param, commandType: CommandType.StoredProcedure);
                     


                result = param.Get<int>("ID");

            }

            return result;

        }

        public int InsertArtitsWithTX(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn
                = new SqlConnection(GetConnection()))
            {

                cn.Open();
                var tx = cn.BeginTransaction();

                try
                {
                    result = cn.Query<int>("usp_InsertArtist",
                     new { Name = entity.Name },
                     commandType: CommandType.StoredProcedure,
                     transaction: tx
                     ).Single();
                    tx.Commit(); //confirmando la tx

                }
                catch(Exception ex) {
                    tx.Rollback(); //deshaciendo la tx
                }



            }

            return result;

        }

        public int InsertArtitsWithTXDist(Artist entity)
        {
            var result = 0;
            using (var tx = new TransactionScope())
            {
                try
                {
                    using (IDbConnection cn
                    = new SqlConnection(GetConnection()))
                    {
                        result = cn.Query<int>("usp_InsertArtist",
                             new { Name = entity.Name },
                             commandType: CommandType.StoredProcedure).Single();



                    }
                    tx.Complete();

                }
                catch (Exception ex)
                {
                    
                }
            }









            return result;

        }

        public bool UpdateArtits(Artist entity)
        {
            var result = false;
            using (IDbConnection cn
                = new SqlConnection(GetConnection()))
            {
                cn.Query("usp_UpdateArtist",
                     new { Name = entity.Name },
                     commandType: CommandType.StoredProcedure);

                result = true;

            }

            return result;

        }

    }
}
