/*
 * AnonymousProjectExecutionOptions.cs
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
/// Options used when executing an anonymous script on a Project
/// </summary>
/// <remarks></remarks>
    public class AnonymousProjectExecutionOptions : ProjectExecutionOptions
    {

        private Boolean m_blackbox = false;
        private Boolean m_recycle = false;
        private String m_gridCluster = "";

        /// <summary>
        /// Enable noproject option if project persistence following
        /// job execution is not required. Typically used when
        /// ProjectStorageOptions have already been specified.
        /// </summary>
        /// <value>no project value</value>
        /// <returns>no project value</returns>
        /// <remarks></remarks>
        public Boolean blackbox
        {
            get
            {
                return m_blackbox;
            }
            set
            {
                m_blackbox = value;
            }
        }

        /// <summary>
        /// Enable noproject option if project persistence following
        /// job execution is not required. Typically used when
        /// ProjectStorageOptions have already been specified.
        /// </summary>
        /// <value>no project value</value>
        /// <returns>no project value</returns>
        /// <remarks></remarks>
        public Boolean recycle
        {
            get
            {
                return m_recycle;
            }
            set
            {
                m_recycle = value;
            }
        }

        ///<summary>
        /// Identifies the DeployR grid cluster where the caller would
        /// like the job (R session) to execute. If there are no slots
        /// available on any of the grid nodes within the cluster indicated
        /// then the server will attempt to execute the job on a slot
        /// on an available grid node that supports MIXED-operations. If no
        /// slot meeting these criteria is found, the job will be queued
        /// until a suitable slot becomes available. This feature is optional
        /// and available on DeployR Enterprise only.
        /// </summary>
        /// <value>gridCluster value</value>
        /// <returns>gridCluster value</returns>
        /// <remarks></remarks>
        public String gridCluster
        {
            get
            {
                return m_gridCluster;
            }
            set
            {
                m_gridCluster = value;
            }
        }

    }
}