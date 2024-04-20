using DirectCelik;
using Newtonsoft.Json;
while (true)
{
	using var celikLifetime = Celik.Create();

	try
	{
		Console.Write("Press enter to read card data...");
		Console.ReadLine();
		var reader = CardReaders.Readers()?.FirstOrDefault();
		celikLifetime.Execute(reader, session => {
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
