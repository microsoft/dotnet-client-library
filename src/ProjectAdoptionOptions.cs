/*
 * ProjectAdoptionOptions.cs
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
/// Options used when creating a new Project based on an exisiting Project
/// </summary>
/// <remarks></remarks>
    public class ProjectAdoptionOptions
    {

        private String m_adoptDirectory = "";
        private String m_adoptPackages = "";
        private String m_adoptWorkspace = "";

        /// <summary>
        /// Adopt project directory
        /// </summary>
        /// <value>name of the project where the directory should be adopted</value>
        /// <returns>name of the project where the directory should be adopted</returns>
        /// <remarks></remarks>
        public String adoptDirectory
        {
            get
            {
                return m_adoptDirectory;
            }
            set
            {
                m_adoptDirectory = value;
            }
        }

        /// <summary>
        /// Adopt project packages
        /// </summary>
        /// <value>name of the project where the packages should be adopted</value>
        /// <returns>name of the project where the packages should be adopted</returns>
        /// <remarks></remarks>
        public String adoptPackages
        {
            get
            {
                return m_adoptPackages;
            }
            set
            {
                m_adoptPackages = value;
            }
        }

        /// <summary>
        /// Adopt project workspace
        /// </summary>
        /// <value>name of the project where the workspace should be adopted</value>
        /// <returns>name of the project where the workspace should be adopted</returns>
        /// <remarks></remarks>
        public String adoptWorkspace
        {
            get
            {
                return m_adoptWorkspace;
            }
            set
            {
                m_adoptWorkspace = value;
            }
        }

    }
}