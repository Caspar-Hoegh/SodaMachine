using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodaMachineApp
{
    public class Money
    {
        private static int _balance;

        public int AvailableFounds()
        {
            return _balance;
        }
        public bool HasEnouhToPay(int amountToPay)
        {
            if (amountToPay <= _balance)
            {
                return true;
            }

            Console.WriteLine("Need " + (amountToPay - _balance) + " more");
            return false;
        }

        public void InsertMoney(int amount)
        {
            Console.WriteLine("Adding " + amount + " to credit");
            _balance += amount;
        }

        public void Pay(int amountToPay)
        {
            _balance -= amountToPay;
        }

        public void PayOutBalance(bool isRecall)
        {
            if (isRecall)
            {
                Console.WriteLine("Returning " + _balance + " to customer");
            }
            else
            {
                Console.WriteLine("Giving " + _balance + " out in change");
            }
            _balance = 0;
        }
    }
}
