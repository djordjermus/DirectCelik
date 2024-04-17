using DirectCelik;
using DirectCelik.Model.Enum;
using Newtonsoft.Json;
while (true)
{
	using var celikLifetime = Celik.Create();
	Console.Write("Press enter...");
	Console.ReadLine();

	try
	{
		var data = celikLifetime.Read(ReadOperations.All);
		if (data != null)
			Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
	}
	catch { }
}
