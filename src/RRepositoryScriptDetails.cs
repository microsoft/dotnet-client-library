/*
 * RRepositoryScriptDetails.cs
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
    /// Details of a script contained in the repository
    /// </summary>
    /// <remarks></remarks>
    public class RRepositoryScriptDetails
    {

        private String m_descr = "";
        private List<Dictionary<String, String>> m_inputs;
        private String m_name = "";
        private List<Dictionary<String, String>> m_outputs;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RRepositoryScriptDetails()
        {

        }

        internal RRepositoryScriptDetails(String descr, List<Dictionary<String, String>> inputs, String name, List<Dictionary<String, String>> outputs)
        {

            m_descr = descr;
            m_inputs = inputs;
            m_name = name;
            m_outputs = outputs;

        }

        /// <summary>
        /// Description of Script
        /// </summary>
        /// <returns>String containing script description</returns>
        /// <remarks></remarks>
        public String descr
        {
            get
            {
                return m_descr;
            }
        }

        /// <summary>
        /// List of inputs to the script
        /// </summary>
        /// <returns>list containing the inputs to the script</returns>
        /// <remarks></remarks>
        public List<Dictionary<String, String>> inputs
        {
            get
            {
                return m_inputs;
            }
        }

        /// <summary>
        /// Name of the script
        /// </summary>
        /// <returns>String containing the name of the script</returns>
        /// <remarks></remarks>
        public String name
        {
            get
            {
                return m_name;
            }
        }

        /// <summary>
        /// List of outputs from the script
        /// </summary>
        /// <returns>list containing the outputs of the script</returns>
        /// <remarks></remarks>
        public List<Dictionary<String, String>> outputs
        {
            get
            {
                return m_outputs;
            }
        }


    }
}