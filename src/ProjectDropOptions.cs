/*
 * ProjectDropOptions.cs
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
/// Options used when saving a Project
/// </summary>
/// <remarks></remarks>
    public class ProjectDropOptions
    {

        private Boolean m_dropDirectory = false;
        private Boolean m_dropHistory = false;
        private Boolean m_dropWorkspace = false;

        /// <summary>
        /// Drop project directory
        /// </summary>
        /// <value>flag indicating whether to drop the contents of the working directory for a project</value>
        /// <returns>flag indicating whether to drop the contents of the working directory for a project</returns>
        /// <remarks></remarks>
        public Boolean dropDirectory
        {
            get
            {
                return m_dropDirectory;
            }
            set
            {
                m_dropDirectory = value;
            }
        }

        /// <summary>
        /// Drop project history
        /// </summary>
        /// <value>flag indicating whether to drop the history for a project</value>
        /// <returns>flag indicating whether to drop the history for a project</returns>
        /// <remarks></remarks>
        public Boolean dropHistory
        {
            get
            {
                return m_dropHistory;
            }
            set
            {
                m_dropHistory = value;
            }
        }

        /// <summary>
        /// Drop project workspace
        /// </summary>
        /// <value>flag indicating whether to drop the workspace for a project</value>
        /// <returns>flag indicating whether to drop the workspace for a project</returns>
        /// <remarks></remarks>
        public Boolean dropWorkspace
        {
            get
            {
                return m_dropWorkspace;
            }
            set
            {
                m_dropWorkspace = value;
            }
        }

    }
}