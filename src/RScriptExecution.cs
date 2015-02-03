/*
 * RScriptExecution.cs
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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
/// <summary>
/// Represents the result of an Script execution
/// </summary>
/// <remarks></remarks>
    public class RScriptExecution
    {

        private RClient m_client;
        private RProjectExecutionDetails m_executionDetails = null;
        private RProjectDetails m_projectDetails = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RScriptExecution()
        {

        }

        internal RScriptExecution(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseScriptExecution(jresponse, ref m_executionDetails, ref m_projectDetails);
            }

        }

        /// <summary>
        /// Gets the details associated with this execution
        /// </summary>
        /// <returns>RProjectExecutionDetails object</returns>
        /// <remarks></remarks>
        public RProjectExecutionDetails about()
        {
            return m_executionDetails;
        }

        private void parseScriptExecution(JSONResponse jresponse, ref RProjectExecutionDetails executionDetails, ref RProjectDetails projectDetails)
        {

            RProjectBaseImpl.parseProjectExecution(jresponse, ref executionDetails, ref projectDetails, m_client);

        }

    }
}