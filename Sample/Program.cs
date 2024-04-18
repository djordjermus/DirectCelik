using DirectCelik;
using Newtonsoft.Json;
using var celikLifetime = Celik.Create();
while (true)
{
	try
	{
		Console.Write("Press enter to read card data...");
		Console.ReadLine();
		celikLifetime.Execute(null, session => {
			Console.WriteLine(JsonConvert.SerializeObject(session.SessionBeginResult, Formatting.Indented));
			Console.WriteLine();
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyDocumentData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyFixedPersonalData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyVariablePersonalData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyPortrait(), Formatting.Indented));
			Console.WriteLine();
			Console.WriteLine(JsonConvert.SerializeObject(session.ReadDocumentData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.ReadFixedPersonalData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.ReadVariableParsonalData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.ReadPortrait(), Formatting.Indented));
		});
	}
	catch { }
}
