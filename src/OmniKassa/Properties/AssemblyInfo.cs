﻿// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyDescription("C# API for OmniKassa")]
[assembly: AssemblyCompany("Dirk Lemstra")]
[assembly: AssemblyProduct("OmniKassa")]
[assembly: AssemblyCopyright("Copyright 2017 Dirk Lemstra")]
[assembly: AssemblyTitle("OmniKassa")]

[assembly: AssemblyVersion("1.0.1")]
[assembly: AssemblyFileVersion("1.0.1")]
[assembly: AssemblyInformationalVersion("1.0.1")]

#if NET35
[assembly: InternalsVisibleTo("OmniKassa.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001003748ed84631cfec3a61aa2371cabf4e3c6a8d208a1d9320ccfb83aa604f24c12e8cbfde1836e6dbf9a13d306247559edb6aa2ffca5bc2ed4315db707a8ecd716f84ed6d6fd6796d91f46d324005f0663f555286ef78d0a3abf1f1e23529cc2ff0ec2682353706b6aec39fb20c7cbac9305192beae3640f04ee61a8cdc5e392c3")]
#else
[assembly: InternalsVisibleTo("OmniKassa.Tests")]
#endif