/*
 * RJob.cs
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
using System.Net;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
/// <summary>
/// Represents a Job in RevoDeployR
/// </summary>
/// <remarks></remarks>
    public class RJob
    {

        /// <summary>
        /// String enum describing status of a job
        /// </summary>
        public class Status
        {
            private Status(String value) { m_value = value; }

            private String m_value { get; set; }

            /// <summary>
            /// Enum value defining scheduled status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status SCHEDULED { get { return new Status("Scheduled"); } }
            /// <summary>
            /// Enum value defining queue status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status QUEUED { get { return new Status("Queued"); } }
            /// <summary>
            /// Enum value defining running status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status RUNNING { get { return new Status("Running"); } }
            /// <summary>
            /// Enum value defining completed status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status COMPLETED { get { return new Status("Completed"); } }
            /// <summary>
            /// Enum value defining cancelled status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status CANCELLED { get { return new Status("Cancelled"); } }
            /// <summary>
            /// Enum value defining interrupted status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status INTERRUPTED { get { return new Status("Interrupted"); } }
            /// <summary>
            /// Enum value defining failed status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status FAILED { get { return new Status("Failed"); } }
            /// <summary>
            /// Enum value defining aborted status of a job
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Status ABORTED { get { return new Status("Aborted"); } }

            /// <summary>
            /// Return the equivalent enum value based on a string 
            /// </summary>
            /// <param name="value">String to be evaluated</param>
            /// <returns>Status enum value</returns>
            /// <remarks></remarks>
            public static Status fromString(String value)
            {
                String s = value.ToLower();
                if (s == SCHEDULED.ToString().ToLower())
                {
                    return SCHEDULED;
                }
                else if (s == QUEUED.ToString().ToLower())
                {
                    return QUEUED;
                }
                else if (s == RUNNING.ToString().ToLower())
                {
                    return RUNNING;
                }
                else if (s == ABORTED.ToString().ToLower())
                {
                    return ABORTED;
                }
                else if (s == CANCELLED.ToString().ToLower())
                {
                    return CANCELLED;
                }
                else if (s == INTERRUPTED.ToString().ToLower())
                {
                    return INTERRUPTED;
                }
                else if (s == FAILED.ToString().ToLower())
                {
                    return FAILED;
                }
                else
                {
                    return COMPLETED;
                }
            }
        }

        private RClient m_client;
        private RJobDetails m_jobDetails;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RJob()
        {

        }

        internal RJob(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseJob(jresponse, ref m_jobDetails);
            }

        }

        /// <summary>
        /// Gets the details associated with this job
        /// </summary>
        /// <returns>RJobDetails object</returns>
        /// <remarks></remarks>
        public RJobDetails about()
        {
            return m_jobDetails;
        }

        /// <summary>
        /// Queries the job to get the current status
        /// </summary>
        /// <returns>RJobDetails object</returns>
        /// <remarks></remarks>
        public RJobDetails query()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RJOBQUERY;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&job=" + HttpUtility.UrlEncode(m_jobDetails.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            parseJob(jresponse, ref m_jobDetails);
            return m_jobDetails;
        }

        /// <summary>
        /// Cancels the job
        /// </summary>
        /// <returns>RJobDetails object</returns>
        /// <remarks></remarks>
        public RJobDetails cancel()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RJOBCANCEL;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&job=" + HttpUtility.UrlEncode(m_jobDetails.id));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

            parseJob(jresponse, ref m_jobDetails);
            return m_jobDetails;
        }

        /// <summary>
        /// Deletes the job
        /// </summary>
        /// <remarks></remarks>
        public void delete()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RJOBDELETE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&job=" + HttpUtility.UrlEncode(m_jobDetails.id));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

        }

        private void parseJob(JSONResponse jresponse, ref RJobDetails jobDetails)
        {

            String descr = "";
            String id = "";
            String name = "";
            int onrepeat = 0;
            String project = "";
            long schedinterval = 0;
            int schedrepeat = 0;
            long schedstart = 0;
            String status = "";
            String statusMsg = "";
            long timeStart = 0;
            long timeCode = 0;
            long timeTotal = 0;
            String tag = "";
            JObject jjob = default(JObject);

            if (jresponse.JSONMarkup["job"].Type == JTokenType.Object)
            {
                jjob = jresponse.JSONMarkup["job"].Value<JObject>();
            }
            else
            {
                jjob = jresponse.JSONMarkup;
            }
            if (!(jjob == null))
            {
                descr = JSONUtilities.trimXtraQuotes(jjob["descr"].Value<String>());
                id = JSONUtilities.trimXtraQuotes(jjob["job"].Value<String>());
                name = JSONUtilities.trimXtraQuotes(jjob["name"].Value<String>());
                onrepeat = Convert.ToInt32(jjob["onrepeat"].Value<String>());
                project = JSONUtilities.trimXtraQuotes(jjob["project"].Value<String>());
                schedinterval = Convert.ToInt64(jjob["schedinterval"].Value<String>());
                schedrepeat = Convert.ToInt32(jjob["schedrepeat"].Value<String>());
                schedstart = Convert.ToInt64(jjob["schedstart"].Value<String>());
                status = JSONUtilities.trimXtraQuotes(jjob["status"].Value<String>());
                statusMsg = JSONUtilities.trimXtraQuotes(jjob["statusMsg"].Value<String>());
                timeStart = Convert.ToInt64(jjob["timeStart"].Value<String>());
                timeCode = Convert.ToInt64(jjob["timeCode"].Value<String>());
                timeTotal = Convert.ToInt64(jjob["timeTotal"].Value<String>());
                tag = JSONUtilities.trimXtraQuotes(jjob["tag"].Value<String>());

                jobDetails = new RJobDetails(descr, id, name, onrepeat, project, schedinterval, schedrepeat, schedstart, status, statusMsg, timeStart, timeCode, timeTotal, tag);

            }

        }

    }
}