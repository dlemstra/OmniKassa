// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;
using System.IO;

namespace OmniKassa.Tests
{
    [ExcludeFromCodeCoverage]
    public class TemporaryFile : IDisposable
    {
        public TemporaryFile()
        {
            File = new FileInfo(Path.GetTempFileName());
        }

        public FileInfo File { get; private set; }

        public void Dispose()
        {
            if (File.Exists)
                File.Delete();
        }
    }
}
