using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using TheAuction.Infrastructure;
using CodeFirst;

namespace TheAuction.Models
{
    public class CategoryModel
    {
        MyDbContext _dbContext;
        public CategoryModel(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Category> getCategories()
        {
            List<Category> Categories = _dbContext.Categories.ToList();
            return Categories;
        }
        public Category setCategory(Category _Category)
        {

            _dbContext.Categories.Add(_Category);
            _dbContext.SaveChanges();
            return _Category;
        }
        public Category getCategoryById(int _id)
        {
            List<Category> Categories = getCategories();
            Category _Category = Categories.FirstOrDefault(item => item.Category_id == _id);
            return _Category;
        }
        public Category getCategoryByName(string _name)
        {
            List<Category> Categories = getCategories();
            Category _Category = Categories.FirstOrDefault(item => item.Name == _name);
            return _Category;
        }
        public Category getCategoryById(int _id, List<Category> Categories)
        {
            Category _Category = Categories.FirstOrDefault(item => item.Category_id == _id);
            return _Category;
        }
        public Category getCategoryByName(string _name, List<Category> Categories)
        {
            Category _Category = Categories.FirstOrDefault(item => item.Name == _name);
            return _Category;
        }
        public Category editCategory(Category updCategory)
        {
            Category extCategory = getCategoryById(updCategory.Category_id);

            extCategory = (Category)UpdateValues.Update(extCategory, updCategory);
            _dbContext.SaveChanges();
            return extCategory;
        }
    }
}