using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

 
namespace Sitefinity_Helper
{
    class Program
    {
        static string folderPath = "";

        static void Main(string[] args)
        {

          bool status =  configureMvcFolder();

            
            if (!status) { 
                Console.ReadKey();
                return;
            }

            //check if the folder path has three folders , controlllers, views, models


            if (!validateFiles())
            {
                Console.ReadKey();
                return;
            }
             


            string Controllertemplatedata = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ControllerTemplate.txt"));
            string Modeltemplatedata = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModelTemplate.txt"));
            string Viewtemplatedata = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ViewTemplate.txt"));


            Console.WriteLine("Enter Widget name you want to create:");
            string userInput = Console.ReadLine();
            string controllerpath = Path.Combine(folderPath, @"Controllers\" + userInput + "Controller.cs");
            string modelpath = Path.Combine(folderPath, @"Models\" + userInput + "Model.cs");
            string viewpath = Path.Combine(folderPath, @"Views\" + userInput + @"\Index.cshtml");
            if (!File.Exists(controllerpath) || !File.Exists(modelpath) || !File.Exists(viewpath))
            {

                string controllerData = Controllertemplatedata.Replace("{controllerName}", userInput);
                string modelData = Modeltemplatedata.Replace("{controllerName}", userInput);
                string viewData = Viewtemplatedata.Replace("{controllerName}", userInput);

                File.Create(controllerpath).Close();
                File.WriteAllText(controllerpath, controllerData);

                File.Create(modelpath).Close();//test
                File.WriteAllText(modelpath, modelData);

                Directory.CreateDirectory(Path.Combine(folderPath, @"Views\" + userInput));

                File.Create(viewpath).Close();
                File.WriteAllText(viewpath, viewData);

                Console.WriteLine("Succcess");

            }
            else
            {
                Console.WriteLine("Error: File already exist.. closing app.");
            }



            Console.ReadKey();
        }


        static bool configureMvcFolder()
        {
            //check if the path.txt file exist in the curent directory
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "path.txt")))
            {
                folderPath = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "path.txt"));
                return true;
            }
            else
            {
               
                Console.WriteLine("Please enter the full relative path of the solution MVC folder:");
                //open directory dialog

                folderPath = Console.ReadLine();
                File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "path.txt")).Close();
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "path.txt"), folderPath);

                if (validateFiles())
                {
                    Console.WriteLine("Path Added : " + folderPath);
                    Console.WriteLine("-> Models folder found!");
                    Console.WriteLine("-> Views folder found!");
                    Console.WriteLine("-> Controllers folder found!");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Success  - Restart to use it.");
                    return false;
                }else
                {
                    return false;
                }
            }
        }

        static bool validateFiles()
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Error: Solution MVC folder not exist on selected path.");
                File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "path.txt")); 
                Console.WriteLine("Restart to reset MVC Folder Path.");
                return false;
            }


            if (!Directory.Exists(Path.Combine(folderPath, "Controllers")) || !Directory.Exists(Path.Combine(folderPath, "Views")) || !Directory.Exists(Path.Combine(folderPath, "Models")))
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Error: Controllers, Views, Models Folder not found!"); 
                File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "path.txt")); 
                Console.WriteLine("Restart to reset MVC Folder Path.");
                return false;
            }

            return true;
        }
    }
}
