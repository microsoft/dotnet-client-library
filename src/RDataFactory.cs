/*
 * RDataFactory.cs
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

namespace DeployR
{
/// <summary>
/// Factory to simplify the creation of RData in client applications
/// </summary>
/// <remarks></remarks>
    public class RDataFactory
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RDataFactory()
        {

        }
        /// <summary>
        /// Create a RBoolean object
        /// </summary>
        /// <param name="name">Name of the RBoolean object</param>
        /// <param name="value">Boolean that represents the value of the RBoolean object</param>
        /// <returns>RBoolean object</returns>
        /// <remarks></remarks>
        static public RBoolean createBoolean(String name, Boolean value)
        {
            return new RBoolean(name, value);
        }
        /// <summary>
        /// Create RBooleanMatrix object
        /// </summary>
        /// <param name="name">Name of the RBooleanMatrix object</param>
        /// <param name="value">2-dimensional array of booleans that represents the value of the RBooleanMatrix object</param>
        /// <returns>RBooleanMatrix object</returns>
        /// <remarks></remarks>
        static public RBooleanMatrix createBooleanMatrix(String name, List<List<Boolean?>> value)
        {
            return new RBooleanMatrix(name, value);
        }
        /// <summary>
        /// Create RBooleanVector object
        /// </summary>
        /// <param name="name">Name of the RBooleanVector object</param>
        /// <param name="value">Array of booleans that represents the value of the RBooleanVector object</param>
        /// <returns>RBooleanVector object</returns>
        /// <remarks></remarks>
        static public RBooleanVector createBooleanVector(String name, List<Boolean?> value)
        {
            return new RBooleanVector(name, value);
        }
        /// <summary>
        /// Create RDataFrame object
        /// </summary>
        /// <param name="name">Name of the RDataFrame object</param>
        /// <param name="value">Array of RData objects that represents the value of the RDataFrame object</param>
        /// <returns>RDataFrame object</returns>
        /// <remarks></remarks>
        static public RDataFrame createDataFrame(String name, List<RData> value)
        {
            return new RDataFrame(name, value);
        }
        /// <summary>
        /// Create RDate object
        /// </summary>
        /// <param name="name">Name of the RDate object</param>
        /// <param name="value">Date object that represents the value of the RDate object</param>
        /// <param name="format">Format used by the RDate object value</param>
        /// <returns>RDate object</returns>
        /// <remarks></remarks>
        static public RDate createDate(String name, DateTime value, String format)
        {
            return new RDate(name, value.ToString(), format);
        }
        /// <summary>
        /// Create RDateVector object
        /// </summary>
        /// <param name="name">Name of the RDateVector object</param>
        /// <param name="value">Array of Dates that represents the value of the RDateVector object</param>
        /// <param name="format">Format used by the RDateVector object value</param>
        /// <returns>RDateVector object</returns>
        /// <remarks></remarks>
        static public RDateVector createDateVector(String name, List<String> value, String format)
        {
            return new RDateVector(name, value, format);
        }
        /// <summary>
        /// Create RFactor object
        /// </summary>
        /// <param name="name">Name of RFactor object</param>
        /// <param name="value">Array of strings that represents the values for the RFactor object</param>
        /// <returns>RFactor object</returns>
        /// <remarks></remarks>
        static public RFactor createFactor(String name, List<String> value)
        {
            return new RFactor(name, value);
        }
        /// <summary>
        /// Create RFactor object
        /// </summary>
        /// <param name="name">Name of RFactor object</param>
        /// <param name="value">Array of strings that represents the values for the RFactor object</param>
        /// <param name="levels">Array of strings that represents the levels for the RFactor object</param>
        /// <param name="labels">Array of strings that represents the labels for the RFactor object</param>
        /// <param name="ordered">Boolean that indicates if the factor is ordered</param>
        /// <returns>RFactor object</returns>
        /// <remarks></remarks>
        static public RFactor createFactorEx(String name, List<String> value, List<String> levels, List<String> labels, Boolean ordered)
        {
            return new RFactor(name, value, levels, labels, ordered);
        }
        /// <summary>
        /// Create RList object
        /// </summary>
        /// <param name="name">Name of the RList object</param>
        /// <param name="value">Array of RData objects that represents the value of the RList object</param>
        /// <returns>RList object</returns>
        /// <remarks></remarks>
        static public RList createList(String name, List<RData> value)
        {
            return new RList(name, value);
        }
        /// <summary>
        /// Create RNumeric object
        /// </summary>
        /// <param name="name">Name of the RNumeric object</param>
        /// <param name="value">Double that represents the value of the RNumeric object</param>
        /// <returns>RNumeric object</returns>
        /// <remarks></remarks>
        static public RNumeric createNumeric(String name, Double value)
        {
            return new RNumeric(name, value);
        }
        /// <summary>
        /// Create RNumericMatrix object
        /// </summary>
        /// <param name="name">Name of the RNumericMatrix object</param>
        /// <param name="value">2-dimensional array of doubles that represents the value of the RNumericMatrix object</param>
        /// <returns>RNumericMatrix object</returns>
        /// <remarks></remarks>
        static public RNumericMatrix createNumericMatrix(String name, List<List<Double?>> value)
        {
            return new RNumericMatrix(name, value);
        }
        /// <summary>
        /// Create RNumericVector object
        /// </summary>
        /// <param name="name">Name of the RNumericVector object</param>
        /// <param name="value">Array of doubles that represents the value of the RNumericVector object</param>
        /// <returns>RNumericVector object</returns>
        /// <remarks></remarks>
        static public RNumericVector createNumericVector(String name, List<Double?> value)
        {
            return new RNumericVector(name, value);
        }
        /// <summary>
        /// Create RString object
        /// </summary>
        /// <param name="name">Name of the RString object</param>
        /// <param name="value">String that represents the value of the RString object</param>
        /// <returns>RString object</returns>
        /// <remarks></remarks>
        static public RString createString(String name, String value)
        {
            return new RString(name, value);
        }
        /// <summary>
        /// Create RStringMatrix object
        /// </summary>
        /// <param name="name">Name of the RStringMatrix object</param>
        /// <param name="value">2-dimensional array of strings that represents the value of the RStringMatrix object</param>
        /// <returns>RStringMatrix object</returns>
        /// <remarks></remarks>
        static public RStringMatrix createStringMatrix(String name, List<List<String>> value)
        {
            return new RStringMatrix(name, value);
        }
        /// <summary>
        /// Create RStringVector object
        /// </summary>
        /// <param name="name">Name of the RStringVector object</param>
        /// <param name="value">Array of strings that represents the value of the RStringVector object</param>
        /// <returns>RStringVector object</returns>
        /// <remarks></remarks>
        static public RStringVector createStringVector(String name, List<String> value)
        {

            return new RStringVector(name, value);
        }

    }
}