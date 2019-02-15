using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Chinook.Data
{
    public class GenreDA:BaseConnection
    {
        public List<Genre> GetGenreWithSP(string filterByName)
        {
            var result = new List<Genre>();
            var sql = "usp_GetGenre";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                /*2: Create ua instancia de Command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                //Agregando el parametro
                cmd.Parameters.Add(new SqlParameter("@pNombre", filterByName));

                cn.Open(); //Abriendo la conexion a la DB
                           /*3. ejecutando el comando*/

                var indice = 0;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    indice = reader.GetOrdinal("GenreId");
                    var genreId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    var name = reader.GetString(indice);

                    result.Add(
                            new Genre()
                            {
                                GenreId = genreId,
                                Name = name
                            }
                        );
                }
            }

            return result;
        }
        public int InsertGenre(Genre entity)
        {
            var result = 0;
            using (IDbConnection cn
                = new SqlConnection(GetConnection()))
            {
                cn.Open();
                IDbCommand command =
                    new SqlCommand("usp_InsertGenre");
                command.Connection = cn;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(
                    new SqlParameter("@Name", entity.Name)
                    );

                result = Convert.ToInt32(command.ExecuteScalar());

            }

            return result;

        }
        public int InsertGenreWithOutput(Genre entity)
        {
            var result = 0;
            using (IDbConnection cn
                = new SqlConnection(GetConnection()))
            {
                cn.Open();
                IDbCommand command =
                    new SqlCommand("usp_InsertGenreWithOutput");
                command.Connection = cn;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(
                    new SqlParameter("@Name", entity.Name)
                    );

                var paramOutID = new SqlParameter();
                paramOutID.ParameterName = "@ID";
                paramOutID.Direction = ParameterDirection.Output;
                paramOutID.DbType = DbType.Int32;
                command.Parameters.Add(paramOutID);

                command.ExecuteScalar();

                result = Convert.ToInt32(paramOutID.Value);

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

                //Iniciando la transacción local
                var transaction = cn.BeginTransaction();

                try
                {
                    IDbCommand command =
                    new SqlCommand("usp_InsertGenre");
                    command.Connection = cn;
                    command.Transaction = transaction; //Local transaction
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(
                        new SqlParameter("@Name", entity.Name)
                        );

                    result = Convert.ToInt32(command.ExecuteScalar());

                    //Simulando un error
                    //throw new Exception("Error al insertar");

                    //Confirmando la transaccion local
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); //Deshaciendo la transacción local
                    result = 0;
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
                        cn.Open();




                        IDbCommand command =
                        new SqlCommand("usp_InsertGenre");
                        command.Connection = cn;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(
                            new SqlParameter("@Name", entity.Name)
                            );

                        result = Convert.ToInt32(command.ExecuteScalar());



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
