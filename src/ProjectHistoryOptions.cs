/*
 * ProjectHistoryOptions.cs
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
/// Options used when retreving a Project's history
/// </summary>
/// <remarks></remarks>
    public class ProjectHistoryOptions
    {

        private int m_depthFilter = 0;
        private Boolean m_reversed = false;
        private String m_tagfilter = "";

        /// <summary>
        /// History depth filter
        /// </summary>
        /// <value>depth filter value</value>
        /// <returns>depth filter value</returns>
        /// <remarks></remarks>
        public int depthFilter
        {
            get
            {
                return m_depthFilter;
            }
            set
            {
                m_depthFilter = value;
            }
        }

        /// <summary>
        /// Reverse history chronology
        /// </summary>
        /// <value>history chronology</value>
        /// <returns>history chronology</returns>
        /// <remarks></remarks>
        public Boolean reversed
        {
            get
            {
                return m_reversed;
            }
            set
            {
                m_reversed = value;
            }
        }

        /// <summary>
        /// History tag filter
        /// </summary>
        /// <value>filter</value>
        /// <returns>filter</returns>
        /// <remarks></remarks>
        public String tagfilter
        {
            get
            {
                return m_tagfilter;
            }
            set
            {
                m_tagfilter = value;
            }
        }
    }
}