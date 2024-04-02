using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitefinity_Helper
{
    class Program
    {
        static string folderPath = @"C:\Users\wfth8817\Desktop\Clone\web\Mvc\";

        static void Main(string[] args)
        { 
            string Controllertemplatedata = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ControllerTemplate.txt"));
            string Modeltemplatedata = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModelTemplate.txt"));
            string Viewtemplatedata = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ViewTemplate.txt"));

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Error: Solution MVC folder not exist on selected path.");
                Console.WriteLine(folderPath);
                Console.ReadKey();
                return;
            }

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

    }
}
