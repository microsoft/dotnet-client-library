/*
 * AuthProjectCreate.cs
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
    public class AuthProjectCreate
    {
        static public void Execute()
        {
            Console.WriteLine("AuthProjectCreate - start");

            // 
            // 1. Connect to the DeployR Server
            //
            RClient rClient = Utility.Connect();

            // 
            // 2. Authenticate the user
            //
            RUser rUser = Utility.Authenticate(rClient);

            //
            //  3. Create a temporary project (R session).
            //
            //  Optionally:
            //  ProjectCreationOptions options = new ProjectCreationOptions();
            //
            //  Populate options as needed, then:
            //
            //  rProject = rUser.createProject(options);
            //
            RProject rProject = rUser.createProject();

            Console.WriteLine("AuthProjectCreate: created temporary R session, rProject=" + rProject);

            //
            //  4. Cleanup
            //
            rProject.close();
            Utility.Cleanup(rUser, rClient);

            Console.WriteLine("AuthProjectCreate - end");

        }
    }
}
