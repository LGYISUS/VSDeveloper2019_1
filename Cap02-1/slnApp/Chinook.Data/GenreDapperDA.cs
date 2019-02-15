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
    public class GenreDapperDA:BaseConnection
    {
        public int GetCount()
        {
            var result = 0;

            var sql = "SELECT COUNT(1) FROM Genre";
            /*1: Create una instancia sql DbC   onnection*/
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result= cn.Query<int>(sql).First();
                
            }

            return result;
        }

        public List<Genre> GetGenre()
        {
            var result = new List<Genre>();
            var sql = "SELECT GenreId,Name FROM Genre";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Query<Artist>(sql).ToList();

            }

            return result;
        }



        public List<Genre> GetGenreWithSP(string filterByName)
        {
            var result = new List<Genre>();
            var sql = "usp_GetGenre";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {

                result = cn.Query<Genre>(sql
                    , new { pNombre = filterByName }
                    , commandType:CommandType.StoredProcedure).ToList();

            }

            return result;
        }

        public List<Genre> GetGenre(string filterByName)
        {
            var result = new List<Genre>();
            var sql = "SELECT GenreId,Name FROM Genre WHERE Name like @name";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<Genre>(sql
                    , new { name = filterByName }).ToList();
                

            }

            return result;
        }



        public int InsertGenre(Genre entity)
        {
            var result = 0;
            using (IDbConnection cn 
                = new SqlConnection(GetConnection()))
            {
               result =  cn.Query<int>("usp_InsertGenre",
                    new { Name = entity.Name },
                    commandType: CommandType.StoredProcedure).Single();
                    


            }

            return result;

        }

        public int InsertGenreWithOutput(Genre entity)
        {
            var result = 0;
            using (IDbConnection cn
                = new SqlConnection(GetConnection()))
            {

                var param = new DynamicParameters();
                param.Add("Name", entity.Name);
                param.Add("ID", dbType: DbType.Int32, direction: ParameterDirection.Output);


                cn.Query("usp_InsertGenreWithOutput",
                    param, commandType: CommandType.StoredProcedure);
                     


                result = param.Get<int>("ID");

            }

            return result;

        }

        public int InsertGenreWithTX(Genre entity)
        {
            var result = 0;
            using (IDbConnection cn
                = new SqlConnection(GetConnection()))
            {

                cn.Open();
                var tx = cn.BeginTransaction();

                try
                {
                    result = cn.Query<int>("usp_InsertGenre",
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

        public int InsertGenreWithTXDist(Genre entity)
        {
            var result = 0;
            using (var tx = new TransactionScope())
            {
                try
                {
                    using (IDbConnection cn
                    = new SqlConnection(GetConnection()))
                    {
                        result = cn.Query<int>("usp_InsertGenre",
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

    }
}
