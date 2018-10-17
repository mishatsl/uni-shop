using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Binders
{
    public class FilterSideBarBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Получаем поставщик значений
            var valueProvider = bindingContext.ValueProvider;
            string brand, category;
            bool IsChecked;

            IList<FilterOfBrand> filterOfBrands = new List<FilterOfBrand>();
            IList<FilterOfCategory> filterOfCategories = new List<FilterOfCategory>();

            for(int i = 0; valueProvider.GetValue($"Brands[{i}].IsChecked") !=null; i++)
            {
                if (valueProvider.GetValue($"Brands[{i}].Brand") != null)
                    brand = (string)valueProvider.GetValue($"Brands[{i}].Brand").ConvertTo(typeof(string));
                else
                    return null;

                if (valueProvider.GetValue($"Brands[{i}].IsChecked") != null)
                    IsChecked = (bool)valueProvider.GetValue($"Brands[{i}].IsChecked").ConvertTo(typeof(bool));
                else
                    return null;
                filterOfBrands.Add(new FilterOfBrand
                {
                    Brand = brand,
                   // CountOfProducts = (int)valueProvider.GetValue("CountOfProducts").ConvertTo(typeof(int)),
                    IsChecked = IsChecked
                });
            }

            for (int i = 0; valueProvider.GetValue($"Categories[{i}].IsChecked") != null; i++)
            {
                if (valueProvider.GetValue($"Categories[{i}].Category") != null)
                    category = (string)valueProvider.GetValue($"Categories[{i}].Category").ConvertTo(typeof(string));
                else
                    return null;

                if (valueProvider.GetValue($"Categories[{i}].IsChecked") != null)
                    IsChecked = (bool)valueProvider.GetValue($"Categories[{i}].IsChecked").ConvertTo(typeof(bool));
                else
                    return null;
                filterOfCategories.Add(new FilterOfCategory
                {
                    Category = category,
                  //  CountOfProducts = (int)valueProvider.GetValue("CountOfProducts").ConvertTo(typeof(int)),
                    IsChecked = IsChecked
                });
            }

            if (valueProvider.GetValue("PriceMax") == null)
                return null;
            int pMax = (int)Convert.ToDouble((string)valueProvider.GetValue("PriceMax").ConvertTo(typeof(string)));
        
            if (valueProvider.GetValue("PriceMin") == null)
                return null;
                int pMin = (int)Convert.ToDouble((string)valueProvider.GetValue("PriceMin").ConvertTo(typeof(string)));

            ProductFilterViewModel productFilterViewModel = new ProductFilterViewModel
            {
                Brands = filterOfBrands,
                Categories = filterOfCategories, 
                PriceMax = pMax,
                PriceMin = pMin
            };

            return productFilterViewModel;
        }
    }
}