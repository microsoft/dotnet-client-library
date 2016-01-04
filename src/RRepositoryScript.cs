/*
 * RRepositoryScript.cs
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
/// <summary>
/// Represents a Script contained in the repository
/// </summary>
/// <remarks></remarks>
    public class RRepositoryScript
    {

        private RClient m_client;
        private RRepositoryScriptDetails m_scriptDetails;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RRepositoryScript()
        {

        }

        internal RRepositoryScript(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseRepositoryScript(jresponse, ref m_scriptDetails);
            }

        }
        /// <summary>
        /// Gets the details associated with this script
        /// </summary>
        /// <returns>RRepositoryScriptDetails object</returns>
        /// <remarks></remarks>
        public RRepositoryScriptDetails about()
        {
            return m_scriptDetails;
        }

        private void parseRepositoryScript(JSONResponse jresponse, ref RRepositoryScriptDetails scriptDetails)
        {

            List<Dictionary<String, String>> inputs = new List<Dictionary<String, String>>();
            List<Dictionary<String, String>> outputs = new List<Dictionary<String, String>>();
            Dictionary<String, String> dic = new Dictionary<String, String>();

            JObject jscript = jresponse.JSONMarkup;
            if (!(jscript == null))
            {
                String name = JSONUtilities.trimXtraQuotes(jscript["name"].Value<string>());
                String descr = JSONUtilities.trimXtraQuotes(jscript["descr"].Value<string>());

                if (!(jscript["inputs"] == null))
                {
                    JArray jvalues = jscript["inputs"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            dic = new Dictionary<String, String>();
                            dic.Add("name", JSONUtilities.trimXtraQuotes(j["name"].Value<String>()));
                            dic.Add("rclass", JSONUtilities.trimXtraQuotes(j["rclass"].Value<String>()));
                            dic.Add("descr", JSONUtilities.trimXtraQuotes(j["descr"].Value<String>()));
                            dic.Add("type", JSONUtilities.trimXtraQuotes(j["type"].Value<String>()));
                            inputs.Add(dic);
                        }
                    }
                }

                if (!(jscript["outputs"] == null))
                {
                    JArray jvalues = jscript["outputs"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            dic = new Dictionary<String, String>();
                            dic.Add("name", JSONUtilities.trimXtraQuotes(j["name"].Value<String>()));
                            dic.Add("rclass", JSONUtilities.trimXtraQuotes(j["rclass"].Value<String>()));
                            dic.Add("descr", JSONUtilities.trimXtraQuotes(j["descr"].Value<String>()));
                            dic.Add("type", JSONUtilities.trimXtraQuotes(j["type"].Value<String>()));
                            outputs.Add(dic);
                        }
                    }
                }

                scriptDetails = new RRepositoryScriptDetails(descr, inputs, name, outputs);

            }




        }

    }
}