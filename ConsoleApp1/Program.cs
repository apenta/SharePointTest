using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using SPClient = Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using Microsoft.ProjectServer.Client;
using Microsoft.SharePoint.Client;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {


        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\pentaa\\Desktop\\TEST FILE.docx";
            string title = filePath.Split('\\').Reverse().First();


            //// Create a memory stream from those bytes.
            //using (MemoryStream memory = new MemoryStream(file))
            //{
            //    // Use the memory stream in a binary reader.
            //    using (BinaryReader reader = new BinaryReader(memory))
            //    {
            //        // Read in each byte from memory.
            //        for (int i = 0; i < file.Length; i++)
            //        {
            //            byte result = reader.ReadByte();
            //            //Console.WriteLine(result);
            //        }
            //    }

            //}
            string userName = "annapenta@lexmarktest.onmicrosoft.com";
            Console.WriteLine("Enter your password.");
            SecureString password = GetPassword();
            // ClienContext - Get the context for the SharePoint Online Site  
            // SharePoint site URL - https://lexmarktest.sharepoint.com 
            using (var clientContext = new ClientContext("https://lexmarktest.sharepoint.com/sites/initialtest"))
            {
                // SharePoint Online Credentials  
                clientContext.Credentials = new SharePointOnlineCredentials(userName, password);

                // Get the SharePoint web  
                Web web = clientContext.Web;
                //ListCollection collList = web.Lists;
                var list = web.GetList("sites/initialtest/Shared%20Documents");

                // Load the Web properties  
                clientContext.Load(list);

                // Execute the query to the server.  
                clientContext.ExecuteQuery();

                //Web properties -Display the Title and URL for the web

                //Console.WriteLine("Title: " + web.Title + "; URL: " + web.Url);
                Console.WriteLine("Title: " + list.Title);

                FileCreationInformation file1 = new FileCreationInformation();
                byte[] file = System.IO.File.ReadAllBytes(filePath);

                file1.Content = file;
                file1.Url = "https://lexmarktest.sharepoint.com/sites/initialtest" + "/Shared%20Documents/" + title;
                file1.Overwrite = true;
                var viewFile = list.RootFolder.Files.Add(file1);
                viewFile.ListItemAllFields["Title"] = "test";
                viewFile.ListItemAllFields["Created"] = DateTime.Now.AddDays(-1);
                viewFile.ListItemAllFields["Author"] = "testone@lexmarktest.onmicrosoft.com";

                viewFile.ListItemAllFields.Update();

                clientContext.ExecuteQuery();


                ////Fake list number 1 that will be added
                //ListCreationInformation lci1 = new ListCreationInformation();
                //lci1.Title = "test";
                //lci1.TemplateType = (int)ListTemplateType.DocumentLibrary;
                ////lci1.Url = @"https://lexmarktest.sharepoint.com/sites/initialtest/Shared%20Documents/Forms/AllItems.aspx";
                //web.Lists.Add(lci1);

                //clientContext.Load(collList);
                //clientContext.ExecuteQuery();

                //Console.WriteLine("Lists on the current site:\n\n");
                //foreach (List targetList in collList)
                //    Console.WriteLine(targetList.Title);
                //Console.ReadLine();
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
