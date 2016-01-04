/*
 * RProjectPackageDetails.cs
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
/// Details of an RPackage in a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectPackageDetails
    {

        private String m_descr = "";
        private String m_name = "";
        private String m_repo = "";
        private String m_status = "";
        private String m_version = "";
        private Boolean m_attached = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectPackageDetails()
        {

        }

        internal RProjectPackageDetails(String descr, String name, String repo, String status, String version, Boolean attached)
        {

            m_descr = descr;
            m_name = name;
            m_repo = repo;
            m_status = status;
            m_version = version;
            m_attached = attached;

        }

        /// <summary>
        /// R Package description
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
        /// R Package name
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
        /// R Package repository
        /// </summary>
        /// <returns>String containing the repository name</returns>
        /// <remarks></remarks>
        public String repo
        {
            get
            {
                return m_repo;
            }
        }

        /// <summary>
        /// R Package status
        /// </summary>
        /// <returns>String containing the status of the package</returns>
        /// <remarks></remarks>
        public String status
        {
            get
            {
                return m_status;
            }
        }

        /// <summary>
        /// R Package version
        /// </summary>
        /// <returns>String containing the version of the package</returns>
        /// <remarks></remarks>
        public String version
        {
            get
            {
                return m_version;
            }
        }

        /// <summary>
        /// R Package description
        /// </summary>
        /// <returns>boolean indicating if package is attached to the current project</returns>
        /// <remarks></remarks>
        public Boolean attached
        {
            get
            {
                return m_attached;
            }
        }

    }
}