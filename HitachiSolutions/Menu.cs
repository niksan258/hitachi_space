using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitachiSolutions
{
    public class Menu
    {
        public Menu() { }

        static public void MenuLoop()
        {
            try
            {

                Console.WriteLine("Enter filePath to the weather forecast (or press enter for default path ).");
                string filePath = Console.ReadLine();

                if (filePath == "") filePath = Constants.INPUT_FILE_NAME;


                List<WeatherDayModel> parsedList = WeatherCsvParser.Parse(filePath);
                if (parsedList.Count == 0) throw new Exception("Could not open file for parsing!");


                WeatherReport weatherReport = new WeatherReport(parsedList);
                bool writeStatus = WeatherCsvParser.WriteToFile(weatherReport.ReportTable(),
                                                                    Constants.OUTPUT_FILENAME);

                if (!writeStatus)
                    throw new Exception("Could not save report");


                Console.WriteLine("Enter sender email: ");
                string senderEmail = Console.ReadLine();

                Console.WriteLine("Enter sender password: ");
                string senderPassword = Console.ReadLine();

                Console.WriteLine("Enter receiver email: ");
                string receiverEmail = Console.ReadLine();


                SMTPClient client = new SMTPClient();
                bool emailSent = client.SendMail(senderEmail, senderPassword,
                    receiverEmail, Constants.OUTPUT_FILENAME);

                if (!emailSent) throw new Exception("Failed to send email.");
                
                Console.WriteLine("Email with report sent. Mission successful :)");

            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
