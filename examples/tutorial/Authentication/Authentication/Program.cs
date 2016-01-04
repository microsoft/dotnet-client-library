/*
 * Program.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeployR;

namespace Authentication
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Establish a connection to DeployR.
            //
            // This example assumes the DeployR server is running on localhost.
            //
            String deployrEndpoint = "http://localhost:7400/deployr";
            RClient rClient = RClientFactory.createClient(deployrEndpoint);

            //
            // 2. Authenticate an end-user or client application.
            //
            // The RBasicAuthentication supports basic username/password authentication.
            // The RUser returned represents an authenticated end-user or application.
            //
            RAuthentication authToken = new RBasicAuthentication("testuser", "changeme");
            RUser rUser = rClient.login(authToken);

            //
            // 3. Logout the user
            //
            // The authenticated user is logged out from the DeployR server
            //
            rClient.logout(rUser);

        }
    }
}
