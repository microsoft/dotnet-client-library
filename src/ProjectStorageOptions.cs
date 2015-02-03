/*
 * ProjectStorageOptions.cs
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

namespace DeployR
{
/// <summary>
/// Project post-execution repository storage options.
/// </summary>
/// <remarks></remarks>
    public class ProjectStorageOptions
    {

        private String m_files = "";
        private String m_objects = "";
        private String m_workspace = "";
        private Boolean m_newVersion = false;
        private Boolean m_published = false;
        private String m_directory = "";

        /// <summary>
        /// Comma-separated list of working directory files to be stored
        /// in the repository following an execution.
        /// </summary>
        /// <value>String specifying working directory files to be stored</value>
        /// <returns>String specifying working directory files to be stored</returns>
        /// <remarks></remarks>
        public String files
        {
            get
            {
                return m_files;
            }
            set
            {
                m_files = value;
            }
        }

        /// <summary>
        /// Comma-separated list of workspace objects to be stored in the
        /// repository following an execution.
        /// </summary>
        /// <value>String specifying list of R objects to be stored</value>
        /// <returns>String specifying list of R objects to be stored</returns>
        /// <remarks></remarks>
        public String objects
        {
            get
            {
                return m_objects;
            }
            set
            {
                m_objects = value;
            }
        }

        /// <summary>
        /// Specify a filename and the contents of the entire workspace will
        /// be saved as filename.rData in the repository.
        /// </summary>
        /// <value>String containing filename for the workspace</value>
        /// <returns>tring containing filename for the workspace</returns>
        /// <remarks></remarks>
        public String workspace
        {
            get
            {
                return m_workspace;
            }
            set
            {
                m_workspace = value;
            }
        }

        /// <summary>
        /// Enable to create new version of files being stored in the
        /// repository following an execution. Default behavior is to overwrite
        /// files that already exist in the repository.
        /// </summary>
        /// <value>flag to indicate that a new version of the files should be stored in the repository</value>
        /// <returns>flag that indicates if a new version of the files will be stored in the repository</returns>s
        /// <remarks></remarks>
        public Boolean newVersion
        {
            get
            {
                return m_newVersion;
            }
            set
            {
                m_newVersion = value;
            }
        }

        /// <summary>
        /// Enable to assign public access on stored files in the
        /// repository following an execution. By default private access is
        /// assigned to files being stored in the repository.
        /// </summary>
        /// <value>flag to indicate public access should be allowed on the stored files</value>
        /// <returns>flag that indicates if public access is allowed on the stored files</returns>
        /// <remarks></remarks>
        public Boolean published
        {
            get
            {
                return m_published;
            }
            set
            {
                m_published = value;
            }
        }


        /// <summary>
        /// Specify a directory into which each of the stored files, objects and/or workspace will be saved in the repository.
        /// </summary>
        /// <value>directory name to store files, objects and/or workspace</value>
        /// <returns>directory name to store files, objects and/or workspace</returns>
        /// <remarks></remarks>
        public String directory
        {
            get
            {
                return m_directory;
            }
            set
            {
                m_directory = value;
            }
        }

    }
}