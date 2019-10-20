using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace TourManagement.API.Helpers
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        // run in the same stage
        public int Order
        {
            get { return 0; }
        }

        private readonly string[] _mediaTypes;
        private readonly string _requestHeaderToMatch;

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeadersToMatch, string[] mediaTypes)
        {
            _requestHeaderToMatch = requestHeadersToMatch;
            _mediaTypes = mediaTypes;
        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            // false if no reqHeader that matches inputted reqHeader to match
            if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
            {
                return false;
            }

            // if one of the media types matches, return true
            return (from mediaType in _mediaTypes
                let headerValues = requestHeaders[_requestHeaderToMatch]
                    .ToString()
                    .Split(',')
                    .ToList()
                from headerValue in headerValues
                where string.Equals(headerValue, mediaType, StringComparison.OrdinalIgnoreCase)
                select mediaType).Any();

//            foreach (var mediaType in _mediaTypes)
//            {
//                var headerValues = requestHeaders[_requestHeaderToMatch]
//                    .ToString().Split(',').ToList();
//                foreach (var headerValue in headerValues)
//                {
//                    if (string.Equals(headerValue, mediaType,
//                        StringComparison.OrdinalIgnoreCase))
//                    {
//                        return true;
//                    }
//                }
//            }
//            return false;
        }
    }
}