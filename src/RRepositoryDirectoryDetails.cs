/*
 * RRepositoryDirectoryDetails.cs
 *
 * Copyright (C) 2010-2015 by Microsoft Corporation
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

namespace DeployR
{
/// <summary>
/// Details of a directory contained in the repository
/// </summary>
/// <remarks></remarks>
    public class RRepositoryDirectoryDetails
    {
        private String m_name = "";
        private Boolean m_systemDirectory = false;
        private List<RRepositoryFile> m_files;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RRepositoryDirectoryDetails()
        {

        }

        internal RRepositoryDirectoryDetails(String name, Boolean systemDirectory, List<RRepositoryFile> files)
        {

            m_name = name;
            m_systemDirectory = systemDirectory;
            m_files = files;


        }

        /// <summary>
        /// Repository directory name
        /// </summary>
        /// <returns>String containing the directory name</returns>
        /// <remarks></remarks>
        public String name
        {
            get
            {
                return m_name;
            }
        }

        /// <summary>
        /// Repository directory type: system or user
        /// </summary>
        /// <returns>indicator if the directory is a system directory</returns>
        /// <remarks></remarks>
        public Boolean systemDirectory
        {
            get
            {
                return m_systemDirectory;
            }
        }

        /// <summary>
        /// List of files in the directory
        /// </summary>
        /// <returns>list of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> files
        {
            get
            {
                return m_files;
            }
        }

    }
}