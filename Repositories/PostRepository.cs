using Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        private const string ColumnIdName = "ID";
        private const string ColumnTitleName = "Title";
        private const string ColumnPublicationDateName = "PublicationDate";
        private const string ColumnCategoryIdName = "CategoryID";
        private const string ColumnContentName = "Content";

        public PostRepository(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(Post post)
        {
            base.Insert(post);
        }

        protected override string GetInsertCommandText()
        {
            // PRAGMA foreign_keys = ON; for enabling SQLite FK support
            return "PRAGMA foreign_keys = ON; INSERT INTO Post(Title, CategoryID, PublicationDate, Content) VALUES (@title, @categoryId, @publicationDate, @content);";
        }

        protected override IEnumerable<SQLiteParameter> GetInsertParamaters(object entity)
        {
            var post = (Post)entity;
            var parameters = new List<SQLiteParameter>();

            parameters.Add(new SQLiteParameter("title", post.Title));
            parameters.Add(new SQLiteParameter("categoryId", post.CategoryId));
            parameters.Add(new SQLiteParameter("publicationDate", post.PublicationDate));
            parameters.Add(new SQLiteParameter("content", post.Content));

            return parameters;
        }

        public void Update(Post post)
        {
            base.Update(post);
        }

        protected override string GetUpdateCommandText()
        {
            // PRAGMA foreign_keys = ON; for enabling SQLite FK support
            return "PRAGMA foreign_keys = ON; UPDATE Post SET Title = @title, CategoryID = @categoryId, PublicationDate = @publicationDate, Content =  @content WHERE ID = @id";
        }

        protected override IEnumerable<SQLiteParameter> GetUpdateParamaters(object entity)
        {
            var post = (Post)entity;
            var parameters = new List<SQLiteParameter>();

            parameters.Add(new SQLiteParameter("id", post.Id));
            parameters.Add(new SQLiteParameter("title", post.Title));
            parameters.Add(new SQLiteParameter("categoryId", post.CategoryId));
            parameters.Add(new SQLiteParameter("publicationDate", post.PublicationDate));
            parameters.Add(new SQLiteParameter("content", post.Content));

            return parameters;
        }
        
        public Post GetById(int id)
        {
            var commandText = "SELECT * FROM Post WHERE ID = @id";
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("id", id));

            return base.GetSingle<Post>(commandText, parameters);
        }

        public Post GetByTitle(string title)
        {
            var commandText = "SELECT * FROM Post WHERE UPPER(Title) = @title";
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("title", title.ToUpper()));

            return base.GetSingle<Post>(commandText, parameters);
        }

        public IEnumerable<Post> GetAll()
        {
            var commandText = "SELECT * FROM Post";
            return base.GetList<Post>(commandText, null);
        }

        public IEnumerable<Post> GetByCategoryId(int categoryId)
        {
            var commandText = "SELECT * FROM Post WHERE CategoryID = @categoryId";
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("categoryId", categoryId));

            return base.GetList<Post>(commandText, parameters);
        }

        protected override object ConvertToEntity(SQLiteDataReader dataReader)
        {
            var post = new Post
            {
                Id = dataReader.GetInt32(dataReader.GetOrdinal(ColumnIdName)),
                Title = dataReader.GetString(dataReader.GetOrdinal(ColumnTitleName)),
                Content = dataReader.GetString(dataReader.GetOrdinal(ColumnContentName)),
                PublicationDate = dataReader.GetDateTime(dataReader.GetOrdinal(ColumnPublicationDateName)),
                CategoryId = dataReader.GetInt32(dataReader.GetOrdinal(ColumnCategoryIdName))
            };

            return post;
        }
    }
}
