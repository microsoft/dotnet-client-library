/*
 * RProjectExecutionDetails.cs
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
using System.Collections.Generic;

namespace DeployR
{
/// <summary>
/// Details of an Execution on a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectExecutionDetails
    {

        private List<RProjectFile> m_artifacts;
        private String m_code = "";
        private long m_timeStart = 0;
        private long m_timeCode = 0;
        private long m_timeTotal = 0;
        private String m_tag = "";
        private String m_console = "";
        private String m_errorDescr = "";
        private int m_errorCode = 0;
        private String m_id = "";
        private Boolean m_interrupted = false;
        private List<RRepositoryFile> m_repositoryFiles;
        private List<RProjectResult> m_results;
        private List<String> m_warnings;
        private List<RData> m_workspaceObjects;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectExecutionDetails()
        {

        }

        internal RProjectExecutionDetails(List<RProjectFile> artifacts, String code, long timeStart, long timeCode, long timeTotal, String tag, String console, String errorDescr, int errorCode, String id, Boolean interrupted, List<RRepositoryFile> repositoryFiles, List<RProjectResult> results, List<String> warnings, List<RData> workspaceObjects)
        {

            m_artifacts = artifacts;
            m_code = code;
            m_timeStart = timeStart;
            m_timeCode = timeCode;
            m_timeTotal = timeTotal;
            m_tag = tag;
            m_console = console;
            m_errorDescr = errorDescr;
            m_errorCode = errorCode;
            m_id = id;
            m_interrupted = interrupted;
            m_repositoryFiles = repositoryFiles;
            m_results = results;
            m_warnings = warnings;
            m_workspaceObjects = workspaceObjects;

        }

        /// <summary>
        /// List of Artifacts created from Project Execution
        /// </summary>
        /// <returns>List of RProjectFile objects</returns>
        /// <remarks></remarks>
        public List<RProjectFile> artifacts
        {
            get
            {
                return m_artifacts;
            }
        }

        /// <summary>
        /// R code that was executed
        /// </summary>
        /// <returns>String containing R code</returns>
        /// <remarks></remarks>
        public String code
        {
            get
            {
                return m_code;
            }
        }

        /// <summary>
        /// Start time (millis) for execution
        /// </summary>
        /// <returns>Long value containing Start time (millis) for execution</returns>
        /// <remarks></remarks>
        public long timeStart
        {
            get
            {
                return m_timeStart;
            }
        }

        /// <summary>
        /// Code execution time (millis) for execution
        /// </summary>
        /// <returns>Long value containing Code execution time (millis) for execution</returns>
        /// <remarks></remarks>
        public long timeCode
        {
            get
            {
                return m_timeCode;
            }
        }

        /// <summary>
        /// Total time (millis) for execution
        /// </summary>
        /// <returns>Long value containing Total time (millis) for execution</returns>
        /// <remarks></remarks>
        public long timeTotal
        {
            get
            {
                return m_timeTotal;
            }
        }

        /// <summary>
        /// Tag associated with R execution
        /// </summary>
        /// <returns>Tag associated with R execution</returns>
        /// <remarks></remarks>
        public String tag
        {
            get
            {
                return m_tag;
            }
        }

        /// <summary>
        /// Console output from executed R code
        /// </summary>
        /// <returns>String containing Console output</returns>
        /// <remarks></remarks>
        public String console
        {
            get
            {
                return m_console;
            }
        }

        /// <summary>
        /// Error generated from executed R code
        /// </summary>
        /// <returns>String containing Error message</returns>
        /// <remarks></remarks>
        public String errorDescr
        {
            get
            {
                return m_errorDescr;
            }
        }

        /// <summary>
        /// Error code from executed R code
        /// </summary>
        /// <returns>Integer containing Error code</returns>
        /// <remarks></remarks>
        public int errorCode
        {
            get
            {
                return m_errorCode;
            }
        }

        /// <summary>
        /// ID of the R code execution
        /// </summary>
        /// <returns>String contianing ID</returns>
        /// <remarks></remarks>
        public String id
        {
            get
            {
                return m_id;
            }
        }

        /// <summary>
        /// Indicatator if the R Code was interrupted (i.e. canceled)
        /// </summary>
        /// <returns>Boolean flag indicated interrupted status</returns>
        /// <remarks></remarks>
        public Boolean interrupted
        {
            get
            {
                return m_interrupted;
            }
        }

        /// <summary>
        /// List of Repository Files generated from an execution of R code
        /// </summary>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> repositoryFiles
        {
            get
            {
                return m_repositoryFiles;
            }
        }

        /// <summary>
        /// List of results generated from an R execution
        /// </summary>
        /// <returns>List of RProjectResult objects</returns>
        /// <remarks></remarks>
        public List<RProjectResult> results
        {
            get
            {
                return m_results;
            }
        }

        /// <summary>
        /// Warnings generated from an R execution
        /// </summary>
        /// <returns>List of strings containing any warnings</returns>
        /// <remarks></remarks>
        public List<String> warnings
        {
            get
            {
                return m_warnings;
            }
        }

        /// <summary>
        /// Workspace objects created during an R Execution
        /// </summary>
        /// <returns>List of RData objects</returns>
        /// <remarks></remarks>
        public List<RData> workspaceObjects
        {
            get
            {
                return m_workspaceObjects;
            }
        }

    }
}