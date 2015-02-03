/*
 * RUserRepositoryDirectoryImpl.cs
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
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
    internal class RUserRepositoryDirectoryImpl
    {

        static public void copyDirectory(String source, String destination, List<RRepositoryFile> files, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();
            StringBuilder filenames = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(source));
            data.Append("&destination=" + HttpUtility.UrlEncode(destination));

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

        static public RRepositoryDirectory createDirectory(String directory, RClient client, String uri)
        {
            RRepositoryDirectory returnValue = default(RRepositoryDirectory);

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(directory.Trim()));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["directory"] == null))
                {
                    JObject jdir = jrepo["directory"].Value<JObject>();
                    returnValue = new RRepositoryDirectory(new JSONResponse(jdir, true, "", 0), client);
                }
            }

            return returnValue;
        }

        static public List<RRepositoryDirectory> listDirectories(Boolean userfiles, Boolean archived, Boolean sharedUsers, Boolean published, Boolean external, String directory, String category, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&userfiles=" + userfiles.ToString());
            data.Append("&archived=" + archived.ToString());
            data.Append("&shared=" + sharedUsers.ToString());
            data.Append("&published=" + published.ToString());
            data.Append("&external=" + external.ToString());
            data.Append("&directory=" + HttpUtility.UrlEncode(directory.Trim()));
            data.Append("&categoryFilter=" + HttpUtility.UrlEncode(category.Trim()));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RRepositoryDirectory> returnValue = new List<RRepositoryDirectory>();

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["directories"] == null))
                {
                    JObject jdirs = jrepo["directories"].Value<JObject>();
                    if (!(jdirs["user"] == null))
                    {
                        JArray jvalues = jdirs["user"].Value<JArray>();
                        foreach (var j in jvalues)
                        {
                            if (j.Type != JTokenType.Null)
                            {
                                returnValue.Add(new RRepositoryDirectory(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                            }
                        }
                    }
                    if (!(jdirs["system"] == null))
                    {
                        JArray jvalues = jdirs["system"].Value<JArray>();
                        foreach (var j in jvalues)
                        {
                            if (j.Type != JTokenType.Null)
                            {
                                returnValue.Add(new RRepositoryDirectory(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                            }
                        }
                    }
                }
            }

            return returnValue;
        }

        static public void moveDirectory(String source, String destination, List<RRepositoryFile> files, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();
            StringBuilder filenames = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&directory=" + HttpUtility.UrlEncode(source));
            data.Append("&destination=" + HttpUtility.UrlEncode(destination));

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

        static public void uploadDirectory(String file, RepoUploadOptions options, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            //create the input String
            if (!(options == null))
            {
                parameters.Add("format", "json");
                parameters.Add("directory", HttpUtility.UrlEncode(options.directory));
                parameters.Add("descr", HttpUtility.UrlEncode(options.descr));
                parameters.Add("shared", options.sharedUser.ToString());
                parameters.Add("published", options.published.ToString());
                parameters.Add("restricted", HttpUtility.UrlEncode(options.restricted));
                parameters.Add("newversion", options.newversion.ToString());
                parameters.Add("newversionmsg", HttpUtility.UrlEncode(options.newversionmsg));
            }
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTFileUploadPost(uri, parameters, file, ref client);

        }


    }
}