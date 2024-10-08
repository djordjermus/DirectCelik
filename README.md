Library for consuming CelikApi.dll (compatible with CelikApi v4).<br><br>
As of version 4.0.1, you must provide your own CelikApi DLLs as CelikApiX32.dll and CelikApiX64.dll in the project output.<br>
This was done because dll imports using NuGet packages are done differently for .Net Framework(with .packages file) and .Net Core. And each method only works for one of the NuGet package standards<br><br>
x32 and x64 CelikApi.dll versions can be found [here](http://ca.mup.gov.rs/ca/ca_lat/start/kes/)
