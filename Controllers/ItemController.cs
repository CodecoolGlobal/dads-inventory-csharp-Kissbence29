using DadsInventory.Models;
using DadsInventory.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DadsInventory.Controllers
{

    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemController(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }
        [Authorize]
        public ViewResult List(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                searchTerm = "";

            var itemsListViewModel = new ItemsListViewModel
            {
                Items = _itemRepository.AllItems.Where(x => x.Name.Contains(searchTerm)),
                Title = "Dad's Inventory"
            };


            return View(itemsListViewModel);
        }
        [Authorize(Roles = "Dad")]
        public IActionResult Details(int id)
        {
            var item = _itemRepository.GetItemById(id);
            return item == null ? (IActionResult)NotFound() : View(item);
        }
    }
}