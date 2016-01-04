/*
 * RRepositoryFile.cs
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
/// Represents a file contained in the repository
/// </summary>
/// <remarks></remarks>
    public class RRepositoryFile
    {

        /// <summary>
        /// String enum describing file types in the repository
        /// </summary>
        public class Category
        {
            private Category(String value) { m_value = value; }

            private String m_value { get; set; }

            /// <summary>
            /// Enum value defining a data file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category DATAFILE { get { return new Category("data"); } }
            /// <summary>
            /// Enum value defining a plot file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category GRAPHICSPLOT { get { return new Category("plot"); } }
            /// <summary>
            /// Enum value defining a generic file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category OTHER { get { return new Category("file"); } }
            /// <summary>
            /// Enum value defining a pdf file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category PDFFILE { get { return new Category("pdf"); } }
            /// <summary>
            /// Enum value defining a R binary file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category RBINARY { get { return new Category("R"); } }
            /// <summary>
            /// Enum value defining a R script file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category RSCRIPT { get { return new Category("script"); } }
            /// <summary>
            /// Enum value defining a shell script file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category SHELLSCRIPT { get { return new Category("shell"); } }
            /// <summary>
            /// Enum value defining a text file in the repository
            /// </summary>
            /// <returns>Enum value</returns>
            /// <remarks></remarks>
            public static Category TEXTFILE { get { return new Category("text"); } }

            /// <summary>
            /// Return the equivalent enum value based on a string 
            /// </summary>
            /// <param name="value">String to be evaluated</param>
            /// <returns>Category enum value</returns>
            /// <remarks></remarks>
            public static Category fromString(String value)
            {
                String s = value.ToLower();
                if ( s == DATAFILE.ToString().ToLower())
                {
                    return DATAFILE;
                }
                else if (s == GRAPHICSPLOT.ToString().ToLower())
                {
                    return GRAPHICSPLOT;
                }
                else if (s == PDFFILE.ToString().ToLower())
                {
                    return PDFFILE;
                }
                else if (s == RBINARY.ToString().ToLower())
                {
                    return RBINARY;
                }
                else if (s == RSCRIPT.ToString().ToLower())
                {
                    return RSCRIPT;
                }
                else if (s == SHELLSCRIPT.ToString().ToLower())
                {
                    return SHELLSCRIPT;
                }
                else if (s == TEXTFILE.ToString().ToLower())
                {
                    return TEXTFILE;
                }
                else
                {
                    return OTHER;
                }
            }
        }

        private RClient m_client;
        private RRepositoryFileDetails m_fileDetails;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RRepositoryFile()
        {

        }

        internal RRepositoryFile(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseRepositoryFile(jresponse, ref m_fileDetails);
            }

        }

        /// <summary>
        /// Gets the details associated with this repository file
        /// </summary>
        /// <returns>RRepositoryFileDetails object</returns>
        /// <remarks></remarks>
        public RRepositoryFileDetails about()
        {
            return m_fileDetails;
        }

        /// <summary>
        /// Delete the repository file
        /// </summary>
        /// <remarks></remarks>
        public void delete()
        {

            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYFILEDELETE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(m_fileDetails.directory));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

        }

        /// <summary>
        /// Get the URL associated with the repository file
        /// </summary>
        /// <returns>URL of the repository file</returns>
        /// <remarks></remarks>
        public String download()
        {
            //set the url
            String uri = Constants.RREPOSITORYFILEDOWNLOAD;

            String urlParams = "";
            if (m_fileDetails.version == null)
            {
                urlParams = HttpUtility.UrlEncode(m_fileDetails.author) + "/" + HttpUtility.UrlEncode(m_fileDetails.filename);
            }
            else
            {
                urlParams = HttpUtility.UrlEncode(m_fileDetails.author) + "/" + HttpUtility.UrlEncode(m_fileDetails.version) + "/" + HttpUtility.UrlEncode(m_fileDetails.filename);
            }
            String returnValue = m_client.URL + uri + "/" + urlParams + ";jsessionid=" + m_client.Cookie.Value;

            return returnValue;
        }

        /// <summary>
        /// Grant repository file to another user
        /// </summary>
        /// <param name="newauthor">name of the user to grant authorship</param>
        /// <param name="revokeauthor">name of the user to revoke authorship</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile grant(String newauthor, String revokeauthor)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYFILEGRANT;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(m_fileDetails.directory));
            data.Append("&newauthor=" + HttpUtility.UrlEncode(newauthor));
            data.Append("&revokeauthor=" + HttpUtility.UrlEncode(revokeauthor));

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
        /// Revert repository file to an earlier version
        /// </summary>
        /// <param name="version">the version of the file to revert to</param>
        /// <param name="descr">description of the file</param>
        /// <param name="restricted">indicates if file is restricted</param>
        /// <param name="sharedUser">indicates if file is shared</param>
        /// <param name="published">indicates if file is published</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile revert(String version, String descr, String restricted, Boolean sharedUser, Boolean published)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYFILEREVERT;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(m_fileDetails.directory));
            data.Append("&version=" + HttpUtility.UrlEncode(version));
            data.Append("&shared=" + sharedUser.ToString());
            data.Append("&descr=" + HttpUtility.UrlEncode(descr));
            data.Append("&published=" + published.ToString());
            data.Append("&restricted=" + HttpUtility.UrlEncode(restricted));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref m_client);

            returnValue = null;
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
        /// Diff managed repository file version against the latest version. Available only for text based files in the repository. Generates a HTML encoded response diff.
        /// </summary>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public String diff()
        {
            //set the url
            String uri =  Constants.RREPOSITORYFILEDIFF;
            if (m_fileDetails.version == "")
            {
                Exception e = new Exception("Repository file diff can only be requested on a version of a file, not on the latest.");
                throw (e);
            }


            String urlParams = HttpUtility.UrlEncode(m_fileDetails.latestby) + "/" + HttpUtility.UrlEncode(m_fileDetails.version) + "/" + HttpUtility.UrlEncode(m_fileDetails.filename);
            String returnValue = m_client.URL + uri + "/" + urlParams + ";jsessionid=" + m_client.Cookie.Value;

            return returnValue;
        }


        /// <summary>
        /// Update repository file
        /// </summary>
        /// <param name="restricted">indicates if file is restricted</param>
        /// <param name="sharedUser">indicates if file is shared</param>
        /// <param name="published">indicates if file is published</param>
        /// <param name="descr">description of the file</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile update(String restricted, Boolean sharedUser, Boolean published, String descr)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYFILEUPDATE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(m_fileDetails.directory));
            data.Append("&shared=" + sharedUser.ToString());
            data.Append("&published=" + published.ToString());
            data.Append("&restricted=" + HttpUtility.UrlEncode(restricted));
            data.Append("&descr=" + HttpUtility.UrlEncode(descr));
            data.Append("&inputs=" + HttpUtility.UrlEncode(m_fileDetails.inputs));
            data.Append("&outputs=" + HttpUtility.UrlEncode(m_fileDetails.outputs));
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
        /// Update repository file
        /// </summary>
        /// <param name="restricted">indicates if file is restricted</param>
        /// <param name="sharedUser">indicates if file is shared</param>
        /// <param name="published">indicates if file is published</param>
        /// <param name="descr">description of the file</param>
        /// <param name="inputs">description of inputs to the script</param>
        /// <param name="outputs">description of outputs from the script</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile update(String restricted, Boolean sharedUser, Boolean published, String descr, String inputs, String outputs)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYFILEUPDATE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(m_fileDetails.directory));
            data.Append("&shared=" + sharedUser.ToString());
            data.Append("&published=" + published.ToString());
            data.Append("&restricted=" + HttpUtility.UrlEncode(restricted));
            data.Append("&descr=" + HttpUtility.UrlEncode(descr));
            data.Append("&inputs=" + HttpUtility.UrlEncode(inputs));
            data.Append("&outputs=" + HttpUtility.UrlEncode(outputs));
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
        /// Retrieve the versions of a repository file
        /// </summary>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> versions()
        {

            StringBuilder data = new StringBuilder();

            //set the url
            String uri =  Constants.RREPOSITORYFILELIST;

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&ispublic=false");
            data.Append("&isordered=false");
            data.Append("&filename=" + HttpUtility.UrlEncode(m_fileDetails.filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(m_fileDetails.directory));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            List<RRepositoryFile> returnValue = new List<RRepositoryFile>();

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["files"] == null))
                {
                    JArray jvalues = jrepo["files"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            returnValue.Add(new RRepositoryFile(new JSONResponse(j.Value<JObject>(), true, "", 0), m_client));
                        }
                    }
                }
            }

            return returnValue;
        }

        private void parseRepositoryFile(JSONResponse jresponse, ref RRepositoryFileDetails fileDetails)
        {
            List<String> authors = new List<String>();

            JObject jfile = jresponse.JSONMarkup;
            if (!(jfile == null))
            {

                String category = JSONUtilities.trimXtraQuotes(jfile["category"].Value<string>());
                String filename = JSONUtilities.trimXtraQuotes(jfile["filename"].Value<string>());
                String author = JSONUtilities.trimXtraQuotes(jfile["author"].Value<string>());
                int length = jfile["length"].Value<int>();
                String type = JSONUtilities.trimXtraQuotes(jfile["type"].Value<string>());
                String url = JSONUtilities.trimXtraQuotes(jfile["url"].Value<string>());
                String latestby = JSONUtilities.trimXtraQuotes(jfile["latestby"].Value<string>());
                String lastModified = JSONUtilities.trimXtraQuotes(jfile["lastModified"].Value<string>());
                Boolean sharedUsers = jfile["shared"].Value<Boolean>();
                Boolean published = jfile["published"].Value<Boolean>();
                String restricted = JSONUtilities.trimXtraQuotes(jfile["restricted"].Value<string>());
                String access = JSONUtilities.trimXtraQuotes(jfile["access"].Value<string>());
                String directory = JSONUtilities.trimXtraQuotes(jfile["directory"].Value<string>());
                String inputs = "";
                if (!(jfile["inputs"] == null))
                {
                    inputs = JSONUtilities.trimXtraQuotes(jfile["inputs"].Value<string>());
                }
                String outputs = "";
                if (!(jfile["outputs"] == null))
                {
                    outputs = JSONUtilities.trimXtraQuotes(jfile["outputs"].Value<string>());
                }
                String version = JSONUtilities.trimXtraQuotes(jfile["version"].Value<string>());
                if (version == "null")
                {
                    version = "";
                }

                if (!(jfile["authors"] == null))
                {
                    JArray jvalues = jfile["authors"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            authors.Add(j.Value<string>());
                        }
                    }
                }

                fileDetails = new RRepositoryFileDetails(category,
                    filename,
                    author,
                    version,
                    latestby,
                    lastModified,
                    length,
                    type,
                    url,
                    sharedUsers,
                    published,
                    restricted,
                    access,
                    authors,
                    inputs,
                    outputs,
                    directory);

            }

        }

    }
}