/*
 * ProjectWorkspaceOptions.cs
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
/// Options used when retreiving information about objects in a Project's workspace
/// </summary>
/// <remarks></remarks>
    public class ProjectWorkspaceOptions
    {

        private String m_alternateRoot = "";
        private String m_classFilter = "";
        private int m_pageoffset = 0;
        private int m_pagesize = 0;
        private String m_startsWithFilter = "";

        /// <summary>
        /// Workspace alternate root object
        /// </summary>
        /// <value>name of alternate root object</value>
        /// <returns>name of alternate root object</returns>
        /// <remarks></remarks>
        public String alternateRoot
        {
            get
            {
                return m_alternateRoot;
            }
            set
            {
                m_alternateRoot = value;
            }
        }

        /// <summary>
        /// Workspace class filter
        /// </summary>
        /// <value>class filter</value>
        /// <returns>class filter</returns>
        /// <remarks></remarks>
        public String classFilter
        {
            get
            {
                return m_classFilter;
            }
            set
            {
                m_classFilter = value;
            }
        }

        /// <summary>
        /// Workspace page offset
        /// </summary>
        /// <value>page offset</value>
        /// <returns>page offset</returns>
        /// <remarks></remarks>
        public int pageoffset
        {
            get
            {
                return m_pageoffset;
            }
            set
            {
                m_pageoffset = value;
            }
        }

        /// <summary>
        /// Workspace page size
        /// </summary>
        /// <value>page size</value>
        /// <returns>page size</returns>
        /// <remarks></remarks>
        public int pagesize
        {
            get
            {
                return m_pagesize;
            }
            set
            {
                m_pagesize = value;
            }
        }

        /// <summary>
        /// Workspace starts with filter
        /// </summary>
        /// <value>starts with filter</value>
        /// <returns>starts with filter</returns>
        /// <remarks></remarks>
        public String startsWithFilter
        {
            get
            {
                return m_startsWithFilter;
            }
            set
            {
                m_startsWithFilter = value;
            }
        }

    }
}