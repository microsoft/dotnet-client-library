/*
 * HTTPRestException.cs
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
/// Exeception thrown on failed call on an HTTPPost or HTTPGet
/// </summary>
/// <remarks>
/// </remarks>
    public class HTTPRestException : System.ApplicationException
    {


        private int m_errorCode = 0;
        private String m_console = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected HTTPRestException()
        {

        }

        internal HTTPRestException(String message, String console, int errorCode)
            :base(message.Replace("\\n", "\r\n").Replace("\"", ""))
        {
            m_console = console;
            m_console.Replace("\\n", "\r\n").Replace("\"", "");            
            m_errorCode = errorCode;

        }

        /// <summary>
        /// Error code associated with the exception
        /// </summary>
        /// <returns>error code</returns>
        /// <remarks></remarks>
        public int errorCode
        {
            get
            {
                return m_errorCode;
            }
        }
        /// <summary>
        /// R Console text, if available
        /// </summary>
        /// <returns>error code</returns>
        /// <remarks></remarks>
        public String console
        {
            get
            {
                return m_console;
            }
        }

    }
}