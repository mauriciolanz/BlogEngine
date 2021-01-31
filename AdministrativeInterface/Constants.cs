using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativeInterface
{
    public class Constants
    {
        public const string CategorySaveErrorMessage = "One error occured when trying to save the category. Exception: {0}";
        public const string CategoryDuplicatedTitleErrorMessage = "A category called \"{0}\" already exists.";
        public const string PostSaveErrorMessage = "One error occured when trying to save the post. Exception: {0}";
        public const string PostDuplicatedTitleErrorMessage = "A post with the title \"{0}\" already exists.";
    }
}
