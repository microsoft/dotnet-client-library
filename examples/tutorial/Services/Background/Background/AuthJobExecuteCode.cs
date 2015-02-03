/*
 * AuthJobExecuteCode.cs
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
using System.Threading;
using DeployR;

namespace Background
{
    public class AuthJobExecuteCode
    {
        static public void Execute()
        {

            Console.WriteLine("AuthJobExecuteCode - start");

            // 
            // 1. Connect to the DeployR Server
            //
            RClient rClient = Utility.Connect();

            // 
            // 2. Authenticate the user
            //
            RUser rUser = Utility.Authenticate(rClient);

            //
            // 3. Submit a background job for execution based on an 
            // arbitrary block of R code: [codeBlock]
            //
            String codeBlock = "demo(graphics)";
            
            JobExecutionOptions options = new JobExecutionOptions();
            options.priority = JobExecutionOptions.MEDIUM_PRIORITY;   //Make this a Medium Priority job
            
            RJob rJob = rUser.submitJobCode("Sample Job", 
                                        "Sample description.", 
                                        codeBlock, 
                                        options);

            Console.WriteLine("AuthJobExecuteCode: submitted background job for execution, rJob=" + rJob);
            
            //
            // 4. Query the execution status of a background job and loop until the job has finished
            //
            if (rJob != null)
            {
                while (true)
                {
                    RJob.Status status = rJob.query().status;

                    if (status == RJob.Status.COMPLETED |
                        status == RJob.Status.FAILED |
                        status == RJob.Status.CANCELLED |
                        status == RJob.Status.ABORTED)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            }


            //
            // 5. Retrieve the project from completed job
            //
            RProject rProject = null;
            if (rJob != null)
            {
                // make sure we have a valid project id
                if (rJob.query().project.Length > 0)
                {
                    //get the project using the project id
                    rProject = rUser.getProject(rJob.query().project);
                
                    Console.WriteLine("AuthJobExecuteCode: retrieved background " +
                            "job result on project, rProject=" + rProject);
                }
            }

            //
            //  6. Cleanup
            //
            if (rProject != null)
            {
                rProject.close();
                //rProject.delete();  //un-comment if you wish to delete the project
            }

            if (rJob != null)
            {
                //rJob.delete();  //un-comment if you wish to delete the job
            }

            Utility.Cleanup(rUser, rClient);

            Console.WriteLine("AuthJobExecuteCode - end");
        }
   }
}
