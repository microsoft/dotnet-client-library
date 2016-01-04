/*
 * JobExecutionOptions.cs
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
    /// Options used when submitting a Job for execution
    /// </summary>
    /// <remarks></remarks>
    public class JobExecutionOptions : ProjectExecutionOptions
    {

        private Boolean m_noProject = false;
        private String m_priority = "";
        private JobSchedulingOptions m_schedulingOptions;
        private String m_gridCluster = "";

        /// <summary>
        /// Enable noproject option if project persistence following
        /// job execution is not required. Typically used when
        /// ProjectStorageOptions have already been specified.
        /// </summary>
        /// <value>no project value</value>
        /// <returns>no project value</returns>
        /// <remarks></remarks>
        public Boolean noProject
        {
            get
            {
                return m_noProject;
            }
            set
            {
                m_noProject = value;
            }
        }

        /// <summary>
        /// Set a scheduling priority for your job. Default
        /// scheduling priority defaults to LOW_PRIORITY.
        ///
        /// Possible values are:
        /// LOW_PRIORITY
        /// MEDIIUM_PRIORITY
        /// HIGH_PRIORITY
        /// </summary>
        /// <value>job priority value</value>
        /// <returns>job priority value</returns>
        /// <remarks></remarks>
        public String priority
        {
            get
            {
                return m_priority;
            }
            set
            {
                m_priority = value;
            }
        }

        /// <summary>
        /// Job scheduling options specify the start time and
        /// optionally the repeat interval and count for a
        /// scheduled job.
        /// </summary>
        /// <value>options for scheduling the job</value>
        /// <returns>options for scheduling the job</returns>
        /// <remarks></remarks>
        public JobSchedulingOptions schedulingOptions
        {
            get
            {
                return m_schedulingOptions;
            }
            set
            {
                m_schedulingOptions = value;
            }
        }

        ///<summary>
        /// Identifies the DeployR grid cluster where the caller would
        /// like the job (R session) to execute. If there are no slots
        /// available on any of the grid nodes within the cluster indicated
        /// then the server will attempt to execute the job on a slot
        /// on an available grid node that supports MIXED-operations. If no
        /// slot meeting these criteria is found, the job will be queued
        /// until a suitable slot becomes available. This feature is optional
        /// and available on DeployR Enterprise only.
        /// </summary>
        /// <value>gridCluster value</value>
        /// <returns>gridCluster value</returns>
        /// <remarks></remarks>
        public String gridCluster
        {
            get
            {
                return m_gridCluster;
            }
            set
            {
                m_gridCluster = value;
            }
        }

        /// <summary>
        /// Constant used with "priority" property to specify this job should be 'high priority'
        /// </summary>
        public const String HIGH_PRIORITY = "high";
        
        /// <summary>
        /// Constant used with "priority" property to specify this job should be 'medium priority'
        /// </summary>
        public const String MEDIUM_PRIORITY = "medium";

        /// <summary>
        /// Constant used with "priority" property to specify this job should be 'low priority'
        /// </summary>
        public const String LOW_PRIORITY = "low";


    }
}