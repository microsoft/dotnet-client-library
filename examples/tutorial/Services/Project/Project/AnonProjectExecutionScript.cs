/*
 * AnonProjectExecutionScript.cs
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
    class AnonProjectExecutionScript
    {
        public static RScriptExecution exec;
        public static String console;
        public static List<RProjectResult> plots;
        public static List<RProjectFile> files;
        public static List<RData> objects;

        static public void Execute()
        {
            Console.WriteLine("AnonProjectExecuteScript - start");
            
            // 
            // 1. Connect to the DeployR Server
            //
            RClient rClient = Utility.Connect();

            //
            // 2. Execute a public analytics Web service as an anonymous
            // user based on a repository-managed R script:
            // /testuser/root/Histogram of Auto Sales.R.
            //
            // Optionally:
            // AnonymousProjectExecutionOptions options = new AnonymousProjectExecutionOptions();
            //
            // Populate options as needed, then:
            //
            // RScriptExecution exec =
            // rClient.executeScript(filename, directory, author, version, options);
            //
            exec = rClient.executeScript("Histogram of Auto Sales",
                                            "root", 
                                            "testuser", 
                                            "",
                                            null);

            Console.WriteLine("AnonProjectExecuteScript: public repository-managed " +
                    "script execution completed, exec=" + exec);

            //
            // 3. Retrieve script execution results.
            //
            console = exec.about().console;
            plots = exec.about().results;
            files = exec.about().artifacts;
            objects = exec.about().workspaceObjects;

            Console.WriteLine("AnonProjectExecuteScript - end");

        }

    }
}
