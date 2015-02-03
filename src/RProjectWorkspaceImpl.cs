/*
 * RProjectWorkspaceImpl.cs
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

    internal class RProjectWorkspaceImpl
    {

        static public void deleteObject(RProjectDetails details, List<String> objectNames, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(objectNames == null))
            {
                if (objectNames.Count > 0)
                {
                    data.Append("&name=");
                    foreach (var s in objectNames)
                    {
                        data.Append(HttpUtility.UrlEncode(s) + ",");
                    }
                    data.Remove(data.Length - 1, 1);
                }
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public List<RData> getObject(RProjectDetails details, List<String> objectNames, Boolean encodeDataFramePrimitiveAsVector, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(objectNames == null))
            {
                if (objectNames.Count > 0)
                {
                    data.Append("&name=");
                    foreach (var s in objectNames)
                    {
                        data.Append(HttpUtility.UrlEncode(s) + ",");
                    }
                    data.Remove(data.Length - 1, 1);
                }
            }
            data.Append("&encodeDataFramePrimitiveAsVector=" + encodeDataFramePrimitiveAsVector.ToString());

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RData> returnValue = new List<RData>();
            returnValue = JSONUtilities.parseRObjects(jresponse.JSONMarkup);

            return returnValue;
        }

        static public List<RData> listObjects(RProjectDetails details, ProjectWorkspaceOptions options, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(options == null))
            {
                data.Append("&root=" + HttpUtility.UrlEncode(options.alternateRoot));
                data.Append("&filter=" + HttpUtility.UrlEncode(options.startsWithFilter));
                data.Append("&clazz=" + HttpUtility.UrlEncode(options.classFilter));
                data.Append("&pagesize=" + options.pagesize.ToString());
                data.Append("&pageoffset=" + options.pageoffset.ToString());
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RData> returnValue = JSONUtilities.parseRObjects(jresponse.JSONMarkup);


            return returnValue;
        }

        static public void loadObject(RProjectDetails details, RRepositoryFile file, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(file == null))
            {
                data.Append("&filename=" + HttpUtility.UrlEncode(file.about().filename));
                data.Append("&author=" + HttpUtility.UrlEncode(file.about().author));
                data.Append("&version=" + HttpUtility.UrlEncode(file.about().version));
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public void pushObject(RProjectDetails details, List<RData> inputs, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(inputs == null))
            {
                if (inputs.Count > 0)
                {
                    data.Append("&inputs=");
                    String sJSON = JSONSerialize.createJSONfromRData(inputs);
                    data.Append(HttpUtility.UrlEncode(sJSON));
                    if (HTTPUtilities.DEBUGMODE == true)
                    {
                        Console.Write(sJSON);
                    }
                }
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public RProjectFile saveObject(RProjectDetails details, String name, String descr, Boolean versioning, RClient client, String uri)
        {
            RProjectFile returnValue = default(RProjectFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&name=" + HttpUtility.UrlEncode(name));
            data.Append("&descr=" + HttpUtility.UrlEncode(descr));
            data.Append("&version=" + versioning.ToString());

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

        static public RRepositoryFile storeObject(RProjectDetails details, String name, Boolean sharedUser, Boolean published, String restricted, String descr, Boolean versioning, RClient client, String uri)
        {
            RRepositoryFile returnValue = default(RRepositoryFile);
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&name=" + HttpUtility.UrlEncode(name));
            data.Append("&descr=" + HttpUtility.UrlEncode(descr));
            data.Append("&version=" + versioning.ToString());
            data.Append("&shared=" + sharedUser.ToString());
            data.Append("&published=" + published.ToString());
            data.Append("&restricted=" + HttpUtility.UrlEncode(restricted));

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

        static public void transferObject(RProjectDetails details, String name, String url, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&name=" + HttpUtility.UrlEncode(name));
            data.Append("&url=" + HttpUtility.UrlEncode(url));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public void uploadObject(RProjectDetails details, String name, String filename, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            //create the input String
            parameters.Add("format", "json");
            parameters.Add("project", HttpUtility.UrlEncode(details.id));
            parameters.Add("name", HttpUtility.UrlEncode(name));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTFileUploadPost(uri, parameters, filename, ref client);

        }

    }
}