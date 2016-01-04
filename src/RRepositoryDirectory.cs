/*
 * RRepositoryDirectory.cs
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
/// Represents a directory contained in the repository
/// </summary>
/// <remarks></remarks>
    public class RRepositoryDirectory
    {

        private RClient m_client;
        private RRepositoryDirectoryDetails m_directoryDetails;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RRepositoryDirectory()
        {

        }

        internal RRepositoryDirectory(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseRepositoryDirectory(jresponse, ref m_directoryDetails);
            }

        }
        /// <summary>
        /// Gets the details associated with this repository directory
        /// </summary>
        /// <returns>RRepositoryFileDetails object</returns>
        /// <remarks></remarks>
        public RRepositoryDirectoryDetails about()
        {
            return m_directoryDetails;
        }

        /// <summary>
        /// Archive files found in repository-managed user directory.
        /// </summary>
        /// <param name="archiveDirectoryName">Name to be given to the archive directory</param>
        /// <param name="files">List of Repository file to archive</param>
        /// <returns>RRepositoryDirectory object</returns>
        /// <remarks></remarks>
        public RRepositoryDirectory archive(String archiveDirectoryName, List<RRepositoryFile> files)
        {
            RRepositoryDirectory returnValue = default(RRepositoryDirectory);
            StringBuilder data = new StringBuilder();
            StringBuilder filenames = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYDIRECTORYARCHIVE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&archive=" + HttpUtility.UrlEncode(archiveDirectoryName));
            data.Append("&directory=" + HttpUtility.UrlEncode(m_directoryDetails.name));

            if (!(files == null))
            {
                foreach (var file in files)
                {
                    if (filenames.Length != 0)
                    {
                        filenames.Append(",");
                        filenames.Append(file.about().filename);
                    }
                    else
                    {
                        filenames.Append(file.about().filename);
                    }
                }
            }
            data.Append("&filename=" + HttpUtility.UrlEncode(filenames.ToString()));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["directory"] == null))
                {
                    JObject jdir = jrepo["directory"].Value<JObject>();
                    returnValue = new RRepositoryDirectory(new JSONResponse(jdir, true, "", 0), m_client);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Delete repository-managed user directory.
        /// </summary>
        /// <remarks></remarks>
        public void delete()
        {

            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYDIRECTORYDELETE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(m_directoryDetails.name));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);
        }

        /// <summary>
        /// Download zip archive of files found in repository-managed user directory. If the files parameter is null, all files in the directory are downloaded
        /// </summary>
        /// <param name="files">List of Repository file to delete</param>
        /// <returns>Byte arrary containing contents of downloaded zip file</returns>
        /// <remarks></remarks>
        public byte[] download(List<RRepositoryFile> files)
        {
            StringBuilder data = new StringBuilder();
            StringBuilder filenames = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYDIRECTORYDOWNLOAD;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(m_directoryDetails.name));

            if (!(files == null))
            {
                foreach (var file in files)
                {
                    if (filenames.Length != 0)
                    {
                        filenames.Append(",");
                        filenames.Append(file.about().filename);
                    }
                    else
                    {
                        filenames.Append(file.about().filename);
                    }
                }
            }
            data.Append("&filename=" + HttpUtility.UrlEncode(filenames.ToString()));

            //call the server
            byte[] returnValue = HTTPUtilities.callRESTBytesGet(uri, data.ToString(), ref m_client);

            return returnValue;
        }

        /// <summary>
        /// Rename repository-managed user directory.
        /// </summary>
        /// <param name="destination">New name of the directory</param>
        /// <returns>RRepositoryDirectory object</returns>
        /// <remarks></remarks>
        public RRepositoryDirectory rename(String destination)
        {
            RRepositoryDirectory returnValue = default(RRepositoryDirectory);
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYDIRECTORYRENAME;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(m_directoryDetails.name));
            data.Append("&destination=" + HttpUtility.UrlEncode(destination.Trim()));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["directory"] == null))
                {
                    JObject jdir = jrepo["directory"].Value<JObject>();
                    returnValue = new RRepositoryDirectory(new JSONResponse(jdir, true, "", 0), m_client);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Update access-controls on files found in repository-managed user directory. If the files parameter is null, all files in the directory are updated.
        /// </summary>
        /// <param name="accessControls">RepoAccessControlOptions object to apply to the files</param>
        /// <param name="files">List of Repository file to update</param>
        /// <remarks></remarks>
        public void update(RepoAccessControlOptions accessControls, List<RRepositoryFile> files)
        {

            StringBuilder data = new StringBuilder();
            StringBuilder filenames = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYDIRECTORYUPDATE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(m_directoryDetails.name));
            data.Append("&shared=" + HttpUtility.UrlEncode(accessControls.sharedUser.ToString()));
            data.Append("&restricted=" + HttpUtility.UrlEncode(accessControls.restricted));
            data.Append("&published=" + HttpUtility.UrlEncode(accessControls.published.ToString()));


            if (!(files == null))
            {
                foreach (var file in files)
                {
                    if (filenames.Length != 0)
                    {
                        filenames.Append(",");
                        filenames.Append(file.about().filename);
                    }
                    else
                    {
                        filenames.Append(file.about().filename);
                    }
                }
            }
            data.Append("&filename=" + HttpUtility.UrlEncode(filenames.ToString()));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);


        }

        /// <summary>
        /// Upload files in a zip archive to repository-managed user directory
        /// </summary>
        /// <param name="file">Full path to the zip file to be uploaded</param>
        /// <param name="options">RepoUploadOptions object to apply to the upload</param>
        /// <remarks></remarks>
        public void uploadDirectory(String file, RepoUploadOptions options)
        {

            StringBuilder data = new StringBuilder();
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            //set the url
            String uri = Constants.RREPOSITORYDIRECTORYUPLOAD;
            //create the input String
            if (!(options == null))
            {
                parameters.Add("format", "json");
                parameters.Add("directory", HttpUtility.UrlEncode(m_directoryDetails.name));
                parameters.Add("descr", HttpUtility.UrlEncode(options.descr));
                parameters.Add("newversion", options.newversion.ToString());
                parameters.Add("shared", options.sharedUser.ToString());
                parameters.Add("published", options.published.ToString());
                parameters.Add("restricted", HttpUtility.UrlEncode(options.restricted));
            }
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTFileUploadPost(uri, parameters, file, ref m_client);

        }

        private void parseRepositoryDirectory(JSONResponse jresponse, ref RRepositoryDirectoryDetails directoryDetails)
        {

            Boolean systemDirectory = default(Boolean);
            List<RRepositoryFile> files = new List<RRepositoryFile>();

            JObject jdir = jresponse.JSONMarkup;
            if (!(jdir == null))
            {

                String name = JSONUtilities.trimXtraQuotes(jdir["directory"].ToString());
                if ((name == Constants.SYSTEM_SHARED) || (name == Constants.SYSTEM_RESTRICTED) || (name == Constants.SYSTEM_PUBLIC))
                {
                    systemDirectory = true;
                }
                else
                {
                    systemDirectory = false;
                }

                if (!(jdir["files"] == null))
                {
                    JArray jvalues = jdir["files"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            RRepositoryFile file = new RRepositoryFile(new JSONResponse(j.Value<JObject>(), true, "", 0), m_client);
                            files.Add(file);
                        }
                    }
                }

                directoryDetails = new RRepositoryDirectoryDetails(name, systemDirectory, files);

            }

        }


    }
}