/*
 * AuthProjectWorkspace.cs
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
    public class AuthProjectWorkspace
    {
        static public void Execute()
        {

            Console.WriteLine("AuthProjectWorkspace - start");

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

            Console.WriteLine("AuthProjectWorkspace: created temporary " +
                                        "R session, rProject=" + rProject);
            
            //
            // 4. Execute a block of R code to create an object
            // in the R session's workspace.
            //
            String rCode = "x <- T";
            RProjectExecution exec = rProject.executeCode(rCode);

            //
            // 5. Retrieve the object "x" from the R session's workspace.
            //
            RData encodedX = rProject.getObject("x");
            if(encodedX is RBoolean) 
            {
                Console.WriteLine("retrieved object x from workspace, x=" + (Boolean)encodedX.Value);
            }

            //
            // 6. Create R object data in the R sesssion's workspace
            // by pushing DeployR-encoded data from the client application.
            //
            // - Prepare sample R object vector data.
            // - Use RDataFactory to encode the sample R object vector data.
            // - Push encoded R object into the workspace.
            //
            List<Double?> vectorValues = new List<Double?>();
            vectorValues.Add(10.0);
            vectorValues.Add(11.1);
            vectorValues.Add(12.2);
            vectorValues.Add(13.3);
            vectorValues.Add(14.4);

            RData encodedY = RDataFactory.createNumericVector("y", vectorValues);
            rProject.pushObject(encodedY);

            //
            // 7. Retrieve the DeployR-encoding of the R object
            // from the R session's workspace.
            //
            encodedY = rProject.getObject("y");

            if(encodedY is RNumericVector) {
                List<Double?> numVectorValues = (List<Double?>)encodedY.Value;
                StringBuilder str = new StringBuilder();
                foreach (Double? val in numVectorValues)
                {
                    str.Append(val + " ");
                }
                Console.WriteLine("retrieved object y from workspace, encodedY=" + str.ToString());
            }

            //
            // 8. Retrieve a list of R objects in the R session's workspace.
            //
            // Optionally:
            // ProjectWorkspaceOptions options = new ProjectWorkspaceOptions();
            //
            // Populate options as needed, then:
            //
            // objs = rProject.listObjects(options);
            //
            ///
            List<RData> objs = rProject.listObjects();            
            
            //
            // 9. Cleanup
            //
            rProject.close();
            Utility.Cleanup(rUser, rClient);

            Console.WriteLine("AuthProjectWorkspace - end");

        }
    }
}
