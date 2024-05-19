using System;
using System.Linq;
using DirectCelik;
using Newtonsoft.Json;

namespace FrameworkSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var celikLifetime = Celik.Create())
            {
                try
                {
                    Console.Write("Press enter to read card data...");
                    Console.ReadLine();
                    var reader = CardReaders.Readers()?.FirstOrDefault();
                    celikLifetime.Execute(reader, session => {
                        Console.WriteLine(JsonConvert.SerializeObject(session.SessionBeginResult));
                        Console.WriteLine();
                        Console.WriteLine(JsonConvert.SerializeObject(session.VerifyDocumentData()));
                        Console.WriteLine(JsonConvert.SerializeObject(session.VerifyFixedPersonalData()));
                        Console.WriteLine(JsonConvert.SerializeObject(session.VerifyVariablePersonalData()));
                        Console.WriteLine(JsonConvert.SerializeObject(session.VerifyPortrait()));
                        Console.WriteLine();
                        Console.WriteLine(JsonConvert.SerializeObject(session.ReadDocumentData()));
                        Console.WriteLine(JsonConvert.SerializeObject(session.ReadFixedPersonalData()));
                        Console.WriteLine(JsonConvert.SerializeObject(session.ReadVariableParsonalData()));
                        Console.WriteLine(JsonConvert.SerializeObject(session.ReadPortrait()));
                    });
                }
                catch { }
            }
            Console.ReadLine();
        }
    }
}
