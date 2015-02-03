/*
 * RUserLimitDetails.cs
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

namespace DeployR
{

    /// <summary>
    /// Details of an authenticated User's limits
    /// </summary>
    /// <remarks></remarks>
    public class RUserLimitDetails
    {

        private int m_maxConcurrentLiveProjectCount = 0;
        private int m_maxFileUploadSize = 0;
        private int m_maxIdleLiveProjectTimeout = 0;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RUserLimitDetails()
        {

        }

        internal RUserLimitDetails(int maxConcurrentLiveProjectCount, int maxFileUploadSize, int maxIdleLiveProjectTimeout)
        {

            m_maxConcurrentLiveProjectCount = maxConcurrentLiveProjectCount;
            m_maxFileUploadSize = maxFileUploadSize;
            m_maxIdleLiveProjectTimeout = maxIdleLiveProjectTimeout;

        }
        /// <summary>
        /// Max concurrent live project limit
        /// </summary>
        /// <returns>concurrent project limit</returns>
        /// <remarks></remarks>
        public int maxConcurrentLiveProjectCount
        {
            get
            {
                return m_maxConcurrentLiveProjectCount;
            }
        }

        /// <summary>
        /// Max file upload size limit.
        /// </summary>
        /// <returns>file upload size limit</returns>
        /// <remarks></remarks>
        public int maxFileUploadSize
        {
            get
            {
                return m_maxFileUploadSize;
            }
        }

        /// <summary>
        /// Max idle live project timeout limit.
        /// </summary>
        /// <returns>live project timeout limit</returns>
        /// <remarks></remarks>
        public int maxIdleLiveProjectTimeout
        {
            get
            {
                return m_maxIdleLiveProjectTimeout;
            }
        }

    }
}