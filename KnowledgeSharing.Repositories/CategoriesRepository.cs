using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowlegdeSharing.DomainModels;

namespace KnowledgeSharing.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryid);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryID(int categoryID);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        KnowledgeSharingDbContext knowledgesharingDB;

        public CategoriesRepository()
        {
            knowledgesharingDB = new KnowledgeSharingDbContext();
        }

        public void InsertCategory(Category category)
        {
            knowledgesharingDB.Categories.Add(category);
            knowledgesharingDB.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category changeCategory = knowledgesharingDB.Categories.Where(temp => temp.CategoryID == category.CategoryID).FirstOrDefault();
            if (changeCategory != null)
            {
                changeCategory.CategoryName = category.CategoryName;
                knowledgesharingDB.SaveChanges();
            }
        }
        public void DeleteCategory(int categoryid)
        {
            Category changeCategory = knowledgesharingDB.Categories.Where(temp => temp.CategoryID == categoryid).FirstOrDefault();
            if (changeCategory != null)
            {
                knowledgesharingDB.Categories.Remove(changeCategory);
                knowledgesharingDB.SaveChanges();
            }
        }
        public List<Category> GetCategories()
        {
            List<Category> categoryList = knowledgesharingDB.Categories.ToList();
            return categoryList;
        }
        public List<Category> GetCategoryByCategoryID(int CategoryID)
        {
            List<Category> categoryList = knowledgesharingDB.Categories.Where(temp => temp.CategoryID == CategoryID).ToList();
            return categoryList;
        }
    }
}
