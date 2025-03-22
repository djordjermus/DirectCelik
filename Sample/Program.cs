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
		var result = Celik.ExecuteStatic(reader, session => {
			Console.WriteLine(JsonConvert.SerializeObject(session.SessionBeginResult, Formatting.Indented));
			Console.WriteLine("\tVerifikacija podataka kartice:");
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyDocumentData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyFixedPersonalData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyVariablePersonalData(), Formatting.Indented));
			Console.WriteLine(JsonConvert.SerializeObject(session.VerifyPortrait(), Formatting.Indented));
            Console.WriteLine("\tČitanje podataka sa kartice:");
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadDocumentData(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadFixedPersonalData(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadVariablePersonalData(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadPortrait(), Formatting.Indented));
            Console.WriteLine("\tČitanje sertifikata:");
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadMoiIntermediateCACertificate(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadAuthentificationCertificate(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadSignatureCertificate(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadFixedCertificate(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(session.ReadVariableCertificate(), Formatting.Indented));
        });
	}
	catch { }
}
