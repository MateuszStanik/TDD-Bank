using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace TDDLab
{
    class Program
    {
        static void Main(string[] args)
        {
          
            AutofacConfig.ConfigureBuilder();           

            int selectedOption= 0;

            Console.WriteLine("Witam w programie BANK TDD");

            while (true)
            {
                
                Console.WriteLine("Wybierz odpowiednią funkcjonalność:");
                Console.WriteLine("1 - Logowanie do systemu");
                Console.WriteLine("2 - Wpłata na konto");
                Console.WriteLine("3 - Wypłata z konta");
                Console.WriteLine("4 - Przelew zwykły");
                Console.WriteLine("5 - Sprawdzenie salda");
                Console.WriteLine("6 - Generuj raport transakcji użytkownika");
                Console.WriteLine("7 - Generuj raport kont");
                selectedOption = Convert.ToInt32(Console.ReadLine());
                IEFDbContext _db = new EFDbContext();

                switch (selectedOption)
                {
                    case 1:
                        Console.WriteLine("Wybrano 1");
                        string login, password;
                        Console.WriteLine("Wprowadź login");
                        login = Console.ReadLine();
                        Console.WriteLine("Wybrano hasło");
                        password = Console.ReadLine();                        
                        UserOperations user = new UserOperations(_db);
                        user.Login(login, password);
                        break;
                    case 2:
                        Console.WriteLine("Wybrano 2");
                        long amountIn, NRBto;
                        Console.WriteLine("Wprowadź sume do wpłacenia");
                        amountIn = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Wprowadź numer rachunku");
                        NRBto = Convert.ToInt64(Console.ReadLine());
                        AccountOperations accountTo = new AccountOperations(_db);
                        accountTo.AddAmountToAccount(amountIn, NRBto);
                        break;
                    case 3:
                        Console.WriteLine("Wybrano 3");
                        long amountOut, NRBFrom;
                        Console.WriteLine("Wprowadź sume do wpłacenia");
                        amountOut = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Wprowadź numer rachunku");
                        NRBFrom = Convert.ToInt64(Console.ReadLine());
                        AccountOperations accountFrom = new AccountOperations(_db);
                        accountFrom.GetAmountFromAccount(amountOut, NRBFrom);
                        break;
                    case 4:
                        Console.WriteLine("Wybrano 4");
                        long amount, fromNRB, toNRB;
                        Console.WriteLine("Wprowadź sume do przelania");
                        amount = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Wprowadź numer rachunku źródłowego");
                        fromNRB = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Wprowadź numer rachunku docelowego");
                        toNRB = Convert.ToInt64(Console.ReadLine());
                        AccountOperations account = new AccountOperations(_db);
                        account.TransferMoney(amount, fromNRB, toNRB);
                        break;
                    case 5:
                        Console.WriteLine("Wybrano 5");
                        long NRB;
                        Console.WriteLine("Wprowadź numer rachunku");
                        NRB = Convert.ToInt64(Console.ReadLine());                       
                        AccountOperations accountNRB = new AccountOperations(_db);
                        Console.WriteLine("Saldo rachunku " + NRB + " wynosi: "+ accountNRB.CheckBalance(NRB));
                        break;
                    case 6:
                        Console.WriteLine("Wybrano 6");
                        string userName;
                        Console.WriteLine("Wprowadź nazwę użytkownika");
                        userName = Console.ReadLine();
                        TransactionOperation transaction = new TransactionOperation(_db);
                        var transactions = transaction.GetTransactionForUser(userName);
                        foreach(var t in transactions)
                        {
                            Console.WriteLine("Wartość transakcji: " + t.Amount + " typ: " + (t.IsIncome ? "Wpłata" : "Wypłata"));
                        }
                        
                        break;
                    case 7:
                        Console.WriteLine("Wybrano 7");

                        AccountOperations accounts = new AccountOperations(_db);
                        var accountsList = accounts.RaportAccounts();
                        foreach (var t in accountsList)
                        {
                            Console.WriteLine("Id użytkownika: " + t.user.Login + " saldo: " + t.Ammount);
                        }

                        break;
                    default:
                        Console.WriteLine("Błędny wybór");
                        break;
                }
            }
            


            for (;;) { }
        }
    }
}
