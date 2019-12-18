using System;
using System.Collections.Generic;

namespace SodaMachineApp
{
    public class SodaMachineInventory
    {
        private static Dictionary<SodaTypesEnum, int> _inventory = new Dictionary<SodaTypesEnum, int>();

        public void AddSodaToInventory(SodaTypesEnum sodaType, int quantity)
        {
            if (_inventory.ContainsKey(sodaType))
            {
                _inventory[sodaType] += quantity;
            }
            else
            {
                _inventory.Add(sodaType, quantity);
            }
        }

        public void SeedSodaInventory()
        {
            AddSodaToInventory(SodaTypesEnum.coke, 5);
            AddSodaToInventory(SodaTypesEnum.sprite, 3);
            AddSodaToInventory(SodaTypesEnum.fanta, 3);
        }

        public bool RemoveSodaFromInventory(SodaTypesEnum sodaType, int quantity = 1)
        {
            if (_inventory.ContainsKey(sodaType) && _inventory[sodaType] >= quantity)
            {
                Console.WriteLine("Giving " + sodaType + " out");
                _inventory[sodaType] -= quantity;
                return true;
            }
            else
            {
                Console.WriteLine("No " + sodaType + " left.");
                return false;
            }
        }
    }
}
