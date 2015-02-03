/*
 * AnonProjectExecScriptInOut.cs
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
    class AnonProjectExecScriptInOut
    {
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
            // /testuser/root/DeployR - Hello World.R
            //
            // Create the AnonymousProjectExecutionOptions object 
            // to specify inputs and output to the script
            // The R object that is an input to the script is 'input_randomNum'
            // The R object that we want to retrieve after script execution is 'x'
            //
            AnonymousProjectExecutionOptions options = new AnonymousProjectExecutionOptions();
            options.rinputs.Add(RDataFactory.createNumeric("input_randomNum", 100));
            options.routputs.Add("x");

            RScriptExecution exec = rClient.executeScript("DeployR - Hello World",
                                                            "root",
                                                            "testuser",
                                                            "",
                                                            options);

            Console.WriteLine("AnonProjectExecuteScript: public repository-managed " +
                    "script execution completed, exec=" + exec);

            //
            // 3. Retrieve script execution results.
            //
            String console = exec.about().console;
            List<RProjectResult> plots = exec.about().results;
            List<RProjectFile> files = exec.about().artifacts;
            List<RData> objects = exec.about().workspaceObjects;

            RNumericVector xVec = (RNumericVector)objects[0];

            Console.WriteLine("AnonProjectExecuteScript - end");

        }
    }
}
