/*
 * RUserDetails.cs
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
using System.Net;
using System.Web;
using System.Text;


namespace DeployR
{
/// <summary>
/// Details of an authenticated User
/// </summary>
/// <remarks></remarks>
    public class RUserDetails
    {

        private String m_username = "";
        private String m_displayname = "";
        private String m_cookie = "";
        private RUserLimitDetails m_userlimitdetails;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RUserDetails()
        {
        }

        internal RUserDetails(String username, String displayname, String cookie, RUserLimitDetails userlimitdetails)
        {

            m_username = username;
            m_displayname = displayname;
            m_cookie = cookie;
            m_userlimitdetails = userlimitdetails;

        }

        /// <summary>
        /// The username on this user identity
        /// </summary>
        /// <value></value>
        /// <returns>String containing the username</returns>
        /// <remarks></remarks>
        public String Username
        {
            get
            {
                return m_username;
            }
        }
        /// <summary>
        /// The display name on this user identity
        /// </summary>
        /// <value></value>
        /// <returns>String containing the display name</returns>
        /// <remarks></remarks>
        public String DisplayName
        {
            get
            {
                return m_displayname;
            }
        }
        /// <summary>
        /// Custom cookie for currently authenticated user
        /// </summary>
        /// <value></value>
        /// <returns>Cookie object containing the cookie</returns>
        /// <remarks></remarks>
        public String cookie
        {
            get
            {
                return m_cookie;
            }
        }
        /// <summary>
        ///  The limits for currently authenticated user
        /// </summary>
        /// <value></value>
        /// <returns>Cookie object containing the cookie</returns>
        /// <remarks></remarks>
        public RUserLimitDetails limits
        {
            get
            {
                return m_userlimitdetails;
            }
        }

    }
}