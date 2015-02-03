/*
 * HTTPUtilities.cs
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

namespace DeployR
{

/// <summary>
/// Utility functions for communicating with the DeployR Server
/// </summary>
/// <remarks></remarks>
    sealed class HTTPUtilities
    {

        public static Boolean DEBUGMODE = false;
        public static int HTTP_TIMEOUT = 1000000;

        /// <summary>
        /// Make an HTTP POST request on one of the DeployR APIs
        /// </summary>
        /// <param name="uri">Base URL of the DeployR server</param>
        /// <param name="data">The data to send on the HTTP POST call</param>
        /// <param name="client">reference to the RClient calling this function</param>
        /// <returns>JSONResponse object</returns>
        /// <remarks></remarks>
        public static JSONResponse callRESTPost(String uri, String data, ref RClient client)
        {

            HttpWebResponse response = null;
            Stream postStream = null;
            String responseText = "";
            String address = client.URL + uri;

            // Create the web request
            HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(address));
            request.Timeout = HTTP_TIMEOUT;

            // Set the cookie
            CookieContainer cookieJar = new CookieContainer();
            if (!(client.Cookie == null))
            {
                cookieJar.Add(client.Cookie);
            }

            // Set type to POST
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookieJar;

            // Create a byte array of the data we want to send
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

            // Set the content length in the request headers
            request.ContentLength = byteData.Length;

            if (HTTPUtilities.DEBUGMODE == true)
            {
                Console.WriteLine("\r\n" + request.Method + "  " + request.Address.AbsoluteUri);
                if (!(client.Cookie == null))
                {
                    Console.WriteLine(client.Cookie.Name + "=" + client.Cookie.Value);
                }
                Console.WriteLine(data);
            }

            // Write data
            try
            {
                postStream = request.GetRequestStream();
                postStream.Write(byteData, 0, byteData.Length);
            }
            finally
            {
                if (!(postStream == null))
                {
                    postStream.Close();
                }
            }
            try
            {
                // Get response
                response = (HttpWebResponse)(request.GetResponse());
                // Save the cookie (if there is one)
                if (response.Cookies.Count > 0)
                {
                    client.Cookie = response.Cookies[0];
                }

                // Get the response stream into a reader
                StreamReader reader = new StreamReader(response.GetResponseStream());

                responseText = reader.ReadToEnd();

                if (HTTPUtilities.DEBUGMODE == true)
                {
                    Console.WriteLine(responseText);
                }


            }
            finally
            {
                if (!(response == null))
                {
                    response.Close();
                }
            }

            //return a JSON response
            JSONResponse returnValue = JSONUtilities.checkForSuccess(responseText);

            //throw an exception if the call failed
            if (returnValue.Success == false)
            {
                HTTPRestException e = new HTTPRestException(returnValue.ErrorMsg, returnValue.Console, returnValue.ErrorCode);
                throw (e);
            }

            return returnValue;
        }
        /// <summary>
        /// Make an HTTP GET request on one of the DeployR APIs
        /// </summary>
        /// <param name="uri">Base URL of the DeployR server</param>
        /// <param name="data">The data to send on the HTTP GET call</param>
        /// <param name="client">reference to the RClient calling this function</param>
        /// <returns>JSONResponse object</returns>
        /// <remarks></remarks>
        public static JSONResponse callRESTGet(String uri, String data, ref RClient client)
        {

            HttpWebResponse response = null;
            StreamReader reader = default(StreamReader);
            String responseText = "";
            String address = client.URL + uri;

            // Create the web request
            String url = address + "?" + data;
            HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(url));
            request.Timeout = HTTP_TIMEOUT;

            // Set the cookie
            CookieContainer cookieJar = new CookieContainer();
            if (!(client.Cookie == null))
            {
                cookieJar.Add(client.Cookie);
            }

            // Set type to POST
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookieJar;

            if (HTTPUtilities.DEBUGMODE == true)
            {
                Console.WriteLine("\r\n" + request.Method + "  " + request.Address.AbsoluteUri);
                if (!(client.Cookie == null))
                {
                    Console.WriteLine(client.Cookie.Name + "=" + client.Cookie.Value);
                }
                Console.WriteLine(data);
            }

            try
            {
                // Get response
                response = (HttpWebResponse)(request.GetResponse());
                // Save the cookie (if there is one)
                if (response.Cookies.Count > 0)
                {
                    client.Cookie = response.Cookies[0];
                }

                // Get the response stream into a reader
                reader = new StreamReader(response.GetResponseStream());

                responseText = reader.ReadToEnd();

                if (HTTPUtilities.DEBUGMODE == true)
                {
                    Console.WriteLine(responseText);
                }

            }
            finally
            {
                if (!(response == null))
                {
                    response.Close();
                }
            }

            //return a JSON response
            JSONResponse returnValue = JSONUtilities.checkForSuccess(responseText);

            //throw an exception if the call failed
            if (returnValue.Success == false)
            {
                HTTPRestException e = new HTTPRestException(returnValue.ErrorMsg, returnValue.Console, returnValue.ErrorCode);
                throw (e);
            }

            return returnValue;
        }
        /// <summary>
        /// Make an HTTP GET request on one of the DeployR APIs that returns HTML Markup
        /// </summary>
        /// <param name="uri">Base URL of the DeployR server</param>
        /// <param name="data">The data to send on the HTTP GET call</param>
        /// <param name="client">reference to the RClient calling this function</param>
        /// <returns>HTML Markup</returns>
        /// <remarks></remarks>
        public static String callRESTHTMLGet(String uri, String data, ref RClient client)
        {
            HttpWebResponse response = null;
            StreamReader reader = default(StreamReader);
            String responseText = "";
            String address = client.URL + uri;

            // Create the web request
            String url = address + "?" + data;
            HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(url));
            request.Timeout = HTTP_TIMEOUT;

            // Set the cookie
            CookieContainer cookieJar = new CookieContainer();
            if (!(client.Cookie == null))
            {
                cookieJar.Add(client.Cookie);
            }

            // Set type to POST
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookieJar;

            if (HTTPUtilities.DEBUGMODE == true)
            {
                Console.WriteLine("\r\n" + request.Method + "  " + request.Address.AbsoluteUri);
                if (!(client.Cookie == null))
                {
                    Console.WriteLine(client.Cookie.Name + "=" + client.Cookie.Value);
                }
                Console.WriteLine(data);
            }

            try
            {
                // Get response
                response = (HttpWebResponse)(request.GetResponse());
                // Save the cookie (if there is one)
                if (response.Cookies.Count > 0)
                {
                    client.Cookie = response.Cookies[0];
                }

                // Get the response stream into a reader
                reader = new StreamReader(response.GetResponseStream());

                responseText = reader.ReadToEnd();

                if (HTTPUtilities.DEBUGMODE == true)
                {
                    Console.WriteLine(responseText);
                }

            }
            finally
            {
                if (!(response == null))
                {
                    response.Close();
                }
            }

            //return a JSON response
            String returnValue = responseText;

            return returnValue;
        }

        /// <summary>
        ///Make an HTTP GET request on one of the DeployR APIs that downloads a file.
        /// </summary>
        /// <param name="uri">Base URL of the DeployR server</param>
        /// <param name="data">The data to send on the HTTP GET call</param>
        /// <param name="client">reference to the RClient calling this function</param>
        /// <returns>Byte Array that contains the content of the file</returns>
        /// <remarks></remarks>
        public static byte[] callRESTBytesGet(String uri, String data, ref RClient client)
        {

            HttpWebResponse response = null;
            byte[] returnBytes = null;
            String address = client.URL + uri;

            // Create the web request
            String url = address + "?" + data;
            HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(url));
            request.Timeout = HTTP_TIMEOUT;

            // Set the cookie
            CookieContainer cookieJar = new CookieContainer();
            if (!(client.Cookie == null))
            {
                cookieJar.Add(client.Cookie);
            }

            // Set type to POST
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookieJar;

            try
            {
                // Get response
                response = (HttpWebResponse)(request.GetResponse());
                // Save the cookie (if there is one)
                if (response.Cookies.Count > 0)
                {
                    client.Cookie = response.Cookies[0];
                }

                //get the bytes assoscated with the response
                Stream bytereader = response.GetResponseStream();
                MemoryStream memStream = new MemoryStream();
                byte[] byteData = new byte[401];

                int i = bytereader.Read(byteData, 0, byteData.Length);
                while (!(i == 0))
                {
                    memStream.Write(byteData, 0, i);
                    i = bytereader.Read(byteData, 0, byteData.Length);
                }

                returnBytes = memStream.ToArray();
            }
            finally
            {
                if (!(response == null))
                {
                    response.Close();
                }
            }

            //return the byte array
            byte[] returnValue = returnBytes;

            return returnValue;
        }
        /// <summary>
        /// A function to handle a mulitp-part HTTP POST that includes a file upload
        /// </summary>
        /// <param name="uri">Base URL of the DeployR server</param>
        /// <param name="parameters">Dictionary of form parameters to send with the POST</param>
        /// <param name="path">Fully qualified path to file to upload</param>
        /// <param name="client">reference to the RClient calling this function</param>
        /// <returns>JSONResponse object</returns>
        /// <remarks></remarks>
        public static JSONResponse callRESTFileUploadPost(String uri, Dictionary<String, String> parameters, String path, ref RClient client)
        {

            HttpWebResponse response = null;
            Stream postStream = null;
            String responseText = "";
            String address = client.URL + uri;

            // Create the web request
            HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(address));
            request.Timeout = HTTP_TIMEOUT;

            // Set the cookie
            CookieContainer cookieJar = new CookieContainer();
            if (!(client.Cookie == null))
            {
                cookieJar.Add(client.Cookie);
            }

            // Set type to POST
            request.Method = "POST";
            //this is a multipart request, so we need a boundary and need to setup the proper ContentType
            String boundary = System.Guid.NewGuid().ToString();
            request.ContentType = String.Format("multipart/form-data;boundary={0}", boundary);
            request.CookieContainer = cookieJar;

            //header and footer to be used for the boundary
            String header = String.Format("--{0}", boundary);
            String footer = header + "--";

            StringBuilder contents = new StringBuilder();
            //loop through the parameters in this POST call
            foreach (var param in parameters)
            {
                contents.Append(header);
                contents.Append("\r\n");
                contents.Append("Content-Disposition: form-data;name=\"" + param.Key + "\"" + "\r\n");
                contents.Append("\r\n");
                contents.Append(param.Value);
                contents.Append("\r\n");
            }

            //ok, here we do something special for the file we are going to upload
            contents.Append(header);
            contents.Append("\r\n");
            contents.Append("Content-Disposition:form-data;name=\"file\";filename=\"" + path + "\"" + "\r\n");
            contents.Append("Content-Type: application/octet-stream" + "\r\n");
            contents.Append("\r\n");

            //get all the various byte streams we need
            byte[] bodyBytes = Encoding.UTF8.GetBytes(contents.ToString());
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            byte[] footerBytes = Encoding.UTF8.GetBytes("\r\n" + footer + "\r\n");

            // Set the content length in the request headers
            request.ContentLength = bodyBytes.Length + fileData.Length + footerBytes.Length;

            if (HTTPUtilities.DEBUGMODE == true)
            {
                Console.WriteLine("\r\n" + request.Method + "  " + request.Address.AbsoluteUri);
                if (!(client.Cookie == null))
                {
                    Console.WriteLine(client.Cookie.Name + "=" + client.Cookie.Value);
                }
                Console.Write(contents.ToString());
            }

            // Write data
            try
            {
                postStream = request.GetRequestStream();
                postStream.Write(bodyBytes, 0, bodyBytes.Length);
                postStream.Write(fileData, 0, fileData.Length);
                postStream.Write(footerBytes, 0, footerBytes.Length);
            }
            finally
            {
                if (!(postStream == null))
                {
                    postStream.Close();
                }
            }

            try
            {
                // Get response
                response = (HttpWebResponse)(request.GetResponse());
                // Save the cookie (if there is one)
                if (response.Cookies.Count > 0)
                {
                    client.Cookie = response.Cookies[0];
                }

                // Get the response stream into a reader
                StreamReader reader = new StreamReader(response.GetResponseStream());

                responseText = reader.ReadToEnd();

                if (HTTPUtilities.DEBUGMODE == true)
                {
                    Console.WriteLine(responseText);
                }

            }
            finally
            {
                if (!(response == null))
                {
                    response.Close();
                }
            }

            //return a JSON response
            JSONResponse returnValue = JSONUtilities.checkForSuccess(responseText);

            //throw an exception if the call failed
            if (returnValue.Success == false)
            {
                HTTPRestException e = new HTTPRestException(returnValue.ErrorMsg, returnValue.Console, returnValue.ErrorCode);
                throw (e);
            }

            return returnValue;
        }

    }
}
