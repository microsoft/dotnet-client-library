/*
 * AuthProjectExecuteScript.cs
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
    public class AuthProjectExecuteScript
    {
        public static RProjectExecution exec;
        public static String console;
        public static List<RProjectResult> plots;
        public static List<RProjectFile> files;
        public static List<RData> objects;

        static public void Execute()
        {
            Console.WriteLine("AuthProjectExecuteScript - start");

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

            Console.WriteLine("AuthProjectExecuteScript: created temporary R session, rProject=" + rProject);

            //
            // 4. Execute an analytics Web service based on a repository-managed
            // R script: /testuser/root/Histogram of Auto Sales.R.
            //
            // Optionally:
            // ProjectExecutionOptions options = new ProjectExecutionOptions();
            //
            // Populate options as needed, then:
            //
            // exec = rProject.executeScript(filename, directory, author, version, options);
            //
            exec = rProject.executeScript("Histogram of Auto Sales",
                                            "root",
                                            "testuser", 
                                            "", 
                                            null);

            Console.WriteLine("AuthProjectExecuteScript: repository-managed script execution completed, exec=" + exec);

            //
            // 5. Retrieve code execution results.
            //
            console = exec.about().console;
            plots = exec.about().results;
            files = exec.about().artifacts;
            objects = exec.about().workspaceObjects;

            //
            //  6. Cleanup
            //
            rProject.close();
            Utility.Cleanup(rUser, rClient);

            Console.WriteLine("AuthProjectExecuteScript - end");

        }
    }
}
