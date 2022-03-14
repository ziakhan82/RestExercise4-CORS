using RestExercise4.Models;
using System.Collections.Generic;

namespace RestExercise4.Managers
{
    public interface IItemsManager
    {
        Item Add(Item newItem);
        Item Delete(int id);
        List<Item> GetAll(string substring = null, int minimumQuality = 0, int minimumQuantity = 0);
        List<Item> GetAllBetweenQuality(int minQuality, int maxQuality);
        Item GetById(int id);
        Item Update(int id, Item updates);
    }
}