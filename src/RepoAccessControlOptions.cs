/*
 * RepoAccessControlOptions.cs
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
/// Options used when setting access contorls on files in the Repository
/// </summary>
/// <remarks></remarks>
    public class RepoAccessControlOptions
    {

        private String m_restricted = "";
        private Boolean m_sharedUser = false;
        private Boolean m_published = false;

        /// <summary>
        /// Repository file to be restricted to comma-separated list of Roles on upload
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
        /// Repository file to be shared on upload
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
        /// Repository file to be public on upload
        /// </summary>
        /// <value>public flag</value>
        /// <returns>public flag</returns>
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

    }
}