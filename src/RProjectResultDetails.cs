/*
 * RProjectResultDetails.cs
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
/// Represents the details of a Result contained in a code execution on a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectResultDetails
    {

        private String m_execution = "";
        private String m_name = "";
        private int m_size = 0;
        private String m_type = "";
        private String m_url = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectResultDetails()
        {

        }

        internal RProjectResultDetails(String execution, String name, int size, String type, String url)
        {

            m_execution = execution;
            m_name = name;
            m_size = size;
            m_type = type;
            m_url = url;

        }
        /// <summary>
        /// Project execution identifier
        /// </summary>
        /// <returns>String containing the execution identifier</returns>
        /// <remarks></remarks>
        public String execution
        {
            get
            {
                return m_execution;
            }
        }

        /// <summary>
        /// Name of the project execution result file
        /// </summary>
        /// <returns>String containing the name</returns>
        /// <remarks></remarks>
        public String name
        {
            get
            {
                return m_name;
            }
        }

        /// <summary>
        /// Size of the project execution result file
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
        /// MIME type of the project execution result file
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
        /// URL of the project execution result file
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

    }
}