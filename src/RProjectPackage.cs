/*
 * RProjectPackage.cs
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
/// <summary>
/// Represents an RPackage in a Project
/// </summary>
/// <remarks></remarks>
    public class RProjectPackage
    {

        private RClient m_client;
        private RProjectPackageDetails m_packageDetails;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProjectPackage()
        {

        }

        internal RProjectPackage(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseProjectPackage(jresponse, ref m_packageDetails);
            }

        }
        /// <summary>
        /// Gets the details associated with this R Package
        /// </summary>
        /// <returns>RProjectPackageDetails object</returns>
        /// <remarks></remarks>
        public RProjectPackageDetails about()
        {

            return m_packageDetails;
        }

        private void parseProjectPackage(JSONResponse jresponse, ref RProjectPackageDetails packageDetails)
        {

            JObject jprojectfile = jresponse.JSONMarkup;
            if (!(jprojectfile == null))
            {
                String descr = JSONUtilities.trimXtraQuotes(jprojectfile["descr"].Value<String>());
                String name = JSONUtilities.trimXtraQuotes(jprojectfile["name"].Value<String>());
                String repo = JSONUtilities.trimXtraQuotes(jprojectfile["repo"].Value<String>());
                String status = JSONUtilities.trimXtraQuotes(jprojectfile["status"].Value<String>());
                String version = JSONUtilities.trimXtraQuotes(jprojectfile["version"].Value<String>());
                Boolean attached = jprojectfile["attached"].Value<Boolean>();

                packageDetails = new RProjectPackageDetails(descr, name, repo, status, version, attached);
            }

        }

    }
}