/*
 * RRepositoryFileDetails.cs
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
/// Details of a file contained in the repository
/// </summary>
/// <remarks></remarks>
    public class RRepositoryFileDetails
    {

        private String m_category = "";
        private String m_filename = "";
        private String m_author = "";
        private String m_version = "";
        private String m_latestby = "";
        private String m_lastModified = "";
        private int m_size = 0;
        private String m_type = "";
        private String m_url = "";
        private Boolean m_sharedUsers = false;
        private Boolean m_published = false;
        private String m_restricted = "";
        private String m_access = "";
        private List<String> m_authors;
        private String m_inputs = "";
        private String m_outputs = "";
        private String m_directory = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RRepositoryFileDetails()
        {

        }

        internal RRepositoryFileDetails(String category, String filename, String author, String version, String latestby, String lastModified, int size, String type, String url, Boolean sharedUsers, Boolean published, String restricted, String access, List<String> authors, String inputs, String outputs, String directory)
        {

            m_category = category;
            m_filename = filename;
            m_author = author;
            m_version = version;
            m_latestby = latestby;
            m_lastModified = lastModified;
            m_size = size;
            m_type = type;
            m_url = url;
            m_sharedUsers = sharedUsers;
            m_published = published;
            m_restricted = restricted;
            m_access = access;
            m_authors = authors;
            m_inputs = inputs;
            m_outputs = outputs;
            m_directory = directory;

        }

        /// <summary>
        /// Category of the repository file
        /// </summary>
        /// <returns>String containing the category</returns>
        /// <remarks></remarks>
        public RRepositoryFile.Category category
        {
            get
            {
                return RRepositoryFile.Category.fromString(m_category);
            }
            
        }

        /// <summary>
        /// Name of the repository file
        /// </summary>
        /// <returns>String containing the name</returns>
        /// <remarks></remarks>
        public String filename
        {
            get
            {
                return m_filename;
            }
        }

        /// <summary>
        /// Repository file author
        /// </summary>
        /// <returns>String containing the author of the file</returns>
        /// <remarks></remarks>
        public String author
        {
            get
            {
                return m_author;
            }
        }

        /// <summary>
        /// Repository file version
        /// </summary>
        /// <returns>String containing the version of the file</returns>
        /// <remarks></remarks>
        public String version
        {
            get
            {
                return m_version;
            }
        }

        /// <summary>
        /// Latest author to modify repository file
        /// </summary>
        /// <returns>String containing the Latest author to modify repository file</returns>
        /// <remarks></remarks>
        public String latestby
        {
            get
            {
                return m_latestby;
            }
        }

        /// <summary>
        /// Date repository file was last modified
        /// </summary>
        /// <returns>String containing the date of last modification of the repository file</returns>
        /// <remarks></remarks>
        public String lastModified
        {
            get
            {
                return m_lastModified;
            }
        }

        /// <summary>
        /// Size of the repository file
        /// </summary>
        /// <returns>integer containing the size of the file</returns>
        /// <remarks></remarks>
        public int size
        {
            get
            {
                return m_size;
            }
        }

        /// <summary>
        /// MIME type of the repository file
        /// </summary>
        /// <returns>String containing the MIME type of the file</returns>
        /// <remarks></remarks>
        public String type
        {
            get
            {
                return m_type;
            }
        }

        /// <summary>
        /// URL of the repository file
        /// </summary>
        /// <returns>String containing the URL of the file</returns>
        /// <remarks></remarks>
        public String url
        {
            get
            {
                return m_url;
            }
        }

        /// <summary>
        /// Is repository file shared
        /// </summary>
        /// <returns>indicator if repository file is shared</returns>
        /// <remarks></remarks>
        public Boolean sharedUsers
        {
            get
            {
                return m_sharedUsers;
            }
        }

        /// <summary>
        /// Is repository file published
        /// </summary>
        /// <returns>indicator if repository file is published</returns>
        /// <remarks></remarks>
        public Boolean published
        {
            get
            {
                return m_published;
            }
        }

        /// <summary>
        /// Is repository file restricted
        /// </summary>
        /// <returns>indicator if repository file is restricted</returns>
        /// <remarks></remarks>
        public String restricted
        {
            get
            {
                return m_restricted;
            }
        }

        /// <summary>
        /// Repository file access rights
        /// </summary>
        /// <returns>repository file access rights</returns>
        /// <remarks></remarks>
        public String access
        {
            get
            {
                return m_access;
            }
        }

        /// <summary>
        /// List of inputs to the script
        /// </summary>
        /// <returns>list containing the inputs to the script</returns>
        /// <remarks></remarks>
        public String inputs
        {
            get
            {
                return m_inputs;
            }
        }

        /// <summary>
        /// Script authors
        /// </summary>
        /// <returns>list of script authors</returns>
        /// <remarks></remarks>
        public List<String> authors
        {
            get
            {
                return m_authors;
            }
        }

        /// <summary>
        /// List of outputs from the script
        /// </summary>
        /// <returns>list containing the outputs of the script</returns>
        /// <remarks></remarks>
        public String outputs
        {
            get
            {
                return m_outputs;
            }
        }

        /// <summary>
        /// Name of the directory containing the repository file
        /// </summary>
        /// <returns>Name of the directory containing the repository file</returns>
        /// <remarks></remarks>
        public String directory
        {
            get
            {
                return m_directory;
            }
        }

    }
}