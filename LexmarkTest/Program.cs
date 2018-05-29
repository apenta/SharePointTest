using System;  
using System.Security;  
using Microsoft.SharePoint.Client;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;

namespace CSOMOffice365
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SharePoint Online site URL:");
            stringwebSPOUrl = Console.ReadLine();
            Console.WriteLine("User Name:");
            stringuserName = Console.ReadLine();
            Console.WriteLine("Password:");
            SecureString password = FetchPasswordFromConsole();
            try
            {
                using (var context = new ClientContext(webSPOUrl))
                {
                    context.Credentials = new SharePointOnlineCredentials(userName, password);
                    Web web = context.Web;
                    context.Load(web.Lists,
                        lists => lists.Include(list => list.Title,
                            list => list.Id));
                    context.ExecuteQuery();
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (List list in web.Lists)
                    {
                        Console.WriteLine("List title is: " + list.Title);
                    }
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is: " + ex.Message);
            }
        }
        private static SecureStringFetchPasswordFromConsole()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        intpos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            varsecurePassword = new SecureString();
            //Convert string to secure string  
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            returnsecurePassword;
        }
    }
}