using System;
using System.Text;

namespace SodaMachineApp
{
    public class SodaMachine
    {
        private static readonly Money Founds = new Money();

        private static readonly SodaMachineInventory Inventory = new SodaMachineInventory();

        /// <summary>
        /// This is the starter method for the machine
        /// </summary>
        public void Start()
        {
            Inventory.SeedSodaInventory();

            while (true)
            {
                MainMenu();
            }
        }

        private void MainMenu()
        {
            DisplayMainMenu();
            HandleMainMenuInputCommand();
        }
        
        private void DisplayMainMenu()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("\n\nAvailable commands:");
            stringBuilder.AppendLine(CommandsEnum.insert + " (money) - Money put into money slot");
            stringBuilder.AppendLine(CommandsEnum.order + " (" + AllSodaTypes() + ") - Order from machines buttons");
            stringBuilder.AppendLine(CommandsEnum.sms + " order (" + AllSodaTypes() + ") - Order sent by sms");
            stringBuilder.AppendLine(CommandsEnum.recall + " - gives money back");
            stringBuilder.AppendLine("-------");
            stringBuilder.AppendLine("Inserted money: " + Founds.AvailableFounds());
            stringBuilder.AppendLine("-------\n\n");
            Console.Write(stringBuilder.ToString());
        }

        private string AllSodaTypes()
        {
            var stringBuilder = new StringBuilder();
            var allSodaTypes = Enum.GetValues(typeof(SodaTypesEnum));
            foreach (var sodaType in allSodaTypes)
            {
                if (stringBuilder.Length == 0)
                {
                    stringBuilder.Append(sodaType);
                }
                else
                {
                    stringBuilder.Append(", " + sodaType);
                }
            }

            return stringBuilder.ToString();
        }

        private void HandleMainMenuInputCommand()
        {
            var input = Console.ReadLine();

            if (input == null) return;
            var command = input.Split(' ')[0];
            if (Enum.TryParse(command, out CommandsEnum cmdEnum))
            {
                SodaTypesEnum sodaType;
                switch (cmdEnum)
                {
                    case CommandsEnum.insert:
                        var potentialAmount = input.Split(' ')[1];
                        if (int.TryParse(potentialAmount, out var amountToInsert))
                        {
                            Founds.InsertMoney(amountToInsert);
                        }
                        else
                        {
                            Console.WriteLine(potentialAmount + " is not a recognized amount");
                        }
                        break;

                    case CommandsEnum.order:
                        var sodaTypeInput = input.Split(' ')[1];
                        if (Enum.TryParse(sodaTypeInput, out sodaType))
                        {
                            var sodaPrice = GetPriceForSoda(sodaType);
                            if (!Founds.HasEnouhToPay(sodaPrice))
                                break;

                            if (Inventory.RemoveSodaFromInventory(sodaType))
                            {
                                Founds.Pay(sodaPrice);
                                Founds.PayOutBalance(false); //false for 'returned change' text
                            }
                        }
                        else
                        {
                            Console.WriteLine(sodaTypeInput + " is not recognized as a soda type");
                        }
                        break;

                    case CommandsEnum.sms:
                        var sodaTypeInputSms = input.Split(' ')[2];
                        if (Enum.TryParse(sodaTypeInputSms, out sodaType))
                        {
                            Inventory.RemoveSodaFromInventory(sodaType);
                        }
                        else
                        {
                            Console.WriteLine(sodaTypeInputSms + " is not recognized as a soda type");
                        }
                        break;

                    case CommandsEnum.recall:
                        Founds.PayOutBalance(true); //true for 'recall' text
                        break;
                }
            }
            else
            {
                Console.WriteLine(command + " is not recognized as a command");
            }
        }

        //Great potential for a Service, if we load price data from an external services
        private int GetPriceForSoda(SodaTypesEnum sodaType)
        {
            switch (sodaType)
            {
                case SodaTypesEnum.coke:
                    return 20;
                default:
                    return 15;
            }
        }
    }
}
