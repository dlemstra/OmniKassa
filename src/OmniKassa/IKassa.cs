// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the communication with OmniKassa.
    /// </summary>
    public partial interface IKassa
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        IKassaConfiguration Configuration { get; }
    }
}
