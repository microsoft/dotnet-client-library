/*
 * Utility.cs
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

namespace Repository
{
    public class Utility
    {
        static public RClient Connect()
        {

            //
            // Establish a connection to DeployR.
            //
            // This example assumes the DeployR server is running on localhost.
            //
            String deployrEndpoint = "http://localhost:7400/deployr";
            RClient rClient = RClientFactory.createClient(deployrEndpoint);

            //RClientFactory.setDebugMode(true); //un-comment if you want to have the API calls printed to the console

            Console.WriteLine("Client connection made to the DeployR Server: " + deployrEndpoint);

            return rClient;
        }


        static public RUser Authenticate(RClient rClient)
        {

            //
            // Authenticate an end-user or client application.
            //
            // The RBasicAuthentication supports basic username/password authentication.
            // The RUser returned represents an authenticated end-user or application.
            //
            RAuthentication authToken = new RBasicAuthentication("testuser", "changeme");
            RUser rUser = rClient.login(authToken);

            Console.WriteLine("User Authenticated: user: " + rUser.about().Username);

            return rUser;
        }

        static public void Cleanup(RUser rUser, RClient rClient)
        {
            //
            // Clenaup and logout when we are finished
            //
            if (rUser != null)
            {
                Console.WriteLine("User logged out: " + rUser.about().Username);

                rClient.logout(rUser);
            }
        }
    }
}
