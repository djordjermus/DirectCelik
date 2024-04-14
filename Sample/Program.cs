using DirectCelik;
using Newtonsoft.Json;
using var celikLifetime = CelikLifetime.Create();
while (true)
{
	Console.Write("Press enter...");
	Console.ReadLine();

	using var reader  = celikLifetime.BeginTransaction(null);
	if (reader != null)
	{
		var documentData  = reader.ReadDocumentData();
		var fixedData     = reader.ReadFixedPersonalData();
		var variableData  = reader.ReadVariablePersonalData();
		var portrait      = reader.ReadPortraitData();

		Console.WriteLine(
			$"\tDocument Data: {JsonConvert.SerializeObject(documentData, Formatting.Indented)};\n" +
			$"\tFixed Data:    {JsonConvert.SerializeObject(fixedData, Formatting.Indented)};\n" +
			$"\tVariable Data: {JsonConvert.SerializeObject(variableData, Formatting.Indented)};\n" +
			$"\tPortrait Data: {JsonConvert.SerializeObject(portrait, Formatting.Indented)};");
	}

	File.AppendAllText("finalizer.txt", "Hello from main");
	GC.WaitForPendingFinalizers();
}
