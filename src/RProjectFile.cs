/*
 * RProjectFile.cs
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
/// Represents a File contained within a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectFile
    {

        private RClient m_client;
        private RProjectFileDetails m_fileDetails;
        private String m_project = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectFile()
        {

        }

        internal RProjectFile(JSONResponse jresponse, RClient client, String project)
        {

            m_client = client;
            m_project = project;
            if (!(jresponse == null))
            {
                parseProjectFile(jresponse, ref m_fileDetails);
            }

        }

        /// <summary>
        /// Gets the details associated with this Project File
        /// </summary>
        /// <returns>RProjectFileDetails object</returns>
        /// <remarks></remarks>
        public RProjectFileDetails about()
        {
            return m_fileDetails;
        }

        /// <summary>
        /// Deletes this Project File
        /// </summary>
        /// <remarks></remarks>
        public void delete()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTDIRECTORYDELETE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + m_project);
            data.Append("&name=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

        }

        /// <summary>
        /// Gets the URL associated with this Project File
        /// </summary>
        /// <returns>URL of project file</returns>
        /// <remarks></remarks>
        public String download()
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTDIRECTORYDOWNLOAD;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + m_project);
            data.Append("&name=" + HttpUtility.UrlEncode(m_fileDetails.filename));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            String returnValue = System.Convert.ToString(HttpUtility.UrlEncode(uri + "/" + m_project + "/" + m_fileDetails.filename + ";jsessionid=" + m_client.Cookie.Value));

            return returnValue;
        }

        /// <summary>
        /// Store project directory file in the repository
        /// </summary>
        /// <param name="options">RepoUploadOptions object describing the file</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile store(RepoUploadOptions options)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);

            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTDIRECTORYSTORE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + m_project);
            data.Append("&filename=" + HttpUtility.UrlEncode(options.filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(options.directory));
            data.Append("&descr=" + HttpUtility.UrlEncode(options.descr));
            data.Append("&shared=" + options.sharedUser.ToString());
            data.Append("&published=" + options.published.ToString());
            data.Append("&restricted=" + options.restricted.ToString());
            data.Append("&newversion=" + options.newversion.ToString());
            data.Append("&newversionmsg=" + HttpUtility.UrlEncode(options.newversionmsg));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["file"] == null))
                {
                    JObject jfile = jrepo["file"].Value<JObject>();
                    returnValue = new RRepositoryFile(new JSONResponse(jfile, true, "", 0), m_client);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Update project file details
        /// </summary>
        /// <param name="name">name of project file</param>
        /// <param name="descr">description of project file</param>
        /// <param name="overwrite">flag indicating if file should be overwritten</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RProjectFile update(String name, String descr, Boolean overwrite)
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RPROJECTDIRECTORYUPDATE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + m_project);
            data.Append("&name=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            data.Append("&rename=" + HttpUtility.UrlEncode(name));
            data.Append("&descr=" + HttpUtility.UrlEncode(descr));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

            RProjectFile returnValue = new RProjectFile(jresponse, m_client, m_project);

            return returnValue;
        }

        private void parseProjectFile(JSONResponse jresponse, ref RProjectFileDetails fileDetails)
        {
            JObject jprojectfile = jresponse.JSONMarkup;
            if (!(jprojectfile == null))
            {
                String descr = JSONUtilities.trimXtraQuotes(jprojectfile["descr"].Value<String>());
                String name = JSONUtilities.trimXtraQuotes(jprojectfile["filename"].Value<String>());
                int size = jprojectfile["length"].Value<int>();
                String type = JSONUtilities.trimXtraQuotes(jprojectfile["type"].Value<String>());
                String url = JSONUtilities.trimXtraQuotes(jprojectfile["url"].Value<String>());
                String category = JSONUtilities.trimXtraQuotes(jprojectfile["category"].Value<String>());

                fileDetails = new RProjectFileDetails(descr, name, size, type, url, category);
            }

        }

    }
}