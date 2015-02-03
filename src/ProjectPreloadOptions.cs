/*
 * ProjectPreloadOptions.cs
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
/// Options used to specify Repository objects that should be preloaded into a Project
/// </summary>
/// <remarks></remarks>
    public class ProjectPreloadOptions
    {

        private String m_filename = "";
        private String m_directory = "";
        private String m_author = "";
        private String m_version = "";

        /// <summary>
        /// Comma-separated list of repository filenames
        /// </summary>
        /// <value>file name</value>
        /// <returns>file name</returns>
        /// <remarks></remarks>
        public String filename
        {
            get
            {
                return m_filename;
            }
            set
            {
                m_filename = value;
            }
        }

        /// <summary>
        /// Comma-separated list of repository directory names
        /// </summary>
        /// <value>directory name</value>
        /// <returns>directory name</returns>
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

        /// <summary>
        /// Comma-separated list of authors, one author per filename
        /// </summary>
        /// <value>repository file author</value>
        /// <returns>repository file author</returns>
        /// <remarks></remarks>
        public String author
        {
            get
            {
                return m_author;
            }
            set
            {
                m_author = value;
            }
        }

        /// <summary>
        /// Optional, comma-separated list of versions, one version per filename
        /// </summary>
        /// <value>file version</value>
        /// <returns>file version</returns>
        /// <remarks></remarks>
        public String version
        {
            get
            {
                return m_version;
            }
            set
            {
                m_version = value;
            }
        }

    }
}