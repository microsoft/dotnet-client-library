/*
 * RProjectDirectoryImpl.cs
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

    internal class RProjectDirectoryImpl
    {

        static public String downloadFiles(RProjectDetails details, List<String> files, RClient client, String uri)
        {
            String returnValue = "";
            StringBuilder filenames = new StringBuilder();

            if (!(files == null))
            {
                if (files.Count > 0)
                {
                    foreach (var s in files)
                    {
                        filenames.Append(HttpUtility.UrlEncode(s) + ",");
                    }
                    filenames.Remove(filenames.Length - 1, 1);
                }
            }


            if (filenames.Length > 0)
            {
                returnValue = client.URL + uri + "/" + details.id + "/" + HttpUtility.UrlEncode(filenames.ToString()) + ";jsessionid=" + client.Cookie.Value;
            }
            else
            {
                returnValue = client.URL + uri + "/" + details.id + ";jsessionid=" + client.Cookie.Value;
            }

            return returnValue;
        }

        static public List<RProjectFile> listFiles(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RProjectFile> returnValue = new List<RProjectFile>();

            if (!(jresponse.JSONMarkup["directory"] == null))
            {
                JObject jdir = jresponse.JSONMarkup["directory"].Value<JObject>();
                if (!(jdir["files"] == null))
                {
                    JArray jvalues = jdir["files"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            returnValue.Add(new RProjectFile(new JSONResponse(j.Value<JObject>(), true, "", 0), client, details.id));
                        }
                    }
                }
            }

            return returnValue;
        }

        static public RProjectFile loadFile(RProjectDetails details, RRepositoryFile file, RClient client, String uri)
        {
            RProjectFile returnValue = default(RProjectFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&filename=" + HttpUtility.UrlEncode(file.about().filename));
            data.Append("&author=" + HttpUtility.UrlEncode(file.about().author));
            data.Append("&version=" + HttpUtility.UrlEncode(file.about().version));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            if (!(jresponse.JSONMarkup["directory"] == null))
            {
                JObject jdir = jresponse.JSONMarkup["directory"].Value<JObject>();
                if (!(jdir["file"] == null))
                {
                    JObject jfile = jdir["file"].Value<JObject>();
                    returnValue = new RProjectFile(new JSONResponse(jfile, true, "", 0), client, details.id);
                }
            }

            return returnValue;
        }

        static public RProjectFile transferFile(RProjectDetails details, String url, DirectoryUploadOptions options, RClient client, String uri)
        {
            RProjectFile returnValue = default(RProjectFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&url=" + HttpUtility.UrlEncode(url));
            if (!(options == null))
            {
                data.Append("&filename=" + HttpUtility.UrlEncode(options.filename));
                data.Append("&descr=" + HttpUtility.UrlEncode(options.descr));
                data.Append("&overwrite=" + options.overwrite.ToString());
            }
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            if (!(jresponse.JSONMarkup["directory"] == null))
            {
                JObject jdir = jresponse.JSONMarkup["directory"].Value<JObject>();
                if (!(jdir["file"] == null))
                {
                    JObject jfile = jdir["file"].Value<JObject>();
                    returnValue = new RProjectFile(new JSONResponse(jfile, true, "", 0), client, details.id);
                }
            }

            return returnValue;
        }

        static public RProjectFile uploadFile(RProjectDetails details, String file, DirectoryUploadOptions options, RClient client, String uri)
        {
            RProjectFile returnValue = default(RProjectFile);
            StringBuilder data = new StringBuilder();
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("format", "json");
            parameters.Add("project", HttpUtility.UrlEncode(details.id));

            //create the input String
            if (!(options == null))
            {
                parameters.Add("filename", HttpUtility.UrlEncode(options.filename));
                parameters.Add("descr", HttpUtility.UrlEncode(options.descr));
                parameters.Add("overwrite", options.overwrite.ToString());
            }
            else
            {
                parameters.Add("filename", HttpUtility.UrlEncode(Path.GetFileName(file)));
            }
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTFileUploadPost(uri, parameters, file, ref client);

            if (!(jresponse.JSONMarkup["directory"] == null))
            {
                JObject jdir = jresponse.JSONMarkup["directory"].Value<JObject>();
                if (!(jdir["file"] == null))
                {
                    JObject jfile = jdir["file"].Value<JObject>();
                    returnValue = new RProjectFile(new JSONResponse(jfile, true, "", 0), client, details.id);
                }
            }

            return returnValue;
        }

        static public RProjectFile writeFile(RProjectDetails details, String text, DirectoryUploadOptions options, RClient client, String uri)
        {
            RProjectFile returnValue = default(RProjectFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&text=" + HttpUtility.UrlEncode(text));
            if (!(options == null))
            {
                data.Append("&filename=" + HttpUtility.UrlEncode(options.filename));
                data.Append("&descr=" + HttpUtility.UrlEncode(options.descr));
                data.Append("&overwrite=" + options.overwrite.ToString());
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            if (!(jresponse.JSONMarkup["directory"] == null))
            {
                JObject jdir = jresponse.JSONMarkup["directory"].Value<JObject>();
                if (!(jdir["file"] == null))
                {
                    JObject jfile = jdir["file"].Value<JObject>();
                    returnValue = new RProjectFile(new JSONResponse(jfile, true, "", 0), client, details.id);
                }
            }

            return returnValue;
        }

    }
}