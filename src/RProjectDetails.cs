/*
 * RProjectDetails.cs
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
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeployR
{

/// <summary>
/// Details of a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectDetails
    {

        private String m_cookie = "";
        private String m_descr = "";
        private String m_id = "";
        private Boolean m_live = false;
        private String m_longdescr = "";
        private String m_modified = "";
        private String m_name = "";
        private String m_origin = "";
        private Boolean m_sharedUsers = false;
        private List<String> m_authors;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectDetails()
        {

        }

        internal RProjectDetails(String cookie, String descr, String id, Boolean live, String longdescr, String modified, String name, String origin, Boolean sharedUsers, List<String> authors)
        {

            m_cookie = cookie;
            m_descr = descr;
            m_id = id;
            m_live = live;
            m_longdescr = longdescr;
            m_modified = modified;
            m_name = name;
            m_origin = origin;
            m_sharedUsers = sharedUsers;
            m_authors = authors;

        }

        /// <summary>
        /// Custom cookied for the project
        /// </summary>
        /// <returns>Cookie</returns>
        /// <remarks></remarks>
        public String cookie
        {
            get
            {
                return m_cookie;
            }
        }

        /// <summary>
        /// Short description for the project
        /// </summary>
        /// <value>Description String</value>
        /// <returns>Description String</returns>
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
        /// Project id
        /// </summary>
        /// <returns>id String</returns>
        /// <remarks></remarks>
        public String id
        {
            get
            {
                return m_id;
            }
        }

        /// <summary>
        /// Indicatator if project is live
        /// </summary>
        /// <returns>boolean indicating project is live</returns>
        /// <remarks></remarks>
        public Boolean islive
        {
            get
            {
                return m_live;
            }
        }

        /// <summary>
        /// Long description of project
        /// </summary>
        /// <value>long description String</value>
        /// <returns>long description String</returns>
        /// <remarks></remarks>
        public String longdescr
        {
            get
            {
                return m_longdescr;
            }
            set
            {
                m_longdescr = value;
            }
        }

        /// <summary>
        /// Last modified date of project
        /// </summary>
        /// <returns>modifed date</returns>
        /// <remarks></remarks>
        public String modified
        {
            get
            {
                return m_modified;
            }
        }

        /// <summary>
        /// Project name
        /// </summary>
        /// <value>Name</value>
        /// <returns>Name</returns>
        /// <remarks></remarks>
        public String name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// Project origin
        /// </summary>
        /// <returns>origin String</returns>
        /// <remarks></remarks>
        public String origin
        {
            get
            {
                return m_origin;
            }
        }

        /// <summary>
        /// Indicator if project is shared
        /// </summary>
        /// <value>boolean indicating project is shared</value>
        /// <returns>boolean indicating project is shared</returns>
        /// <remarks></remarks>
        public Boolean sharedUsers
        {
            get
            {
                return m_sharedUsers;
            }
            set
            {
                m_sharedUsers = value;
            }
        }

        /// <summary>
        /// Project authors
        /// </summary>
        /// <returns>list of project authors</returns>
        /// <remarks></remarks>
        public List<String> authors
        {
            get
            {
                return m_authors;
            }
        }
    }
}