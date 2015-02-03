/*
 * JobSchedulingOptions.cs
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
/// Options used when scheduling a Job to be run
/// </summary>
/// <remarks></remarks>
    public class JobSchedulingOptions
    {

        private int m_repeatCount = 0;
        private long m_repeatInterval = 0;
        private long m_startTime = 0;

        /// <summary>
        /// Job Schdedule repeat count
        /// </summary>
        /// <value>repeat count</value>
        /// <returns>repeat count</returns>
        /// <remarks></remarks>
        public int repeatCount
        {
            get
            {
                return m_repeatCount;
            }
            set
            {
                m_repeatCount = value;
            }
        }

        /// <summary>
        /// Job Schdedule repeat interval
        /// </summary>
        /// <value>repeat interval</value>
        /// <returns>repeat interval</returns>
        /// <remarks></remarks>
        public long repeatInterval
        {
            get
            {
                return m_repeatInterval;
            }
            set
            {
                m_repeatInterval = value;
            }
        }

        /// <summary>
        /// Job Schdedule start time
        /// </summary>
        /// <value>start time</value>
        /// <returns>start time</returns>
        /// <remarks></remarks>
        public long startTime
        {
            get
            {
                return m_startTime;
            }
            set
            {
                m_startTime = value;
            }
        }

    }
}