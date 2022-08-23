using DadsInventory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DadsInventory.ViewModels
{
    public class ItemsListViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public string Title { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
    }
}
