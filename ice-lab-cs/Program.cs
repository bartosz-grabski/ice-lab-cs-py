using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ice_lab_cs
{
    class Program
    {
        static Ice.Communicator ic = null;
        static string SERVER_IP = "127.0.0.1";
        static string managerString = "manager:tcp -h " + SERVER_IP + " -p 10000:ssl -p 12001:udp -p 10000";

        static void Main(string[] args)
        {
            ic = Ice.Util.initialize(ref args);
            String line = null;

            Ice.ObjectPrx prx = ic.stringToProxy(managerString);
            Bank.BankManagerPrx bankManager = Bank.BankManagerPrxHelper.checkedCast(prx);

            do
            {
                line = Console.ReadLine();

                if (line == "new")
                {
                    newAccount(bankManager,ic);
                }
                else if (line == "info")
                {
                    getBalance(ic);
                }
                else if (line == "transfer")
                {
                    transfer();
                }


            } while (!line.Equals("x"));



        }

        static void getBalance(Ice.Communicator ic)
        {
            Console.WriteLine("Account #");
            String acc = Console.ReadLine();
            Ice.ObjectPrx prx = ic.stringToProxy(acc + ":ssl -h " + SERVER_IP + " -p 10001");
            Bank.AccountPrx account = Bank.AccountPrxHelper.checkedCast(prx);
            Console.WriteLine(account.getBalance());
        }

        static void transfer()
        {

        }

        static void newAccount(Bank.BankManagerPrx bankManager, Ice.Communicator ic)
        {
            String firstName = Console.ReadLine();
            String lastName = Console.ReadLine();
            String NationalID = Console.ReadLine();
            String t = Console.ReadLine();
            Bank.accountType type = Bank.accountType.PREMIUM;
            if (t == "SILVER")
            {
                type = Bank.accountType.SILVER;
            }

            Bank.PersonalData data = new Bank.PersonalData(firstName,lastName,NationalID);

            string accountID = null;

            bankManager.createAccount(data, type, out accountID);

            Console.WriteLine(accountID);
        }
    }
}
