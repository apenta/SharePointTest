using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using SPClient=Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using Microsoft.ProjectServer.Client;
using Microsoft.SharePoint.Client;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName = "annapenta@lexmarktest.onmicrosoft.com";
            Console.WriteLine("Enter your password.");
            SecureString password = GetPassword();
            // ClienContext - Get the context for the SharePoint Online Site  
            // SharePoint site URL - https://lexmarktest.sharepoint.com 
            using (var clientContext = new ClientContext("https://lexmarktest.sharepoint.com"))
            {
                // SharePoint Online Credentials  
                clientContext.Credentials = new SharePointOnlineCredentials(userName, password);
                // Get the SharePoint web  
                Web web = clientContext.Web;
                // Load the Web properties  
                clientContext.Load(web);
                // Execute the query to the server.  
                clientContext.ExecuteQuery();
                // Web properties - Display the Title and URL for the web  
                Console.WriteLine("Title: " + web.Title + "; URL: " + web.Url);
                //Console.ReadLine();

                //var lists = web.Lists;
                //clientContext.Load(lists);
                //clientContext.ExecuteQuery();
                //Console.Read();
                //Console.WriteLine(lists);

                Microsoft.SharePoint.Client.List spList = clientContext.Web.Lists.GetByTitle("List Template Gallery");
                clientContext.Load(spList);
                clientContext.ExecuteQuery();


                if (spList != null && spList.ItemCount > 0)
                {
                    Microsoft.SharePoint.Client.CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml =
                        @"<View>
                            <ViewFields><FieldRef Name= 'TemplateTitle' /></ViewFields>
                          </View>";

                    ListItemCollection listItems = spList.GetItems(camlQuery);
                    clientContext.Load(listItems);
                    clientContext.ExecuteQuery();
                    Console.WriteLine(listItems);
                    Console.ReadLine();
                }

            }
        }
        private static SecureString GetPassword()
        {
            ConsoleKeyInfo info;
            //Get the user's password as a SecureString  
            SecureString securePassword = new SecureString();
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter)
                {
                    securePassword.AppendChar(info.KeyChar);
                }
            }
            while (info.Key != ConsoleKey.Enter);
            return securePassword;
        }
    }
}

