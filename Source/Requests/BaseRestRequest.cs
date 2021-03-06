﻿/*
 * Copyright(c) 2017 Microsoft Corporation. All rights reserved. 
 * 
 * This code is licensed under the MIT License (MIT). 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do 
 * so, subject to the following conditions: 
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE. 
*/

using System;
using System.Runtime.Serialization;

namespace BingMapsRESTToolkit
{
    /// <summary>
    /// An abstract class in which all REST service requests derive from.
    /// </summary>
    [DataContract]
    public abstract class BaseRestRequest
    { 
        #region Public Properties

        /// <summary>
        /// The Bing Maps key for making the request.
        /// </summary>
        public string BingMapsKey;

        /// <summary>
        /// The culture to use for the request.
        /// </summary>
        public string Culture;

        /// <summary>
        /// The geographic region that corresponds to the current viewport.
        /// </summary>
        public BoundingBox UserMapView;

        /// <summary>
        /// The user’s current position.
        /// </summary>
        public Coordinate UserLocation;

        /// <summary>
        /// An Internet Protocol version 4 (IPv4) address.
        /// </summary>
        public string UserIp;

        #endregion

        /// <summary>
        /// Abstract method which generates the Bing Maps REST request URL.
        /// </summary>
        /// <returns>A Bing Maps REST request URL.</returns>
        public abstract string GetRequestUrl();

        internal string GetBaseRequestUrl()
        {
            var url = string.Empty;

            if (!string.IsNullOrWhiteSpace(Culture))
            {
                url += "&c=" + Culture;
            }

            if(UserMapView != null){
                //South Latitude, West Longitude, North Latitude, East Longitude
                url += string.Format("&umv={0}", UserMapView.ToString());
            } 

            if (UserLocation != null)
            {
                url += string.Format("&ul={0:0.#####},{1:0.#####}", UserLocation.Latitude, UserLocation.Longitude);
            }

            if (!string.IsNullOrWhiteSpace(UserIp))
            {
                url += "&uip=" + UserIp;
            }

            return url + "&key=" + BingMapsKey + "&clientApi=CSToolkit";
        }
    }
}
