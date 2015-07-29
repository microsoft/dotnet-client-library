/*
 * AuthJobStoreResultToRepository.cs
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
    public class AuthJobStoreResultToRepository
    {
        static public void Execute()
        {

            Console.WriteLine("AuthJobStoreResultToRepository - start");

            // 
            // 1. Connect to the DeployR Server
            //
            RClient rClient = Utility.Connect();

            // 
            // 2. Authenticate the user
            //
            RUser rUser = Utility.Authenticate(rClient);

            //
            // 3. Submit a background job to execute a block of R code
            // that will generate a vector object. We will then use
            // JobExecutionOtpions to request the following behavior:
            //
            // a. Execute the job at low (default) priority.
            // b. Skip the persistence of results to a project.
            // c. Request the persistence of a named vector object
            // as a binary R object file to the repository.
            //

            String rCode = "tutorialVector <- rnorm(100)";

            JobExecutionOptions options = new JobExecutionOptions();
            options.priority = JobExecutionOptions.LOW_PRIORITY;
            options.noProject = true;
            
            //
            // 4. Use ProjectStorageOptions to identify the name of one
            // or more workspace objects for storage to the repository.
            //
            // In this case, the named object matches the name of the
            // object created the rCode being executed on the job. We
            // request that the binary R object file be stored into
            // the root directory in the repository.
            //
            options.storageOptions = new ProjectStorageOptions();
            options.storageOptions.objects = "tutorialVector";
            options.storageOptions.directory = "root";

            RJob rJob = rUser.submitJobCode("Background Code Execution",
                                            "Demonstrate storing job results to repository.",
                                            rCode, 
                                            options);

            Console.WriteLine("AuthJobStoreResultToRepository: submitted background job " +
                                        "for execution, rJob=" + rJob);

            //
            // 5. Query the execution status of a background job and loop until the job has finished
            //
            if (rJob != null)
            {
                while (true)
                {
                    String sMsg = rJob.query().status.Value;

                    if (sMsg == RJob.Status.COMPLETED.Value |
                        sMsg == RJob.Status.FAILED.Value |
                        sMsg == RJob.Status.CANCELLED.Value |
                        sMsg == RJob.Status.ABORTED.Value)
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
            // 6. Retrieve the RepositoryFile from completed job
            //
            RRepositoryFile rRepositoryFile = null;
            if (rJob != null)
            {
                //
                // 7. Retrieve the results of the background job
                // execution. Given the custom JobExecutionOptions
                // specified for this job (noproject=true) there will
                // be no RProject to hold the results.
                //
                // However, the custom JobExecutionOptions specified
                // for this job requested the storage of the 
                // "tutorialVector" vector object as a binary R
                // object file (.rData) in the repository.
                //
                // The following code demonstrates how we can
                // retrieve that result from the repository:
                //
                String sUserName = rUser.about().Username;
                rRepositoryFile = rUser.fetchFile("tutorialVector.rData",
                                                    sUserName,
                                                    "root",
                                                    "");

                Console.WriteLine("AuthJobStoreResultToRepository: retrieved background " +
                        "job result from repository, rRepositoryFile=" + rRepositoryFile);
            }

            //
            //  8. Cleanup
            //
            if (rRepositoryFile != null)
            {
                //rRepositoryFile.delete();  //un-comment if you wish to delete the RepositoryFile
            }

            if (rJob != null)
            {
                //rJob.delete();  //un-comment if you wish to delete the job
            }

            Utility.Cleanup(rUser, rClient);

            Console.WriteLine("AuthJobStoreResultToRepository - end");
        }
    }
}
