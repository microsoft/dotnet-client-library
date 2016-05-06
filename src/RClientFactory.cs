/*
 * RClientFactory.cs
 *
 * Copyright (C) 2010-2015 by Microsoft Corporation
 *
 * This program is licensed to you under the terms of Version 2.0 of the
 * Apache License. This program is distributed WITHOUT
 * ANY EXPRESS OR IMPLIED WARRANTY, INCLUDING THOSE OF NON-INFRINGEMENT,
 * MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE. Please refer to the
 * Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0) for more details.
 *
 */

using System;

namespace DeployR
{

    /// <summary>
    /// Create a client connection at the specified RevoDeployR URL
    /// </summary>
    /// <remarks></remarks>
    public class RClientFactory
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RClientFactory()
        {

        }
        /// <summary>
        /// Create connection at the specified RevoDeployR URL
        /// 
        /// The concurrent call limit is defaulted to 10
        /// </summary>
        /// <param name="deployRURL">URL address of RevoDeployR server</param>
        /// <returns>RClient object</returns>
        /// <remarks></remarks>
        static public RClient createClient(String deployRURL)
        {
            return createClient(deployRURL, 10);
        }
        
        /// <summary>
        /// Create connection at the specified RevoDeployR URL
        /// </summary>
        /// <param name="deployRURL">URL address of RevoDeployR server</param>
        /// <param name="concurrentCallLimit">(optional) the maximum number of conccurent calls, beyond which they are queued (default = 3)</param>
        /// <returns>RClient object</returns>
        /// <remarks></remarks>
        static public RClient createClient(String deployRURL, int concurrentCallLimit)
        {
            RClient returnValue = new RClient(deployRURL, concurrentCallLimit);
            System.Net.ServicePointManager.DefaultConnectionLimit = concurrentCallLimit + 10;
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.SetTcpKeepAlive(true, 10000, 10000);

            return returnValue;
        }

        /// <summary>
        /// Sets debug mode on the deployR library.  This will print extra information to the console
        /// </summary>
        /// <param name="value">True to enable debug information to be created</param>
        /// <remarks></remarks>
        static public void setDebugMode(Boolean value)
        {

            HTTPUtilities.DEBUGMODE = value;

        }

        /// <summary>
        /// Sets the HTTP REQUEST timeout in ms (default is 1000000 ms or 10000 sec)
        /// </summary>
        /// <param name="value">timeout in ms</param>
        /// <remarks></remarks>
        static public void setHTTP_Timeout(int value)
        {

            HTTPUtilities.HTTP_TIMEOUT = value;

        }


        /// <summary>
        /// Gets the HTTP REQUEST timeout in ms (default is 1000000 ms or 10000 sec)
        /// </summary>
        /// <returns>timeout in ms</returns>
        /// <remarks></remarks>
        static public int getHTTP_Timeout()
        {
            int returnValue = System.Convert.ToInt32(HTTPUtilities.HTTP_TIMEOUT);

            return returnValue;
        }

    }
}