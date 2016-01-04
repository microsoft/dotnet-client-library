/*
 * DirectoryUploadOptions.cs
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

namespace DeployR
{

/// <summary>
/// Options used when uploading a file to the working directory for a Project
/// </summary>
/// <remarks></remarks>
    public class DirectoryUploadOptions
    {

        private String m_descr = "";
        private String m_filename = "";
        private Boolean m_overwrite = false;

        /// <summary>
        /// Project directory file description
        /// </summary>
        /// <value>description</value>
        /// <returns>description</returns>
        /// <remarks></remarks>
        public String descr
        {
            get
            {
                return m_descr;
            }
            set
            {
                m_descr = value;
            }
        }

        /// <summary>
        /// Project directory file name
        /// </summary>
        /// <value>name</value>
        /// <returns>name</returns>
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
        /// Flag indicating whether to overwrite an exisiting file
        /// </summary>
        /// <value>overwrite flag</value>
        /// <returns>overwrite flag</returns>
        /// <remarks></remarks>
        public Boolean overwrite
        {
            get
            {
                return m_overwrite;
            }
            set
            {
                m_overwrite = value;
            }
        }

    }
}