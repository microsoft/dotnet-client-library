/*
 * AuthProjectPoolCreate.cs
 *
 * Copyright (C) 2010-2014 by Revolution Analytics Inc.
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

namespace Project
{
    public class AuthProjectPoolCreate
    {
        static public void Execute()
        {
            Console.WriteLine("AuthProjectPoolCreate - start");

            // 
            // 1. Connect to the DeployR Server
            //
            RClient rClient = Utility.Connect();

            // 
            // 2. Authenticate the user
            //
            RUser rUser = Utility.Authenticate(rClient);

            //
            // 3. Create a pool of temporary projects (R sessions).
            //
            // Population options as needed.
            //
            int requestedPoolSize = 4;
            ProjectCreationOptions options = new ProjectCreationOptions();
            List<RProject> pool = rUser.createProjectPool(requestedPoolSize, options);

            Console.WriteLine("AuthProjectPoolCreate: created pool of " +
                    pool.Count + " temporary R sessions, pool=" + pool);


            //
            //  5. Cleanup
            //
            foreach(RProject rProject in pool)
            {
                rProject.close();
            }

            Utility.Cleanup(rUser, rClient);

            Console.WriteLine("AuthProjectPoolCreate - end");
        }
    }
}
