/*
 * AuthRepositoryManagement.cs
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

namespace Repository
{
    public class AuthRepositoryManagement
    {
        static public void Execute()
        {
            Console.WriteLine("AuthRepositoryManagement - start");

            // 
            // 1. Connect to the DeployR Server
            //
            RClient rClient = Utility.Connect();

            // 
            // 2. Authenticate the user
            //
            RUser rUser = Utility.Authenticate(rClient);

            //
            // 3. Create a file in the authenticated user's private
            // repository and set shared access on the file so
            // other authenticated users can access the file.
            //
            RepoUploadOptions options = new RepoUploadOptions();
            options.filename = "hello.txt";
            options.sharedUser = true;
            String fileContent = "Hello World!";
            RRepositoryFile rRepositoryFile = rUser.writeFile(fileContent, options);

            //
            // 4. Download working directory file content using 
            // standard Java URL.
            //
            String fileURL = rRepositoryFile.download();

            //
            // 5. Retrieve a list of files in the authenticated user's
            // private repository.
            //
            List<RRepositoryFile> files = rUser.listFiles();

            foreach(RRepositoryFile file in files) {
                Console.WriteLine("AuthRepositoryManagement: private repository, " +
                        "found file name=" +
                        file.about().filename + ", directory="  +
                        file.about().directory + ", access=" +
                        file.about().access);
            }

            //
            //  6. Cleanup
            //
            Utility.Cleanup(rUser, rClient);

            Console.WriteLine("AuthRepositoryManagement - end");

        }
    }
}
