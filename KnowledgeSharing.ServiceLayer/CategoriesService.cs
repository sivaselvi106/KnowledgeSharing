using AutoMapper;
using KnowledgeSharing.Repositories;
using KnowledgeSharing.ViewModels;
using KnowlegdeSharing.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeSharing.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel categoryVM);
        void UpdateCategory(CategoryViewModel categoryVM);
        void DeleteCategory(int categoryid);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryID(int CategoryID);
    }
    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository categoriesRepository;

        public CategoriesService()
        {
            categoriesRepository = new CategoriesRepository();
        }
        public void InsertCategory(CategoryViewModel categoryVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(categoryVM);
            categoriesRepository.InsertCategory(category);
        }
        public void UpdateCategory(CategoryViewModel categoryVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(categoryVM);
            categoriesRepository.UpdateCategory(category);
        }
        public void DeleteCategory(int categoryid)
        {
            categoriesRepository.DeleteCategory(categoryid);
        }
        public List<CategoryViewModel> GetCategories()
        {
            List<Category> categoryList = categoriesRepository.GetCategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> categoryVM = mapper.Map<List<Category>, List<CategoryViewModel>>(categoryList);
            return categoryVM;
        }
        public CategoryViewModel GetCategoryByCategoryID(int CategoryID)
        {
            Category category = categoriesRepository.GetCategoryByCategoryID(CategoryID).FirstOrDefault();
            CategoryViewModel categoryVM = null;
            if (category != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                categoryVM = mapper.Map<Category, CategoryViewModel>(category);
            }
            return categoryVM;
        }
    }
}
