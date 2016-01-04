/*
 * RAuthentication.cs
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
/// RAuthentication Interface
/// </summary>
/// <remarks></remarks>
    public interface RAuthentication
    {
        /// <summary>
        /// Username for authentication
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        String Username { get; set; }
        /// <summary>
        /// Password for Username
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        String Password { get; set; }

    }
}