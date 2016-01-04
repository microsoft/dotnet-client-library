/*
 * RFactor.cs
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
using System.Collections.Generic;

namespace DeployR
{
/// <summary>
/// Wrapper class for an R factor object
/// </summary>
/// <remarks>Instances of this class can be created from RDataFactory
/// </remarks>
    public class RFactor : RData
    {

        private List<String> m_value;
        private List<String> m_levels;
        private List<String> m_labels;
        private Boolean m_ordered = false;
        private String m_name = "";
        private String m_type = "";
        private String m_rclass = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RFactor()
        {

            m_type = Constants.TYPE_FACTOR;
            m_rclass = Constants.RCLASS_FACTOR;

        }

        internal RFactor(String name, List<String> value)
        {
            m_type = Constants.TYPE_FACTOR;
            m_rclass = Constants.RCLASS_FACTOR;

            m_value = value;
            m_name = name;
        }

        internal RFactor(String name, List<String> value, List<String> levels, List<String> labels, Boolean ordered)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            m_type = Constants.TYPE_FACTOR;
            m_rclass = Constants.RCLASS_FACTOR;

            m_levels = levels;
            m_labels = labels;
            m_value = value;
            m_name = name.Replace(" ", "_");
            m_ordered = ordered;
        }
        /// <summary>
        /// Get the vector of factor levels for this class.
        /// </summary>
        /// <returns>Array of String values</returns>
        /// <remarks></remarks>
        public List<String> Levels
        {
            get
            {
                return m_levels;
            }
        }
        /// <summary>
        /// Get the vector of factor labels for this class.
        /// </summary>
        /// <returns>Array of String values</returns>
        /// <remarks></remarks>
        public List<String> Labels
        {
            get
            {
                return m_labels;
            }
        }
        /// <summary>
        /// Get the ordered attribute for this class.
        /// </summary>
        /// <returns>boolean - true indicating this is an ordered factor, false it is unordered.</returns>
        /// <remarks></remarks>
        public Boolean Ordered
        {
            get
            {
                return m_ordered;
            }
        }
        /// <summary>
        /// Gets the array of factor values for this RData.
        /// </summary>
        /// <value></value>
        /// <returns>Array of strings cast as type Object</returns>
        /// <remarks></remarks>
        public object Value
        {
            get
            {
                return m_value;
            }
        }
        /// <summary>
        /// Gets the underlying R object name of this RData.
        /// </summary>
        /// <value></value>
        /// <returns>String name</returns>
        /// <remarks></remarks>
        public String Name
        {
            get
            {
                return m_name;
            }
        }
        /// <summary>
        /// Gets the underlying R class of this RData.
        /// </summary>
        /// <value></value>
        /// <returns>String rclass</returns>
        /// <remarks></remarks>
        public String RClass
        {
            get
            {
                return m_rclass;
            }
        }
        /// <summary>
        /// Gets the underlying RevoDeployR type of this RData.
        /// </summary>
        /// <value></value>
        /// <returns>String type</returns>
        /// <remarks></remarks>
        public String Type
        {
            get
            {
                return m_type;
            }
        }

    }
}