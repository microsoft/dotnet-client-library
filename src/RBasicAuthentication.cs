/*
 * RBasicAuthentication.cs
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
/// Basic Authentication class (username, password)
/// </summary>
/// <remarks></remarks>
    public class RBasicAuthentication : RAuthentication
    {

        private String m_username = "";
        private String m_password = "";


        /// <summary>
        /// Constructor for specifying a user to be authenticated
        /// </summary>
        /// <param name="username">username to be authenticated</param>
        /// <param name="password">password for the specified username</param>
        /// <remarks></remarks>
        public RBasicAuthentication(String username, String password)
        {
            m_username = username;
            m_password = password;

        }

        /// <summary>
        /// Password for the specified Username
        /// </summary>
        /// <value>Password String</value>
        /// <returns>Password String</returns>
        /// <remarks></remarks>
        public String Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
            }
        }
        /// <summary>
        /// Username for login
        /// </summary>
        /// <value>Username String</value>
        /// <returns>Username String</returns>
        /// <remarks></remarks>
        public String Username
        {
            get
            {
                return m_username;
            }
            set
            {
                m_username = value;
            }
        }
    }
}