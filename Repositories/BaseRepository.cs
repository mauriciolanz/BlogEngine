using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class BaseRepository
    {
        protected readonly string connectionString;

        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected void Update(object entity)
        {
            var commandText = GetUpdateCommandText();
            var parameters = GetUpdateParamaters(entity);
            ExecuteNonQuery(commandText, parameters);
        }

        protected void Insert(object entity)
        {
            var commandText = GetInsertCommandText();
            var parameters = GetInsertParamaters(entity);
            ExecuteNonQuery(commandText, parameters);
        }

        protected void ExecuteNonQuery(string commandText, IEnumerable<SQLiteParameter> parameters)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(commandText, connection);
                AddParametersToCommand(command, parameters);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public T GetSingle<T>(string commandText, IEnumerable<SQLiteParameter> parameters) where T : class
        {
            T result = null;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(commandText, connection);
                AddParametersToCommand(command, parameters);

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    result = (T)ConvertToEntity(dataReader);
                }

                connection.Close();
            }

            return result;
        }

        public IEnumerable<T> GetList<T>(string commandText, IEnumerable<SQLiteParameter> parameters) where T : class
        {
            var resultList = new List<T>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = new SQLiteCommand(commandText, connection);
                AddParametersToCommand(command, parameters);

                var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    T result = (T)ConvertToEntity(dataReader);
                    resultList.Add(result);
                }

                connection.Close();
                connection.Dispose();
            }

            return resultList;
        }

        private void AddParametersToCommand(SQLiteCommand command, IEnumerable<SQLiteParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);
            }
        }

        protected abstract string GetInsertCommandText();
        protected abstract string GetUpdateCommandText();
        protected abstract IEnumerable<SQLiteParameter> GetInsertParamaters(object entity);
        protected abstract IEnumerable<SQLiteParameter> GetUpdateParamaters(object entity);
        protected abstract object ConvertToEntity(SQLiteDataReader dataReader);
    }
}
