using RestExercise4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestExercise4.Managers
{
    public class ItemManagerDB : IItemsManager
    {

        public ItemContext _context;

        public ItemManagerDB(ItemContext context)
        {
            _context = context;
        }
        public Item Add(Item newItem)
        {
              newItem.Id = 0;//to ignore the id suppply from user
            _context.Add(newItem);
            _context.SaveChanges();
            return newItem;
        }

        public Item Delete(int id)
        {
            //Finds the Item that should be deleted using the Id
            //Uses the GetByID method because if we optimize this method, or change how to find a specific Item, we only need to implement this once
            Item itemToBeDeleted = GetById(id);
            _context.Items.Remove(itemToBeDeleted);
            //Remember to call the savechanges everytime you make changes
            _context.SaveChanges();
            return itemToBeDeleted;
        }

            public List<Item> GetAll(string substring = null, int minimumQuality = 0, int minimumQuantity = 0)
        {
            return _context.Items.ToList<Item>();
          
        }

        public List<Item> GetAllBetweenQuality(int minQuality, int maxQuality)
        {
            throw new NotImplementedException();
        }

        public Item GetById(int id)
        {
            return _context.Items.Find(id);
        }

        public Item Update(int id, Item updates)
        {
            Item itemToBeUpdated = GetById(id);

            //update the values
            //Notice we don't update the Id, as it comes from the first parameter, and if the id is different in the updates, it gets ignored
            //We want the database to handle the Id's for us
            itemToBeUpdated.Name = updates.Name;
            itemToBeUpdated.ItemQuality = updates.ItemQuality;
            itemToBeUpdated.Quantity = updates.Quantity;

            //Remember to call the savechanges everytime you make changes
            //In this case it can see that the itemToBeUpdated has been updated
            _context.SaveChanges();

            return itemToBeUpdated;
        }
    }
}
