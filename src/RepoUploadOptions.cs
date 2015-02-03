/*
 * RepoUploadOptions.cs
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
/// Options used when uploading files to the Repository
/// </summary>
/// <remarks></remarks>
    public class RepoUploadOptions
    {

        private String m_descr = "";
        private String m_filename = "";
        private Boolean m_newversion = false;
        private String m_newversionmsg = "";
        private String m_inputs = "";
        private String m_outputs = "";
        private Boolean m_published = false;
        private Boolean m_sharedUser = false;
        private String m_restricted = "";
        private String m_directory = "";

        /// <summary>
        /// Repository file description
        /// </summary>
        /// <value>file description</value>
        /// <returns>file description</returns>
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
        /// Repository file name
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
        /// Repository file new version on upload
        /// </summary>
        /// <value>make new version flag</value>
        /// <returns>make new version flag</returns>
        /// <remarks></remarks>
        public Boolean newversion
        {
            get
            {
                return m_newversion;
            }
            set
            {
                m_newversion = value;
            }
        }

        /// <summary>
        /// Repository file new version message on upload.
        /// </summary>
        /// <value>message associated with file when a new version is created via upload</value>
        /// <returns>message associated with file when a new version is created via upload</returns>
        /// <remarks></remarks>
        public String newversionmsg
        {
            get
            {
                return m_newversionmsg;
            }
            set
            {
                m_newversionmsg = value;
            }
        }
        /// <summary>
        /// Repository file (script) inputs.
        /// </summary>
        /// <value>script inputs</value>
        /// <returns>script inputs</returns>
        /// <remarks></remarks>
        public String inputs
        {
            get
            {
                return m_inputs;
            }
            set
            {
                m_inputs = value;
            }
        }

        /// <summary>
        /// Repository file (script) outputs.
        /// </summary>
        /// <value>script outputs</value>
        /// <returns>script outputs</returns>
        /// <remarks></remarks>
        public String outputs
        {
            get
            {
                return m_outputs;
            }
            set
            {
                m_outputs = value;
            }
        }

        /// <summary>
        /// Repository file to be published on upload.
        /// </summary>
        /// <value>publish flag</value>
        /// <returns>publish flag</returns>
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
        ///  Repository file to be shared on upload.
        /// </summary>
        /// <value>shared flag</value>
        /// <returns>shared flag</returns>
        /// <remarks></remarks>
        public Boolean sharedUser
        {
            get
            {
                return m_sharedUser;
            }
            set
            {
                m_sharedUser = value;
            }
        }


        /// <summary>
        ///  Repository file to be restricted to comma-separated list of Roles on upload.
        /// </summary>
        /// <value>restricted list of users</value>
        /// <returns>restricted list of users</returns>
        /// <remarks></remarks>
        public String restricted
        {
            get
            {
                return m_restricted;
            }
            set
            {
                m_restricted = value;
            }
        }

        /// <summary>
        /// Repository directory name
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
    }
}