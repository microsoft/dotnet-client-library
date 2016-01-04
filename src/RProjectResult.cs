/*
 * RProjectResult.cs
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
using System.Net;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
/// <summary>
/// Represents an Result contained in a code execution on a Project
/// </summary>
/// <remarks>This is usually a plot or collection of plots</remarks>
    public class RProjectResult
    {

        private RClient m_client;
        private RProjectResultDetails m_resultDetails;
        private String m_project = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectResult()
        {

        }

        internal RProjectResult(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseProjectResult(jresponse, ref m_resultDetails);
            }

        }
        /// <summary>
        /// Gets the details associated with this Execution Result
        /// </summary>
        /// <returns>RProjectResultDetails object</returns>
        /// <remarks></remarks>
        public RProjectResultDetails about()
        {
            return m_resultDetails;
        }

        /// <summary>
        /// Deletes this execution result
        /// </summary>
        /// <remarks></remarks>
        public void delete()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTEXECUTERESULTDELETE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&execution=" + m_resultDetails.execution);
            data.Append("&project=" + m_project);
            data.Append("&name=" + HttpUtility.UrlEncode(m_resultDetails.name));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

        }

        private void parseProjectResult(JSONResponse jresponse, ref RProjectResultDetails resultDetails)
        {

            JObject jresult = jresponse.JSONMarkup;
            if (!(jresult == null))
            {
                String execution = JSONUtilities.trimXtraQuotes(jresult["execution"].Value<string>());
                String  name = JSONUtilities.trimXtraQuotes(jresult["filename"].Value<string>());
                int size = jresult["length"].Value<int>();
                String type = JSONUtilities.trimXtraQuotes(jresult["type"].Value<string>());
                String url = JSONUtilities.trimXtraQuotes(jresult["url"].Value<string>());

                resultDetails = new RProjectResultDetails(execution, name, size, type, url);
            }
        }

    }
}