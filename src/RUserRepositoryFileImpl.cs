/*
 * RUserRepositoryFileImpl.cs
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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
    internal class RUserRepositoryFileImpl
    {

        static public List<RRepositoryFile> listFiles(String filename, String directory, Boolean archived, Boolean sharedUsers, Boolean published, Boolean external, String categoryFilter, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(directory));
            data.Append("&archived=" + archived.ToString());
            data.Append("&shared=" + sharedUsers.ToString());
            data.Append("&published=" + published.ToString());
            data.Append("&external=" + external.ToString());
            data.Append("&categoryFilter=" + HttpUtility.UrlEncode(categoryFilter));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

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
                            returnValue.Add(new RRepositoryFile(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                        }
                    }
                }
            }

            return returnValue;
        }

        static public RRepositoryFile transferFile(String url, RepoUploadOptions options, RClient client, String uri)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&url=" + HttpUtility.UrlEncode(url));
            if (!(options == null))
            {
                data.Append("&filename=" + HttpUtility.UrlEncode(options.filename));
                data.Append("&directory=" + HttpUtility.UrlEncode(options.directory));
                data.Append("&descr=" + HttpUtility.UrlEncode(options.descr));
                data.Append("&shared=" + options.sharedUser.ToString());
                data.Append("&published=" + options.published.ToString());
                data.Append("&restricted=" + HttpUtility.UrlEncode(options.restricted));
                data.Append("&inputs=" + HttpUtility.UrlEncode(options.inputs));
                data.Append("&outputs=" + HttpUtility.UrlEncode(options.outputs));
                data.Append("&newversion=" + options.newversion.ToString());
                data.Append("&newversionmsg=" + HttpUtility.UrlEncode(options.newversionmsg));
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["file"] == null))
                {
                    JObject jfile = jrepo["file"].Value<JObject>();
                    returnValue = new RRepositoryFile(new JSONResponse(jfile, true, "", 0), client);
                    return returnValue;
                }
            }

            return returnValue;
        }

        static public RRepositoryFile uploadFile(String file, RepoUploadOptions options, RClient client, String uri)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            //create the input String
            if (!(options == null))
            {
                parameters.Add("format", "json");
                parameters.Add("filename", HttpUtility.UrlEncode(options.filename));
                parameters.Add("directory", HttpUtility.UrlEncode(options.directory));
                parameters.Add("descr", HttpUtility.UrlEncode(options.descr));
                parameters.Add("shared", options.sharedUser.ToString());
                parameters.Add("published", options.published.ToString());
                parameters.Add("restricted", HttpUtility.UrlEncode(options.restricted));
                parameters.Add("inputs", HttpUtility.UrlEncode(options.inputs));
                parameters.Add("outputs", HttpUtility.UrlEncode(options.outputs));
                parameters.Add("newversion", options.newversion.ToString());
                parameters.Add("newversionmsg", HttpUtility.UrlEncode(options.newversionmsg));
            }
            else
            {
                parameters.Add("filename", HttpUtility.UrlEncode(Path.GetFileName(file)));
            }
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTFileUploadPost(uri, parameters, file, ref client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["file"] == null))
                {
                    JObject jfile = jrepo["file"].Value<JObject>();
                    returnValue = new RRepositoryFile(new JSONResponse(jfile, true, "", 0), client);
                    return returnValue;
                }
            }

            return returnValue;
        }

        static public RRepositoryFile writeFile(String text, RepoUploadOptions options, RClient client, String uri)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&text=" + HttpUtility.UrlEncode(text));
            if (!(options == null))
            {
                data.Append("&filename=" + HttpUtility.UrlEncode(options.filename));
                data.Append("&directory=" + HttpUtility.UrlEncode(options.directory));
                data.Append("&descr=" + HttpUtility.UrlEncode(options.descr));
                data.Append("&shared=" + options.sharedUser.ToString());
                data.Append("&published=" + options.published.ToString());
                data.Append("&restricted=" + HttpUtility.UrlEncode(options.restricted));
                data.Append("&inputs=" + HttpUtility.UrlEncode(options.inputs));
                data.Append("&outputs=" + HttpUtility.UrlEncode(options.outputs));
                data.Append("&newversion=" + options.newversion.ToString());
                data.Append("&newversionmsg=" + HttpUtility.UrlEncode(options.newversionmsg));
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["file"] == null))
                {
                    JObject jfile = jrepo["file"].Value<JObject>();
                    returnValue = new RRepositoryFile(new JSONResponse(jfile, true, "", 0), client);
                }
            }

            return returnValue;
        }

        static public RRepositoryFile fetchFile(String filename, String author, String directory, String version, RClient client, String uri)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(directory));
            data.Append("&author=" + HttpUtility.UrlEncode(author));
            data.Append("&version=" + HttpUtility.UrlEncode(version));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["file"] == null))
                {
                    JObject jfile = jrepo["file"].Value<JObject>();
                    returnValue = new RRepositoryFile(new JSONResponse(jfile, true, "", 0), client);
                }
            }

            return returnValue;
        }

        static public void copyFiles(String destination, List<RRepositoryFile> files, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();
            StringBuilder filenames = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(destination));

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
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public void moveFiles(String destination, List<RRepositoryFile> files, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();
            StringBuilder filenames = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(destination));

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
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

    }
}