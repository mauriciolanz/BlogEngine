using Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private const string ColumnIdName = "ID";
        private const string ColumnTitleName = "Title";

        public CategoryRepository(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(Category category)
        {
            base.Insert(category);
        }

        protected override string GetInsertCommandText()
        {
            return "INSERT INTO Category(Title) VALUES (@title)";
        }

        protected override IEnumerable<SQLiteParameter> GetInsertParamaters(object entity)
        {
            var category = (Category)entity;
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("title", category.Title));

            return parameters;
        }

        public void Update(Category category)
        {
            base.Update(category);
        }

        protected override string GetUpdateCommandText()
        {
            return "UPDATE Category SET Title = @title WHERE ID = @id";
        }

        protected override IEnumerable<SQLiteParameter> GetUpdateParamaters(object entity)
        {
            var category = (Category)entity;

            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("id", category.Id));
            parameters.Add(new SQLiteParameter("title", category.Title));

            return parameters;
        }

        public Category GetById(int id)
        {
            var commandText = "SELECT * FROM Category WHERE ID = @id";
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("id", id));

            return base.GetSingle<Category>(commandText, parameters);
        }

        public Category GetByTitle(string title)
        {
            var commandText = "SELECT * FROM Category WHERE UPPER(Title) = @title";
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("title", title.ToUpper()));

            return base.GetSingle<Category>(commandText, parameters);
        }

        public IEnumerable<Category> GetAll()
        {
            var commandText = "SELECT * FROM Category";
            return base.GetList<Category>(commandText, null);
        }

        protected override object ConvertToEntity(SQLiteDataReader dataReader)
        {
            var category = new Category
            {
                Id = dataReader.GetInt32(dataReader.GetOrdinal(ColumnIdName)),
                Title = dataReader.GetString(dataReader.GetOrdinal(ColumnTitleName))
            };

            return category;
        }
    }
}
