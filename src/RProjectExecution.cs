/*
 * RProjectExecution.cs
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
using System.Net;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeployR
{

/// <summary>
/// Represents the result of an Execution on a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectExecution
    {

        private RClient m_client;
        private RProjectDetails m_projectDetails = null;
        private RProjectExecutionDetails m_executionDetails = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectExecution()
        {

        }

        internal RProjectExecution(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseProjectExecution(jresponse, ref m_executionDetails, ref m_projectDetails);
            }

        }

        /// <summary>
        /// Gets the details associated with this Execution
        /// </summary>
        /// <returns>RProjectExecutionDetails object</returns>
        /// <remarks></remarks>
        public RProjectExecutionDetails about()
        {
            return m_executionDetails;
        }

        /// <summary>
        /// Deletes the results assocaited with this Execution
        /// </summary>
        /// <remarks></remarks>
        public void deleteResults()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTEXECUTERESULTDELETE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&execution=" + HttpUtility.UrlEncode(m_executionDetails.id));
            data.Append("&project=" + HttpUtility.UrlEncode(m_projectDetails.id));
            data.Append("&name=" + "");
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

        }

        /// <summary>
        /// Gets the URL of the results associated with this Execution
        /// </summary>
        /// <returns>URL of the results</returns>
        /// <remarks></remarks>
        public String downloadResults()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTEXECUTERESULTDOWNLOAD;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&execution=" + HttpUtility.UrlEncode(m_executionDetails.id));
            data.Append("&project=" + HttpUtility.UrlEncode(m_projectDetails.id));
            data.Append("&name=" + "");
            data.Append("&inline=false");

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            String returnValue = System.Convert.ToString(HttpUtility.UrlEncode(uri + "/" + m_projectDetails.id + "/" + m_executionDetails.id + ";jsessionid=" + m_client.Cookie.Value));

            return returnValue;
        }

        /// <summary>
        /// Flushes this Execution
        /// </summary>
        /// <remarks></remarks>
        public void flush()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTEXECUTEFLUSH;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&execution=" + HttpUtility.UrlEncode(m_executionDetails.id));
            data.Append("&project=" + HttpUtility.UrlEncode(m_projectDetails.id));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

        }

        /// <summary>
        /// Lists the results associated with this Execution
        /// </summary>
        /// <returns>List of RProjectResult objects</returns>
        /// <remarks></remarks>
        public List<RProjectResult> listResults()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTEXECUTERESULTLIST;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&execution=" + HttpUtility.UrlEncode(m_executionDetails.id));
            data.Append("&project=" + HttpUtility.UrlEncode(m_projectDetails.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            List<RProjectResult> returnValue = new List<RProjectResult>();

            if (!(jresponse.JSONMarkup["execution"] == null))
            {
                JObject jexec = jresponse.JSONMarkup["execution"].Value<JObject>();
                if (!(jexec["results"] == null))
                {
                    JArray jvalues = jexec["results"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            returnValue.Add(new RProjectResult(new JSONResponse(j.Value<JObject>(), true, "", 0), m_client));
                        }
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Prints the results associated with this Execution to a PDF file
        /// </summary>
        /// <returns>URL of the printed results</returns>
        /// <remarks></remarks>
        public String printResults()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri =  Constants.RPROJECTEXECUTERESULTPRINT;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&execution=" + HttpUtility.UrlEncode(m_executionDetails.id));
            data.Append("&project=" + HttpUtility.UrlEncode(m_projectDetails.id));
            data.Append("&name=" + "");
            data.Append("&report=false");

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            String returnValue = System.Convert.ToString(HttpUtility.UrlEncode(uri + "/" + m_projectDetails.id + "/" + m_executionDetails.id + ";jsessionid=" + m_client.Cookie.Value));

            return returnValue;
        }

        private void parseProjectExecution(JSONResponse jresponse, ref RProjectExecutionDetails executionDetails, ref RProjectDetails projectDetails)
        {

            RProjectBaseImpl.parseProjectExecution(jresponse, ref executionDetails, ref projectDetails, m_client);

        }

    }
}