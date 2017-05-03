// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System;

namespace OmniKassa
{
    /// <summary>
    /// Encapsulates the http functionality.
    /// </summary>
    public interface IWebHelper : IDisposable
    {
        /// <summary>
        /// Post the data to the specified url and return the response.
        /// </summary>
        /// <param name="uri">The url.</param>
        /// <param name="postData">The data to post.</param>
        /// <returns>The response of the post request.</returns>
        byte[] PostData(Uri uri, IPaymentPostData postData);

        /// <summary>
        /// Returns the data that was posted by OmniKassa.
        /// </summary>
        /// <returns>The data that was posted by OmniKassa.</returns>
        IPaymentPostData GetPostData();
    }
}
