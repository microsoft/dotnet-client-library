/*
 * ProjectCloseOptions.cs
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
using System.Net;
using System.Web;
using System.Text;


namespace DeployR
{
/// <summary>
/// Options used when closing Project
/// </summary>
/// <remarks></remarks>
    public class ProjectCloseOptions
    {

        private String m_cookie = "";
        private Boolean m_disableAutosave = false;
        private ProjectDropOptions m_dropOptions = new ProjectDropOptions();
        private Boolean m_flushHistory = false;

        /// <summary>
        /// Project Cookie
        /// </summary>
        /// <value>project cookie</value>
        /// <returns>project cookie</returns>
        /// <remarks></remarks>
        public String cookie
        {
            get
            {
                return m_cookie;
            }
            set
            {
                m_cookie = value;
            }
        }

        /// <summary>
        /// Disable project autosave
        /// </summary>
        /// <value>flag indicating to disable project autosave</value>
        /// <returns>flag indicating to disable project autosave</returns>
        /// <remarks></remarks>
        public Boolean disableAutosave
        {
            get
            {
                return m_disableAutosave;
            }
            set
            {
                m_disableAutosave = value;
            }
        }

        /// <summary>
        /// Project Drop Options
        /// </summary>
        /// <value>drop options</value>
        /// <returns>drop options</returns>
        /// <remarks></remarks>
        public ProjectDropOptions dropOptions
        {
            get
            {
                return m_dropOptions;
            }
            set
            {
                m_dropOptions = value;
            }
        }

        /// <summary>
        /// Flush project history
        /// </summary>
        /// <value>flag indicating to flush project history</value>
        /// <returns>flag indicating to flush project history</returns>
        /// <remarks></remarks>
        public Boolean flushHistory
        {
            get
            {
                return m_flushHistory;
            }
            set
            {
                m_flushHistory = value;
            }
        }


    }
}