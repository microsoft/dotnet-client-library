/*
 * JSONSerialize.cs
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
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
/// <summary>
/// Set of helper functions to serialize/deserialize RData objects
/// </summary>
/// <remarks></remarks>
    sealed class JSONSerialize
    {

        /// <summary>
        /// Create an RData object from a json.net JObject referenceing some JSON markup
        /// </summary>
        /// <param name="jobject">JObject containing JSON markup</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseRObject(JToken jobject)
        {
            String type = JSONUtilities.trimXtraQuotes(jobject["type"].Value<string>());
            String rclass = JSONUtilities.trimXtraQuotes(jobject["rclass"].Value<string>());
            String name = JSONUtilities.trimXtraQuotes(jobject["name"].Value<string>());

            RData data = checkEmptyValue(jobject, type, rclass, name);
            if (data == null)
            {
                if (type == Constants.TYPE_MATRIX)
                {
                    data = parseMatrix(name, jobject);
                }
                else if (type == Constants.TYPE_VECTOR)
                {
                    data = parseVector(name, jobject, rclass);
                }
                else if (type == Constants.TYPE_PRIMITIVE)
                {
                    data = parsePrimitive(name, jobject);
                }
                else if (type == Constants.TYPE_DATE)
                {
                    data = parseDate(name, jobject);
                }
                else if (type == Constants.TYPE_FACTOR)
                {
                    data = parseFactor(name, jobject);
                }
                else if (type == Constants.TYPE_DATAFRAME)
                {
                    data = parseDataFrame(name, jobject);
                }
                else if (type == Constants.TYPE_LIST)
                {
                    data = parseRList(name, jobject);
                }
            }

            return data;
        }
        /// <summary>
        /// Create an RData object from a json.net JObject referenceing some JSON markup
        /// </summary>
        /// <param name="jobject">JObject containing JSON markup</param>
        /// <param name="jparent">Parent object (JProperty, JObject, JArray)</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseRObject(JToken jobject, JToken jparent)
        {

            String type = JSONUtilities.trimXtraQuotes(jparent["type"].Value<string>());
            String rclass = JSONUtilities.trimXtraQuotes(jparent["rclass"].Value<string>());
            String name = System.Convert.ToString(jobject.Value<JObject>().Property("Key"));

            RData data = checkEmptyValue(jparent, type, rclass, name);
            if (data == null)
            {
                if (type == Constants.TYPE_MATRIX)
                {
                    data = parseMatrix(name, jobject);
                }
                else if (type == Constants.TYPE_VECTOR)
                {
                    data = parseVector(name, jobject, rclass);
                }
                else if (type == Constants.TYPE_PRIMITIVE)
                {
                    data = parsePrimitive(name, jobject);
                }
                else if (type == Constants.TYPE_DATE)
                {
                    data = parseDate(name, jobject);
                }
                else if (type == Constants.TYPE_FACTOR)
                {
                    data = parseFactor(name, jobject);
                }
                else if (type == Constants.TYPE_DATAFRAME)
                {
                    data = parseDataFrame(name, jobject);
                }
                else if (type == Constants.TYPE_LIST)
                {
                    data = parseRList(name, jobject);
                }
            }

            return data;
        }
        /// <summary>
        /// Create the appropriate RData class representing a matrix (RNumericMatrix, RBooleanMatrix, RStringMatrix)
        /// </summary>
        /// <param name="name">name of the RData instance</param>
        /// <param name="jparent">Parent json.net object</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseMatrix(String name, JToken jparent)
        {
            int primitiveType = -1;
            int iRows = -1;
            int iCols = -1;
            RData data = null;

            if (!(jparent["value"] == null))
            {
                JArray jvalues = jparent["value"].Value<JArray>();
                iRows = jvalues.Count - 1;
                foreach (JArray jsubvalues in jvalues)
                {
                    iCols = jsubvalues.Count - 1;
                    foreach (var j in jsubvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            if ((j.Type == JTokenType.Float) || (j.Type == JTokenType.Integer))
                            {
                                primitiveType = Constants._NUMERIC;
                            }
                            else if (j.Type == JTokenType.String)
                            {
                                primitiveType = Constants._CHARACTER;
                            }
                            else if (j.Type == JTokenType.Boolean)
                            {
                                primitiveType = Constants._LOGICAL;
                            }
                        }
                        if (primitiveType != -1)
                        {
                            break;
                        }
                    }
                    if (primitiveType != -1)
                    {
                        break;
                    }
                }
            }

            if (iRows < 0 | iCols < 0 | primitiveType < 0)
            {
                return data;
            }


            List<List<Double?>> numRowList = new List<List<Double?>>();
            List<List<String>> charRowList = new List<List<String>>();
            List<List<Boolean?>> logRowList = new List<List<Boolean?>>();
            List<Double?> numColList = new List<Double?>();
            List<String> charColList = new List<String>();
            List<Boolean?> logColList = new List<Boolean?>();

            if (!(jparent["value"] == null))
            {
                JArray jvalues = jparent["value"].Value<JArray>();
                foreach (JArray jsubvalues in jvalues)
                {
                    if (primitiveType == Constants._NUMERIC)
                    {
                        numColList = new List<Double?>();
                    }
                    else if (primitiveType == Constants._CHARACTER)
                    {
                        charColList = new List<String>();
                    }
                    else if (primitiveType == Constants._LOGICAL)
                    {
                        logColList = new List<Boolean?>();
                    }

                    foreach (var j in jsubvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            if (primitiveType == Constants._NUMERIC)
                            {
                                numColList.Add(j.Value<double>());
                            }
                            else if (primitiveType == Constants._CHARACTER)
                            {
                                charColList.Add(j.Value<string>());
                            }
                            else if (primitiveType == Constants._LOGICAL)
                            {
                                logColList.Add(j.Value<bool>());
                            }
                        }
                        else
                        {
                            if (primitiveType == Constants._NUMERIC)
                            {
                                numColList.Add(null);
                            }
                            else if (primitiveType == Constants._CHARACTER)
                            {
                                charColList.Add(null);
                            }
                            else if (primitiveType == Constants._LOGICAL)
                            {
                                logColList.Add(null);
                            }
                        }
                    }
                    if (primitiveType == Constants._NUMERIC)
                    {
                        numRowList.Add(numColList);
                    }
                    else if (primitiveType == Constants._CHARACTER)
                    {
                        charRowList.Add(charColList);
                    }
                    else if (primitiveType == Constants._LOGICAL)
                    {
                        logRowList.Add(logColList);
                    }
                }
            }

            if (primitiveType == Constants._NUMERIC)
            {
                data = new RNumericMatrix(name, numRowList);
            }
            else if (primitiveType == Constants._CHARACTER)
            {
                data = new RStringMatrix(name, charRowList);
            }
            else if (primitiveType == Constants._LOGICAL)
            {
                data = new RBooleanMatrix(name, logRowList);
            }

            return data;
        }
        /// <summary>
        /// Create the appropriate RData class representing a matrix (RNumericVector, RBooleanVector, RStringVector, RDateVector)
        /// </summary>
        /// <param name="name">name of the RData instance</param>
        /// <param name="jparent">Parent json.net object</param>
        /// <param name="rclass">R class of the object to create</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseVector(String name, JToken jparent, String rclass)
        {
            RData data = null;

            List<Double?> numList = new List<Double?>();
            List<String> charList = new List<String>();
            List<Boolean?> logList = new List<Boolean?>();
            List<String> dateList = new List<String>();

            if (!(jparent["value"] == null))
            {
                JArray jvalues = jparent["value"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        if (rclass == Constants.RCLASS_NUMERIC)
                        {
                            numList.Add(j.Value<double>());
                        }
                        else if (rclass == Constants.RCLASS_CHARACTER)
                        {
                            charList.Add(j.Value<string>());
                        }
                        else if (rclass == Constants.RCLASS_BOOLEAN)
                        {
                            logList.Add(j.Value<bool>());
                        }
                        else if (rclass == Constants.RCLASS_DATE)
                        {
                            dateList.Add(j.Value<string>());
                        }
                    }
                    else
                    {
                        if (rclass == Constants.RCLASS_NUMERIC)
                        {
                            numList.Add(null);
                        }
                        else if (rclass == Constants.RCLASS_CHARACTER)
                        {
                            charList.Add(null);
                        }
                        else if (rclass == Constants.RCLASS_BOOLEAN)
                        {
                            logList.Add(null);
                        }
                        else if (rclass == Constants.RCLASS_DATE)
                        {
                            dateList.Add(null);
                        }
                    }
                }
            }

            if (rclass == Constants.RCLASS_NUMERIC)
            {
                data = new RNumericVector(name, numList);
            }
            else if (rclass == Constants.RCLASS_CHARACTER)
            {
                data = new RStringVector(name, charList);
            }
            else if (rclass == Constants.RCLASS_BOOLEAN)
            {
                data = new RBooleanVector(name, logList);
            }
            else if (rclass == Constants.RCLASS_DATE)
            {
                String f = jparent["format"].Value<string>();
                data = new RDateVector(name, dateList, f);
            }

            return data;
        }
        /// <summary>
        /// Create the appropriate RData class representing a primitive (RNumeric, RBoolean, RString)
        /// </summary>
        /// <param name="name">name of the RData instance</param>
        /// <param name="jparent">Parent json.net object</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parsePrimitive(String name, JToken jparent)
        {
            RData data = null;

            JToken j = jparent["value"].Value<JToken>();
            if ((j.Type == JTokenType.Float) || (j.Type == JTokenType.Integer))
            {
                data = new RNumeric(name, j.Value<Double>());
            }
            else if (j.Type == JTokenType.String)
            {
                data = new RString(name, j.Value<string>());
            }
            else if (j.Type == JTokenType.Boolean)
            {
                data = new RBoolean(name, j.Value<bool>());
            }

            return data;
        }
        /// <summary>
        /// Create an RDate
        /// </summary>
        /// <param name="name">name of the RData instance</param>
        /// <param name="jparent">Parent json.net object</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseDate(String name, JToken jparent)
        {
            RData data = null;

            JToken j = jparent["value"].Value<JToken>();
            JToken f = jparent["format"].Value<JToken>();
            if ((j.Type == JTokenType.Date) || (j.Type == JTokenType.String))
            {
                data = new RDate(name, j.Value<string>(), f.Value<string>());
            }

            return data;
        }
        /// <summary>
        /// Create an RFactor
        /// </summary>
        /// <param name="name">name of the RData instance</param>
        /// <param name="jparent">Parent json.net object</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseFactor(String name, JToken jparent)
        {
            RData data = null;
            List<String> fValues = new List<String>();
            List<String> fLevels = new List<String>();
            List<String> fLabels = new List<String>();

            if (!(jparent["value"] == null))
            {
                JArray jvalues = jparent["value"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        fValues.Add(j.Value<string>());
                    }
                    else
                    {
                        fValues.Add(null);
                    }
                }
            }

            if (!(jparent["labels"] == null))
            {
                JArray jvalues = jparent["labels"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        fLabels.Add(j.Value<string>());
                    }
                    else
                    {
                        fLabels.Add(null);
                    }
                }
            }

            if (!(jparent["levels"] == null))
            {
                JArray jvalues = jparent["levels"].Value<JArray>();
                foreach (JValue j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        fLevels.Add(j.Value<string>());
                    }
                    else
                    {
                        fLevels.Add(null);
                    }
                }
            }

            JToken f = jparent["ordered"].Value<JToken>();
            data = new RFactor(name, fValues, fLevels, fLabels, f.Value<bool>());

            return data;
        }
        /// <summary>
        /// Create a RDataFrame
        /// </summary>
        /// <param name="name">name of the RData instance</param>
        /// <param name="jparent">Parent json.net object</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseDataFrame(String name, JToken jparent)
        {
            RData data = null;
            List<RData> values = new List<RData>();

            JArray jvalues = jparent["value"].Value<JArray>();
            if (!(jvalues == null))
            {
                foreach (JObject j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        values.Add(parseRObject(j));
                    }
                }
            }

            data = new RDataFrame(name, values);

            return data;
        }
        /// <summary>
        /// Create an RList
        /// </summary>
        /// <param name="name">name of the RData instance</param>
        /// <param name="jparent">Parent json.net object</param>
        /// <returns>RData object</returns>
        /// <remarks></remarks>
        public static RData parseRList(String name, JToken jparent)
        {
            RData data = null;
            List<RData> values = new List<RData>();

            JArray jvalues = jparent["value"].Value<JArray>();
            if (!(jvalues == null))
            {
                foreach (JObject j in jvalues)
                {
                    values.Add(parseRObject(j));
                }
            }

            data = new RList(name, values);

            return data;
        }

        private static RData checkEmptyValue(JToken jparent, String type, String rclass, String name)
        {
            RData data = null;

            if (!(jparent["value"] == null))
            {
                return null;
            }

            if (type == Constants.TYPE_MATRIX)
            {
                if (rclass == Constants.RCLASS_NUMERIC)
                {
                    data = new RNumericMatrix(name, null);
                }
                else if (rclass == Constants.RCLASS_CHARACTER)
                {
                    data = new RStringMatrix(name, null);
                }
                else if (rclass == Constants.RCLASS_BOOLEAN)
                {
                    data = new RBooleanMatrix(name, null);
                }
                else
                {
                    data = new RNumericMatrix(name, null);
                }
            }
            else if (type == Constants.TYPE_VECTOR)
            {
                if (rclass == Constants.RCLASS_NUMERIC)
                {
                    data = new RNumericVector(name, null);
                }
                else if (rclass == Constants.RCLASS_CHARACTER)
                {
                    data = new RStringVector(name, null);
                }
                else if (rclass == Constants.RCLASS_BOOLEAN)
                {
                    data = new RBooleanVector(name, null);
                }
                else if (rclass == Constants.RCLASS_DATE)
                {
                    data = new RDateVector(name, null, "");
                }
                else
                {
                    data = new RNumericVector(name, null);
                }
            }
            else if (type == Constants.TYPE_PRIMITIVE)
            {
                if (rclass == Constants.RCLASS_NUMERIC)
                {
                    data = new RNumeric(name, 0);
                }
                else if (rclass == Constants.RCLASS_CHARACTER)
                {
                    data = new RString(name, null);
                }
                else if (rclass == Constants.RCLASS_BOOLEAN)
                {
                    data = new RBoolean(name, false);
                }
                else
                {
                    data = new RNumeric(name, 0);
                }
            }
            else if (type == Constants.TYPE_DATE)
            {
                data = new RDate(name, null, "");
            }
            else if (type == Constants.TYPE_FACTOR)
            {
                data = new RFactor(name, null);
            }
            else if (type == Constants.TYPE_DATAFRAME)
            {
                data = new RDataFrame(name, null);
            }
            else if (type == Constants.TYPE_LIST)
            {
                data = new RList(name, null);
            }

            return data;
        }

        /// <summary>
        /// Create JSON markup from an RData object
        /// </summary>
        /// <param name="data">RData object to deserialize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// 
        public static String createJSONfromRData(List<RData> data)
        {
            JObject parent = new JObject();
            JToken token = parent.Value<JToken>();
            return createJSONfromRData(data, ref token);
        }

        /// <summary>
        /// Create JSON markup from an RData object
        /// </summary>
        /// <param name="data">RData object to deserialize</param>
        /// <param name="parent">Parent JObject (only used when constructing R lists and R dataframes)</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// 
        public static String createJSONfromRData(List<RData> data, ref JToken parent)
        {
            JProperty value = null;
            JObject json = default(JObject);

            foreach (RData r in data)
            {
                //basic information for each object (rclass, type)
                json = new JObject(new JProperty(r.Name, new JObject(new JProperty("rclass", r.RClass), new JProperty("type", r.Type))));

                //get "value" for each object, plus special information for those types that need it (i.e. format for Date type)
                if (r.Type == Constants.TYPE_PRIMITIVE)
                {
                    value = new JProperty("value", r.Value);
                }
                else if (r.Type == Constants.TYPE_VECTOR)
                {
                    if (r.RClass == Constants.RCLASS_DATE)
                    {

                        //format
                        JProperty format = default(JProperty);
                        JArray datearray = new JArray();
                        String formatvalue = "";

                        formatvalue = System.Convert.ToString(((RDateVector)r).Format);

                        format = new JProperty("format", formatvalue);
                        json.First.First.Last.AddAfterSelf(format);

                        //value
                        foreach (string d in (List<String>)r.Value)
                        {
                            datearray.Add(d);
                        }
                        value = new JProperty("value", datearray);
                    }
                    else if (r.RClass == Constants.RCLASS_NUMERIC)
                    {
                        value = new JProperty("value", new JArray(((List<Double?>)r.Value).ToArray()));
                    }
                    else if (r.RClass == Constants.RCLASS_BOOLEAN)
                    {
                        value = new JProperty("value", new JArray(((List<Boolean?>)r.Value).ToArray()));
                    }
                    else 
                    {
                        value = new JProperty("value", new JArray(((List<String>)r.Value).ToArray()));
                    }
                }
                else if (r.Type == Constants.TYPE_MATRIX)
                {
                    JArray parentarray = new JArray();
                    JArray childarray = default(JArray);

                    //value
                    value = new JProperty("value", parentarray);
                    foreach (var v in (List<Object>)r.Value)
                    {
                        childarray = new JArray(((List<Object>)v).ToArray());
                        parentarray.Add(childarray);
                    }
                }
                else if ((r.Type == Constants.TYPE_DATAFRAME) || (r.Type == Constants.TYPE_LIST))
                {
                    JArray parentarray = new JArray();

                    //value and recurse
                    value = new JProperty("value", parentarray);
                    JToken arrayobj = parentarray;
                    createJSONfromRData((List<RData>)r.Value, ref arrayobj);
                }
                else if (r.Type == Constants.TYPE_DATE)
                {
                    String formatvalue = "";

                    formatvalue = System.Convert.ToString(((RDate)r).Format);
                    value = new JProperty("value", r.Value);

                    //format
                    JProperty format = default(JProperty);
                    format = new JProperty("format", formatvalue);
                    json.First.First.Last.AddAfterSelf(format);
                }
                else if (r.Type == Constants.TYPE_FACTOR)
                {
                    value = new JProperty("value", r.Value);

                    //ordered
                    JProperty ordered = default(JProperty);
                    ordered = new JProperty("ordered", ((RFactor)r).Ordered);
                    json.First.First.Last.AddAfterSelf(ordered);

                    //labels
                    JProperty labels = default(JProperty);
                    if (!(((RFactor)r).Labels == null))
                    {
                        labels = new JProperty("labels", new JArray(((RFactor)r).Labels));
                        json.First.First.Last.AddAfterSelf(labels);
                    }

                    //levels
                    JProperty levels = default(JProperty);
                    if (!(((RFactor)r).Levels == null))
                    {
                        levels = new JProperty("levels", new JArray(((RFactor)r).Levels));
                        json.First.First.Last.AddAfterSelf(levels);
                    }
                }


                //add the "value"
                if (!(value == null))
                {
                    json.First.First.Last.AddAfterSelf(value);
                }

                //Matrix and DataFrame
                if (!(parent == null))
                {
                    if (parent is JArray)
                    {
                        json.First.First.Last.AddAfterSelf(new JProperty("name", r.Name));
                        ((JArray)parent).Add(json.First.First);
                    }
                    else if (parent is JObject)
                    {
                        ((JObject)parent).Add(json.First);
                    }
                }

            }

            return parent.ToString();
        }

    }
}