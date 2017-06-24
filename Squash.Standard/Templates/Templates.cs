using System;
using System.Collections.Generic;
using System.Text;

namespace Squash.Standard.Templates
{
    public static class Templates
    {
        public static string Template
        {
            get
            {
                return @"<!DOCTYPE html>
                        <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
                        <head>
                            <meta charset=""utf-8"" />
                            <title></title>
                            <style type=""text/css"">
                                {{templateStyle}}
                            </style>
                            <script src=""https://code.jquery.com/jquery-3.2.1.min.js""
                                    integrity=""sha256-hwg4gsxgFZhOsEEamdOYGBf13FyQuiTwlAQgxVSNgt4=""
                                    crossorigin=""anonymous""></script>
                            <script type=""text/javascript"">
                                jQuery(document).ready(function () {
                                    {{templateScript}}
                                });
                            </script>
                        </head>
                        <body>
                            <div class=""menu"">
                                {{menu}}
                            </div>
                            <div class=""content"">
                                {{content}}
                            </div>
                        </body>
                        </html>";
            }
        }

        public static string Feature
        {
            get
            {
                return @"<h1>{{name}}</h1>
                        <div class=""description"">{{description}}</div>
                        <div class=""scenarios"">
                            {{scenarios}}
                        </div>";
            }
        }

        public static string FeatureDirectory
        {
            get
            {
                return @"<div class=""directory-name"">{{dirName}}</div>
                         <div class=""features"">{{features}}</div>";
            }
        }

        public static string Scenario
        {
            get
            {
                return @"<h3>{{name}}</h3>
                        <div>{{tags}}</div>
                        <div>{{stepDefinitions}}</div>

                        <div>{{scenarios}}</div>";
            }
        }

        public static string StepDefinition
        {
            get
            {
                return @"<span class=""action"">{{action}} </span><span class=""statement"">{{statement}}</span>
                        {{table}}";
            }
        }
    }
}
