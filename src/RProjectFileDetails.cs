/*
 * RProjectFileDetails.cs
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
/// Details of a File in a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectFileDetails
    {

        private String m_descr = "";
        private String m_filename = "";
        private int m_size = 0;
        private String m_type = "";
        private String m_url = "";
        private String m_category = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectFileDetails()
        {

        }

        internal RProjectFileDetails(String descr, String filename, int size, String type, String url, String category)
        {

            m_descr = descr;
            m_filename = filename;
            m_size = size;
            m_type = type;
            m_url = url;
            m_category = category;


        }

        /// <summary>
        /// Description of the project file
        /// </summary>
        /// <returns>String containing the description</returns>
        /// <remarks></remarks>
        public String descr
        {
            get
            {
                return m_descr;
            }
        }

        /// <summary>
        /// Name of the project file
        /// </summary>
        /// <returns>String containing the file name</returns>
        /// <remarks></remarks>
        public String filename
        {
            get
            {
                return m_filename;
            }
        }

        /// <summary>
        /// Size of the project file
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
        /// MIME type of the project file
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
        /// URL of the project file
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
        /// category of the project file
        /// </summary>
        /// <returns>String containing the category of the file</returns>
        /// <remarks></remarks>
        public String category
        {
            get
            {
                return m_category;
            }
        }

    }
}