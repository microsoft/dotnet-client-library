/*
 * RBoolean.cs
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
/// Wrapper class for an R logical object
/// </summary>
/// <remarks>Instances of this class can be created from RDataFactory
/// </remarks>
    public class RBoolean : RData
    {

        private Boolean m_value = false;
        private String m_name = "";
        private String m_type = "";
        private String m_rclass = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RBoolean()
        {
            m_type = Constants.TYPE_PRIMITIVE;
            m_rclass = Constants.RCLASS_BOOLEAN;
        }

        internal RBoolean(String name, Boolean value)
        {
            m_type = Constants.TYPE_PRIMITIVE;
            m_rclass = Constants.RCLASS_BOOLEAN;

            m_value = value;
            m_name = name.Replace(" ", "_");
        }

        /// <summary>
        /// Gets the boolean value for this RData.
        /// </summary>
        /// <value></value>
        /// <returns>Boolean cast as type Object</returns>
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