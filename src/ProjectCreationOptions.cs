/*
 * ProjectCreationOptions.cs
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
/// Project creation options. Can be used to pre-initialize data in the workspace and working directory for the new project.
/// </summary>
/// <remarks></remarks>
    public class ProjectCreationOptions
    {

        private ProjectAdoptionOptions m_adoptionOptions;
        private ProjectPreloadOptions m_preloadDirectory;
        private ProjectPreloadOptions m_preloadWorkspace;
        private List<RData> m_rinputs = new List<RData>();
        private String m_preloadByDirectory = "";
        private Boolean m_blackbox = false;
        private String m_gridCluster = "";

        /// <summary>
        /// Preload working directory options allow the loading
        /// of one or more files from the repository into the
        /// working directory of the current R session
        /// </summary>
        /// <value>preload working directory options</value>
        /// <returns>preload working directory options</returns>
        /// <remarks></remarks>
        public ProjectPreloadOptions preloadDirectory
        {
            get
            {
                return m_preloadDirectory;
            }
            set
            {
                m_preloadDirectory = value;
            }
        }

        /// <summary>
        /// Preload workspace options allow the loading of one
        /// or more binary R objects from the repository into the
        /// workspace of the current R session
        /// </summary>
        /// <value>preload workspace options</value>
        /// <returns>preload workspace options</returns>
        /// <remarks></remarks>
        public ProjectPreloadOptions preloadWorkspace
        {
            get
            {
                return m_preloadWorkspace;
            }
            set
            {
                m_preloadWorkspace = value;
            }
        }

        /// <summary>
        /// Project adoption options allow the pre-loading
        /// of a pre-existing project workspace, project working directory,
        /// project history and/or project package dependencies
        /// into the current R session
        /// </summary>
        /// <value>project adoption options</value>
        /// <returns>project adoption options</returns>
        /// <remarks></remarks>
        public ProjectAdoptionOptions adoptionOptions
        {
            get
            {
                return m_adoptionOptions;
            }
            set
            {
                m_adoptionOptions = value;
            }
        }

        /// <summary>
        /// Allows the loading of all files
        /// found in one or more repository-managed directories
        /// into the working directory of the current R sesssion
        /// prior to execution.
        ///
        /// When loading the contents of more than one directory,
        /// use a comma-separated list of directory names.
        /// </summary>
        /// <value>preloadByDirectory option</value>
        /// <returns>preloadByDirectory option</returns>
        /// <remarks></remarks>
        public String preloadByDirectory
        {
            get
            {
                return m_preloadByDirectory;
            }
            set
            {
                m_preloadByDirectory = value;
            }
        }

        /// <summary>
        /// List of DeployR-encoded R objects to be added to the
        /// workspace of the current R session
        /// </summary>
        /// <value>List of RData encoded inputs for an R Script</value>
        /// <returns>List of RData encoded inputs for an R Script</returns>
        /// <remarks></remarks>
        public List<RData> rinputs
        {
            get
            {
                return m_rinputs;
            }
            set
            {
                m_rinputs = value;
            }
        }

        /// <summary>
        /// Enable to create a blackbox project.
        /// Blackbox projects are a special type of temporary project
        /// that limit API access on the underlying R session.
        /// </summary>
        /// <value>blackbox value</value>
        /// <returns>blackbox value</returns>
        /// <remarks></remarks>
        public Boolean blackbox
        {
            get
            {
                return m_blackbox;
            }
            set
            {
                m_blackbox = value;
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

    }
}